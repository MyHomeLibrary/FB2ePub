using System;
using System.Collections.Generic;
using System.Linq;
using EPubLibraryContracts;

namespace EPubLibrary
{

    /// <summary>
    /// used as base class to all data containers supporting language
    /// </summary>
    public class DataWithLanguage : IDataWithLanguage
    {
        public string Language { get; set; }
    }

    /// <summary>
    /// Class to store person with role
    /// </summary>
    public class PersoneWithRole :  DataWithLanguage , IPersoneWithRole
    {
        /// <summary>
        /// default role - author if not set otherwise
        /// </summary>
        private RolesEnum _role = RolesEnum.Author;

        public string PersonName { get; set; }
        public RolesEnum Role
        {
            get { return _role;}
            set { _role = value; }
        }

        /// <summary>
        /// Name in a normalized form of the contents, suitable for machine processing
        /// </summary>
        public string FileAs { get; set; }
    }

    /// <summary>
    /// Class to store coverage information
    /// </summary>
    public class Coverage : DataWithLanguage, ICoverage
    {
        public string CoverageData { get; set; }
    }

    /// <summary>
    /// Class to store book description
    /// </summary>
    public class Description : DataWithLanguage , IDescription
    {
        public string DescInfo { get; set; }
    }

    /// <summary>
    /// Class to store publisher data
    /// </summary>
    public class Publisher : DataWithLanguage, IPublisher
    {
        public string PublisherName { get; set; }
    }

    /// <summary>
    /// Class to store relation info
    /// </summary>
    public class Relation : DataWithLanguage, IRelation
    {
        public string RelationInfo { get; set; }
    }

    /// <summary>
    /// Class to store rights/copyrights info
    /// </summary>
    public class Rights : DataWithLanguage, IRights
    {
        public string RightsInfo { get; set; }
    }

    /// <summary>
    /// Class to store source data
    /// </summary>
    public class Source : DataWithLanguage, ISource
    {
        public string SourceData { get; set; }
    }

    /// <summary>
    /// Class to store subject data
    /// </summary>
    public class Subject : DataWithLanguage, ISubject
    {
        public string SubjectInfo { get; set; }
    }


    /// <summary>
    /// Class to store one title
    /// </summary>
    public class Title : DataWithLanguage , ITitle
    {
        public string TitleName { get; set; }
        public TitleType TitleType { get; set; }
    }

    /// <summary>
    /// Class to store one identifier
    /// </summary>
    public class Identifier : IEPubIdentifier
    {
        private string _id = string.Empty;

        /// <summary>
        /// Name of the identifier 
        /// </summary>
        public string IdentifierName { get; set;}

        /// <summary>
        /// ID value of the identifier
        /// </summary>
        public string ID
        {
            get 
            {
                if (Scheme.ToUpper() == "URI")
                {
                    return string.Format("urn:uuid:{0}", _id);
                }
                return _id; 
            }
            set { _id = value; }
        }

        /// <summary>
        /// Scheme used by identifier
        /// </summary>
        public string Scheme { get; set; }
    }

    /// <summary>
    /// EPub book title settings
    /// </summary>
    public class EPubTitleSettings : IBookInformationData
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

        /// <summary>
        /// Publisher element
        /// </summary>
        private readonly Publisher _publisher = new Publisher();

        private readonly IDescription _description = new Description();

        
        /// <summary>
        /// Get list of identifiers
        /// </summary>
        public IList<ITitle> BookTitles
        {
            get { return _bookTitles.Select(x=>x as ITitle).ToList(); }
        }

        /// <summary>
        /// Get list of book languages
        /// </summary>
        public IList<string> Languages 
        {
            get { return _languages; }
        }

        /// <summary>
        /// Get list of book Identifiers
        /// </summary>
        public IList<IEPubIdentifier> Identifiers
        {
            get { return _identifiers; }
        }

        /// <summary>
        /// Get list of creators of the book 
        /// </summary>
        public IList<IPersoneWithRole> Creators 
        {
            get { return _creators; }
        }

        /// <summary>
        /// Get list of contributors
        /// </summary>
        public IList<IPersoneWithRole> Contributors
        {
            get { return _contributors; }
        }

        /// <summary>
        /// Get publisher element
        /// </summary>
        public IPublisher Publisher
        {
            get { return _publisher; }
        }

        /// <summary>
        /// Get list of subjects for the book (usually genres)
        /// </summary>
        public IList<ISubject> Subjects 
        {
            get { return _subjects; }
        }

        public IDescription Description { get { return _description; } }

        public DateTime? DateFileCreation { set; get; }

        public DateTime? DatePublished{ set; get; }

        public DateTime? DataFileModification { set; get; }

        public string Type  { set; get; }

        public string Format  { set; get; }

        public ISource Source { set; get; }

        public IRelation Relation { set; get; }

        public ICoverage Coverage { set; get; }

        public IRights Rights { set; get; }

        public string CoverID { set; get; }

        /// <summary>
        /// Check if Title settings is valid meaning 
        /// minimal requirements a filled
        /// </summary>
        /// <returns></returns>
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
