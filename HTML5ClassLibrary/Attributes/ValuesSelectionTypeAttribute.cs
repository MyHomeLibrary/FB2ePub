using System;
using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class ValuesSelectionTypeAttribute<T> : SimpleSingleTypeAttribute<T> where T : IAttributeDataType, new()
    {
        private readonly List<string> _possibleValues = new List<string>();
        private readonly Type _dataType;


        /// <summary>
        /// Initialize based on enum values
        /// </summary>
        /// <param name="attrName">attribute id name</param>
        /// <param name="possibleValues">thould be Enum based type</param>
        public ValuesSelectionTypeAttribute(string attrName, Type possibleValues)
            : base(attrName)
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
            _dataType = possibleValues;
        }


        public override object Value
        {
            set
            {
                // check that it string or of proper enum type
                if (!(_dataType.IsInstanceOfType(value)) && !(value is string))
                    throw new ArgumentException(string.Format("The value set can be only of string or {0} type", _dataType.Name));

                var passedValue = value as string;
                if (value is Enum) // if of enum type
                {
                    var enumValue = value as Enum;
                    passedValue = ReadAttributeFromEnumMember(enumValue);
                }
                if (_possibleValues.Count > 0)
                {
                    if (!_possibleValues.Contains(passedValue))
                    {
                        throw new ArgumentException(string.Format("The value {0} is not one of the possible values", passedValue));
                    }
                }
                AttrObject.Value = passedValue;
                AttributeHasValue = (passedValue != string.Empty);
            }
        }

        private string ReadAttributeFromEnumMember(Enum enumValue)
        {
            Type type = enumValue.GetType();
            var names = type.GetFields();
            foreach (var nameInfo in names)
            {
                if (nameInfo.ReflectedType != null && 
                    (nameInfo.ReflectedType.IsEnum && string.Compare(enumValue.ToString(), nameInfo.Name, StringComparison.Ordinal) == 0))
                {
                    var attributes =
                        (DescriptionAttribute[]) nameInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);
                    if (attributes.Length < 1)
                    {
                        throw new ArgumentException(string.Format("The argument type {0} have to have Description attribute set on all it's members, Description on member {1} is missing", type, nameInfo.Name), "enumValue");
                    }
                    return attributes[0].Description;
                }
            }
            throw new Exception(string.Format("Value of {0} not found.",enumValue));
        }

    }
}
