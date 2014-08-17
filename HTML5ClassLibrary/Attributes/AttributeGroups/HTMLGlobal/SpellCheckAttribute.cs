using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The spellcheck attribute specifies whether the element is to have its spelling and grammar checked or not.
    /// The following can be spellchecked:
    ///    Text values in input elements (not password)
    ///    Text in "textarea" elements
    ///    Text in editable elements
    /// </summary>
    public class SpellCheckAttribute : BaseAttribute
    {
        private enum SpellCheckValues
        {
            Invalid,
            True,
            False,
        }

        private SpellCheckValues _spellcheck = SpellCheckValues.Invalid;

        private const string AttributeName = "spellcheck";

        #region Overrides of BaseAttribute


        public override string Value
        {
            get
            {
                switch (_spellcheck)
                {
                    case SpellCheckValues.True:
                        return "true";
                    case SpellCheckValues.False:
                        return "false";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "true":
                        _spellcheck = SpellCheckValues.True;
                        break;
                    case "false":
                        _spellcheck = SpellCheckValues.False;
                        break;
                    default:
                        _spellcheck = SpellCheckValues.Invalid;
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
