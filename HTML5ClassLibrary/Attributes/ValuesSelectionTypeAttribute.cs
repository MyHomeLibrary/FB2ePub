using System;
using System.Collections.Generic;
using System.ComponentModel;
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

   
        /// <summary>
        /// Initialize based on enum values
        /// </summary>
        /// <param name="possibleValues">thould be Enum based type</param>
        public ValuesSelectionTypeAttribute(Type possibleValues)
        {
            if (!possibleValues.IsEnum)
            {
                throw new ArgumentException(string.Format("The argument passed should be enumiration, the {0} is not enum",possibleValues),"possibleValues");
            }
            var names = Enum.GetNames(possibleValues);
            // iterate over all enum members
            foreach (var name in names)
            {
                // try to read custom [Description] attribute from the member
                var type = possibleValues;
                var memInfo = type.GetMember(name);
                var attributes = (DescriptionAttribute[])memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                    false);
                // if attribute present - use it , if not use enum member name directly
                _possibleValues.Add(attributes.Length > 1 ? attributes[0].Description : name);
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
