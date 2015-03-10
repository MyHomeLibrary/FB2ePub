using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace EPubLibrary.Content
{
    class EPubCoreMediaTypesConverter<T> : TypeConverter where T : struct
    {
            private readonly Dictionary<T, string> _enumValueToStringMap = new Dictionary<T, string>();
            private readonly Dictionary<string, T> _stringToEnumValueMap = new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);

            public EPubCoreMediaTypesConverter()
            {
                if (!typeof(T).IsEnum)
                    throw new InvalidOperationException("Type is not Enum");

                if (typeof(T).IsDefined(typeof(FlagsAttribute), false))
                    throw new InvalidOperationException("Flags Attribute not supported");

                // This allows us to also parse the original enum values also.
                foreach (var enumValue in (T[])Enum.GetValues(typeof(T)))
                {
                    _stringToEnumValueMap[enumValue.ToString()] = enumValue;
                }

                foreach (var mapping in GetEnumMappingsFromMediaTypeAttribute())
                {
                    _enumValueToStringMap[mapping.Key] = mapping.Value;
                    _stringToEnumValueMap[mapping.Value] = mapping.Key;
                }
            }

            /// <summary>
            /// Return a collection of key value pairs describing the enum
            /// Key will be the Enum value
            /// Value will be the description from the EPubCoreMediaTypeAttribute, or standard ToString if not present.
            /// </summary>
            /// <returns></returns>
            private static IEnumerable<KeyValuePair<T, string>> GetEnumMappingsFromMediaTypeAttribute()
            {
                return (from value in (T[])Enum.GetValues(typeof(T))
                        let field = typeof(T).GetField(value.ToString())
                        let attribute = (EPubCoreMediaTypeAttribute[])field.GetCustomAttributes(typeof(EPubCoreMediaTypeAttribute), false)
                        let entry = attribute.Length == 1 ? attribute[0].XmlEntryName : value.ToString()
                        select new { value, entry }).ToDictionary(t => t.value, t => t.entry);
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(string);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                var valueAsString = value as string;
                if (valueAsString != null)
                {
                    return GetValue(valueAsString);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == null) { throw new ArgumentNullException("destinationType"); }

                if (value is T && destinationType == typeof(string))
                {
                    return GetMediaTypeAsString((T)value);
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool IsValid(ITypeDescriptorContext context, object value)
            {
                // IsValid is used to check if value is a valid enum value - not if its possible to convert to the type
                if (value == null) { throw new ArgumentNullException("value"); }

                if (value is T)
                {
                    return _enumValueToStringMap.ContainsKey((T)value);
                }

                var key = value as string;
                if (key != null)
                {
                    return _stringToEnumValueMap.ContainsKey(key);
                }

                if (value is int)
                {
                    // This will fall back to reflection, however its none-trivial to roll yourself.
                    return typeof(T).IsEnumDefined(value);
                }

                throw new InvalidOperationException("Unknown enum type.");
            }

            private T GetValue(string value)
            {
                T convert;
                if (_stringToEnumValueMap.TryGetValue(value, out convert))
                    return convert;

                throw new FormatException(String.Format("{0} is not a valid value for {1}.", value, typeof(T).Name));
            }

            private string GetMediaTypeAsString(T value)
            {
                string convert;
                if (_enumValueToStringMap.TryGetValue(value, out convert))
                    return convert;

                throw new ArgumentException(String.Format("The value '{0}' is not a valid value for the enum '{1}'.", value, typeof(T).Name));
            }
        }
}
