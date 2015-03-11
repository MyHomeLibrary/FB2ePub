using System.Collections.Generic;
using System.Text;
using EPubLibraryContracts;
using XHTMLClassLibrary.AttributeDataTypes;

namespace EPubLibrary.V3ePubType
{
    public class EPubV3VocabularyStyles : IEPubV3VocabularyStyles
    {
        private readonly List<EpubV3Vocabulary> _typeStyles = new List<EpubV3Vocabulary>();
        private const string AttributeName = "type";

        public void SetType(EpubV3Vocabulary typeStyle)
        {
            if (!_typeStyles.Contains(typeStyle))
            {
                _typeStyles.Add(typeStyle);
            }
        }

        public void Remove(EpubV3Vocabulary typeStyle)
        {
            if (_typeStyles.Contains(typeStyle))
            {
                _typeStyles.Remove(typeStyle);
            }
        }

        public void Clear()
        {
            _typeStyles.Clear();
        }


        public bool IsOfType(EpubV3Vocabulary typeStyle)
        {
            return _typeStyles.Contains(typeStyle);
        }


        public bool IsPresent()
        {
            return (_typeStyles.Count != 0);
        }


        public CustomAttribute GetAsCustomAttribute()
        {
            var sb = new StringBuilder();
            foreach (var epubV3VocabularyType in _typeStyles)
            {
                if (sb.Length != 0)
                {
                    sb.Append(" ");
                }
                sb.Append(EPubV3VocabularyTypeUtils.ConvertTypeToAttributeText(epubV3VocabularyType));
            }
            return new CustomAttribute(EPubNamespaces.OpsNamespace + AttributeName, sb.ToString());
        }
    }
}
