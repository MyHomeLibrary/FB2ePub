﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.BaseElements.BlockElements;
using Script = HTML5ClassLibrary.BaseElements.InlineElements.Script;

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The head element contains information about the current document, such as its title, 
    /// keywords that may be useful to search engines, 
    /// and other data that is not considered to be document content. 
    /// This information is usually not displayed by browsers.
    /// </summary>
    public class Head : BaseContainingElement
    {
        internal const string ElementName = "head";


        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";




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
            if (item is Title   || 
                item is Base    ||
                item is Link    ||
                item is Meta    ||
                item is Script  ||
                item is Style   ||
                item is NoScript
                )
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
            return (Content.Count(x => x is Title) <= 1);
        }

    }
}
