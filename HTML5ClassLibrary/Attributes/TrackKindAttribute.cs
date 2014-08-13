using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class TrackKindAttribute : BaseAttribute
    {
        private enum TrackKind
        {
            Invalid,
            Captions,
            Chapters,
            Descriptions,
            Metadata,
            Subtitles,
        }

        private TrackKind _trackKind = TrackKind.Invalid;

        private const string AttributeName = "kind";

        #region Overrides of BaseAttribute

        public override string Value
        {
            get
            {
                switch (_trackKind)
                {
                    case TrackKind.Captions:
                        return "captions";
                    case TrackKind.Chapters:
                        return "chapters";
                    case TrackKind.Descriptions:
                        return "descriptions";
                    case TrackKind.Metadata:
                        return "metadata";
                    case TrackKind.Subtitles:
                        return "subtitles";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "captions":
                        _trackKind = TrackKind.Captions;
                        break;
                    case "chapters":
                        _trackKind = TrackKind.Chapters;
                        break;
                    case "descriptions":
                        _trackKind = TrackKind.Descriptions;
                        break;
                    case "metadata":
                        _trackKind = TrackKind.Metadata;
                        break;
                    case "subtitles":
                        _trackKind = TrackKind.Subtitles;
                        break;
                    default:
                        _trackKind = TrackKind.Invalid;
                        break;
                }
            }
        }



        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion

    }
}
