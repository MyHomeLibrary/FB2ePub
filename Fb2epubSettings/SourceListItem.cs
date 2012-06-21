using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontsSettings;

namespace Fb2epubSettings
{
    internal class SourceListItem
    {
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Source.Location))
                {
                    return "Undefined source";
                }
                return Source.Location;
            }
        }
        public FontSource Source {get;set;}

        public override string ToString()
        {
            return Name;
        }
    }
}
