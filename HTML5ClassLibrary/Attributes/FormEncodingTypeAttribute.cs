using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class FormEncodingTypeAttribute : BaseAttribute
    {
        private enum EncodingTypes
        {
            Invalid,
            ApplicationXwwwFormUrlencoded,
            MultipartFormData,
            TextPlain,
        }

        public override string Value
        {
            get
            {
                switch (_encodingType)
                {
                    case EncodingTypes.ApplicationXwwwFormUrlencoded:
                        return "application/x-www-form-urlencoded";
                    case EncodingTypes.MultipartFormData:
                        return "multipart/form-data";
                    case EncodingTypes.TextPlain:
                        return "text/plain";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "application/x-www-form-urlencoded":
                        _encodingType = EncodingTypes.ApplicationXwwwFormUrlencoded;
                        break;
                    case "multipart/form-data":
                        _encodingType = EncodingTypes.MultipartFormData;
                        break;
                    case "text/plain":
                        _encodingType = EncodingTypes.TextPlain;
                        break;
                    default:
                        _encodingType = EncodingTypes.Invalid;
                        break;
                }
            }
        }


        private EncodingTypes _encodingType = EncodingTypes.Invalid;

        private const string AttributeName = "formenctype";

        #region Overrides of BaseAttribute

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
