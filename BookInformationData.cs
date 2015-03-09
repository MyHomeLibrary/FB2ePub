
using System.Collections.Generic;
using EPubLibraryContracts;

namespace EPubLibrary
{
    internal class BookInformationData : IBookInformationData
    {
        public IList<string> Authors
        {
            get { throw new System.NotImplementedException(); }
        }

        public IList<string> Series
        {
            get { throw new System.NotImplementedException(); }
        }

        public IList<string> AllSequences
        {
            get { throw new System.NotImplementedException(); }
        }

        public string BookMainTitle
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}
