using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.BaseElements.ListElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The dl element is used to create a list where each item in the list comprises two parts: a term and a description. 
    /// A glossary of terms is a typical example of a definition list, 
    /// where each item consists of the term being defined and a definition of the term.
    /// </summary>
    public class DefinitionList : BaseBlockElement
    {
        public const string ElementName = "dl";


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

            ReadAttributes(xElement);

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
            if (item is DefinitionDescription)
            {
                return item.IsValid();
            }
            if (item is DefinitionTerms)
            {
                return item.IsValid();
            }
            return false;
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        public override bool IsValid()
        {
            return (Content.Count > 0);
        }
    }
}
