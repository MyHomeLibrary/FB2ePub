using EPubLibrary.Content.Guide;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    public class AnnotationPageFile : BaseXHTMLFile
    {

        public AnnotationPageFile(HTMLElementType compatibility) : base(compatibility)
        {
            pageTitle = "Annotation";
            DocumentType = GuideTypeEnum.Preface;
            FileName = "annotation.xhtml";
            Id = "annotation";
            FileEPubInternalPath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
            
        }

        public Div BookAnnotation { get; set; }


        public override void GenerateBody()
        {
            base.GenerateBody();
            Div annotationPage = new Div();
            annotationPage.GlobalAttributes.Class.Value = "annotation";
            if (BookAnnotation != null)
            {
                foreach (var item in BookAnnotation.SubElements())
                {
                    annotationPage.Add(item);
                }
            }
            else
            {
                annotationPage.Add(new SimpleHTML5Text { Text = "Unnamed" });
            }

            annotationPage.Add(new EmptyLine());

            BodyElement.Add(annotationPage);
        }


        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">elemenrt to check</param>
        /// <returns>true idf part of this document, false otherwise</returns>
        public new bool PartOfDocument(IHTMLItem value)
        {
            if (BookAnnotation == null)
            {
                return false;
            }
            IHTMLItem parent = value;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
            }
            if (parent == BookAnnotation)
            {
                return true;
            }
            return false;
        }

    }
}
