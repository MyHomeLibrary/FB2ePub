using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Spine
{
    class SpineSectionV3 : List<SpineItemV3>
    {
        public enum PageProgressionDirectionOptions
        {
            NotSet,
            Default,        // reader system default
            LeftToRight,
            RightToLeft,
        }

        private V3Standard _standard;

        public SpineSectionV3(V3Standard standard)
        {
            _standard = standard;
        }

        /// <summary>
        /// ID of the NCX TOC element , used only for compatibility with v2
        /// </summary>
        public string TOCId { get; set; }


        /// <summary>
        /// Optional ID element for a spine
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Defines page progression (reading) direction
        /// </summary>
        public PageProgressionDirectionOptions PageProgressionDirection { get; set; }

        public XElement GenerateSpineElement()
        {
            var spineElement = new XElement(EPubNamespaces.OpfNameSpace + "spine");
            if (!string.IsNullOrEmpty(TOCId))
            {
                spineElement.Add(new XAttribute("toc", TOCId)); 
            }
            if (!string.IsNullOrEmpty(ID))
            {
                spineElement.Add(new XAttribute("id", ID));
            }

            AddPageProgressionDirectionm(spineElement);

            foreach (var spineItem in this)
            {
                var itemref = new XElement(EPubNamespaces.OpfNameSpace + "itemref");
                itemref.Add(new XAttribute("idref", spineItem.Name));
                if (!spineItem.Linear) // true by default so no need to set
                {
                    itemref.Add(new XAttribute("linear", "false"));
                }
                if (!string.IsNullOrEmpty(spineItem.ID))
                {
                    itemref.Add(new XAttribute("id", spineItem.ID));
                }
                var properties = new List<string>();
                AddAlignXCenter(spineItem,properties);
                AddFlowOptions(spineItem, properties);
                AddLayoutOptions(spineItem, properties);
                AddPageOrientationOptions(spineItem, properties);
                AddPageSpreadOptions(spineItem, properties);
                AddSpreadOrientationOptions(spineItem, properties);
                if (properties.Count > 0)
                {
                    var sb = new StringBuilder();
                    bool first = true;
                    foreach (var property in properties)
                    {
                        if (!first)
                        {
                            sb.AppendFormat(" {0}", property);
                        }
                        else
                        {
                            sb.AppendFormat("{0}", property);
                            first = false;
                        }
                    }
                    itemref.Add(new XAttribute("properties", sb.ToString()));
                  
                }
                spineElement.Add(itemref);
            }
            return spineElement;
        }

        private void AddPageProgressionDirectionm(XElement spineElement)
        {
            switch (PageProgressionDirection)
            {
                case PageProgressionDirectionOptions.Default:
                    spineElement.Add(new XAttribute("page-progression-direction", "default")); 
                    break;
                case PageProgressionDirectionOptions.LeftToRight:
                    spineElement.Add(new XAttribute("page-progression-direction", "ltr")); 
                    break;
                case PageProgressionDirectionOptions.RightToLeft:
                    spineElement.Add(new XAttribute("page-progression-direction", "rtl")); 
                    break;
            }
        }

        private void AddPageSpreadOptions(SpineItemV3 spineItem, List<string> properties)
        {
            switch (spineItem.PageSpread)
            {
                case SpineItemV3.PageSpreadOptions.Center:
                    properties.Add("rendition:page-spread-center");
                    break;
                case SpineItemV3.PageSpreadOptions.Left:
                    properties.Add("page-spread-left");
                    break;
                case SpineItemV3.PageSpreadOptions.Right:
                    properties.Add("page-spread-right");
                    break;
            }
        }

        private void AddSpreadOrientationOptions(SpineItemV3 spineItem, List<string> properties)
        {
            switch (spineItem.SpreadOrientation)
            {
                case SpineItemV3.SpreadOrientationOptions.Auto:
                    properties.Add("rendition:spread-auto");
                    break;
                case SpineItemV3.SpreadOrientationOptions.Both:
                    properties.Add("rendition:spread-both");
                    break;
                case SpineItemV3.SpreadOrientationOptions.Landscape:
                    properties.Add("rendition:spread-landscape");
                    break;
                case SpineItemV3.SpreadOrientationOptions.None:
                    properties.Add("rendition:spread-none");
                    break;
                case SpineItemV3.SpreadOrientationOptions.Portrait:
                    properties.Add("rendition:spread-portrait");
                    break;
            }
        }

        private void AddPageOrientationOptions(SpineItemV3 spineItem, List<string> properties)
        {
            switch (spineItem.PageOrientation)
            {
                case SpineItemV3.OrientationOptions.Auto:
                    properties.Add("rendition:orientation-auto");
                    break;
                case SpineItemV3.OrientationOptions.Landscape:
                    properties.Add("rendition:orientation-landscape");
                    break;
                case SpineItemV3.OrientationOptions.Portrait:
                    properties.Add("rendition:orientation-portrait");
                    break;
            }
        }

        private void AddLayoutOptions(SpineItemV3 spineItem, List<string> properties)
        {
            switch (spineItem.Layout)
            {
                case SpineItemV3.LayoutOptions.PrePaginated:
                    properties.Add("rendition:layout-pre-paginated");
                    break;
                case SpineItemV3.LayoutOptions.Reflowable:
                    properties.Add("rendition:layout-reflowable");
                    break;
            }
        }

        private void AddFlowOptions(SpineItemV3 spineItem, List<string> properties)
        {
            switch (spineItem.Flow)
            {
                case SpineItemV3.FlowOptions.Auto:
                    properties.Add("rendition:flow-auto");
                    break;
                case SpineItemV3.FlowOptions.Paginated:
                    properties.Add("rendition:flow-paginated");
                    break;
                case SpineItemV3.FlowOptions.ScrolledContinuous:
                    properties.Add("rendition:flow-scrolled-continuous");
                    break;
                case SpineItemV3.FlowOptions.ScrolledDoc:
                    properties.Add("rendition:flow-scrolled-doc");
                    break;
            }
        }

        private void AddAlignXCenter(SpineItemV3 spineItem, List<string> properties)
        {
            if (spineItem.AlignXCenter)
            {
                properties.Add("rendition:align-x-center");
            }
        }

    }
}
