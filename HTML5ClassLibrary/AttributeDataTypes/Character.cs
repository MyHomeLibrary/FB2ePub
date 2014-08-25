using System;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A single character from the document character set
    /// </summary>
    public class Character : IAttributeDataType
    {
        private char? _character = null;

        public string Value
        {
            get
            {
                if (_character.HasValue)
                {
                    return _character.ToString();
                }
                return null;
            }

            set
            {
                if (value.Length > 1)
                {
                    throw new ArgumentException(string.Format("The value set should be single character, not {0}",value));
                }
                if (string.IsNullOrEmpty(value))
                {
                    _character = null;
                }
                _character = value[0];
            }
        }
    }
}
