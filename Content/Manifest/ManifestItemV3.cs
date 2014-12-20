using System.Collections.Generic;

namespace EPubLibrary.Content.Manifest
{
    class ManifestItemV3
    {
        public string ID { get; set; }
        public string HRef { get; set; }
        public EPubCoreMediaType MediaType { get; set; }

        // the following are supported only in V3
        public string Fallback { get; set; }

        private readonly List<string> _properties = new List<string>();

        public List<string> Properties
        {
            get { return _properties; }
        }

        public string MediaOverlay { get; set; }
    }
}
