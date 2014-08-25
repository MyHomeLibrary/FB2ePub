using System;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// The value may be either in pixels or a percentage of the available horizontal or vertical space. 
    /// Thus, the value "50%" means half of the available space.
    /// </summary>
    public class Length : IAttributeDataType
    {
        private enum DataType
        {
            Unknown,
            Points,
            Percents
        }

        private DataType _valueType = DataType.Unknown;
        private int _dataValue;


        public string Value
        {
            get
            {
                switch (_valueType)
                {
                    case DataType.Percents:
                        return string.Format("{0}%", _valueType);
                    case DataType.Points:
                        return _dataValue.ToString();
                }
                return string.Empty;
            }

            set
            {
                _valueType = DataType.Unknown;
                if (value.Contains("%"))
                {
                    int pos = value.IndexOf("%", StringComparison.Ordinal);
                    string temp = value.Remove(pos);
                    if (int.TryParse(temp,out _dataValue))
                    {
                       _valueType = DataType.Percents;
                    }
                }
                else
                {
                    if (int.TryParse(value,out _dataValue))
                    {
                       _valueType = DataType.Points;
                    }
                }
            }
        }
    }
}
