using System.Collections.Generic;

namespace FontSettingsContracts
{
    public interface ICSSStylableElement
    {
        string Name { get; set; }
        string Class { get; set; }
        List<string> AssignedFontFamilies { get; }
        void CopyFrom(ICSSStylableElement element);

    }
}
