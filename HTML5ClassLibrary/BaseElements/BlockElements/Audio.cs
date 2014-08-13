using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    public class Audio : BaseBlockElement
    {
        public const string ElementName = "audio";

        private readonly SourceAttribute _src = new SourceAttribute();
        private readonly AutoPlayAttribute _autoplay = new AutoPlayAttribute();
        private readonly ControlsAttribute _controls = new ControlsAttribute();
        private readonly LoopAttribute  _loop = new LoopAttribute();
        private readonly MutedAttribute _muted = new MutedAttribute();
        private readonly PreloadAttribute _preload = new PreloadAttribute();

        public Audio ()
        {
            Attributes.Add(_src);
            Attributes.Add(_autoplay);
            Attributes.Add(_controls);
            Attributes.Add(_loop);
            Attributes.Add(_muted);
            Attributes.Add(_preload);

        }

        public SourceAttribute Src
        {
            get { return _src; }
        }

        public AutoPlayAttribute AutoPlay
        {
            get { return _autoplay; }
        }

        public ControlsAttribute Controls
        {
            get { return _controls; }    
        }

        public LoopAttribute Loop
        {
            get { return _loop; }
        }

        public MutedAttribute Muted
        {
            get { return _muted; }
        }

        public PreloadAttribute Preload
        {
            get { return _preload; }
        }

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

            Content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Content.Add(item);
                }
            }
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }
       
    }
}
