using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XHTMLClassLibrary
{
    public class CustomNamespace
    {
        public CustomNamespace(XName prefix, XNamespace xNamespace)
        {
            Prefix = prefix;
            XNamespace = xNamespace;
        }

        public XNamespace XNamespace { get; set; }
        public XName Prefix { get; set; }
    }
}
