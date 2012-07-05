using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontsSettings;

namespace Fb2epubSettings
{
    public class CSSElementListItem 
    {
        private readonly List<CSSFontFamily> _fonts = new List<CSSFontFamily>();

        public string Name { get; set; }
        public string Class { get; set; }
        public string FullName { get { return ToString(); } }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Class))
            {
                return Name;
            }
            return string.Format("{0}.{1}",Name,Class);
        }

        public List<CSSFontFamily> Fonts { get { return _fonts; } }
        
    }
}
