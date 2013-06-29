using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fb2ePubConverter;
using FB2EPubConverter;
using Fb2ePubGui.Properties;
using FolderSettingsHelper.IniLocations;


namespace Fb2ePubGui
{
    public partial class FormGUI : Form
    {
        private readonly CultureInfo _russianCulture = new CultureInfo("ru");
        private readonly CultureInfo _englishCulture = new CultureInfo("en");
        private readonly CultureInfo _autoCulture = Thread.CurrentThread.CurrentUICulture;

        private delegate void ConversionDelegate(string[] files);

        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        public FormGUI()
        {
            SetLanguage((LanguageSetting)Fb2EpubGUI.Default.Language);
            InitializeComponent();
        }

        private enum LanguageSetting
        {
            Auto = 0,
            English = 1,
            Russian = 2,
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = settingsToolStripMenuItem_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
           
            if ( ConvertProcessor.ShowSettingsDialog(this) )
            {
                LoadPaths();
                comboBoxDestination.SelectedIndex = 0;               
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = exitToolStripMenuItem_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
            Close();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.AutoUpgradeEnabled = true;
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            ofDialog.DefaultExt = "fb2";
            ofDialog.Filter = string.Format("FB2 file | *.fb2;*.fb2.zip;*.fb2.rar |Fb2 file and any archive | *.fb2;*.zip;*.rar;*.fb2.zip;*.fb2.rar |Any file | *.*");
            ofDialog.FilterIndex = 1;
            ofDialog.Multiselect = true;
            ofDialog.Title = Resources.FormGUI_buttonConvert_Click_Please_select_FB2_files_to_convert;
            ofDialog.RestoreDirectory = true;
            ofDialog.ShowReadOnly = false;
            ofDialog.SupportMultiDottedExtensions = true;
            ofDialog.ValidateNames = true;
            if ( ofDialog.ShowDialog(this) == DialogResult.OK )
            {
                PerformConversion(ofDialog.FileNames);
            }
        }

        private void PerformConversion(string[] files)
        {
            if (InvokeRequired)
            {
                ConversionDelegate d  = PerformConversion;
                Invoke(d, new object[] { files });
            }
            else
            {
                SetUIConverting(true);
                ConvertFiles(files);
                SetUIConverting(false);
            }
        }

        private void SetUIConverting(bool converting)
        {
            toolStripStatus.Text = converting ? Resources.Status_Converting : Resources.Status_Ready;
            UseWaitCursor = converting;
            Cursor = converting ? Cursors.WaitCursor : Cursors.Default;
            buttonConvert.Enabled = !converting;
            buttonShowFolder.Enabled = !converting;
            comboBoxDestination.Enabled = !converting;
            //Enabled = !converting;
        }

        private void ConvertFiles(string[] files)
        {
            ConvertProcessor processor = new ConvertProcessor();
            processor.LoadSettings();
            processor.ProcessorSettings.Settings.OutPutPath = comboBoxDestination.Text;
            processor.ProcessorSettings.Settings.ResourcesPath =
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            List<string> allFiles = SelectValidFiles(files);
            processor.PerformConvertOperation(allFiles, null);
        }

        private List<string> SelectValidFiles(string[] files)
        {
            List<string> allFiles = new List<string>();
            // detect all files that require conversion
            foreach (var fileItem in files)
            {
                // in case of folder detect all sub files matching mask
                if ((File.GetAttributes(fileItem) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    const string superPattern = "*.fb2,*.zip,*.rar";
                    foreach (var pattern in superPattern.Split(','))
                    {
                        foreach (var subfile in Directory.GetFiles(fileItem, pattern, SearchOption.AllDirectories))
                        {
                            allFiles.Add(subfile);
                        }
                    }
                }
                else // regular fill - just add
                {
                    allFiles.Add(fileItem);
                }
            }
            return allFiles;
        }


        private void buttonConvert_DragDrop(object sender, DragEventArgs e)
        {
              if(e.Data.GetDataPresent(DataFormats.FileDrop) )
              {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    PerformConversion(files);
              }
        }

        private void buttonConvert_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormGuiLoad(object sender, EventArgs e)
        {
            comboBoxDestination.Items.Clear();
            LoadPaths();
            comboBoxDestination.SelectedIndex = 0;
            SetLanguageUI((LanguageSetting)Fb2EpubGUI.Default.Language);
            SetUIConverting(false);
        }

        private void LoadPaths()
        {
            comboBoxDestination.Items.Clear();
            IniLocations locations = new IniLocations();
            locations.Init();
            locations.Load();
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                              "My Books\\");
            if (!Directory.Exists(defaultPath))
            {
                try
                {
                    Directory.CreateDirectory(defaultPath);
                }
                catch (Exception)
                {

                }
            }
            comboBoxDestination.Items.Add(defaultPath);

            foreach (var location in locations.GetGuiLocations())
            {
                comboBoxDestination.Items.Add(location.Path);
            }
        }


        private void buttonShowFolder_Click(object sender, EventArgs e)
        {
            Process.Start(comboBoxDestination.Text);
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage(LanguageSetting.Russian);
            Fb2EpubGUI.Default.Save();
            Close();
            Application.Restart();
        }

        private void autoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage(LanguageSetting.Auto);
            Fb2EpubGUI.Default.Save();
            Close();
            Application.Restart();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage(LanguageSetting.English);
            Fb2EpubGUI.Default.Save();
            Close();
            Application.Restart();
        }

        private void SetLanguage(LanguageSetting languageSetting)
        {
            Fb2EpubGUI.Default.Language = (int)languageSetting;
            switch (languageSetting)
            {
                case LanguageSetting.Auto:
                    Thread.CurrentThread.CurrentUICulture = _autoCulture;
                    Resources.Culture = _autoCulture;
                    break;
                case LanguageSetting.English:
                    Thread.CurrentThread.CurrentUICulture = _englishCulture;
                    Resources.Culture = _englishCulture;
                    break;
                case LanguageSetting.Russian:
                    Thread.CurrentThread.CurrentUICulture = _russianCulture;
                    Resources.Culture = _russianCulture;
                    break;
            }
            SetLanguageUI(languageSetting);
        }

        private void SetLanguageUI(LanguageSetting languageSetting)
        {
            if (autoToolStripMenuItem == null)
            {
                return;
            }
            autoToolStripMenuItem.Checked = false;
            if (englishToolStripMenuItem == null)
            {
                return;
            }
            englishToolStripMenuItem.Checked = false;
            if (russianToolStripMenuItem == null)
            {
                return;
            }
            russianToolStripMenuItem.Checked = false;
            switch (languageSetting)
            {
                case LanguageSetting.Auto:
                    autoToolStripMenuItem.Checked = true;
                    break;
                case LanguageSetting.English:
                    englishToolStripMenuItem.Checked = true;
                    break;
                case LanguageSetting.Russian:
                    russianToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }


    }
}
