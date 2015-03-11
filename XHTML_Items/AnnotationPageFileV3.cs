using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    public class AnnotationPageFileV3 : BaseXHTMLFileV3
    {
        public AnnotationPageFileV3() 
        {
            InternalPageTitle = "Annotation";
            GuideRole = GuideTypeEnum.Preface;
            FileName = "annotation.xhtml";
            Id = "annotation";
            FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder);
            SetDocumentEpubType(EpubV3Vocabulary.Foreword);
        }

        public Div BookAnnotation { get; set; }


        public override void GenerateBody()
        {
            base.GenerateBody();
            var annotationPage = new Div(Compatibility);
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
                annotationPage.Add(new SimpleHTML5Text(Compatibility) { Text = "Unnamed" });
            }

            annotationPage.Add(new EmptyLine(Compatibility));

            BodyElement.Add(annotationPage);
        }


        /// <summary>
        /// Checks if XHTML element is part of current document
        /// </summary>
        /// <param name="value">elemenrt to check</param>
        /// <returns>true idf part of this document, false otherwise</returns>
        public override bool PartOfDocument(IHTMLItem value)
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
