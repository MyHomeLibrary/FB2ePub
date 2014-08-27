using System;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.MapAreas
{
    /// <summary>
    /// The map element specifies a client-side image map that may be referenced by elements such as img, select and object.
    /// </summary>
    [HTMLItemAttribute(ElementName = "map", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Map : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>();

        /// <summary>
        /// Required. Specifies the name of an image-map
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; }}


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
