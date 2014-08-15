using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.BlockElements;
using HTMLClassLibrary.BaseElements.InlineElements;
using HTMLClassLibrary.Exceptions;

namespace HTMLClassLibrary.BaseElements.MapAreas
{
    /// <summary>
    /// The map element specifies a client-side image map that may be referenced by elements such as img, select and object.
    /// </summary>
    [HTMLItemAttribute(ElementName = "map", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Map : HTMLItem, IInlineItem
    {
        public Map()
        {
            RegisterAttribute(_nameAttribute);
        }

        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTMLItem> _content = new List<IHTMLItem>();

        private readonly NameAttribute _nameAttribute = new NameAttribute();

        /// <summary>
        /// Required. Specifies the name of an image-map
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Area)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
            {
                return item.IsValid();
            }            
            return false;
        }


        public override bool IsValid()
        {
            bool valid = _nameAttribute.HasValue();
            if (valid)
            {
                if (GlobalAttributes.ID.HasValue() && // if ID set should have same value as Name
                    string.Compare(GlobalAttributes.ID.Value, _nameAttribute.Value, StringComparison.InvariantCulture) != 0)
                {
                    valid = false;
                }
            }
            return valid;
        }
    }
}
