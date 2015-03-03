using System.Collections.Generic;
using System.Xml.Serialization;

namespace FontSettingsContracts
{
    public interface ICSSStylableElement : IXmlSerializable
    {
        string Name { get; set; }
        string Class { get; set; }
        List<string> AssignedFontFamilies { get; }
        void CopyFrom(ICSSStylableElement element);

    }
}
