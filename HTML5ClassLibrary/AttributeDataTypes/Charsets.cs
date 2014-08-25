using System.Collections.Generic;
using System.Text;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A space-separated list of character encodings.
    /// </summary>
    public class Charsets : IAttributeDataType
    {
        private readonly List<Charset> _charsets = new List<Charset>();

        public string Value
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var charset in _charsets)
                {
                    builder.AppendFormat("{0} ",charset.Value);
                }
                return builder.ToString();
            }

            set
            {
                _charsets.Clear();
                string[] ar = value.Split(' ');
                foreach (var s in ar)
                {
                    var charset = new Charset {Value = s};
                    _charsets.Add(charset);
                }
            }
        }
    }
}
