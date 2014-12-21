using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EPubLibrary.Content.Spine
{
    /// <summary>
    /// Represents a spine item in a V3 spine section of the content document
    /// </summary>
    class SpineItemV3
    {
        public enum FlowOptions
        {
            NotSet,                 //  Not set - default
            Auto,                   //  Indicates no preference for overflow content handling by the Author. 
            Paginated,              //  Indicates the Author preference is to dynamically paginate content overflow. 
            ScrolledContinuous,     //  Indicates the Author preference is to provide a scrolled view for overflow content, and that consecutive spine items with this property are to be rendered as a continuous scroll.
            ScrolledDoc,            //  Indicates the Author preference is to provide a scrolled view for overflow content, and each spine item with this property is to be rendered as separate scrollable document. 
        }

        public enum LayoutOptions
        {
            NotSet,             //  Not set - default
            PrePaginated,       //  Specifies that the given spine item is pre-paginated. 
            Reflowable,         //  Specifies that the given spine item is reflowable. 
        }

        public enum OrientationOptions
        {
            NotSet,             //  Not set - default
            Auto,               //  Specifies that the Reading System can determine the orientation to rendered the spine item in. 
            Landscape,          //  Specifies that the given spine item is to be rendered in landscape orientation. 
            Portrait,           //  Specifies that the given spine item is to be rendered in portrait orientation. 
        }

        public enum PageSpreadOptions
        {
            NotSet,             //  Not set - default
            Center,             //  Specifies the forced placement of a Content Document in a Synthetic Spread 
            Left,               //  The page-spread-left property indicates that the first page of the associated item element's EPUB Content Document represents the left-hand side of a two-page spread.
            Right,              //  The page-spread-right property indicates that the first page of the associated item element's EPUB Content Document represents the right-hand side of a two-page spread.
        }

        public enum SpreadOrientationOptions
        {
            NotSet,             //  Not set - default
            Auto,               //  Specifies the Reading System can determine when to render a synthetic spread for the spine item. 
            Both,               //  Specifies the Reading System should render a synthetic spread for the spine item in both portrait and landscape orientations. 
            Landscape,          //  Specifies the Reading System should render a synthetic spread for the spine item only when in landscape orientation. 
            None,               //  Specifies the Reading System should not render a synthetic spread for the spine item. 
            Portrait,
        }



        /// <summary>
        /// Itemref name - An IDREF [XML] that identifies a manifest item.
        /// </summary>
        public string Name { get; set; }

        // the following is for V3 only

        private bool _linear = true;

        /// <summary>
        /// Specifies whether the referenced content is primary.
        /// </summary>
        public bool Linear
        {
            get { return _linear; }
            set { _linear = value; }
        }


        /// <summary>
        /// The ID [XML] of this element, which must be unique within the document scope.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        ///  Specifies that the given spine item should be centered horizontally in the viewport or spread. 
        /// </summary>
        public bool AlignXCenter { get; set; }

        /// <summary>
        /// One of the flow options 
        /// </summary>
        public FlowOptions Flow { get; set; }

        /// <summary>
        /// One of the layout options
        /// </summary>
        public LayoutOptions Layout { get; set; }

        /// <summary>
        /// One of the page orientation options
        /// </summary>
        public OrientationOptions PageOrientation { get; set; }


        /// <summary>
        /// One of page spread options
        /// </summary>
        public PageSpreadOptions PageSpread { get; set; }

        /// <summary>
        /// One of the spread orientation options
        /// </summary>
        public SpreadOrientationOptions SpreadOrientation { get; set; }
    }
}
