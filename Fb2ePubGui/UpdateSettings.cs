using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fb2ePubGui.Properties;

namespace Fb2ePubGui
{
    public partial class UpdateSettings : UserControl
    {
        public UpdateSettings()
        {
            InitializeComponent();
            comboBoxUpdateFreq.Items.Add(AutoUpdateFreqCheckTimeSlice.EveryRun);
            comboBoxUpdateFreq.Items.Add(AutoUpdateFreqCheckTimeSlice.OnceADay);
            comboBoxUpdateFreq.Items.Add(AutoUpdateFreqCheckTimeSlice.OnceAWeek);
            comboBoxUpdateFreq.Items.Add(AutoUpdateFreqCheckTimeSlice.OnceAMonths);
        }

        /// <summary>
        /// Checks if auto update was enabled
        /// </summary>
        public bool AutoUpdateEnabled
        {
            get { return checkBoxAutomaticUpdate.Checked; }
            set { checkBoxAutomaticUpdate.Checked = value; }
        }

        public AutoUpdateFreqCheckTimeSlice AutoUpdateFrequency
        {
            get { return (AutoUpdateFreqCheckTimeSlice)comboBoxUpdateFreq.SelectedItem; }
            set { comboBoxUpdateFreq.SelectedItem = value; }
        }
    }
}
