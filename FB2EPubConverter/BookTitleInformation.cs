using System.Collections.Generic;
using EPubLibraryContracts;

namespace FB2EPubConverter
{
    internal class BookTitleInformation : IBookTitleInformation
    {
        private readonly List<string>  _authors = new List<string>();
        private readonly List<string>  _series = new List<string>();

        public IList<string> Authors
        {
            get { return _authors; }
        }

        public IList<string> Series
        {
            get { return _series; }
        }

        public string BookMainTitle { get; set; }
      
    }
}
