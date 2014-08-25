namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class FloatingNumber : IAttributeDataType
    {
        private float? _number = null;

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
                float temp;
                if (float.TryParse(value, out temp))
                {
                    _number = temp;
                }
            }
        }
    }
}
