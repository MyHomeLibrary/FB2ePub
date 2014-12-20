using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

            if (memInfo.Length > 0)
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
        RenditionAlignXCenter,
        [PropertyDescription("rendition:flow-auto ")]
        RenditionFlowAuto,
        [PropertyDescription("rendition:flow-paginated")]
        RenditionFlowPaginated,
        [PropertyDescription("rendition:flow-scrolled-continuous")]
        RenditionFlowScrolledContinuous,
        [PropertyDescription("rendition:flow-scrolled-doc")]
        RenditionFlowScrolledDoc,
        [PropertyDescription("rendition:layout-pre-paginated")]
        RenditionLayoutPrePaginated,
        [PropertyDescription("rendition:layout-reflowable")]
        RenditionLayoutReflowable,
        [PropertyDescription("rendition:orientation-auto")]
        RenditionOrientationAuto,
        [PropertyDescription("rendition:orientation-landscape")]
        RenditionOrientationLandscape,
        [PropertyDescription("rendition:orientation-portrait")]
        RenditionOrientationPortrait,
        [PropertyDescription("rendition:page-spread-center")]
        RenditionPageSpreadCenter,
        [PropertyDescription("page-spread-right")]
        PageSpreadRight,
        [PropertyDescription("rendition:spread-auto")]
        RenditionSpreadAuto,
        [PropertyDescription("rendition:spread-both")]
        RenditionSpreadBoth,
        [PropertyDescription("rendition:spread-landscape")]
        RenditionSpreadLandscape,
        [PropertyDescription("rendition:spread-none")]
        RenditionSpreadNone,
        [PropertyDescription("rendition:spread-portrait")]
        RenditionSpreadPortrait,
    }



    class SpineItemV3
    {

        private readonly List<EPubV3Properties> _properties = new List<EPubV3Properties>();

        public List<EPubV3Properties> Properties
        {
            get { return _properties; }
        }


        public string Name { get; set; }

        // the following is for V3 only

        private bool _linear = true;

        public bool Linear
        {
            get { return _linear; }
            set { _linear = value; }
        }


        public string ID { get; set; }

    }
}
