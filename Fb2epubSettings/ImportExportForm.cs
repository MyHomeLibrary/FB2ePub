using System;
using System.Reflection;
using System.Windows.Forms;
using ConverterContracts.Settings;

namespace Fb2epubSettings
{
    public partial class ImportExportForm : Form
    {
        private IConverterSettings _settings = null;
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        public ImportExportForm(IConverterSettings settings)
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
            fileOpenDialog.Title = Resources.Fb2epubSettings.SelectFileToLoadFrom;
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
                catch (Exception ex)
                {               
                    MessageBox.Show(string.Format("Error loading file {0} : {1}", fileOpenDialog.FileName,ex.Message),"Load file error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Log.Error( ex);
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
            fileSaveDialog.Title = Resources.Fb2epubSettings.SelectFileNameSaveTo;
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
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error saving file {0} : {1}", fileSaveDialog.FileName, ex.Message), "Load file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log.Error(ex);
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
