using System.Collections.Generic;

namespace FontSettingsContracts
{
    public interface ICSSFontFamily
    {
        void CopyFrom(ICSSFontFamily cssFontFamily);
        string Name { get; set; }
        List<ICSSFont> Fonts { get; }
    }
}
