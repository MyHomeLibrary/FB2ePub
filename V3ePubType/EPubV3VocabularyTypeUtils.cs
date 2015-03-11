using System;
using System.Collections.Generic;
using System.ComponentModel;
using EPubLibraryContracts;

namespace EPubLibrary.V3ePubType
{
    internal static class EPubV3VocabularyTypeUtils
    {
        private static readonly Dictionary<EpubV3Vocabulary,string> EPubTypesCache = new Dictionary<EpubV3Vocabulary, string>();

        public static string ConvertTypeToAttributeText(EpubV3Vocabulary type)
        {
            if (!EPubTypesCache.ContainsKey(type))
            {
                var fieldInfo = type.GetType().GetField(type.ToString());
                var attributes =
                    (Attribute[])
                        fieldInfo.GetCustomAttributes(typeof (SerializationNameAttribute), false);
                if (attributes.Length == 0)
                {
                    throw new InvalidEnumArgumentException("type", (int) type, typeof (EpubV3Vocabulary));
                }
                EPubTypesCache.Add(type,((SerializationNameAttribute)attributes[0]).Name);
            }
            return EPubTypesCache[type];
        }
    }
}
