using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EPubLibraryContracts;

namespace FB2EPubConverter
{
    internal class BookStructureManagerEnumerator : IEnumerator<IBaseXHTMLFile>
    {
        private readonly BookStructureManager _structureManager;
        private IBaseXHTMLFile _current;
        private BookStructureManager.PageType _currentPageType;

        public BookStructureManagerEnumerator(BookStructureManager structureManager)
        {
            _structureManager = structureManager;
            _current = GetFirstItem(out _currentPageType);
        }


        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            _current = GetNextPage(ref _currentPageType);
        }

        
        public void Reset()
        {
            _current = GetFirstItem(out _currentPageType);
        }

        public IBaseXHTMLFile Current
        {
            get { return _current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        private IBaseXHTMLFile GetFirstItem(out BookStructureManager.PageType type)
        {
            if (_structureManager.CoverPages.Any())
            {
                type = BookStructureManager.PageType.CoverPages;
                return _structureManager.CoverPages.First();
            }
            if (_structureManager.TitlePages.Any())
            {
                type = BookStructureManager.PageType.TitlePages;
                return _structureManager.TitlePages.First();
            }
            if (_structureManager.AnnotationPages.Any())
            {
                type = BookStructureManager.PageType.AnnotationPages;
                return _structureManager.AnnotationPages.First();
            }
            if (_structureManager.NormalPages.Any())
            {
                type = BookStructureManager.PageType.NormalPages;
                return _structureManager.NormalPages.First();
            }
            if (_structureManager.AboutPages.Any())
            {
                type = BookStructureManager.PageType.AboutPages;
                return _structureManager.AboutPages.First();
            }
            throw new Exception("No items in page list");
        }

        private IBaseXHTMLFile GetNextPage(ref BookStructureManager.PageType _currentPageType)
        {

        }

    }

    internal class BookStructureManager : IEnumerable<IBaseXHTMLFile>
    {
        public enum PageType
        {
            CoverPages,
            TitlePages,
            AnnotationPages,
            NormalPages,
            AboutPages,
        }

        private readonly List<IBaseXHTMLFile> _normalPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _coverPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _annotationPages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _titlePages = new List<IBaseXHTMLFile>();
        private readonly List<IBaseXHTMLFile> _aboutPages = new List<IBaseXHTMLFile>();

        public IEnumerable<IBaseXHTMLFile> NormalPages { get { return _normalPages;}}

        public IEnumerable<IBaseXHTMLFile> CoverPages { get { return _coverPages; } }

        public IEnumerable<IBaseXHTMLFile> AnnotationPages { get { return _annotationPages; } }

        public IEnumerable<IBaseXHTMLFile> TitlePages { get { return _titlePages; } }

        public IEnumerable<IBaseXHTMLFile> AboutPages { get { return _aboutPages; } }

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

        public IEnumerator<IBaseXHTMLFile> GetEnumerator()
        {
            return new BookStructureManagerEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
