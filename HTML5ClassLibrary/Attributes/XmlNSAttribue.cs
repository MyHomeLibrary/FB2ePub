using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class XmlNsAttribute : BaseAttribute
    {
        public override void AddAttribute(XElement xElement)
        {
            xElement.Add(new XAttribute("xmlns", @"http://www.w3.org/1999/xhtml"));
        }

        public override void ReadAttribute(XElement element)
        {

        }

        public override string Value
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

}
