using System.Collections.Generic;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    public enum SectionTypeEnum
    {
        Text,
        Links,
    }

    public class BookDocumentV2 : BaseXHTMLFileV2
    {
        public readonly static EPubInternalPath DefaultTextFilesFolder= new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
        private readonly Dictionary<Anchor, IAttributeDataAccess> _references = new Dictionary<Anchor, IAttributeDataAccess>();



        public Dictionary<Anchor, IAttributeDataAccess> Refrences
        {
            get { return _references; }    
        }

        public BookDocumentV2()
        {
            // real limit is 300k but just to be sure
            MaxSize = 300 * 1024;
            Type = SectionTypeEnum.Text;
            FileEPubInternalPath = DefaultTextFilesFolder;
        }


        /// <summary>
        /// Get/Set section type
        /// </summary>
        public SectionTypeEnum Type { get; set; }

        
    }
}
