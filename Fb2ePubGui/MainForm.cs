using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Fb2ePubConverter;
using Fb2ePubGui.Properties;
using Fb2epubSettings;

namespace Fb2ePubGui
{
    public partial class FormGUI : Form
    {
        private readonly Fb2EPubConverterEngine _converter = new Fb2EPubConverterEngine{ ResourcesPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)};

        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        public FormGUI()
        {
            InitializeComponent();
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = settingsToolStripMenuItem_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }
            ConverterSettingsForm settings = new ConverterSettingsForm();
            settings.ShowDialog(this);
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
                ConvertFiles(ofDialog.FileNames);
            }
        }

        private void ConvertFiles(string[] files)
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
                else // regular fil - just add
                {
                    allFiles.Add(fileItem);
                }
            }


            _converter.Transliterate = Fb2epubSettings.Fb2Epub.Default.Transliterate;
            _converter.TransliterateFileName = Fb2epubSettings.Fb2Epub.Default.TransliterateFileName;
            _converter.TransliterateToc = Fb2epubSettings.Fb2Epub.Default.TransliterateTOC;
            _converter.Fb2Info = Fb2epubSettings.Fb2Epub.Default.FB2Info;
            _converter.FixMode = (Fb2EPubConverterEngine.FixOptions)Fb2epubSettings.Fb2Epub.Default.FixMode;
            _converter.AddSeqToTitle = Fb2epubSettings.Fb2Epub.Default.AddSequences;
            _converter.SequenceFormat = Fb2epubSettings.Fb2Epub.Default.SequenceFormat;
            _converter.NoSequenceFormat = Fb2epubSettings.Fb2Epub.Default.NoSequenceFormat;
            _converter.NoSeriesFormat = Fb2epubSettings.Fb2Epub.Default.NoSeriesFormat;
            _converter.Flat = Fb2epubSettings.Fb2Epub.Default.FlatStructure;
            _converter.ConvertAlphaPng = Fb2epubSettings.Fb2Epub.Default.ConvertAlphaPNG;
            _converter.EmbedStyles = Fb2epubSettings.Fb2Epub.Default.EmbedStyles;
            _converter.AuthorFormat = Fb2epubSettings.Fb2Epub.Default.AuthorFormat;
            _converter.FileAsFormat = Fb2epubSettings.Fb2Epub.Default.FileAsFormat;
            _converter.CapitalDrop = Fb2epubSettings.Fb2Epub.Default.Capitalize;
            _converter.SkipAboutPage = Fb2epubSettings.Fb2Epub.Default.SkipAboutPage;
            _converter.Fonts = Fb2epubSettings.Fb2Epub.Default.Fonts;

            // now really convert files
            foreach (var file in allFiles)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                if (fileNameWithoutExtension == null)
                {
                    continue;
                }
                fileNameWithoutExtension = Path.ChangeExtension(fileNameWithoutExtension, "epub");              
                string fileName = Path.Combine(comboBoxDestination.Text,fileNameWithoutExtension);
                try
                {
                    if (!_converter.ConvertFile(file))
                    {
                        continue;
                    }
                    _converter.Save(fileName);
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }



        private void buttonConvert_DragDrop(object sender, DragEventArgs e)
        {
              if(e.Data.GetDataPresent(DataFormats.FileDrop) )
              {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    ConvertFiles(files);
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
        }

        private void LoadPaths()
        {
            comboBoxDestination.Items.Clear();
            IniLocations locations = new IniLocations();
            foreach (var location in locations)
            {
                comboBoxDestination.Items.Add(location);
            }
        }


        private void buttonShowFolder_Click(object sender, EventArgs e)
        {
            Process.Start(comboBoxDestination.Text);
        }

    }
}
