using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    public class CustomAttribute
    {
        public CustomAttribute(XName xname, string value)
        {
            Name = xname;
            Value = value;
        }

        public XName Name { get; set; }
        public string Value { get; set; }
    }
}
