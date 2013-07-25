using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using FolderSettingsHelper.IniLocations;
using FontsSettings;
using System.IO;
using System.Reflection;
using Fb2epubSettings.AppleSettings.ePub_v2;

namespace Fb2epubSettings
{
    public partial class ConverterSettingsForm : Form
    {
        internal static class Logger
        {
            // Create a logger for use in this class
            public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        }

        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        private readonly IniLocations _locations = new IniLocations();
        private readonly CSSFontSettingsCollection _fontSettings = new CSSFontSettingsCollection();
        private readonly BindingSource _myDataSourceCSS = new BindingSource();
        private readonly BindingSource _myDataSourceCSSFonts = new BindingSource();
        private readonly List<CSSElementListItem> _viewCSSElements = new List<CSSElementListItem>();
        private readonly ConverterSettings _settings = new ConverterSettings();

        private bool _locationsLoaded = false;


        /// <summary>
        /// File name and path of the settings file to load
        /// </summary>
        public string SettingsFileName { get; set; }

        public ConverterSettingsForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonSave_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
            savePathsList();
            saveXPGT();
            _fontSettings.StoreTo(_settings.Fonts);
            appleV2SettingsControl.SaveToSettings(_settings);
            ConverterSettingsFile settingsFile = new ConverterSettingsFile();
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

        private void saveXPGT()
        {
            _settings.EnableAdobeTemplate = checkBoxUseXPGT.Checked;
            _settings.AdobeTemplatePath = textBoxTemplatePath.Text;
        }

