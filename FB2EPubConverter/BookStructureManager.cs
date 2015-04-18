using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EPubLibraryContracts;
using EPubLibraryContracts.Settings;
using FB2EPubConverter.PrepearedHTMLFiles;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter
{
    internal class BookStructureManager : IEnumerable<IBaseXHTMLFile>
    {
        private readonly List<IBaseXHTMLFile> _normalPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _coverPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _annotationPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _titlePages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _aboutPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _footnotesContainer = new List<IBaseXHTMLFile>(); 

        public IEnumerable<IBaseXHTMLFile> NormalPages { get { return _normalPages;}}

        public IEnumerable<IBaseXHTMLFile> CoverPages { get { return _coverPages; } }

        public IEnumerable<IBaseXHTMLFile> AnnotationPages { get { return _annotationPages; } }

        public IEnumerable<IBaseXHTMLFile> TitlePages { get { return _titlePages; } }

        public IEnumerable<IBaseXHTMLFile> AboutPages { get { return _aboutPages; } }

        public IEnumerable<IBaseXHTMLFile> Footnotes { get { return _footnotesContainer; }}

        public void AddBookPage(IBaseXHTMLFile pageFile)
        {
            _normalPages.Add(pageFile);
        }

        public void AddCoverPage(IBaseXHTMLFile pageFile)
        {
            _coverPages.Add(pageFile);  
        }

        public void AddAnnotationPage(IBaseXHTMLFile pageFile)
        {
            _annotationPages.Add(pageFile);
        }

        public void AddTitlePage(IBaseXHTMLFile pageFile)
        {
            _titlePages.Add(pageFile);
        }

        public void AddAboutPage(IBaseXHTMLFile pageFile)
        {
            _aboutPages.Add(pageFile);
        }

        public void AddFootnote(IBaseXHTMLFile footnoteFile)
        {
            _footnotesContainer.Add(footnoteFile);
        }

        public IEnumerator<IBaseXHTMLFile> GetEnumerator()
        {
            foreach (var coverPage in _coverPages)
            {
                yield return coverPage;
            }

            foreach (var titlePage in _titlePages)
            {
                yield return titlePage;
            }


            foreach (var annotationPage in _annotationPages)
            {
                yield return annotationPage;
            }

            foreach (var normalPage in _normalPages)
            {
                yield return normalPage;
            }

            foreach (var footNotePage in _footnotesContainer)
            {
                yield return footNotePage;
            }               

            foreach (var aboutPage in _aboutPages)
            {
                yield return aboutPage;
            }           
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IBaseXHTMLFile GetIDOfParentDocument(IHTMLItem value)
        {
            return this.FirstOrDefault(document => document != null && document.PartOfDocument(value));
        }
    }
}
