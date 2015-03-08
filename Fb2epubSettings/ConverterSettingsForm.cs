using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ConverterContracts.Settings;
using FolderSettingsHelper.IniLocations;
using FontsSettings;
using FontSettingsContracts;
using log4net;
using TranslitRuContracts;

namespace Fb2epubSettings
{
    public partial class ConverterSettingsForm : Form
    {
        private static class Logger
        {
            // Create a logger for use in this class
            public static readonly ILog Log = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        }

        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        private readonly IniLocations _locations = new IniLocations();
        private readonly CSSFontSettingsCollection _fontSettings = new CSSFontSettingsCollection();
        private readonly BindingSource _myDataSourceCSS = new BindingSource();
        private readonly BindingSource _myDataSourceCSSFonts = new BindingSource();
        private readonly List<CSSElementListItem> _viewCSSElements = new List<CSSElementListItem>();
        private readonly IConverterSettings _settings = new ConverterSettings();



        private bool _locationsLoaded;


        /// <summary>
        /// File name and path of the settings file to load
        /// </summary>
        public string SettingsFileName { get; set; }

        public ConverterSettingsForm()
        {
            InitializeComponent();
            fontsEditControl.CSSFontSettings = _fontSettings;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonSave_Click;
                Invoke(d, sender, e);
                return;
            }
            SavePathsList();
            SaveXPGT();
            _fontSettings.StoreTo(_settings.ConversionSettings.Fonts);
            appleV2SettingsControl.SaveToSettings(_settings);
            var settingsFile = new ConverterSettingsFile();
            settingsFile.Settings.CopyFrom(_settings);
            try
            {
                settingsFile.Save(SettingsFileName);
            }
            catch(Exception)
            {
                DialogResult = DialogResult.Abort;
            }
            DialogResult = DialogResult.OK;
            Close();

        }

        private void SaveXPGT()
        {
            _settings.V2Settings.EnableAdobeTemplate = checkBoxUseXPGT.Checked;
            _settings.V2Settings.AdobeTemplatePath = textBoxTemplatePath.Text;
        }