        private void savePathsList()
        {
            _locations.Save();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonReset_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
            ConverterSettingsFile settingsFile = new ConverterSettingsFile();
            try
            {
                settingsFile.Load(SettingsFileName);
                _settings.CopyFrom(settingsFile.Settings);
            }
            catch(Exception)
            {
            }
            ConverterSettingsForm_Load(this,null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonCancel_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConverterSettingsForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SettingsFileName)) // if no file name set load defaults
            {
                string filePath = string.Empty;
                ConverterSettings settings = new ConverterSettings();
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath,settings);
                SettingsFileName = filePath;
            }
            else if (!File.Exists(SettingsFileName)) // if file not exist load default settings
            {
                string filePath = string.Empty;
                ConverterSettings settings = new ConverterSettings();
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath, settings);
                SettingsFileName = filePath;
            }

            try
            {
                ConverterSettingsFile settingsFile = new ConverterSettingsFile();
                settingsFile.Load(SettingsFileName);
                _settings.CopyFrom(settingsFile.Settings);
            }
            catch (Exception)
            {
            }
            checkBoxTransliterateTOC.Checked = _settings.TransliterateToc;
            checkBoxTransliterateFileName.Checked = _settings.TransliterateFileName;
            checkBoxTransliterateAdditional.Checked = _settings.Transliterate;
            textBoxAuthorFormat.Text = _settings.AuthorFormat;
            textBoxFileAsFormat.Text = _settings.FileAsFormat;
            textBoxNoSequenceFormat.Text = _settings.NoSequenceFormat;
            textBoxSequenceFormat.Text = _settings.SequenceFormat;
            textBoxNoSeriesFormat.Text = _settings.NoSeriesFormat;
            checkBoxAddSequences.Checked = _settings.AddSeqToTitle;
            checkBoxFb2Info.Checked = _settings.Fb2Info;
            checkBoxConvertAlphaPNG.Checked = _settings.ConvertAlphaPng;
            checkBoxFlatStructure.Checked = _settings.Flat;
            checkBoxEmbedStyles.Checked = _settings.EmbedStyles;
            checkBoxCapitalize.Checked = _settings.CapitalDrop;
            checkBoxCalibreMetadata.Checked = _settings.AddCalibreMetadata;
            checkBoxSkipAboutPage.Checked = _settings.SkipAboutPage;
            checkBoxUseXPGT.Checked = _settings.EnableAdobeTemplate;
            textBoxTemplatePath.Text = _settings.AdobeTemplatePath;
            LoadFixMode();
            LoadIgnoreTitleMode();
            UpdateSequencesGroup();
            _locationsLoaded = _locations.Init();
            if (!_locationsLoaded)
            {
                string error = "No FB2EPUBExt.INI file found in any of the paths";
                toolTipControl.SetToolTip(tabPagePaths, error);
                toolTipControl.SetToolTip(listBoxPaths, error);
                Logger.Log.Error(error);
            }
            LoadPathsGroup();
            UpdateXPGTGroupGUI();
            LoadFontsList();
            UpdateFontsList();
            UpdateFontsButtons();
            SetupCSSElements();
            UpdateCCSElements();
            LoadAppleSettingsTab();
            appleV2SettingsControl.LoadSettings(_settings);
        }

        private void LoadAppleSettingsTab()
        {
            // remove Apple controls first
            // (for future use/ conditional add)
            this.tabControlSettings.Controls.Remove(tabPageAppleV2);

            
            // add needed controls conditionally
            this.tabControlSettings.Controls.Add(tabPageAppleV2);

        }


        private void LoadIgnoreTitleMode()
        {
            comboBoxIgnoreTitle.SelectedIndex = (int)_settings.IgnoreTitle;
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

        private void UpdateFontsButtons()
        {
            bool itemSelected = listViewFonts.SelectedItems.Count > 0;
            buttonEditFont.Enabled = itemSelected;
            buttonRemoveFont.Enabled = itemSelected;
        }

        private void UpdateFontsList()
        {
            listViewFonts.Items.Clear();
            foreach (var cssFontFamily in _fontSettings.Fonts.Keys)
            {
                listViewFonts.Items.Add(cssFontFamily);
            }
        }

        private void LoadFontsList()
        {
            _fontSettings.Load(_settings.Fonts,string.Empty); 
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
                    comboBoxSDValue.Items.Add(value);
                }
            }
            if (_locations.SingleDestination != -1)
            {
                comboBoxSDValue.SelectedItem = _locations.SingleDestination;
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
            comboBoxFixMode.SelectedIndex = (int)_settings.FixMode;
        }

        private void checkBoxTransliterateTOC_CheckedChanged(object sender, EventArgs e)
        {
            _settings.TransliterateToc = checkBoxTransliterateTOC.Checked;
        }

        private void checkBoxTransliterateFileName_CheckedChanged(object sender, EventArgs e)
        {
            _settings.TransliterateFileName = checkBoxTransliterateFileName.Checked;
        }

        private void checkBoxTransliterateAdditional_CheckedChanged(object sender, EventArgs e)
        {
            _settings.Transliterate = checkBoxTransliterateAdditional.Checked;
        }

        private void textBoxSequenceFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.SequenceFormat = textBoxSequenceFormat.Text;
        }

        private void textBoxNoSequenceFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.NoSequenceFormat = textBoxNoSequenceFormat.Text;
        }

        private void textBoxAuthorFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.AuthorFormat = textBoxAuthorFormat.Text;
        }

        private void textBoxFileAsFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.FileAsFormat = textBoxFileAsFormat.Text;
        }

        private void checkBoxAddSequences_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AddSeqToTitle = checkBoxAddSequences.Checked;
            UpdateSequencesGroup();
        }

        private void checkBoxFb2Info_CheckedChanged(object sender, EventArgs e)
        {
            _settings.Fb2Info= checkBoxFb2Info.Checked;
        }

        private void checkBoxConvertAlphaPNG_CheckedChanged(object sender, EventArgs e)
        {
            _settings.ConvertAlphaPng = checkBoxConvertAlphaPNG.Checked;
        }

        private void checkBoxFlatStructure_CheckedChanged(object sender, EventArgs e)
        {
            _settings.Flat = checkBoxFlatStructure.Checked;
        }

        private void checkBoxEmbedStyles_CheckedChanged(object sender, EventArgs e)
        {
            _settings.EmbedStyles = checkBoxEmbedStyles.Checked;
        }

        private void checkBoxCapitalize_CheckedChanged(object sender, EventArgs e)
        {
            _settings.CapitalDrop = checkBoxCapitalize.Checked;
        }

        private void checkBoxSkipAboutPage_CheckedChanged(object sender, EventArgs e)
        {
            _settings.SkipAboutPage = checkBoxSkipAboutPage.Checked;
        }

        private void comboBoxFixMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.FixMode = (FixOptions)comboBoxFixMode.SelectedIndex;
        }

        private void textBoxNoSeriesFormat_TextChanged(object sender, EventArgs e)
        {
            _settings.NoSeriesFormat = textBoxNoSeriesFormat.Text;
        }

        private void buttonDeletePath_Click(object sender, EventArgs e)
        {
            Location currentLocation = (Location)listBoxPaths.SelectedItem;
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
                Location newLocation = new Location();
                newLocation.Name = addPathForm.PathName;
                newLocation.Path = addPathForm.PathFolder;
                _locations.Add(newLocation);
                listBoxPaths.SelectedIndex =  listBoxPaths.Items.Add(newLocation);                
            }
            UpdateSingleDestinationCombo();
        }

        private void checkBoxVisibleInExt_CheckedChanged(object sender, EventArgs e)
        {
            Location currentLocation = (Location)listBoxPaths.SelectedItem;
            if (currentLocation != null)
            {
                currentLocation.ShowInShell= checkBoxVisibleInExt.Checked;
                UpdateSingleDestinationCombo();
            }
        }

        private void checkBoxVisibleInGUI_CheckedChanged(object sender, EventArgs e)
        {
            Location currentLocation =  (Location) listBoxPaths.SelectedItem;
            if (currentLocation != null)
            {
                currentLocation.ShowInGui = checkBoxVisibleInGUI.Checked;
            }
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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0})  {1} [{2}]", e.Index, ((Location) listBoxPaths.Items[e.Index]).Name,
                            ((Location) listBoxPaths.Items[e.Index]).Path);
            Brush textBrush = new SolidBrush(e.ForeColor);
            if (e.Index == _locations.SingleDestination)
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

                AddPathForm editPathForm = new AddPathForm();
                editPathForm.PathName = currentLocation.Name;
                editPathForm.PathFolder = currentLocation.Path;
                DialogResult result = editPathForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    currentLocation.Name = editPathForm.PathName;
                    currentLocation.Path = editPathForm.PathFolder;
                }
            }
        }

        private void radioButtonSDDisabled_CheckedChanged(object sender, EventArgs e)
        {
            _locations.SingleDestination = -1;
            UpdateSingleDestinationCombo();
            listBoxPaths.Refresh();
        }

        private void radioButtonSDEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (!_locations.HasShellLocations() && radioButtonSDEnabled.Checked)
            {
                MessageBox.Show(this, Resources.Fb2epubSettings.ConverterSettingsForm_radioButtonSDEnabled_CheckedChanged_Can_t_enable_Single_Destination_mode___no_path_defined_as_shell_path, Resources.Fb2epubSettings.ConverterSettingsForm_radioButtonSDEnabled_CheckedChanged_Setting_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                radioButtonSDDisabled.Checked = true;
            }
            if (_locations.Count > 0 && _locations[listBoxPaths.SelectedIndex].ShowInShell)
            {
                _locations.SingleDestination = listBoxPaths.SelectedIndex;
            }
            UpdateSingleDestinationCombo();
            listBoxPaths.Refresh();
        }

        private void comboBoxSDValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            _locations.SingleDestination = (int) comboBoxSDValue.SelectedItem;
            listBoxPaths.Refresh();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = buttonClear_Click;
                Invoke(d, new object[] { sender, e });
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
                Invoke(d, new object[] { sender, e });
                return;
            }
            OpenFileDialog selectTemplateDlg = new OpenFileDialog();
            selectTemplateDlg.Title = Resources.Fb2epubSettings.ConverterSettingsForm_buttonBrowseForXPGT_Click_Please_select_Adobe_XPGT_template_file;
            selectTemplateDlg.AutoUpgradeEnabled = true;
            selectTemplateDlg.CheckFileExists = true;
            selectTemplateDlg.CheckPathExists = true;
            selectTemplateDlg.DefaultExt = "*.xpgt";
            selectTemplateDlg.FilterIndex = 1;
            selectTemplateDlg.Multiselect = false;
            selectTemplateDlg.Filter = "XPGT file | *.xpgt|Any file | *.*";
            selectTemplateDlg.RestoreDirectory = true;
            selectTemplateDlg.ShowReadOnly = false;
            selectTemplateDlg.SupportMultiDottedExtensions = true;
            selectTemplateDlg.ValidateNames = true;
            DialogResult result = selectTemplateDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxTemplatePath.Text = selectTemplateDlg.FileName;
            }
            UpdateXPGTGroupGUI();
        }

        private void checkBoxUseXPGT_CheckedChanged(object sender, EventArgs e)
        {
            _settings.EnableAdobeTemplate = checkBoxUseXPGT.Checked;
            UpdateXPGTGroupGUI();
        }


        private void tabPageFonts_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddFont_Click(object sender, EventArgs e)
        {
            CSSFontFamily newFamily = new CSSFontFamily();
            _fontSettings.Fonts.Add(newFamily.Name, newFamily);
            listViewFonts.Items.Add(newFamily.Name);
            UpdateFontsList();
            UpdateFontsButtons();
            EditFontFamily(newFamily.Name);
        }

        private void buttonEditFont_Click(object sender, EventArgs e)
        {
            EditFontFamily(listViewFonts.SelectedItems[0].Text);
        }

        private void EditFontFamily(string familyFontName)
        {
            EditFontFamilyForm editForm = new EditFontFamilyForm(_fontSettings,familyFontName);
            editForm.ShowDialog();
            UpdateFontsList();
            UpdateFontsButtons();
        }

        private void buttonRemoveFont_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selected = listViewFonts.SelectedItems;
            List<ListViewItem> used = GetUsed(selected);
            if (used.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(Resources.Fb2epubSettings.ConverterSettingsForm_buttonRemoveFont_FontNameMessage, used[0].Text);
                MessageBox.Show(this, sb.ToString(), Resources.Fb2epubSettings.ConverterSettingsForm_buttonRemoveFont_Click_Font_used, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
            List<ListViewItem> toDelete = new List<ListViewItem>();
            foreach (ListViewItem item in selected)
            {
                if ((item != null) && !used.Contains(item))
                {
                    toDelete.Add(item);
                }
            }
            listViewFonts.BeginUpdate();
            foreach (ListViewItem selectedItem in toDelete)
            {
                _fontSettings.Fonts.Remove(selectedItem.Text);
                listViewFonts.Items.Remove(selectedItem);
            }
            listViewFonts.EndUpdate();
            UpdateFontsList();
            UpdateFontsButtons();
        }

        private List<ListViewItem> GetUsed(ListView.SelectedListViewItemCollection selected)
        {
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (ListViewItem item in selected)
            {
                if (_fontSettings.IsFontUsed(item.Text))
                {
                    items.Add(item);
                }
            }
            return items;
        }


        private void listViewFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFontsButtons();
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
            CSSElementListItem newViewElement = null;
            if(!_fontSettings.CssElements.ContainsKey(name) )
            {
                _fontSettings.CssElements.Add(name,new Dictionary<string, List<CSSFontFamily>>());
                _fontSettings.CssElements[name].Add(className,new List<CSSFontFamily>());
                newViewElement = new CSSElementListItem {Class = className, Name = name};
                _viewCSSElements.Add(newViewElement);
            }
            else
            {
                if (!_fontSettings.CssElements[name].ContainsKey(className))
                {
                    _fontSettings.CssElements[name].Add(className,new List<CSSFontFamily>());
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
                    CSSFontFamily font = _fontSettings.Fonts[addForm.SelectedFont];
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
            CSSFontFamily currentFont = _myDataSourceCSSFonts.Current as CSSFontFamily;
            if (currentFont != null)
            {
                CSSElementListItem currentElement = _myDataSourceCSS.Current as CSSElementListItem;
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
            _settings.IgnoreTitle= (IgnoreTitleOptions)comboBoxIgnoreTitle.SelectedIndex;
        }

        private void buttonImportExport_Click(object sender, EventArgs e)
        {
            ImportExportForm form = new ImportExportForm(_settings);
            DialogResult result =  form.ShowDialog();
            ConverterSettingsForm_Load(this, null);
        }

        private void checkBoxCalibreMetadata_CheckedChanged(object sender, EventArgs e)
        {
            _settings.AddCalibreMetadata = checkBoxCalibreMetadata.Checked;
        }


 

     
    }
}
