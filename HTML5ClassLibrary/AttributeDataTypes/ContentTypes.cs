using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTML5ClassLibrary.AttributeDataTypes
{
    /// <summary>
    ///     A comma-separated list of media types.
    /// </summary>
    public class ContentTypes
    {
        private readonly List<ContentType> contents = new List<ContentType>();

        public string Value
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (var content in contents)
                {
                    builder.AppendFormat("{0},", content.Value);
                }
                // remove last one
                builder.Remove(builder.Length - 1, 1);
                return builder.ToString();
            }

            set
            {
                contents.Clear();
                string[] ar = value.Split(',');
                foreach (var s in ar)
                {
                    ContentType charset = new ContentType();
                    charset.Value = s;
                    contents.Add(charset);
                }
            }
        }

    }
}
