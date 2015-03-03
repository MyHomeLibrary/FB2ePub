using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ConverterContracts.Settings;
using EPubLibraryContracts.Settings;
using Fb2epubSettings.AppleSettings.ePub_v2;

namespace Fb2epubSettings
{
    public partial class AppleV2Settings : BaseSettingsTabControl
    {
        /// <summary>
        /// used to keep track on selected listbox
        /// </summary>
        private enum SelectedBox
        {
            Undefined = 0,
            Available = 1,
            Used = 2,
        };

        /// <summary>
        /// Used to localize orientation enum
        /// </summary>
        private class OrientationTypeConverter : TypeConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    AppleOrientationLock orientationLock = (AppleOrientationLock)value;
                    switch (orientationLock)
                    {
                        case AppleOrientationLock.None:
                            return Resources.Fb2epubSettings.Applev2BookOrientationLockOff;
                        case AppleOrientationLock.LandscapeOnly:
                            return Resources.Fb2epubSettings.Applev2BookOrientationLockLandscapeOnly;
                        case AppleOrientationLock.PortraitOnly:
                            return Resources.Fb2epubSettings.Applev2BookOrientationLockPortraitOnly;
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                var s = value as string;
                if ( s != null )
                {
                        if ( Resources.Fb2epubSettings.Applev2BookOrientationLockOff == s)
                            return AppleOrientationLock.None;
                        if ( Resources.Fb2epubSettings.Applev2BookOrientationLockLandscapeOnly== s)
                            return AppleOrientationLock.LandscapeOnly;
                        if ( Resources.Fb2epubSettings.Applev2BookOrientationLockPortraitOnly== s)
                            return AppleOrientationLock.PortraitOnly;

                }
                return base.ConvertFrom(context, culture, value);
            }
        };

        private readonly Dictionary<AppleTargetPlatform, AppleEPub2PlatformSettings> _allPlatformObjects = new Dictionary<AppleTargetPlatform, AppleEPub2PlatformSettings>();
        private readonly Dictionary<AppleTargetPlatform, AppleEPub2PlatformSettings> _available = new Dictionary<AppleTargetPlatform, AppleEPub2PlatformSettings>();
        private readonly Dictionary<AppleTargetPlatform, AppleEPub2PlatformSettings> _used = new Dictionary<AppleTargetPlatform, AppleEPub2PlatformSettings>();

        private SelectedBox _selectedListBox = SelectedBox.Undefined;

        public AppleV2Settings()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            comboBoxOrientationLock.SuspendLayout();
            comboBoxOrientationLock.Items.Clear();
            OrientationTypeConverter converter = new OrientationTypeConverter();
            for (var orientation = AppleOrientationLock.None; orientation <= AppleOrientationLock.LastValue; orientation++ )
            {
                comboBoxOrientationLock.Items.Add(converter.ConvertToString(orientation));
            }

