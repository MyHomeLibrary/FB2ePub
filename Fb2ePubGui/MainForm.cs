using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fb2ePubConverter;
using FB2EPubConverter;
using Fb2ePubGui.Properties;
using FolderSettingsHelper.IniLocations;
using log4net;


namespace Fb2ePubGui
{
    public partial class FormGUI : Form
    {
        private readonly ConvertProcessor _processor = new ConvertProcessor();


        private readonly CultureInfo _russianCulture = new CultureInfo("ru");
        private readonly CultureInfo _englishCulture = new CultureInfo("en");
        private readonly CultureInfo _autoCulture = Thread.CurrentThread.CurrentUICulture;

        private delegate void ConversionDelegate(string[] files);

        delegate void OnButtonPressedCallback(object sender, EventArgs e);

        private delegate void SetStatusTextDelegate(string text);

        private delegate void SetUIConvertingDelegate(bool converting);

        private delegate void SetProgressStartDelegate(int total);

        private delegate void SetProgressFinishedDelegate();

        private delegate void SetFileProcessedDelegate();

        private delegate void CheckForUpdateDelegate(bool showIfCurrent);


        public FormGUI()
        {
            SetLanguage((LanguageSetting)Fb2EpubGUI.Default.Language);
            InitializeComponent();
            this.TopMost = Settings.Default.Topmost;
            topmostToolStripMenuItem.Checked = this.TopMost;
            backgroundWorkerProcess.WorkerSupportsCancellation = true;
            backgroundWorkerProcess.WorkerReportsProgress = true;
        }

        private enum LanguageSetting
        {
            Auto = 0,
            English = 1,
            Russian = 2,
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
            OpenSelectDialog();
        }

