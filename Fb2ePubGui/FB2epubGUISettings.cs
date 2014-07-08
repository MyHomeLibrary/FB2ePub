using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fb2ePubGui.Properties;

namespace Fb2ePubGui
{
    public partial class FB2epubGUISettings : Form
    {
        public FB2epubGUISettings()
        {
            InitializeComponent();
            updateSettings.AutoUpdateEnabled = Settings.Default.PerformAutoupdate;
            updateSettings.AutoUpdateFrequency = Settings.Default.AutoUpdateFreqCheck;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Settings.Default.PerformAutoupdate = updateSettings.AutoUpdateEnabled;
            Settings.Default.AutoUpdateFreqCheck = updateSettings.AutoUpdateFrequency;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
