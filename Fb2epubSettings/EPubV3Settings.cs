using System.Xml.Serialization;

namespace Fb2epubSettings
{
    [XmlRoot(ElementName = "EPubV3Settings")]
    public class EPubV3Settings
    {
        private EPubV3SubStandard _v3SubStandard = EPubV3SubStandard.V30;


        public void SetupDefaults()
        {
            _v3SubStandard = EPubV3SubStandard.V30;
        }

        /// <summary>
        /// Variant (revision) of V3 standard 
        /// </summary>
        [XmlElement(ElementName = "EPUB3SubVersion")]
        public EPubV3SubStandard V3SubStandard
        {
            get { return _v3SubStandard; }
            set { _v3SubStandard = value; }
        }


        internal void CopyFrom(EPubV3Settings temp)
        {
            _v3SubStandard = temp._v3SubStandard;
        }
    }
}
