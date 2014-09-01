using System;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace XHTMLClassLibrary
{
    public class HTMLDocument 
    {
        private readonly HTMLElementType _documentStandard;
        private readonly HTML _htmlRoot;

        public HTMLDocument(HTMLElementType standard)
        {
            _documentStandard = standard;
            _htmlRoot = new HTML { HTMLStandard = _documentStandard};
        }

        public HTML RootHTML { get { return _htmlRoot; } }

        public void Load(XDocument xDocument)
        {
            // TODO: check that it's valid document type etc
            _htmlRoot.Load(xDocument.Root);
        }

        public XDocument Generate()
        {
            var mainDocument = new XDocument();
            mainDocument.Add(GetDocumentType(_documentStandard));
            mainDocument.Add(_htmlRoot.Generate());
            return mainDocument;

        }

        private XDocumentType GetDocumentType(HTMLElementType documentStandard)
        {
            switch (documentStandard)
            {
                case HTMLElementType.FrameSet:
                    return new XDocumentType("html", @"-//W3C//DTD XHTML 1.0 Frameset//EN",
                                             @"DTD/xhtml1-frameset.dtd", null);
                case HTMLElementType.Strict:
                    return new XDocumentType("html", @"-//W3C//DTD XHTML 1.0 Strict//EN",
                                             @"DTD/xhtml1-strict.dtd", null);
                case HTMLElementType.Transitional:
                    return new XDocumentType("html", @"-//W3C//DTD XHTML 1.0 Transitional//EN",
                                             @"DTD/xhtml1-transitional.dtd", null);
                case HTMLElementType.XHTML11:
                case HTMLElementType.XHTML5:
                    return new XDocumentType("html", @"-//W3C//DTD XHTML 1.1//EN",
                                            @"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd", null);
                case HTMLElementType.HTML5:
                    return new XDocumentType("html", null, null, null);

            }
            throw new NotImplementedException(string.Format("The case of {0} not implemented yet", documentStandard));
        }

        public bool IsValid()
        {
            return _htmlRoot.IsValid();
        }
    }
}