        private void OpenSelectDialog()
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
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
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
            }
        }

        private void SetUIConverting(bool converting)
        {
            if (InvokeRequired)
            {
                SetUIConvertingDelegate d = SetUIConverting;
                Invoke(d, new object[] { converting });
                return;
            }
            SetStatusText(converting ? Resources.Status_Converting : Resources.Status_Ready);
            UseWaitCursor = converting;
            Cursor = converting ? Cursors.WaitCursor : Cursors.Default;
            buttonConvert.Enabled = !converting;
            buttonShowFolder.Enabled = !converting;
            comboBoxDestination.Enabled = !converting;
            mainMenuStrip.Enabled = !converting;
            if (!converting)
            {
                toolStripProgressBarConversion.Value = 0;
            }
            toolStripProgressBarConversion.Visible = converting;
            toolStripDropDownButtonAbort.Visible = converting;
            toolStripDropDownButtonAbort.Enabled = converting;
            statusStrip1.Update();
        }



        internal void SetStatusText(string text)
        {
            if (InvokeRequired)
            {
                SetStatusTextDelegate d = SetStatusText;
                Invoke(d,new object[]{text});
            }
            else
            {
                if (string.IsNullOrEmpty(text))
                {
                    Debugger.Break();
                }
                toolStripStatus.Text = text;
                statusStrip1.Update();
            }
        }


        internal void SetFileProcessed()
        {
            if (statusStrip1.InvokeRequired)
            {
                SetFileProcessedDelegate d = SetFileProcessed;
                statusStrip1.Invoke(d);
                return;
            }
            toolStripProgressBarConversion.PerformStep();
        }

        internal void SetProgressFinished()
        {
            if (statusStrip1.InvokeRequired)
            {
                SetProgressFinishedDelegate d = SetProgressFinished;
                statusStrip1.Invoke(d);
                return;
            }
            toolStripProgressBarConversion.Value = toolStripProgressBarConversion.Maximum;
        }

        internal void SetProgressStart(int total)
        {
            if (statusStrip1.InvokeRequired)
            {
                SetProgressStartDelegate d = SetProgressStart;
                statusStrip1.Invoke(d, new object[] {total});
                return;
            }
            toolStripProgressBarConversion.Minimum = 0;
            toolStripProgressBarConversion.Maximum = total;
            toolStripProgressBarConversion.Step = 1;
        }


        private void ConvertFiles(string[] files)
        {
            _processor.LoadSettings();
            _processor.ProcessorSettings.Settings.OutPutPath = comboBoxDestination.Text;
            _processor.ProcessorSettings.Settings.ResourcesPath = ConvertProcessor.GetResourcesPath();
            _processor.ProcessorSettings.ProgressCallbacks = new ProgressUpdater(this);
            List<string> allFiles = SelectValidFiles(files);
            backgroundWorkerProcess.RunWorkerAsync(new object[] { _processor, allFiles,this });
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
            ResizeStatusStrip();
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
            if (!locations.Init())
            {
                string error = "No FB2EPUBExt.INI file found in any of the paths";
                Program.log.Error(error);
            }
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

        private void topmostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            topmostToolStripMenuItem.Checked = this.TopMost;
            Settings.Default.Topmost = this.TopMost;
            Settings.Default.Save();
        }

        private void toolStripMenuItemShowLog_Click(object sender, EventArgs e)
        {
            LogWindow log = new LogWindow(string.Format(@"{0}{1}", GlobalContext.Properties["LogName"], "fb2epubGUI.log"));
            log.Show();
        }

        private void toolStripMenuItemConvSettings_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                OnButtonPressedCallback d = toolStripMenuItemConvSettings_Click;
                Invoke(d, new object[] { sender, e });
                return;
            }

            if (ConvertProcessor.ShowSettingsDialog(this))
            {
                LoadPaths();
                comboBoxDestination.SelectedIndex = 0;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectDialog();
        }

        private void backgroundWorkerProcess_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] parameters = (object[]) e.Argument;
            ConvertProcessor processor = (ConvertProcessor)parameters[0];
            processor.PerformConvertOperation((List<string>)parameters[1],null);
        }

        private void backgroundWorkerProcess_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorkerProcess_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            SetUIConverting(false);
        }

        private void statusStrip1_Resize(object sender, EventArgs e)
        {
            ResizeStatusStrip();
        }

        private void ResizeStatusStrip()
        {
            toolStripStatus.Width = statusStrip1.Width - toolStripStatusLabel1.Width -
                                    toolStripProgressBarConversion.Width - toolStripDropDownButtonAbort.Width - (Width - ClientSize.Width);
        }

        private void toolStripDropDownButtonAbort_Click(object sender, EventArgs e)
        {
            AbortConversion();
        }

        private bool AbortConversion()
        {
            DialogResult result = MessageBox.Show(this, Resources.FormGUI_AbortConversion_Are_you_sure_you_wish_to_abort_conversion_,Resources.FormGUI_AbortConversion_Aborting_conversion,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _processor.AbortConversion();
                return true;
            }
            return false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (backgroundWorkerProcess.IsBusy)
            {
                if (!AbortConversion())
                {
                    e.Cancel = true;
                }
            }
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                CheckForUpdateDelegate d = CheckForUpdate;
                Invoke(d, new object[] { true });
                return;
            }
            CheckForUpdate(true);
        }

        /// <summary>
        /// Checks for version and shows message
        /// </summary>
        /// <param name="showIfCurrent">show message or not if current version</param>
        private void CheckForUpdate(bool showIfCurrent)
        {
            UpdateChecker updater = new UpdateChecker(this);
            UpdateCheckResult checkResult = updater.CheckForUpdate(true);
            switch (checkResult)
            {
                case UpdateCheckResult.CheckError:
                    // nothing to do as updater displayed error
                    break;
                case UpdateCheckResult.CurrentVersion:
                    if (showIfCurrent)
                    {
                        MessageBox.Show(this,
                            Resources.FormGUI_checkForUpdateToolStripMenuItem_Click_The_version_is_most_current,
                            Resources.FormGUI_checkForUpdateToolStripMenuItem_Click_Version_check, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    break;
                case UpdateCheckResult.UpdatePresent:
                    // if new version found - display the "new version found" dialog
                    ShowUpdateMessage(updater.CurrentVersion, updater.UpdateVersion, updater.UpdatePath);
                    break;
            }
        }

        private void ShowUpdateMessage(Version versionNow, Version versionServer, string updateURL)
        {
            NewUpdateMessage message = new NewUpdateMessage{VersionNow =  versionNow,VersionServer = versionServer,UpdateURl = updateURL};
            message.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FB2epubGUISettings settings = new FB2epubGUISettings();
            settings.ShowDialog(this);
        }

        /// <summary>
        /// Called first time frame shown (only once)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGUI_Shown(object sender, EventArgs e)
        {
            if (NeedToCheckForUpdate())
            {
                CheckForUpdateDelegate d = CheckForUpdate;
                Invoke(d, new object[] { false });
            }
        }

        /// <summary>
        /// Return if we need to check for update or not
        /// </summary>
        /// <returns></returns>
        private bool NeedToCheckForUpdate()
        {
            return UpdateChecker.IsNeedToUpdate();
        }
    }
}
