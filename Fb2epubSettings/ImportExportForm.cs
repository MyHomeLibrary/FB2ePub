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
    public partial class ImportExportForm : Form
    {
        private ConverterSettings _settings = null;

        public ImportExportForm(ConverterSettings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private ImportExportForm()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpenDialog = new OpenFileDialog();
            fileOpenDialog.Multiselect = false;
            fileOpenDialog.ShowReadOnly = false;
            fileOpenDialog.SupportMultiDottedExtensions = true;
            fileOpenDialog.Title = "Select configuration file to load settings from";
            fileOpenDialog.AddExtension = true;
            fileOpenDialog.AutoUpgradeEnabled = true;
            fileOpenDialog.CheckFileExists = true;
            fileOpenDialog.CheckPathExists = true;
            fileOpenDialog.DefaultExt = "*.xml";
            fileOpenDialog.Filter = "XML file | *.xml|Any file | *.*";
            DialogResult result = fileOpenDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConverterSettingsFile settingsFile = new ConverterSettingsFile();
                try
                {
                    settingsFile.Load(fileOpenDialog.FileName);
                    _settings.CopyFrom(settingsFile.Settings);
                    result = DialogResult.OK;
                    Close();
                }
                catch (Exception)
                {
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileSaveDialog = new SaveFileDialog();
            fileSaveDialog.AddExtension = true;
            fileSaveDialog.AutoUpgradeEnabled = true;
            fileSaveDialog.CheckPathExists = true;
            fileSaveDialog.DefaultExt = "*.xml";
            fileSaveDialog.FileName = "settings.xml";
            fileSaveDialog.Filter = "XML file | *.xml|Any file | *.*";
            fileSaveDialog.OverwritePrompt = true;
            fileSaveDialog.SupportMultiDottedExtensions = true;
            fileSaveDialog.Title = "Select file name to save configuration to";
            fileSaveDialog.ValidateNames = true;
            DialogResult result = fileSaveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConverterSettingsFile settingsFile = new ConverterSettingsFile();
                try
                {
                    settingsFile.Settings.CopyFrom(_settings);
                    settingsFile.Save(fileSaveDialog.FileName);
                    result = DialogResult.OK;
                    Close();
                }
                catch (Exception)
                {
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you wish to reset setting to defaults?", "Configuration reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                _settings.SetupDefaults();
                result = DialogResult.OK;
                Close();
            }

        }
    }
}
