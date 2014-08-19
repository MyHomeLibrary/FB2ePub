using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// The scrolling attribute specifies whether or not to display scrollbars in a "frame".
    /// By default, scrollbars appear in a "frame" if the content is larger than the "frame".
    /// </summary>
    public class ScrollingAttribute : BaseAttribute
    {
        private enum Scrollingype
        {
            Invalid,
            Auto,
            Yes,
            No
        }

        private Scrollingype _scrolling = Scrollingype.Invalid;

        private const string AttributeName = "scrolling";

        public override string Value
        {
            get
            {
                switch (_scrolling)
                {
                    case Scrollingype.Auto:
                        return "auto";
                    case Scrollingype.Yes:
                        return "yes";
                    case Scrollingype.No:
                        return "no ";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "auto":
                        _scrolling = Scrollingype.Auto;
                        break;
                    case "yes":
                        _scrolling = Scrollingype.Yes;
                        break;
                    case "no":
                        _scrolling = Scrollingype.No;
                        break;
                    default:
                        _scrolling = Scrollingype.Invalid;
                        break;
                }
            }
        }



        #region Overrides of BaseAttribute

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
