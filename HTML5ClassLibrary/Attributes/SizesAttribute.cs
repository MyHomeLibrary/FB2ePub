using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class SizesAttribute : BaseAttribute
    {
        private enum SizesValues
        {
            Invalid,
            Any,
            Values,
        }

        private struct Pair
        {
            public int x;
            public int y;
        }

        private SizesValues _type = SizesValues.Invalid;
        private Pair _sizes;


        private const string AttributeName = "sizes";

        #region Overrides of BaseAttribute


        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case SizesValues.Any:
                        return "any";
                    case SizesValues.Values:
                        return string.Format("{0}x{1}",_sizes.x,_sizes.y);
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "any":
                        _type = SizesValues.Any;
                        break;
                    default:
                        int indexOfX = value.IndexOf("x",StringComparison.InvariantCulture);
                        if (indexOfX != -1 && indexOfX +1 < value.Length)
                        {
                            string before = value.Substring(0, indexOfX);
                            string after = value.Substring(indexOfX + 1);
                            int x = 0;
                            int y = 0;
                            if (int.TryParse(before,out x) && int.TryParse(after,out y))
                            {
                                _sizes.x = x;
                                _sizes.y = y;
                                _type = SizesValues.Values;
                            }
                        }
                        _type = SizesValues.Invalid;
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
