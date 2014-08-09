using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTML5ClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A space-separated list of URI values.
    /// </summary>
    public class URIs
    {
        private readonly List<URI> _locations = new List<URI>();

        public string Value
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (var uri in _locations)
                {
                    builder.AppendFormat("{0} ", uri.Value);
                }
                return builder.ToString();
            }

            set
            {
                _locations.Clear();
                string[] ar = value.Split(' ');
                foreach (var s in ar)
                {
                    var uri = new URI {Value = s};
                    _locations.Add(uri);
                }
            }
        }

    }
}
