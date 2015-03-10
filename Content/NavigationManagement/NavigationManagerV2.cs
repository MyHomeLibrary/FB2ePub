using EPubLibrary.TOC;
using EPubLibrary.XHTML_Items;

namespace EPubLibrary.Content.NavigationManagement
{
    internal class NavigationManagerV2
    {
        private readonly TOCFile _tableOfContentFile = new TOCFile();



        public TOCFile TableOfContentFile
        {
            get { return _tableOfContentFile; }
        }


        public void Consolidate()
        {
            _tableOfContentFile.Consolidate();
        }

        public void AddBookSubsection(BaseXHTMLFileV2 subsection, string name)
        {
            if (!subsection.NotPartOfNavigation)
            {
                if (subsection.NavigationLevel <= 1)
                {
                    _tableOfContentFile.AddNavPoint(subsection, name);
                }
                else
                {
                    _tableOfContentFile.AddSubNavPoint(subsection, name);
                }
            }

        }

        public void SetupBookNavigation(string id, string title)
        {
            _tableOfContentFile.ID = id;
            _tableOfContentFile.Title = title;
        }

    }
}
