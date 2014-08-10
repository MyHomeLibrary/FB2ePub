using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The noscript element allows authors to provide alternate content when a script is not executed. 
    /// This can be because the Web browser is configured not to process scripts, or because the given script language is not supported.
    /// </summary>
    public class NoScript : BaseBlockElement 
    {

        internal const string ElementName = "noscript";


        #region Overrides of BaseBlockElement 

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
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
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            return false;
        }


        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>generated XNode</returns>
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

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }

        #endregion

    }
}
