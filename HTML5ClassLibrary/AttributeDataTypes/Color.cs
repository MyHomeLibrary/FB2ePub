using System.Text.RegularExpressions;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    public class Color : IAttributeDataType
    {
        private enum ColorOptions
        {
            Invalid,
            ColorName,
            HexNumber,
            RGBNumber,
        }

        private ColorOptions _colorType = ColorOptions.Invalid;
        private string _colorData = string.Empty;

        public string Value
        {
            get
            {
                switch (_colorType)
                {
                    case ColorOptions.ColorName:
                        return _colorData;
                    case ColorOptions.HexNumber:
                        return string.Format("#{0}", _colorData);
                    case ColorOptions.RGBNumber:
                        return string.Format("rgb({0})", _colorData);
                }
                return string.Empty;
            }
            set
            {
                if (value.StartsWith("#") && value.Length > 1)
                {
                    _colorType = ColorOptions.HexNumber;
                    _colorData = value.Substring(1);
                }
                else
                {
                    var rgbTester = new Regex(@"rgb\(\d+\)$");
                    if (rgbTester.IsMatch(value))
                    {
                        _colorType = ColorOptions.RGBNumber;
                        Match m = rgbTester.Match(value);
                        _colorData = m.Groups[0].Value;
                    }
                    else
                    {
                        _colorType = ColorOptions.ColorName;
                        _colorData = value;
                    }
                }
            }
            
        }
    }
}
