using System.Collections.Generic;


namespace EPubLibraryContracts
{
    public interface IBookTitleInformation
    {
        IList<string> Authors { get; }
        IList<string> Series { get; }
        string BookMainTitle { get; set; }
    }
}
