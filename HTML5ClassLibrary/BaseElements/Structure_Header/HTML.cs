﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "html" tag tells the browser that this is an HTML document.
    ///The "html" tag represents the root of an HTML document.
    ///The "html" tag is the container for all other HTML elements (except for the "!DOCTYPE" tag).
    /// </summary>
    [HTMLItemAttribute(ElementName = "html", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class HTML : HTMLItem
    {
        public HTML()
        {
            RegisterAttribute(_xhtmlNameSpaceAttribute);
            RegisterAttribute(_manifestAttribute);            
        }


        private readonly XmlNsAttribute _xhtmlNameSpaceAttribute = new XmlNsAttribute();
        private readonly ManifestAttribute _manifestAttribute = new ManifestAttribute();


        /// <summary>
        /// This attribute has been deprecated (made outdated). 
        /// It is redundant, because version information is now provided by the DOCTYPE.
        /// </summary>
        public ManifestAttribute Manifest { get {return _manifestAttribute;}}


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (Subitems.Count >= 2) // no more than two sub elements
            {
                return false;
            }
            if (Subitems.Count == 0) // head have to be first
            {
                if (!(item is Head)  )
                {
                    return false;
                }
            }
            if (Subitems.Count == 1) // body have to be second
            {
                if (!(item is Body)  )
                {
                    return false;
                }
            }

            return item.IsValid();
        }


        public override bool IsValid()
        {
            if (Subitems.Count != 2)
            {
                return false;
            }
            if (!(Subitems[0] is Head))
            {
                return false;
            }
            if (!(Subitems[1] is Body))
            {
                return false;
            }
            return (Subitems[0].IsValid() && Subitems[1].IsValid());
        }
    }
}
