using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTML5ClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class FloatingNumber
    {
        private float? _number = null;

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
                float temp;
                if (float.TryParse(value, out temp))
                {
                    _number = temp;
                }
            }
        }
    }
}
