using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    public class HTML :BaseContainingElement
    {

        internal const string ElementName = "html";


        private readonly XmlNsAttribute _xhtmlNameSpaceAttribute = new XmlNsAttribute();
        private readonly ManifestAttribute _manifestAttribute = new ManifestAttribute();


        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";


        /// <summary>
        /// This attribute has been deprecated (made outdated). 
        /// It is redundant, because version information is now provided by the DOCTYPE.
        /// </summary>
        public ManifestAttribute Manifest { get {return _manifestAttribute;}}

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            GlobalAttributes.ReadAttributes(xElement);
            FormEvents.ReadAttributes(xElement);
            KeyboardEvents.ReadAttributes(xElement);
            MediaEvents.ReadAttributes(xElement);
            MouseEvents.ReadAttributes(xElement);
            WindowEvents.ReadAttributes(xElement);
            // XhtmlNameSpaceAttribute.ReadAttribute(xElement); - no need to read , always the same value should be there
            _manifestAttribute.ReadAttribute(xElement);


            Content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                   }
                    catch (Exception)
                    {
                        continue;
                    }
                    Content.Add(item);
                }
            }


        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (Content.Count >= 2) // no more than two sub elements
            {
                return false;
            }
            if (Content.Count == 0) // head have to be first
            {
                if (!(item is Head)  )
                {
                    return false;
                }
            }
            if (Content.Count == 1) // body have to be second
            {
                if (!(item is Body)  )
                {
                    return false;
                }
            }

            return item.IsValid();
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            _xhtmlNameSpaceAttribute.AddAttribute(xElement);
            _manifestAttribute.AddAttribute(xElement);
            GlobalAttributes.AddAttributes(xElement);
            FormEvents.AddAttributes(xElement);
            KeyboardEvents.AddAttributes(xElement);
            MediaEvents.AddAttributes(xElement);
            MouseEvents.AddAttributes(xElement);
            WindowEvents.AddAttributes(xElement);
            
            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }


            return xElement;

        }

        public override bool IsValid()
        {
            if (Content.Count != 2)
            {
                return false;
            }
            if (!(Content[0] is Head))
            {
                return false;
            }
            if (!(Content[1] is Body))
            {
                return false;
            }
            return (Content[0].IsValid() && Content[1].IsValid());
        }
    }
}
