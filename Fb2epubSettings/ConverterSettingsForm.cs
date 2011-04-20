using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fb2epubSettings
{
    public partial class ConverterSettingsForm : Form
    {
        delegate void OnButtonPressedCallback(object sender, EventArgs e);


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
            Fb2Epub.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
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
            LoadPathsGroup();
        }

        private void LoadPathsGroup()
        {
            listBoxPaths.Items.Clear();
            textBoxPath.Text = string.Empty;
            IniLocations.IniLocations locations = new IniLocations.IniLocations();
            foreach (var iniLocation in locations)
            {
                listBoxPaths.Items.Add(iniLocation);
            }
            listBoxPaths.SelectedIndex = 0;
            UpdatePathsTabGui();
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

        }

        private void buttonUpPath_Click(object sender, EventArgs e)
        {

        }

        private void buttonDownPath_Click(object sender, EventArgs e)
        {

        }

        private void buttonPathNew_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxVisibleInExt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxVisibleInGUI_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonBrowsePath_Click(object sender, EventArgs e)
        {

        }

        private void listBoxPaths_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdatePathsTabGui()
        {
        }
    }
}
