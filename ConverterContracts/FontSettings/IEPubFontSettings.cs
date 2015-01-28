using System.Collections.Generic;

namespace ConverterContracts.FontSettings
{
    public interface IEPubFontSettings
    {
        void CopyFrom(IEPubFontSettings ePubFontSettings);
        List<ICSSFontFamily> FontFamilies { get; }
        List<ICSSStylableElement> CssElements { get; }
    }
}
