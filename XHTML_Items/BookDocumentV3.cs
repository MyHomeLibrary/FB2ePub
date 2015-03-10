using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    public class BookDocumentV3  : BaseXHTMLFileV3
    {
        public readonly static EPubInternalPath DefaultTextFilesFolder= new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
        private readonly Dictionary<Anchor, IAttributeDataAccess> _references = new Dictionary<Anchor, IAttributeDataAccess>();


        public Dictionary<Anchor, IAttributeDataAccess> Refrences
        {
            get { return _references; }    
        }

        public BookDocumentV3()
        {
            // real limit is 300k but just to be sure
            MaxSize = 0;
            Type = SectionTypeEnum.Text;
            FileEPubInternalPath = DefaultTextFilesFolder;
        }


        /// <summary>
        /// Get/Set section type
        /// </summary>
        public SectionTypeEnum Type { get; set; }

        



    }
}
