using System.ComponentModel;
using System.Windows.Forms;

namespace Fb2ePubGui.UpdateSettingsControl
{
    public partial class UpdateSettings : UserControl
    {
        public UpdateSettings()
        {
            InitializeComponent();
            comboBoxUpdateFreq.Items.Add(TypeDescriptor.GetConverter(typeof(AutoUpdateFreqCheckTimeSlice)).ConvertToString(AutoUpdateFreqCheckTimeSlice.EveryRun));
            comboBoxUpdateFreq.Items.Add(TypeDescriptor.GetConverter(typeof(AutoUpdateFreqCheckTimeSlice)).ConvertToString(AutoUpdateFreqCheckTimeSlice.OnceADay));
            comboBoxUpdateFreq.Items.Add(TypeDescriptor.GetConverter(typeof(AutoUpdateFreqCheckTimeSlice)).ConvertToString(AutoUpdateFreqCheckTimeSlice.OnceAWeek));
            comboBoxUpdateFreq.Items.Add(TypeDescriptor.GetConverter(typeof(AutoUpdateFreqCheckTimeSlice)).ConvertToString(AutoUpdateFreqCheckTimeSlice.OnceAMonths));
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
            get { return (AutoUpdateFreqCheckTimeSlice)TypeDescriptor.GetConverter(typeof(AutoUpdateFreqCheckTimeSlice)).ConvertFromString((string)comboBoxUpdateFreq.SelectedItem); }
            set { comboBoxUpdateFreq.SelectedItem = TypeDescriptor.GetConverter(typeof(AutoUpdateFreqCheckTimeSlice)).ConvertToString(value); }
        }
    }
}
