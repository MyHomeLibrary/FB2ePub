using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "wbr" (Word Break Opportunity) tag specifies where in a text it would be ok to add a line-break.
    /// Tip: When a word is too long, or you are afraid that the browser will break your lines at the wrong place, you can use the "wbr" element to add word break opportunities.
    /// </summary>
    public class WordBreakOportunity : BaseInlineItem
    {
        public const string ElementName = "wbr";

        private string _text;


        /// <summary>
        /// Contained text
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement) xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception("xNode is not empty line element");
            }
            ReadAttributes(xElement);
            _text = xElement.Value;
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);
            AddAttributes(xElement);
            xElement.Value = _text;
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void Add(IHTML5Item item)
        {
            throw new Exception("");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
