using System;
using System.Collections.Generic;
using EPubLibrary.Content.Collections;
using EPubLibraryContracts;

namespace EPubLibrary
{
    internal class BookInformationData : IBookInformationData
    {
        /// <summary>
        /// list of subjects for the book (usually genres)
        /// </summary>
        private readonly List<ISubject> _subjects = new List<ISubject>();

        /// <summary>
        /// List of creators
        /// </summary>
        private readonly List<IPersoneWithRole> _creators = new List<IPersoneWithRole>();

        /// <summary>
        /// List of contributors 
        /// </summary>
        private readonly List<IPersoneWithRole> _contributors = new List<IPersoneWithRole>();

        /// <summary>
        /// List of identifiers
        /// </summary>
        private readonly List<IEPubIdentifier> _identifiers = new List<IEPubIdentifier>();

        /// <summary>
        /// List of Titles
        /// </summary>
        private readonly List<ITitle> _bookTitles = new List<ITitle>();

        /// <summary>
        /// List of book languages
        /// </summary>
        private readonly List<string> _languages = new List<string>();


        private readonly List<string> _authors = new List<string>(); 

        private readonly List<string> _series = new List<string>();

        private readonly List<string> _sequences = new List<string>();

        private readonly EPubSeriesCollections _seriesCollections = new EPubSeriesCollections();


        public IList<string> Authors
        {
            get { return _authors; }
        }

        public IList<string> Series
        {
            get { return _series; }
        }

        public IList<string> AllSequences
        {
            get { return _sequences; }
        }

        public IList<ITitle> BookTitles
        {
            get { return _bookTitles; }
        }

        public IList<string> Languages
        {
            get { return _languages; }
        }

        public IList<IEPubIdentifier> Identifiers
        {
            get { return _identifiers; }
        }

        public IList<IPersoneWithRole> Creators
        {
            get { return _creators; }
        }

        public IList<IPersoneWithRole> Contributors
        {
            get { return _contributors; }
        }

        public IPublisher Publisher { get; set; }

        public IList<ISubject> Subjects
        {
            get { return _subjects; }
        }

        public IDescription Description { get; set; }

        public DateTime? DateFileCreation { get; set; }

        public DateTime? DatePublished { get; set; }

        public DateTime? DataFileModification { get; set; }

        public string BookMainTitle { get; set; }

        public string Type { get; set; }

        public string Format { get; set; }

        public ISource Source { get; set; }

        public IRelation Relation { get; set; }

        public ICoverage Coverage { get; set; }

        public IRights Rights { get; set; }

        public string CoverID { get; set; }

        public ISeriesCollection SeriesCollection
        {
            get { return _seriesCollections; }
        }

        public bool IsValid()
        {
            if (BookTitles.Count < 1)
            {
                return false;
            }
            if (Languages.Count < 1)
            {
                return false;
            }
            if (Identifiers.Count < 1)
            {
                return false;
            }
            return true;
        }
    }
}
