using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fb2ePubConverter;

namespace Fb2ePubGui
{
    public partial class FormGUI : Form
    {
        private Fb2EPubConverterEngine converter = new Fb2EPubConverterEngine();

        public FormGUI()
        {
            InitializeComponent();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            ofDialog.Title = "Please select FB2 files to convert";
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
                    string superPattern = "*.fb2,*.zip,*.rar";
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

            converter.Transliterate = FB2EpubSettings.Default.Transliterate;
            converter.TransliterateFileName = FB2EpubSettings.Default.TransliterateFileName;
            converter.TransliterateToc = FB2EpubSettings.Default.TransliterateTOC;
            converter.Fb2Info = FB2EpubSettings.Default.FB2Info;
            converter.FixMode = (Fb2EPubConverterEngine.FixOptions)FB2EpubSettings.Default.FixMode;
            converter.AddSeqToTitle = FB2EpubSettings.Default.AddSequences;
            converter.Flat = FB2EpubSettings.Default.FlatStructure;
            converter.ConvertAlphaPng = FB2EpubSettings.Default.ConvertAlphaPNG;
            converter.EmbedStyles = FB2EpubSettings.Default.EmbedStyles;
            converter.Fonts = FB2EpubSettings.Default.Fonts;
            converter.OutPutPath = comboBoxDestination.Text;

            // now really convert files
            foreach (var file in allFiles)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                string fileLocation = string.Format("{0}\\", Path.GetDirectoryName(Path.GetFullPath(file)));
                if (!string.IsNullOrEmpty(converter.OutPutPath))
                {
                    fileLocation = converter.OutPutPath;
                }
                // in case fb2.zip remove the "fb2" part
                if (fileNameWithoutExtension.ToLower().EndsWith(".fb2"))
                {
                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
                }
                string fileName = string.Format("{2}{0}.{1}", fileNameWithoutExtension, "epub", fileLocation);
                try
                {
                    if (!converter.ConvertFile(file))
                    {
                        continue;
                    }
                    converter.Save(fileName);
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

        private void FormGUI_Load(object sender, EventArgs e)
        {
            comboBoxDestination.Items.Clear();
            comboBoxDestination.Items.Add(string.Format("{0}\\My Books\\",Environment.GetFolderPath(Environment.SpecialFolder.Personal)));
            comboBoxDestination.SelectedIndex = 0;
        }
    }
}
