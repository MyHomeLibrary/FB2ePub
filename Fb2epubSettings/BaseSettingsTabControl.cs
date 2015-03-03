using System.Windows.Forms;
using ConverterContracts.Settings;

namespace Fb2epubSettings
{
    public class BaseSettingsTabControl : UserControl
    {
        public virtual void LoadSettings(IConverterSettings settings) { }
        public virtual void SaveToSettings(IConverterSettings settings) { }
    }
}
