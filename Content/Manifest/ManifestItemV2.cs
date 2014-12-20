using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary.Content.Manifest
{
        
    public class ManifestItemV2
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

        public  string MediaOverlay { get; set; }
    }
}