            comboBoxOrientationLock.ResumeLayout();
        }

        /// <summary>
        /// Reset list of platforms to default values
        /// </summary>
        private void ResetPlatforms()
        {
            _allPlatformObjects.Clear();
            _available.Clear();
            _used.Clear();
            _allPlatformObjects.Add(AppleTargetPlatform.All, new AppleEPub2PlatformSettings() { Name = AppleTargetPlatform.All });
            _allPlatformObjects.Add(AppleTargetPlatform.iPad, new AppleEPub2PlatformSettings() { Name = AppleTargetPlatform.iPad });
            _allPlatformObjects.Add(AppleTargetPlatform.iPhone, new AppleEPub2PlatformSettings() { Name = AppleTargetPlatform.iPhone });
        }

        /// <summary>
        /// Used to load settings object into a dialog/control
        /// </summary>
        /// <param name="settings"></param>
        public override void LoadSettings(IConverterSettings settings)
        {
            SuspendLayout();

            LoadUsedPlatformsFromSettings(settings.V2Settings.AppleConverterEPubSettings.Platforms);

            ResumeLayout();            
        }

        /// <summary>
        /// Load Apple ePub V2 settings to the control
        /// </summary>
        /// <param name="platforms"></param>
        private void LoadUsedPlatformsFromSettings(List<IAppleEPub2PlatformSettings> platforms)
        {
            ResetPlatforms();
            // first move al platforms in settings to used, coping values
            foreach (var platform in platforms)
            {
                _allPlatformObjects[platform.Name].CopyFrom(platform);
                _used.Add(platform.Name, _allPlatformObjects[platform.Name]);
            }
            // then all platforms that not used copy to available
            foreach (var platformType in _allPlatformObjects.Keys)
            {
                if (!_used.ContainsKey(platformType))
                {
                    _available.Add(platformType, _allPlatformObjects[platformType]);
                }
            }
            UpdateListBoxes();
            UpdateButtons();
        }

        /// <summary>
        /// Updates GUI buttons status based on internal state
        /// </summary>
        private void UpdateButtons()
        {
            buttonMoveLeft.Enabled = _selectedListBox != SelectedBox.Available;
            buttonMoveRight.Enabled = _selectedListBox != SelectedBox.Used;
        }

        /// <summary>
        /// Updates list boxes GUI based on internal state
        /// </summary>
        private void UpdateListBoxes()
        {
            listBoxAvailablePlatforms.Items.Clear();
            listBoxUsedPlatforms.Items.Clear();
            foreach (var platform in _available)
            {
                listBoxAvailablePlatforms.Items.Add(platform.Key);
            }
            foreach (var platform in _used)
            {
                listBoxUsedPlatforms.Items.Add(platform.Key);
            }
            if (_available.Count > 0)
            {
                listBoxAvailablePlatforms.Focus();
                listBoxAvailablePlatforms.Select();
                listBoxAvailablePlatforms.SelectedIndex = 0;
                listBoxUsedPlatforms.SelectedIndex = -1;
                _selectedListBox = SelectedBox.Available;
            }
            else if (_used.Count > 0)
            {
                listBoxUsedPlatforms.Focus();
                listBoxUsedPlatforms.Select();
                listBoxUsedPlatforms.SelectedIndex = 0;
                listBoxAvailablePlatforms.SelectedIndex = -1;
                _selectedListBox = SelectedBox.Used;
            }
            else
            {
                listBoxUsedPlatforms.SelectedIndex = -1;
                listBoxAvailablePlatforms.SelectedIndex = -1;
                _selectedListBox = SelectedBox.Undefined;
            }
            UpdateSelectedPlatformValues();
        }

        /// <summary>
        /// Updated settings values for currently selected platform based on internal state
        /// </summary>
        private void UpdateSelectedPlatformValues()
        {
            AppleEPub2PlatformSettings selectedPlatform = null;
            if (_selectedListBox == SelectedBox.Available)
            {
                if (_available.Count > 0)
                {
                    AppleTargetPlatform selectedKey = (AppleTargetPlatform)listBoxAvailablePlatforms.SelectedItem;
                    if (selectedKey != AppleTargetPlatform.NotSet)
                    {
                        if (_available.ContainsKey(selectedKey))
                        {
                            selectedPlatform = _allPlatformObjects[selectedKey];
                        }
                        else
                        {
                            throw new Exception("the platform selected in Available box is not in available list");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid platform type selected in the box");
                    }
                }
            }
            else if (_selectedListBox == SelectedBox.Used)
            {
                if (_used.Count > 0)
                {
                    AppleTargetPlatform selectedKey = (AppleTargetPlatform)listBoxUsedPlatforms.SelectedItem;
                    if (selectedKey != AppleTargetPlatform.NotSet)
                    {
                        if (_used.ContainsKey(selectedKey))
                        {
                            selectedPlatform = _allPlatformObjects[selectedKey];
                        }
                        else
                        {
                            throw new Exception("the platform selected in Used box is not in available list");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid platform type selected in the box");
                    }
                }
            }
            DisplayPlatformSettings(selectedPlatform);
        }

        /// <summary>
        /// Update display of currently selected platform settings based on platform object passed
        /// </summary>
        /// <param name="selectedPlatform"></param>
        private void DisplayPlatformSettings(AppleEPub2PlatformSettings selectedPlatform)
        {
            bool displayEnabled = _selectedListBox == SelectedBox.Used && selectedPlatform != null;
            checkBoxFixedLayout.Enabled = displayEnabled;
            checkBoxSpreadToOpen.Enabled = displayEnabled;
            checkBoxUseCustomFonts.Enabled = displayEnabled;
            comboBoxOrientationLock.Enabled = displayEnabled;
            if (displayEnabled)
            {
                checkBoxFixedLayout.Checked = selectedPlatform.FixedLayout;
                checkBoxSpreadToOpen.Checked = selectedPlatform.OpenToSpread;
                checkBoxUseCustomFonts.Checked = selectedPlatform.UseCustomFonts;
                comboBoxOrientationLock.SelectedIndex = (int)selectedPlatform.OrientationLock;
            }
            else
            {
                checkBoxFixedLayout.Checked = false;
                checkBoxSpreadToOpen.Checked= false;
                checkBoxUseCustomFonts.Checked = false;
                comboBoxOrientationLock.SelectedIndex = -1;
            }
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            AppleTargetPlatform platformToMove = (AppleTargetPlatform)listBoxAvailablePlatforms.SelectedItem;
            _available.Remove(platformToMove);
            _used.Add(platformToMove, _allPlatformObjects[platformToMove]);
            listBoxUsedPlatforms.Focus();
            UpdateListBoxes();
            SelectPlatform(platformToMove);
            ResumeLayout();
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            AppleTargetPlatform platformToMove = (AppleTargetPlatform)listBoxUsedPlatforms.SelectedItem;
            _used.Remove(platformToMove);
            _available.Add(platformToMove, _allPlatformObjects[platformToMove]);
            listBoxAvailablePlatforms.Focus();
            UpdateListBoxes();
            SelectPlatform(platformToMove);
            ResumeLayout();
        }

        private void listBoxAvailablePlatforms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAvailablePlatforms.SelectedIndex != -1)
            {
                _selectedListBox = SelectedBox.Available;
                listBoxUsedPlatforms.SelectedIndex = -1;
                SelectPlatform((AppleTargetPlatform)listBoxAvailablePlatforms.SelectedItem);
                UpdateButtons();
            }
        }

        private void SelectPlatform(AppleTargetPlatform itemKey)
        {
            DisplayPlatformSettings(_allPlatformObjects[itemKey]);
            UpdateButtons();
        }

        private void listBoxUsedPlatforms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUsedPlatforms.SelectedIndex != -1)
            {
                _selectedListBox = SelectedBox.Used;
                listBoxAvailablePlatforms.SelectedIndex = -1;
                SelectPlatform((AppleTargetPlatform)listBoxUsedPlatforms.SelectedItem);
            }
        }


        private void comboBoxOrientationLock_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_selectedListBox == SelectedBox.Used)
            {
                AppleTargetPlatform currentPlatform = GetCurrentPlatform();
                OrientationTypeConverter converter = new OrientationTypeConverter();
                string value = (string)comboBoxOrientationLock.SelectedItem;
                var convertFromString = converter.ConvertFromString(value);
                if (convertFromString != null)
                {
                    var orientationLock = (AppleOrientationLock)convertFromString;
                    _allPlatformObjects[currentPlatform].OrientationLock = orientationLock;
                }
            }
        }

        /// <summary>
        /// Returns currently selected platform
        /// </summary>
        /// <returns></returns>
        private AppleTargetPlatform GetCurrentPlatform()
        {
            if (_selectedListBox == SelectedBox.Available)
            {
                return (AppleTargetPlatform)listBoxAvailablePlatforms.SelectedItem;
            }
            else if (_selectedListBox == SelectedBox.Used)
            {
                return (AppleTargetPlatform)listBoxUsedPlatforms.SelectedItem;
            }
            else
            {
                throw new Exception("Can't determine current platform , the box is not selected");
            }
        }


        private void checkBoxUseCustomFonts_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedListBox == SelectedBox.Used)
            {
                AppleTargetPlatform currentPlatform = GetCurrentPlatform();
                _allPlatformObjects[currentPlatform].UseCustomFonts = checkBoxUseCustomFonts.Checked;
            }
        }

        private void checkBoxSpreadToOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedListBox == SelectedBox.Used)
            {
                AppleTargetPlatform currentPlatform = GetCurrentPlatform();
                _allPlatformObjects[currentPlatform].OpenToSpread = checkBoxSpreadToOpen.Checked;
            }
        }

        private void checkBoxFixedLayout_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedListBox == SelectedBox.Used)
            {
                AppleTargetPlatform currentPlatform = GetCurrentPlatform();
                if (checkBoxFixedLayout.Checked)
                {
                    DialogResult result = MessageBox.Show(Resources.Fb2epubSettings.AppleV2Settings_checkBoxFixedLayout_CheckedChanged_Fixed_layout_is_not_fit_for_automatic_convertors,
                        Resources.Fb2epubSettings.AppleV2Settings_checkBoxFixedLayout_CheckedChanged_Are_you_sure_,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                    {
                        checkBoxFixedLayout.Checked = false;
                        return;
                    }
                }
                _allPlatformObjects[currentPlatform].FixedLayout= checkBoxFixedLayout.Checked;
            }
        }

        /// <summary>
        /// Saves current state to settings
        /// </summary>
        /// <param name="settings"></param>
        public override void SaveToSettings(IConverterSettings settings)
        {
            settings.V2Settings.AppleConverterEPubSettings.Platforms.Clear();
            foreach (var platform in _used.Values)
            {
                var createdSettings = new AppleEPub2PlatformSettings();
                createdSettings.CopyFrom(platform);
                settings.V2Settings.AppleConverterEPubSettings.Platforms.Add(createdSettings);
            }
            base.SaveToSettings(settings);

        }


    }
}
