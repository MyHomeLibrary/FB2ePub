using System.Collections.Generic;
using System.Xml.Serialization;

namespace FontSettingsContracts
{
    public interface ICSSFontFamily : IXmlSerializable
    {
        void CopyFrom(ICSSFontFamily cssFontFamily);
        string Name { get; set; }
        List<ICSSFont> Fonts { get; }
    }
}
