using System;
using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class ValuesSelectionTypeAttribute<T> : SimpleSingleTypeAttribute<T> where T : IAttributeDataType, new()
    {
        private readonly List<string> _possibleValues = new List<string>();


        public ValuesSelectionTypeAttribute(string listOfPossibleValues)
        {
            var values = listOfPossibleValues.Split(';');
            foreach (var item in values)
            {
                _possibleValues.Add(item);
            }
        }


        public override string Value
        {
            set
            {
                if (_possibleValues.Count > 0)
                {
                    if (!_possibleValues.Contains(value))
                    {
                        throw new ArgumentException(string.Format("The value {0} is not one of the psossible values", value));
                    }
                }
                AttrObject.Value = value;
                AttributeHasValue = (value != string.Empty);
            }
        }

    }
}
