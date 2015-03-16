namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    ///     One or more digits.
    /// </summary>
    public class Number : IAttributeDataType
    {
        private int? _number = null;

        public string Value
        {
            get
            {
                if (_number.HasValue)
                {
                    return _number.ToString();
                }
                return string.Empty;
            }

            set
            {
                _number = null;
                int temp;
                if(int.TryParse(value, out temp))
                {
                    _number = temp;
                }
            }
        }
    }
}
