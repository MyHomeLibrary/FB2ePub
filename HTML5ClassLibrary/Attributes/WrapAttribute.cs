using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class WrapAttribute : BaseAttribute
    {
        private enum WrapValues
        {
            Invalid,
            Hard,
            Soft,
        }

        private WrapValues _wrap = WrapValues.Invalid;

        private const string AttributeName = "wrap";

        #region Overrides of BaseAttribute


        public override string Value
        {
            get
            {
                switch (_wrap)
                {
                    case WrapValues.Hard:
                        return "hard";
                    case WrapValues.Soft:
                        return "soft";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "hard":
                        _wrap = WrapValues.Hard;
                        break;
                    case "soft":
                        _wrap = WrapValues.Soft;
                        break;
                    default:
                        _wrap = WrapValues.Invalid;
                        break;
                }
            }
        }


        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion

    }
}
