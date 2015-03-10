using EPubLibrary.Content.NavigationDocument;
using EPubLibrary.TOC;
using EPubLibrary.XHTML_Items;

namespace EPubLibrary.Content.NavigationManagement
{
    internal class NavigationManagerV3
    {
        private readonly TOCFileV3Transitional _tableOfContentFile = new TOCFileV3Transitional();
        private readonly NavigationDocumentFile _navigationDocument = new NavigationDocumentFile();



        public TOCFileV3Transitional TableOfContentFile
        {
            get { return _tableOfContentFile; }
        }

        public NavigationDocumentFile NavigationDocument
        {
            get { return _navigationDocument; }
        }


        public void Consolidate()
        {
            _tableOfContentFile.Consolidate();
        }

        public void AddBookSubsection(BaseXHTMLFileV3 subsection, string name)
        {
            if (!subsection.NotPartOfNavigation)
            {
                if (subsection.NavigationLevel <= 1)
                {
                    _tableOfContentFile.AddNavPoint(subsection, name);
                    _navigationDocument.AddTOCNavPoint(subsection, name);
                }
                else
                {
                    _tableOfContentFile.AddSubNavPoint(subsection, name);
                    _navigationDocument.AddSubNavPoint(subsection, name);
                }
            }
            
        }

        public void SetupBookNavigation(string id, string title,bool flatStructure)
        {
            _tableOfContentFile.ID = id;
            _tableOfContentFile.Title = title;
            _navigationDocument.PageTitle = title;
            _navigationDocument.FlatStructure = flatStructure;
        }
    }
}
