using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class ColorAttribute : BaseAttribute
    {
        private enum ColorOptions
        {
            Invalid,
            ColorName,
            HexNumber,
            RGBNumber,
        }

        private ColorOptions _colorType = ColorOptions.Invalid;
        private ContentType _attrObject = new ContentType();
        private const string AttributeName = "color";

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new ContentType();
                _attrObject.Value = xObject.Value;
                AttributeHasValue = true;
            }
        }


        public override string Value
        {
            get
            {
                switch (_colorType)
                {
                    case ColorOptions.ColorName:
                        return _attrObject.Value;
                    case ColorOptions.HexNumber:
                        return string.Format("#{0}",_attrObject.Value);
                    case ColorOptions.RGBNumber:
                        return string.Format("rgb({0})",_attrObject.Value);
                }
                return string.Empty;
            }
            set
            {
                if (value.StartsWith("#") && value.Length >1)
                {
                    _colorType = ColorOptions.HexNumber;
                    _attrObject.Value = value.Substring(1);
                }
                else
                {
                    var rgbTester = new Regex("rgb(*)");
                    if (rgbTester.IsMatch(value))
                    {
                        _colorType = ColorOptions.RGBNumber;
                        Match m = rgbTester.Match(value);
                        _attrObject.Value = m.Groups[0].Value;
                    }
                    else
                    {
                        _colorType = ColorOptions.ColorName;
                        _attrObject.Value = value;
                    }
                }
            }
        }
    }
}
