using System;
using System.Collections.Generic;


namespace EPubLibraryContracts
{
    public interface IBookInformationData
    {
        IList<string> Authors { get; }
        IList<string> Series { get; }
        IList<string> AllSequences { get; }
        IList<ITitle> BookTitles { get; }
        IList<string> Languages { get; }
        IList<IEPubIdentifier> Identifiers { get; }
        IList<IPersoneWithRole> Creators { get; }
        IList<IPersoneWithRole> Contributors { get; }
        IPublisher Publisher { get; }
        IList<ISubject> Subjects { get; }
        IDescription Description { get; }
        DateTime? DateFileCreation { set; get; }
        DateTime? DatePublished { set; get; }
        DateTime? DataFileModification { set; get; }
        string BookMainTitle { get; set; }
        string Type { set; get; }
        string Format { set; get; }
        ISource Source { set; get; }
        IRelation Relation { set; get; }
        ICoverage Coverage { set; get; }
        IRights Rights { set; get; }
        string CoverID { set; get; }

        bool IsValid();
    }
}
