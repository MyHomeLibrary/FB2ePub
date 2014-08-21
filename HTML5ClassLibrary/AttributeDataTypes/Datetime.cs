using System;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    ///     Date and time information in the ISO-8601 format. For example: YYYY-MM-DDThh:mm:ss.
    /// </summary>
    public class Datetime
    {
        private DateTime _date = DateTime.MinValue;
        public string Value
        {
            get
            {
                return _date.ToString("O");
            }
            set
            {
                _date = DateTime.MinValue;
                try
                {
                    _date = DateTime.Parse(value);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
