using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "progress" tag represents the progress of a task. 
    /// </summary>
    public class Progress: BaseInlineItem
    {
        public const string ElementName = "progress";

        public Progress()
        {
            RegisterAttribute(_maxAttribute);
            RegisterAttribute(_valueAttribute);
        }

        private readonly MaxAttribute _maxAttribute = new MaxAttribute();
        private readonly MeterValueAttribute _valueAttribute = new MeterValueAttribute();


        /// <summary>
        /// Specifies how much work the task requires in total
        /// </summary>
        public MaxAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies how much of the task has been completed
        /// </summary>
        public MeterValueAttribute Value { get { return _valueAttribute; }}



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
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
