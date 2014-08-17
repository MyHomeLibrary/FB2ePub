using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// The value is an integer that represents the number of pixels of the canvas (screen, paper). 
    /// Thus, the value "50" means fifty pixels.
    /// </summary>
    public class Pixels
    {
        private int? _number = null;

        public string Value
        {
            get
            {
                if (_number.HasValue)
                {
                    return _number.ToString();
                }
                return string.Empty;
            }

            set
            {
                _number = null;
                int temp;
                if (int.TryParse(value, out temp))
                {
                    _number = temp;
                }
            }
        }

    }
}
