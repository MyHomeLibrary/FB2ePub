using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fb2epubSettings
{
    public class BaseSettingsTabControl : UserControl
    {
        public virtual void LoadSettings(ConverterSettings settings) { }
        public virtual void SaveToSettings(ConverterSettings settings) { }
    }
}
