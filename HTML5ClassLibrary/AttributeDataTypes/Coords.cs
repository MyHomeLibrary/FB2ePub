using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    ///     Comma separated list of coordinates to use in defining areas. For example: 0,0,118,28.
    /// </summary>
    public class Coords
    {
        private readonly List<int> _coords = new List<int>();

        public string Value
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (var content in _coords)
                {
                    builder.AppendFormat("{0},", content);
                }
                // remove last one
                builder.Remove(builder.Length - 1, 1);
                return builder.ToString();
            }

            set
            {
                _coords.Clear();
                string[] ar = value.Split(',');
                foreach (var s in ar)
                {
                    _coords.Add(int.Parse(s));
                }
            }
            
        }
    }
}
