using EPubLibraryContracts;

namespace FB2EPubConverter
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
    public class PersoneWithRole : DataWithLanguage, IPersoneWithRole
    {
        /// <summary>
        /// default role - author if not set otherwise
        /// </summary>
        private RolesEnum _role = RolesEnum.Author;

        public string PersonName { get; set; }
        public RolesEnum Role
        {
            get { return _role; }
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
    public class Description : DataWithLanguage, IDescription
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
    public class Title : DataWithLanguage, ITitle
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
        public string IdentifierName { get; set; }

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

}