        private void SavePathsList()
        {
            _locations.Save();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonReset_Click;
                Invoke(d, sender, e);
                return;
            }
            ConverterSettingsForm_Load(this,null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonCancel_Click;
                Invoke(d, sender, e);
                return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConverterSettingsForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SettingsFileName)) // if no file name set load defaults
            {
                string filePath;
                var settings = new ConverterSettings();
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath,settings);
                SettingsFileName = filePath;
            }
            else if (!File.Exists(SettingsFileName)) // if file not exist load default settings
            {
                string filePath;
                var settings = new ConverterSettings();
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath, settings);
                SettingsFileName = filePath;
            }

            try
            {
                ConverterSettingsFile settingsFile = new ConverterSettingsFile();
                settingsFile.Load(SettingsFileName);
                _settings.CopyFrom(settingsFile.Settings);
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex);
                // ignored
            }
            checkBoxTransliterateTOC.Checked = _settings.CommonSettings.TransliterateToc;
            checkBoxTransliterateFileName.Checked = _settings.ConversionSettings.TransliterateFileName;
            checkBoxTransliterateAdditional.Checked = _settings.ConversionSettings.TransliterationSettings.Transliterate;
            textBoxAuthorFormat.Text = _settings.ConversionSettings.AuthorFormat;
            textBoxFileAsFormat.Text = _settings.ConversionSettings.FileAsFormat;
            textBoxNoSequenceFormat.Text = _settings.ConversionSettings.NoSequenceFormat;
            textBoxSequenceFormat.Text = _settings.ConversionSettings.SequenceFormat;
            textBoxNoSeriesFormat.Text = _settings.ConversionSettings.NoSeriesFormat;
            checkBoxAddSequences.Checked = _settings.ConversionSettings.AddSeqToTitle;
            checkBoxFb2Info.Checked = _settings.ConversionSettings.Fb2Info;
            checkBoxConvertAlphaPNG.Checked = _settings.FB2ImportSettings.ConvertAlphaPng;
            checkBoxFlatStructure.Checked = _settings.CommonSettings.FlatStructure;
            checkBoxEmbedStyles.Checked = _settings.CommonSettings.EmbedStyles;
            checkBoxCapitalize.Checked = _settings.CommonSettings.CapitalDrop;
            checkBoxCalibreMetadata.Checked = _settings.V2Settings.AddCalibreMetadata;
            checkBoxSkipAboutPage.Checked = _settings.ConversionSettings.SkipAboutPage;
            checkBoxUseXPGT.Checked = _settings.V2Settings.EnableAdobeTemplate;
            textBoxTemplatePath.Text = _settings.V2Settings.AdobeTemplatePath;
            LoadFixMode();
            LoadIgnoreTitleMode();
            UpdateSequencesGroup();
            _locationsLoaded = _locations.Init();
            if (!_locationsLoaded)
            {
                const string error = "No FB2EPUBExt.INI file found in any of the paths";
                toolTipControl.SetToolTip(tabPagePaths, error);
                toolTipControl.SetToolTip(listBoxPaths, error);
                Logger.Log.Error(error);
            }
            LoadPathsGroup();
            UpdateXPGTGroupGUI();
            LoadFontsList();
            SetupCSSElements();
            UpdateCCSElements();
            LoadAppleSettingsTab();
            appleV2SettingsControl.LoadSettings(_settings);
        }

        private void LoadAppleSettingsTab()
        {
            // remove Apple controls first
            // (for future use/ conditional add)
            tabControlSettings.Controls.Remove(tabPageAppleV2);

            
            // add needed controls conditionally
            tabControlSettings.Controls.Add(tabPageAppleV2);

        }


        private void LoadIgnoreTitleMode()
        {
            comboBoxIgnoreTitle.SelectedIndex = (int)_settings.ConversionSettings.IgnoreTitle;
        }

        private void SetupCSSElements()
        {
            _viewCSSElements.Clear();
            foreach (var cssElementName in _fontSettings.CssElements.Keys)
            {
                foreach (var cssElementCode in _fontSettings.CssElements[cssElementName].Keys)
                {
                    CSSElementListItem item = new CSSElementListItem {Name = cssElementName, Class = cssElementCode};
                    foreach (var font in _fontSettings.CssElements[cssElementName][cssElementCode])
                    {
                        item.Fonts.Add(font);
                    }
                    _viewCSSElements.Add(item);
                }
            }
            _myDataSourceCSS.DataSource = _viewCSSElements;
            listBoxCSSElements.DataSource = _myDataSourceCSS;
            listBoxCSSElements.DisplayMember = "FullName";

            _myDataSourceCSSFonts.DataSource = _myDataSourceCSS;
            _myDataSourceCSSFonts.DataMember = "Fonts";

            listBoxCSSFonts.DataSource = _myDataSourceCSSFonts;
            listBoxCSSFonts.DisplayMember = "Name";
        }

        private void UpdateCCSElements()
        {
            
        }

        
        private void LoadFontsList()
        {
            _fontSettings.Load(_settings.ConversionSettings.Fonts, string.Empty); 
            fontsEditControl.RefreshData();
        }

        private void UpdateXPGTGroupGUI()
        {
            groupBoxXPGTPath.Enabled = checkBoxUseXPGT.Checked;
            buttonXPGTClear.Enabled = (!string.IsNullOrEmpty(textBoxTemplatePath.Text) && checkBoxUseXPGT.Checked);
        }

        private void LoadPathsGroup()
        {
            listBoxPaths.Items.Clear();
            _locations.Load();
            foreach (var iniLocation in _locations)
            {
                listBoxPaths.Items.Add(iniLocation);
            }
            if (listBoxPaths.Items.Count > 0)
            {
                listBoxPaths.SelectedIndex = 0;                
            }
            radioButtonSDEnabled.Enabled =  _locationsLoaded;
            radioButtonSDDisabled.Enabled = _locationsLoaded;
            radioButtonSDDisabled.Checked = (_locations.SingleDestination == -1);
            radioButtonSDEnabled.Checked = (_locations.SingleDestination != -1) ;
            buttonPathNew.Enabled = _locationsLoaded;
            buttonEdit.Enabled = _locationsLoaded;
            UpdatePathsTabGui();
            UpdateSingleDestinationCombo();
        }

        private void UpdateSingleDestinationCombo()
        {
            comboBoxSDValue.Items.Clear();
            comboBoxSDValue.Enabled = (_locations.SingleDestination != -1) && _locations.HasShellLocations() && _locationsLoaded;
            for (int value = 0; value < _locations.Count; value++ )
            {
                if (_locations[value].ShowInShell)
                {
                    comboBoxSDValue.Items.Add(value+1);
                }
            }
            if (_locations.SingleDestination != -1)
            {
                comboBoxSDValue.SelectedIndex   = _locations.SingleDestination-1;
            }
        }

        private void UpdateSequencesGroup()
        {
            labelSeqFormat.Enabled = checkBoxAddSequences.Checked;
            labelNoSeqFormat.Enabled = checkBoxAddSequences.Checked;
            labelNoSeries.Enabled = checkBoxAddSequences.Checked;
            textBoxSequenceFormat.Enabled = checkBoxAddSequences.Checked;
            textBoxNoSequenceFormat.Enabled = checkBoxAddSequences.Checked;
            textBoxNoSeriesFormat.Enabled = checkBoxAddSequences.Checked;
        }

        private void LoadFixMode()
        {
            comboBoxFixMode.Items.Clear();
            comboBoxFixMode.Items.Add("None");
            comboBoxFixMode.Items.Add("Internal");
            comboBoxFixMode.Items.Add("Fb2Fix");
            comboBoxFixMode.Items.Add("FixAlways");
            comboBoxFixMode.SelectedIndex = (int)_settings.FB2ImportSettings.FixMode;
        }

        private void checkBoxTransliterateTOC_CheckedChanged(object sender, EventArgs e)
        {
            _settings.CommonSettings.TransliterateToc = checkBoxTransliterateTOC.Checked;
        }

        private void checkBoxTransliterateFileName_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.TransliterateFileName = checkBoxTransliterateFileName.Checked;
        }

        private void checkBoxTransliterateAdditional_CheckedChanged(object sender, EventArgs e)
        {
            ((TransliterationSettingsImp)_settings.ConversionSettings.TransliterationSettings).Mode = (checkBoxTransliterateAdditional.Checked)? TranslitModeEnum.TranslitRu : TranslitModeEnum.None;
        }

        private void textBoxSequenceFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.SequenceFormat = textBoxSequenceFormat.Text;
        }

        private void textBoxNoSequenceFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.NoSequenceFormat = textBoxNoSequenceFormat.Text;
        }

        private void textBoxAuthorFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.AuthorFormat = textBoxAuthorFormat.Text;
        }

        private void textBoxFileAsFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.FileAsFormat = textBoxFileAsFormat.Text;
        }

        private void checkBoxAddSequences_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.AddSeqToTitle = checkBoxAddSequences.Checked;
            UpdateSequencesGroup();
        }

        private void checkBoxFb2Info_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.Fb2Info = checkBoxFb2Info.Checked;
        }

        private void checkBoxConvertAlphaPNG_CheckedChanged(object sender, EventArgs e)
        {
            _settings.FB2ImportSettings.ConvertAlphaPng = checkBoxConvertAlphaPNG.Checked;
        }

        private void checkBoxFlatStructure_CheckedChanged(object sender, EventArgs e)
        {
            _settings.CommonSettings.FlatStructure = checkBoxFlatStructure.Checked;
        }

        private void checkBoxEmbedStyles_CheckedChanged(object sender, EventArgs e)
        {
            _settings.CommonSettings.EmbedStyles = checkBoxEmbedStyles.Checked;
        }

        private void checkBoxCapitalize_CheckedChanged(object sender, EventArgs e)
        {
            _settings.CommonSettings.CapitalDrop = checkBoxCapitalize.Checked;
        }

        private void checkBoxSkipAboutPage_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.SkipAboutPage = checkBoxSkipAboutPage.Checked;
        }

        private void comboBoxFixMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.FB2ImportSettings.FixMode = (FixOptions)comboBoxFixMode.SelectedIndex;
        }

        private void textBoxNoSeriesFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.NoSeriesFormat = textBoxNoSeriesFormat.Text;
        }

        private void buttonDeletePath_Click(object sender, EventArgs e)
        {
            var currentLocation = (Location)listBoxPaths.SelectedItem;
            if (currentLocation != null)
            {
                _locations.Remove(currentLocation);
                listBoxPaths.Items.Remove(currentLocation);
                if (listBoxPaths.Items.Count > 0)
                {
                    listBoxPaths.SelectedIndex = 0;
                }
            }
            UpdatePathsTabGui();
            UpdateSingleDestinationCombo();
        }

        private void buttonUpPath_Click(object sender, EventArgs e)
        {
            Location currentLocation = (Location)listBoxPaths.SelectedItem;
            int currentIndex = listBoxPaths.Items.IndexOf(currentLocation);
            if (currentIndex != -1)
            {
                listBoxPaths.Items.Remove(currentLocation);
                listBoxPaths.Items.Insert(currentIndex-1,currentLocation);
                if( currentIndex == _locations.SingleDestination )
                {
                    _locations.SingleDestination--;
                }
            }
            currentIndex = _locations.IndexOf(currentLocation);
            if (currentIndex != -1)
            {
                _locations.Remove(currentLocation);
                _locations.Insert(currentIndex - 1, currentLocation);
            }
            UpdateSingleDestinationCombo();
        }

        private void buttonDownPath_Click(object sender, EventArgs e)
        {
            Location currentLocation = (Location)listBoxPaths.SelectedItem;
            int currentIndex = listBoxPaths.Items.IndexOf(currentLocation);
            if (currentIndex != -1)
            {
                listBoxPaths.Items.Remove(currentLocation);
                listBoxPaths.Items.Insert(currentIndex + 1, currentLocation);
                if (currentIndex == _locations.SingleDestination)
                {
                    _locations.SingleDestination++;
                }
            }
            currentIndex = _locations.IndexOf(currentLocation);
            if (currentIndex != -1)
            {
                _locations.Remove(currentLocation);
                _locations.Insert(currentIndex + 1, currentLocation);
            }
            UpdateSingleDestinationCombo();
        }

        private void buttonPathNew_Click(object sender, EventArgs e)
        {
            AddPathForm addPathForm = new AddPathForm();
            DialogResult result = addPathForm.ShowDialog(this);
            if ( result == DialogResult.OK)
            {
                Location newLocation = new Location {Name = addPathForm.PathName, Path = addPathForm.PathFolder};
                _locations.Add(newLocation);
                listBoxPaths.SelectedIndex =  listBoxPaths.Items.Add(newLocation);                
            }
            UpdateSingleDestinationCombo();
        }

    
        private void listBoxPaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePathsTabGui();
        }

        private void UpdatePathsTabGui()
        {
            buttonUpPath.Enabled = (listBoxPaths.SelectedIndex > 0) && _locationsLoaded;
            buttonDownPath.Enabled = (listBoxPaths.SelectedIndex < listBoxPaths.Items.Count - 1) && _locationsLoaded;
            buttonDeletePath.Enabled = (listBoxPaths.Items.Count != 0) && _locationsLoaded;

            Location currentLocation = (Location) listBoxPaths.SelectedItem;
            checkBoxVisibleInExt.Enabled = (currentLocation != null) && _locationsLoaded;
            checkBoxVisibleInGUI.Enabled = (currentLocation != null) && _locationsLoaded;
            if (currentLocation != null)
            {
                checkBoxVisibleInExt.Checked = currentLocation.ShowInShell;
                checkBoxVisibleInGUI.Checked = currentLocation.ShowInGui;
            }

        }

        private void listBoxPaths_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var sb = new StringBuilder();
            sb.AppendFormat("{0})  {1} [{2}]", e.Index+1, ((Location) listBoxPaths.Items[e.Index]).Name,
                            ((Location) listBoxPaths.Items[e.Index]).Path);
            Brush textBrush = new SolidBrush(e.ForeColor);
            if (e.Index+1 == _locations.SingleDestination)
            {
                sb.Append("*");
            }
            else if (_locations.SingleDestination != -1)
            {
                textBrush = new SolidBrush(SystemColors.ControlLight);
            }
            e.Graphics.DrawString(sb.ToString(), e.Font,textBrush , e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Location currentLocation =  (Location) listBoxPaths.SelectedItem;
            if (currentLocation != null)
            {

                var editPathForm = new AddPathForm {PathName = currentLocation.Name, PathFolder = currentLocation.Path};
                DialogResult result = editPathForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    currentLocation.Name = editPathForm.PathName;
                    currentLocation.Path = editPathForm.PathFolder;
                }
            }
        }

        private void checkBoxVisibleInGUI_Click(object sender, EventArgs e)
        {
            Location currentLocation = (Location)listBoxPaths.SelectedItem;
            if (currentLocation != null)
            {
                currentLocation.ShowInGui = checkBoxVisibleInGUI.Checked;
            }
        }

        private void radioButtonSDEnabled_Click(object sender, EventArgs e)
        {
            if (!_locations.HasShellLocations() && radioButtonSDEnabled.Checked)
            {
                MessageBox.Show(this, Resources.Fb2epubSettings.ConverterSettingsForm_radioButtonSDEnabled_CheckedChanged_Can_t_enable_Single_Destination_mode___no_path_defined_as_shell_path, Resources.Fb2epubSettings.ConverterSettingsForm_radioButtonSDEnabled_CheckedChanged_Setting_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                radioButtonSDDisabled.Checked = true;
            }
            if (_locations.Count > 0 && _locations[listBoxPaths.SelectedIndex].ShowInShell)
            {
                _locations.SingleDestination = listBoxPaths.SelectedIndex +1;
            }
            UpdateSingleDestinationCombo();
            listBoxPaths.Refresh();
        }

        private void radioButtonSDDisabled_Click(object sender, EventArgs e)
        {
            _locations.SingleDestination = -1;
            UpdateSingleDestinationCombo();
            listBoxPaths.Refresh();
        }


        private void comboBoxSDValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            _locations.SingleDestination = comboBoxSDValue.SelectedIndex+1;
            listBoxPaths.Refresh();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonClear_Click;
                Invoke(d, sender, e);
                return;
            }
            textBoxTemplatePath.Text = string.Empty;
            UpdateXPGTGroupGUI();
        }

        private void buttonBrowseForXPGT_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonBrowseForXPGT_Click;
                Invoke(d, sender, e);
                return;
            }
            OpenFileDialog selectTemplateDlg = new OpenFileDialog
            {
                Title =
                    Resources.Fb2epubSettings
                        .ConverterSettingsForm_buttonBrowseForXPGT_Click_Please_select_Adobe_XPGT_template_file,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "*.xpgt",
                FilterIndex = 1,
                Multiselect = false,
                Filter = @"XPGT file | *.xpgt|Any file | *.*",
                RestoreDirectory = true,
                ShowReadOnly = false,
                SupportMultiDottedExtensions = true,
                ValidateNames = true
            };
            DialogResult result = selectTemplateDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxTemplatePath.Text = selectTemplateDlg.FileName;
            }
            UpdateXPGTGroupGUI();
        }

        private void checkBoxUseXPGT_CheckedChanged(object sender, EventArgs e)
        {
            _settings.V2Settings.EnableAdobeTemplate = checkBoxUseXPGT.Checked;
            UpdateXPGTGroupGUI();
        }


        private void tabPageFonts_Click(object sender, EventArgs e)
        {

        }


        private void buttonAddCSS_Click(object sender, EventArgs e)
        {
            AddCSSForm newForm = new AddCSSForm();
            DialogResult result = newForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (newForm.ElementClass == null)
                {
                    newForm.ElementClass = string.Empty;
                }
                if (newForm.ElementName == null)
                {
                    newForm.ElementName = string.Empty;
                }
                AddCSSElement(newForm.ElementName.ToLower(), newForm.ElementClass.ToLower());
            }
        }

        private void AddCSSElement(string name, string className)
        {
            CSSElementListItem newViewElement;
            if (!_fontSettings.CssElements.ContainsKey(name))
            {
                _fontSettings.CssElements.Add(name, new Dictionary<string, List<ICSSFontFamily>>());
                _fontSettings.CssElements[name].Add(className, new List<ICSSFontFamily>());
                newViewElement = new CSSElementListItem {Class = className, Name = name};
                _viewCSSElements.Add(newViewElement);
            }
            else
            {
                if (!_fontSettings.CssElements[name].ContainsKey(className))
                {
                    _fontSettings.CssElements[name].Add(className, new List<ICSSFontFamily>());
                    newViewElement = new CSSElementListItem { Class = className, Name = name };
                    _viewCSSElements.Add(newViewElement);
                }
                else
                {
                    newViewElement = _viewCSSElements.Find(x=>((x.Name== name)&&(x.Class == className)));
                }
            }
            _myDataSourceCSS.ResetBindings(false);
            if (newViewElement != null && _myDataSourceCSS.Count != 0 )
            {
                _myDataSourceCSS.MoveNext();
                while (_myDataSourceCSS.Current != newViewElement)
                {
                    _myDataSourceCSS.MoveNext();
                }
            }
        }

        private void buttonRemoveCSS_Click(object sender, EventArgs e)
        {
            CSSElementListItem current = _myDataSourceCSS.Current as CSSElementListItem;
            if (current != null)
            {
                _myDataSourceCSS.RemoveCurrent();
                if (_fontSettings.CssElements.ContainsKey(current.Name))
                {
                    if (_fontSettings.CssElements[current.Name].ContainsKey(current.Class))
                    {
                        _fontSettings.CssElements[current.Name].Remove(current.Class);
                    }
                    if (_fontSettings.CssElements[current.Name].Count == 0)
                    {
                        _fontSettings.CssElements.Remove(current.Name);
                    }
                }
            }
        }

        private void buttonAddCSSFont_Click(object sender, EventArgs e)
        {
            AddFontsForm addForm = new AddFontsForm(_fontSettings.Fonts);
            DialogResult result = addForm.ShowDialog(this);
            if (result == DialogResult.OK && addForm.SelectedFont != null)
            {
                CSSElementListItem currentElement = _myDataSourceCSS.Current as CSSElementListItem;
                if (currentElement != null)
                {
                    ICSSFontFamily font = _fontSettings.Fonts[addForm.SelectedFont];
                    if (font != null)
                    {
                        currentElement.Fonts.Add(font);
                        _fontSettings.CssElements[currentElement.Name][currentElement.Class].Add(font);
                        _myDataSourceCSSFonts.ResetBindings(false);
                    }
                }
               
            }
        }

        private void buttonDeleteCSSFont_Click(object sender, EventArgs e)
        {
            var currentFont = _myDataSourceCSSFonts.Current as CSSFontFamily;
            if (currentFont != null)
            {
                var currentElement = _myDataSourceCSS.Current as CSSElementListItem;
                if (currentElement != null)
                {
                    _fontSettings.CssElements[currentElement.Name][currentElement.Class].Remove(currentFont);
                    currentElement.Fonts.Remove(currentFont);
                    _myDataSourceCSSFonts.ResetBindings(false);
                }
            }
        }

        private void comboBoxIgnoreTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.ConversionSettings.IgnoreTitle = (IgnoreInfoSourceOptions)comboBoxIgnoreTitle.SelectedIndex;
        }

        private void buttonImportExport_Click(object sender, EventArgs e)
        {
            var form = new ImportExportForm(_settings);
            DialogResult result =  form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConverterSettingsForm_Load(this, null);
            }
        }

        private void checkBoxCalibreMetadata_CheckedChanged(object sender, EventArgs e)
        {
            _settings.V2Settings.AddCalibreMetadata = checkBoxCalibreMetadata.Checked;
        }

        private void checkBoxVisibleInExt_Click(object sender, EventArgs e)
        {
            Location currentLocation = (Location)listBoxPaths.SelectedItem;
            if (currentLocation != null)
            {
                currentLocation.ShowInShell = checkBoxVisibleInExt.Checked;
                UpdateSingleDestinationCombo();
            }
        }

     
    }
}
