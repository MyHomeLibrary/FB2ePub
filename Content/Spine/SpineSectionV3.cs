using System.Collections.Generic;
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

            AddPageProgressionDirection(spineElement);

            foreach (var spineItem in this)
            {
                XElement itemref = spineItem.GenerateElement();
                spineElement.Add(itemref);
            }
            return spineElement;
        }

        private void AddPageProgressionDirection(XElement spineElement)
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

    }
}
