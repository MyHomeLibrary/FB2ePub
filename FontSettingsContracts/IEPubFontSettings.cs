using System.Collections.Generic;

namespace FontSettingsContracts
{
    public interface IEPubFontSettings
    {
        void CopyFrom(IEPubFontSettings ePubFontSettings);
        List<ICSSFontFamily> FontFamilies { get; }
        List<ICSSStylableElement> CssElements { get; }
    }
}
