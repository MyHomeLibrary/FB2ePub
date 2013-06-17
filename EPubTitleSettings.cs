using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EPubLibrary
{

    /// <summary>
    /// used as base class to all data containers supporting language
    /// </summary>
    public class DataWithLanguage
    {
        public string Language { get; set; }
    }

    /// <summary>
    /// Class to store person with role
    /// </summary>
    public class PersoneWithRole :  DataWithLanguage
    {
        /// <summary>
        /// default role - author if not set otherwise
        /// </summary>
        private RolesEnum role = RolesEnum.Author;

        public string PersonName { get; set; }
        public RolesEnum Role
        {
            get { return role;}
            set { role = value; }
        }

        /// <summary>
        /// Name in a normalized form of the contents, suitable for machine processing
        /// </summary>
        public string FileAs { get; set; }
    }

    /// <summary>
    /// Class to store coverage information
    /// </summary>
    public class Coverage : DataWithLanguage
    {
        public string CoverageData { get; set; }
    }

    /// <summary>
    /// Class to store book description
    /// </summary>
    public class Description : DataWithLanguage
    {
        public string DescInfo { get; set; }
    }

    /// <summary>
    /// Class to store publisher data
    /// </summary>
    public class Publisher : DataWithLanguage
    {
        public string PublisherName { get; set; }
    }

    /// <summary>
    /// Class to store relation info
    /// </summary>
    public class Relation : DataWithLanguage
    {
        public string RelationInfo { get; set; }
    }

    /// <summary>
    /// Class to store rights/copyrights info
    /// </summary>
    public class Rights : DataWithLanguage
    {
        public string RightsInfo { get; set; }
    }

    /// <summary>
    /// Class to store source data
    /// </summary>
    public class Source : DataWithLanguage
    {
        public string SourceData { get; set; }
    }

    /// <summary>
    /// Class to store subject data
    /// </summary>
    public class Subject : DataWithLanguage
    {
        public string SubjectInfo { get; set; }
    }

    /// <summary>
    /// Class to store one title
    /// </summary>
    public class Title : DataWithLanguage
    {
        public string TitleName { get; set; }
    }

    /// <summary>
    /// Clas to store one identifier
    /// </summary>
    public class Identifier
    {
        public string IdentifierName { get; set;}
        public string ID { get; set; }
        public string Scheme { get; set; }
    }

    /// <summary>
    /// EPub book title settings
    /// </summary>
    public class EPubTitleSettings
    {
        /// <summary>
        /// list of subjects for the book (usualy genres)
        /// </summary>
        private readonly List<Subject> subjects = new List<Subject>();

        /// <summary>
        /// List of creators
        /// </summary>
        private readonly List<PersoneWithRole> creators = new List<PersoneWithRole>();

        /// <summary>
        /// List of contributors 
        /// </summary>
        private readonly List<PersoneWithRole> contributors = new List<PersoneWithRole>();

        /// <summary>
        /// List of identifiers
        /// </summary>
        private readonly List<Identifier> identifiers = new List<Identifier>();

        /// <summary>
        /// List of Titles
        /// </summary>
        private readonly List<Title> bookTitles = new List<Title>();

        /// <summary>
        /// List of book languages
        /// </summary>
        private readonly List<string> languages = new List<string>();

        /// <summary>
        /// Publisher element
        /// </summary>
        private readonly Publisher publisher = new Publisher();

        
        /// <summary>
        /// Get list of identifiers
        /// </summary>
        public List<Title> BookTitles
        {
            get { return bookTitles; }
        }

        /// <summary>
        /// Get list of book languages
        /// </summary>
        public List<string> Languages 
        {
            get { return languages; }
        }

        /// <summary>
        /// Get list of book Identifiers
        /// </summary>
        public List<Identifier> Identifiers
        {
            get { return identifiers; }
        }

        /// <summary>
        /// Get list of creators of the book 
        /// </summary>
        public List<PersoneWithRole> Creators 
        {
            get { return creators; }
        }

        /// <summary>
        /// Get list of contributors
        /// </summary>
        public List<PersoneWithRole> Contributors
        {
            get { return contributors; }
        }

        /// <summary>
        /// Get publisher element
        /// </summary>
        public Publisher Publisher
        {
            get { return publisher; }
        }

        /// <summary>
        /// Get list of subjects for the book (usually genres)
        /// </summary>
        public List<Subject> Subjects 
        {
            get { return subjects; }
        }

        public string Description { set; get; }

        public DateTime? DateFileCreation { set; get; }

        public DateTime? DatePublished{ set; get; }

        public DateTime? DataFileModification { set; get; }

        public string Type  { set; get; }

        public string Format  { set; get; }

        public Source Source { set; get; }

        public Relation Relation { set; get; }

        public Coverage Coverage { set; get; }

        public Rights Rights { set; get; }

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
