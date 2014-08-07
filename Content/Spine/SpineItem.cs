using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace EPubLibrary.Content.Spine
{
    class PropertyDescription : Attribute
    {
        public string Text;

        public PropertyDescription(string text)
        {
            Text = text;
        }

        public static string GetDescription(Enum en)
        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if ( memInfo.Length > 0)
            {

                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description),
                false);

                if (attrs.Length > 0)

                    return ((PropertyDescription)attrs[0]).Text;

            }

            return en.ToString();

        }
    }

    /// <summary>
    /// http://www.idpf.org/epub/301/spec/epub-publications.html#sec-itemref-property-values
    /// </summary>
    public enum EPubV3Properties
    {
        [PropertyDescription("rendition:align-x-center")]
        rendition_align_x_center,
        [PropertyDescription("rendition:flow-auto ")]
        rendition_flow_auto,
        [PropertyDescription("rendition:flow-paginated")]
        rendition_flow_paginated,
        [PropertyDescription("rendition:flow-scrolled-continuous")]
        rendition_flow_scrolled_continuous,
        [PropertyDescription("rendition:flow-scrolled-doc")]
        rendition_flow_scrolled_doc,
        [PropertyDescription("rendition:layout-pre-paginated")]
        rendition_layout_pre_paginated,
        [PropertyDescription("rendition:layout-reflowable")]
        rendition_layout_reflowable,
        [PropertyDescription("rendition:orientation-auto")]
        rendition_orientation_auto,
        [PropertyDescription("rendition:orientation-landscape")]
        rendition_orientation_landscape,
        [PropertyDescription("rendition:orientation-portrait")]
        rendition_orientation_portrait,
        [PropertyDescription("rendition:page-spread-center")]
        rendition_page_spread_center,
        [PropertyDescription("page-spread-right")]
        page_spread_right,
        [PropertyDescription("rendition:spread-auto")]
        rendition_spread_auto,
        [PropertyDescription("rendition:spread-both")]
        rendition_spread_both,
        [PropertyDescription("rendition:spread-landscape")]
        rendition_spread_landscape,
        [PropertyDescription("rendition:spread-none")]
        rendition_spread_none,
        [PropertyDescription("rendition:spread-portrait")]
        rendition_spread_portrait,
    }

    public class SpineItem
    {

        public string Name { get; set; }

        // the following is for V3 only

        private bool _linear = true;

        public bool Linear
        {
            get { return _linear; } 
            set { _linear = value; }
        }


        public string ID { get; set; }

        private readonly List<EPubV3Properties> _properties = new List<EPubV3Properties>();

        public List<EPubV3Properties> Properties
        {
            get { return _properties; }
        }
    }
}