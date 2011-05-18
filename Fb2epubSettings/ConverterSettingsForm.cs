using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fb2epubSettings.IniLocations;

namespace Fb2epubSettings
{
    public partial class ConverterSettingsForm : Form
    {
        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        private readonly IniLocations.IniLocations _locations = new IniLocations.IniLocations();


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
            Fb2Epub.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
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
            Fb2Epub.Default.Reload();
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
            checkBoxTransliterateTOC.Checked = Fb2Epub.Default.TransliterateTOC;
            checkBoxTransliterateFileName.Checked = Fb2Epub.Default.TransliterateFileName;
            checkBoxTransliterateAdditional.Checked = Fb2Epub.Default.Transliterate;
            textBoxAuthorFormat.Text = Fb2Epub.Default.AuthorFormat;
            textBoxFileAsFormat.Text = Fb2Epub.Default.FileAsFormat;
            textBoxNoSequenceFormat.Text = Fb2Epub.Default.NoSequenceFormat;
            textBoxSequenceFormat.Text = Fb2Epub.Default.SequenceFormat;
            textBoxNoSeriesFormat.Text = Fb2Epub.Default.NoSeriesFormat;
            checkBoxAddSequences.Checked = Fb2Epub.Default.AddSequences;
            checkBoxFb2Info.Checked = Fb2Epub.Default.FB2Info;
            checkBoxConvertAlphaPNG.Checked = Fb2Epub.Default.ConvertAlphaPNG;
            checkBoxFlatStructure.Checked = Fb2Epub.Default.FlatStructure;
            checkBoxEmbedStyles.Checked = Fb2Epub.Default.EmbedStyles;
            checkBoxCapitalize.Checked = Fb2Epub.Default.Capitalize;
            checkBoxSkipAboutPage.Checked = Fb2Epub.Default.SkipAboutPage;
            LoadFixMode();
            UpdateSequencesGroup();
            _locations.Init();
            LoadPathsGroup();
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
            radioButtonSDDisabled.Checked = (_locations.SingleDestination == -1);
            radioButtonSDEnabled.Checked = (_locations.SingleDestination != -1);
            UpdatePathsTabGui();
            UpdateSingleDestinationCombo();
        }

        private void UpdateSingleDestinationCombo()
        {
            comboBoxSDValue.Items.Clear();
            comboBoxSDValue.Enabled = (_locations.SingleDestination != -1) && _locations.HasShellLocations();
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
            comboBoxFixMode.SelectedIndex = Fb2Epub.Default.FixMode;
        }

        private void checkBoxTransliterateTOC_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.TransliterateTOC = checkBoxTransliterateTOC.Checked;
        }

        private void checkBoxTransliterateFileName_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.TransliterateFileName = checkBoxTransliterateFileName.Checked;
        }

        private void checkBoxTransliterateAdditional_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.Transliterate = checkBoxTransliterateAdditional.Checked;
        }

        private void textBoxSequenceFormat_TextChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.SequenceFormat = textBoxSequenceFormat.Text;
        }

        private void textBoxNoSequenceFormat_TextChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.NoSequenceFormat = textBoxNoSequenceFormat.Text;
        }

        private void textBoxAuthorFormat_TextChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.AuthorFormat = textBoxAuthorFormat.Text;
        }

        private void textBoxFileAsFormat_TextChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.FileAsFormat = textBoxFileAsFormat.Text;
        }

        private void checkBoxAddSequences_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.AddSequences = checkBoxAddSequences.Checked;
            UpdateSequencesGroup();
        }

        private void checkBoxFb2Info_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.FB2Info = checkBoxFb2Info.Checked;
        }

        private void checkBoxConvertAlphaPNG_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.ConvertAlphaPNG = checkBoxConvertAlphaPNG.Checked;
        }

        private void checkBoxFlatStructure_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.FlatStructure = checkBoxFlatStructure.Checked;
        }

        private void checkBoxEmbedStyles_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.EmbedStyles = checkBoxEmbedStyles.Checked;
        }

        private void checkBoxCapitalize_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.Capitalize = checkBoxCapitalize.Checked;
        }

        private void checkBoxSkipAboutPage_CheckedChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.SkipAboutPage = checkBoxSkipAboutPage.Checked;
        }

        private void comboBoxFixMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.FixMode = comboBoxFixMode.SelectedIndex;
        }

        private void textBoxNoSeriesFormat_TextChanged(object sender, EventArgs e)
        {
            Fb2Epub.Default.NoSeriesFormat = textBoxNoSeriesFormat.Text;
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
            buttonUpPath.Enabled = (listBoxPaths.SelectedIndex > 0);
            buttonDownPath.Enabled = (listBoxPaths.SelectedIndex < listBoxPaths.Items.Count-1);
            buttonDeletePath.Enabled = (listBoxPaths.Items.Count != 0);

            Location currentLocation = (Location) listBoxPaths.SelectedItem;
            checkBoxVisibleInExt.Enabled = (currentLocation != null);
            checkBoxVisibleInGUI.Enabled = (currentLocation != null);
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


     
    }
}
