using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTML5ClassLibrary.BaseElements.Structure_Header;

namespace HTML5ClassLibrary.Attributes
{
    /// <summary>
    /// The keytype attribute specifies a key type to be used.
    /// Note: The support for key types may vary between browsers.
    /// </summary>
    public class KeyTypeAttribute : BaseAttribute
    {
        private enum KeyTypes
        {
            Invalid,
            RSA,
            DSA,
            EC,
        }

        private KeyTypes _type = KeyTypes.Invalid;

        private const string AttributeName = "keytype";

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case KeyTypes.RSA:
                        return "rsa";
                    case KeyTypes.DSA:
                        return "dsa";
                    case KeyTypes.EC:
                        return "ec";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "rsa":
                        _type = KeyTypes.RSA;
                        break;
                    case "dsa":
                        _type = KeyTypes.DSA;
                        break;
                    case "ec":
                        _type = KeyTypes.EC;
                        break;
                    default:
                        _type = KeyTypes.Invalid;
                        break;
                }
            }
        }



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
