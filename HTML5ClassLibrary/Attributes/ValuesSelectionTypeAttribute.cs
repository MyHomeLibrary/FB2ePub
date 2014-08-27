using System;
using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class ValuesSelectionTypeAttribute<T> : SimpleSingleTypeAttribute<T> where T : IAttributeDataType, new()
    {
        private readonly List<string> _possibleValues = new List<string>();


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
                if (attributes.Length < 1)
                {
                    throw new ArgumentException(string.Format("The argument type {0} have to have Description attribute set on all it's members, Description on member {1} is missing", possibleValues,name), "possibleValues");
                }
                // if attribute present - use it 
                _possibleValues.Add(attributes[0].Description);
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
