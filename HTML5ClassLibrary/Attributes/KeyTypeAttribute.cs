using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
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




        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(GetAttributeName());
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

    }
}
