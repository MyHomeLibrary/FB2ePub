using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RegisterFB2EPub
{
    public partial class MainForm : Form
    {
        public enum PageStates
        {
            IntroPage,
            AdvancedPage,
            RegisterBrowsePage,
            RegisterSelectionFb2Page,
            RegisterSelectionRarPage,
            RegisterSelectionZipPage,
            UnregisterFinishPage,
            RegisterFinishPage,
        }

        private enum InitPageRadioStates
        {
            Register,
            Unregister,
            Change_Repair,
            Custom,
        }

        private const string FileName = "Fb2EpubExt.dll";
        private const string FileName64 = "Fb2EpubExt_x64.dll";
        private PageStates currentPage = PageStates.IntroPage;
        private InitPageRadioStates initPageRadioButtonSelected = InitPageRadioStates.Register;
        private string _location = LocationDetector.DetectLocation((IntPtr.Size == 8) ? FileName64 : FileName);
        private readonly ExtRegistrator _registrator = new ExtRegistrator();
        private ExtRegistrator.RegistrationExtensionEnum _setState;

        private ResourceManager resourceManager = new ResourceManager("RegisterFB2EPub.ManualResource",
                                    System.Reflection.Assembly.GetExecutingAssembly());

        //private CultureInfo russianCulture = new CultureInfo("ru");



        public MainForm()
        {
            //Thread.CurrentThread.CurrentUICulture = russianCulture;
            InitializeComponent();
            _setState = _registrator.GetRegistrationState();
            if (_setState != ExtRegistrator.RegistrationExtensionEnum.None)
            {
                string regLocation = _registrator.GetRegistredLocation();
                if (!string.IsNullOrEmpty(regLocation))
                {
                    _location = regLocation;
                }
            }
            SetupToolTips();
            UpdateWizardButtonsAccordingToState();
            if (_setState == ExtRegistrator.RegistrationExtensionEnum.None)
            {
                radioButtonUnregister.Enabled = false;
                radioButtonChange.Enabled = false;
            }
            else
            {
                radioButtonRegister.Enabled = false;
                initPageRadioButtonSelected = InitPageRadioStates.Change_Repair;
            }
        }

        private void SetupToolTips()
        {
            SetupFb2PageToolTips();
            SetupZipPageToolTips();
            SetupRarPageToolTips();
        }

        private void SetupRarPageToolTips()
        {
            toolTip1.SetToolTip(radioButtonRarRegister, string.Format(resourceManager.GetString("radioButtonRegisterAnyTip"),".RAR"));
            toolTip1.SetToolTip(radioButtonNotRegisterRar, string.Format(resourceManager.GetString("radioButtonNotRegisterTip"),".RAR"));
        }

        private void SetupZipPageToolTips()
        {
            toolTip1.SetToolTip(radioButtonZipRegister, string.Format(resourceManager.GetString("radioButtonRegisterAnyTip"), ".ZIP"));
            toolTip1.SetToolTip(radioButtonNotRegisterZip, string.Format(resourceManager.GetString("radioButtonNotRegisterTip"), ".ZIP"));
        }

        private void SetupFb2PageToolTips()
        {
            toolTip1.SetToolTip(radioButtonFb2Register, string.Format(resourceManager.GetString("radioButtonRegisterAnyTip"), ".FB2"));
            toolTip1.SetToolTip(radioButtonNotRegisterFb2, string.Format(resourceManager.GetString("radioButtonNotRegisterTip"), ".FB2"));
        }


        private void buttonPrev_Click(object sender, EventArgs e)
        {
            switch (currentPage)
            {
                case PageStates.IntroPage:
                    DialogResult result = MessageBox.Show(this, resourceManager.GetString("exitMessage"), resourceManager.GetString("exitTitle"), MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Close();
                    }
                    break;
                case PageStates.AdvancedPage:
                    SetActiveState(PageStates.IntroPage);
                    break;
                case PageStates.RegisterBrowsePage:
                    SetActiveState(PageStates.IntroPage);
                    break;
                case PageStates.RegisterSelectionFb2Page:
                    if (string.IsNullOrEmpty(_location))
                    {
                        SetActiveState(PageStates.RegisterBrowsePage);
                    }
                    else
                    {
                        SetActiveState(PageStates.IntroPage);
                    }
                    break;
                case PageStates.RegisterSelectionRarPage:
                    SetActiveState(PageStates.RegisterSelectionZipPage);
                    break;
                case PageStates.RegisterSelectionZipPage:
                    SetActiveState(PageStates.RegisterSelectionFb2Page);
                    break;
                case PageStates.UnregisterFinishPage:
                    SetActiveState(PageStates.IntroPage);
                    break;
                case PageStates.RegisterFinishPage:
                    SetActiveState(PageStates.RegisterSelectionRarPage);
                    break;
            }
            UpdateWizardButtonsAccordingToState();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            switch (currentPage)
            {
                case PageStates.IntroPage:
                    ReadoutInitialRadioButtonState();
                    switch (initPageRadioButtonSelected)
                    {
                        case InitPageRadioStates.Register:
                            _setState = ExtRegistrator.RegistrationExtensionEnum.BaseSet;
                            if (string.IsNullOrEmpty(_location))
                            {
                                SetActiveState(PageStates.RegisterBrowsePage);
                            }
                            else
                            {
                                SetActiveState(PageStates.RegisterSelectionFb2Page);
                            }
                            break;
                        case InitPageRadioStates.Change_Repair:
                            _setState = _registrator.GetRegistrationState();
                            if (string.IsNullOrEmpty(_location))
                            {
                                SetActiveState(PageStates.RegisterBrowsePage);
                            }
                            else
                            {
                                SetActiveState(PageStates.RegisterSelectionFb2Page);
                            }
                            break;
                        case InitPageRadioStates.Unregister:
                            SetActiveState(PageStates.UnregisterFinishPage);
                            break;
                        case InitPageRadioStates.Custom:
                            SetActiveState(PageStates.AdvancedPage);
                            break;
                    }
                    break;
                case PageStates.AdvancedPage:
                    if (_setState != ExtRegistrator.RegistrationExtensionEnum.None )
                    {
                        _registrator.RegistrationPath = _location;
                        _registrator.Register(_setState);
                        UpdateIni();
                    }
                    else
                    {
                        _registrator.Unregister();
                    }
                    Close();
                    break;
                case PageStates.UnregisterFinishPage:
                    _registrator.Unregister();
                    Close();
                    break;
                case PageStates.RegisterBrowsePage:
                    SetActiveState(PageStates.RegisterSelectionFb2Page);
                    break;
                case PageStates.RegisterSelectionFb2Page:
                    DetectIniState();
                    if (radioButtonFb2Register.Checked)
                    {
                        _setState |= ExtRegistrator.RegistrationExtensionEnum.Fb2;
                    }
                    else
                    {
                        _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Fb2;
                    }
                    SetActiveState(PageStates.RegisterSelectionZipPage);
                    break;
                case PageStates.RegisterSelectionRarPage:
                    if (radioButtonRarRegister.Checked)
                    {
                        _setState |= ExtRegistrator.RegistrationExtensionEnum.Rar;
                    }
                    else
                    {
                        _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Rar;
                    }
                    SetActiveState(PageStates.RegisterFinishPage);
                    break;
                case PageStates.RegisterSelectionZipPage:
                    if (radioButtonZipRegister.Checked)
                    {
                        _setState |= ExtRegistrator.RegistrationExtensionEnum.Zip;
                    }
                    else
                    {
                        _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Zip;
                    }
                    SetActiveState(PageStates.RegisterSelectionRarPage);
                    break;
                case PageStates.RegisterFinishPage:
                    _registrator.RegistrationPath = _location;
                    _registrator.Register(_setState);
                    UpdateIni();
                    Close();
                    break;
            }
            UpdateWizardButtonsAccordingToState();
        }

        private void DetectIniState()
        {
            string iniLocation = string.Format("{0}\\FB2EPUBExt.INI", Path.GetDirectoryName(_location));
            IniAccess iniFile = new IniAccess(iniLocation);
            checkBoxFb2RarOnly.Checked = iniFile.IsFb2RarOnly();
            checkBoxFb2ZipOnly.Checked = iniFile.IsFb2ZipOnly();
        }

        private void UpdateIni()
        {
            string iniLocation = string.Format("{0}\\FB2EPUBExt.INI", Path.GetDirectoryName(_location));
            IniAccess iniFile = new IniAccess(iniLocation);
            iniFile.SetFb2RarOnly(checkBoxFb2RarOnly.Checked);
            iniFile.SetFb2ZipOnly(checkBoxFb2ZipOnly.Checked);
        }

        private void ReadoutInitialRadioButtonState()
        {
            if (radioButtonRegister.Checked)
            {
                initPageRadioButtonSelected = InitPageRadioStates.Register;
            }
            else if (radioButtonUnregister.Checked)
            {
                initPageRadioButtonSelected = InitPageRadioStates.Unregister;
            }
            else if(radioButtonCustom.Checked)
            {
                initPageRadioButtonSelected = InitPageRadioStates.Custom;
            }
            else if (radioButtonChange.Checked)
            {
                initPageRadioButtonSelected = InitPageRadioStates.Change_Repair;
            }
        }

        private void SetActiveState(PageStates state)
        {
            switch (state)
            {
                case PageStates.IntroPage:
                    tabWizard.SelectedTab = tabPageInitial;
                    break;
                case PageStates.AdvancedPage:
                    tabWizard.SelectedTab = tabPageAdvanced;
                    UpdateAdvancedGUI();
                    break;
                case PageStates.RegisterBrowsePage:
                    tabWizard.SelectedTab = tabPageRegisterBrowse;
                    UpdateRegistrationBrowseGUI();
                    break;
                case PageStates.RegisterSelectionFb2Page:
                    tabWizard.SelectedTab = tabPageRegisterFb2;
                    UpdateFb2Gui();
                    break;
                case PageStates.RegisterSelectionRarPage:
                    tabWizard.SelectedTab = tabPageRegisterRar;
                    UpdateRar2Gui();
                    break;
                case PageStates.RegisterSelectionZipPage:
                    tabWizard.SelectedTab = tabPageRegisterZip;
                    UpdateZipGui();
                    break;
                case PageStates.UnregisterFinishPage:
                    tabWizard.SelectedTab = tabPageUnregisterFinish;
                    break;
                case PageStates.RegisterFinishPage:
                    tabWizard.SelectedTab = tabPageRegisterFinish;
                    UpdateRegistrationFinishGUI();
                    break;
            }
            currentPage = state;
        }

        private void UpdateRegistrationFinishGUI()
        {
            textBoxResults.Text = string.Format(resourceManager.GetString("registerExtension"), ".FB2");
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Fb2) == ExtRegistrator.RegistrationExtensionEnum.Fb2)
            {
                textBoxResults.AppendText(resourceManager.GetString("yesAnswer"));
            }
            else
            {
                textBoxResults.AppendText(resourceManager.GetString("noAnswer"));
            }
            textBoxResults.AppendText("\r\n");

            textBoxResults.AppendText(string.Format(resourceManager.GetString("registerExtension"), ".ZIP"));
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Zip) == ExtRegistrator.RegistrationExtensionEnum.Zip)
            {
                textBoxResults.AppendText(resourceManager.GetString("yesAnswer"));
            }
            else
            {
                textBoxResults.AppendText(resourceManager.GetString("noAnswer"));
            }
            textBoxResults.AppendText("\r\n");

            textBoxResults.AppendText(string.Format(resourceManager.GetString("registerExtension"), ".RAR"));
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Rar) == ExtRegistrator.RegistrationExtensionEnum.Rar)
            {
                textBoxResults.AppendText(resourceManager.GetString("yesAnswer"));
            }
            else
            {
                textBoxResults.AppendText(resourceManager.GetString("noAnswer"));
            }
            textBoxResults.AppendText("\r\n");
        }

        private void UpdateZipGui()
        {
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Zip) == ExtRegistrator.RegistrationExtensionEnum.Zip)
            {
                radioButtonZipRegister.Checked = true;
                radioButtonNotRegisterZip.Checked = false;
            }
            else
            {
                radioButtonZipRegister.Checked = false;
                radioButtonNotRegisterZip.Checked = true;

            }
        }

        private void UpdateRar2Gui()
        {
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Rar) == ExtRegistrator.RegistrationExtensionEnum.Rar)
            {
                radioButtonRarRegister.Checked = true;
                radioButtonNotRegisterRar.Checked = false;
            }
            else
            {
                radioButtonRarRegister.Checked = false;
                radioButtonNotRegisterRar.Checked = true;

            }
        }

        private void UpdateFb2Gui()
        {
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Fb2) == ExtRegistrator.RegistrationExtensionEnum.Fb2)
            {
                radioButtonFb2Register.Checked = true;
                radioButtonNotRegisterFb2.Checked = false;
            }
            else
            {
                radioButtonFb2Register.Checked = false;
                radioButtonNotRegisterFb2.Checked = true;

            }
        }

        private void UpdateWizardButtonsAccordingToState()
        {
            switch (currentPage)
            {
                case PageStates.IntroPage:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("exitButton");
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("nextButton");
                    break;
                case PageStates.AdvancedPage:
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("finishButton");
                    break;
                case PageStates.RegisterBrowsePage:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonNext.Enabled = false;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("nextButton");
                    break;
                case PageStates.RegisterSelectionFb2Page:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("nextButton");
                    break;
                case PageStates.RegisterSelectionRarPage:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("nextButton");
                    break;
                case PageStates.RegisterSelectionZipPage:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("nextButton");
                    break;
                case PageStates.UnregisterFinishPage:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonNext.Enabled = true;
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("finishButton");
                    break;
                case PageStates.RegisterFinishPage:
                    buttonPrev.Enabled = true;
                    buttonPrev.Visible = true;
                    buttonPrev.Text = resourceManager.GetString("prevButton");
                    buttonNext.Enabled = (_setState != ExtRegistrator.RegistrationExtensionEnum.None);
                    buttonNext.Visible = true;
                    buttonNext.Text = resourceManager.GetString("finishButton");
                    break;
            }          
        }

        private void tabPageInitial_Enter(object sender, EventArgs e)
        {
            radioButtonRegister.Checked = (initPageRadioButtonSelected == InitPageRadioStates.Register);
            radioButtonUnregister.Checked = (initPageRadioButtonSelected == InitPageRadioStates.Unregister);
            radioButtonCustom.Checked = (initPageRadioButtonSelected == InitPageRadioStates.Custom);
            radioButtonChange.Checked = (initPageRadioButtonSelected == InitPageRadioStates.Change_Repair);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseForDLLLocation();
            UpdateAdvancedGUI();
        }

        private void BrowseForDLLLocation()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string fileName = (IntPtr.Size == 8) ? FileName64 : FileName;
            dialog.Filter = string.Format(resourceManager.GetString("browseRequest"), fileName);
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            dialog.Title = string.Format(resourceManager.GetString("browseTitle"), fileName);
            if (dialog.ShowDialog() == DialogResult.OK && dialog.SafeFileName == fileName)
            {
                _location = dialog.FileName;
            }
        }

        private void UpdateAdvancedGUI()
        {
            if (!string.IsNullOrEmpty(_location))
            {
                dllPath.Text = _location;
                checkBoxFb2.Enabled = !checkBoxAny.Checked;
                checkBoxZip.Enabled = !checkBoxAny.Checked;
                checkBoxRar.Enabled = !checkBoxAny.Checked;
                buttonUnSelectAll.Enabled = true;
                buttonSelectAll.Enabled = true;
                checkBoxAny.Enabled = true;
            }
            else
            {
                dllPath.Text = "???";
                checkBoxFb2.Enabled = false;
                checkBoxZip.Enabled = false;
                checkBoxRar.Enabled = false;
                buttonUnSelectAll.Enabled = false;
                buttonSelectAll.Enabled = false;
                checkBoxAny.Enabled = false;
            }

            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Fb2) == ExtRegistrator.RegistrationExtensionEnum.Fb2)
            {
                checkBoxFb2.CheckState = CheckState.Checked;
            }
            else
            {
                checkBoxFb2.CheckState = CheckState.Unchecked;
            }

            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Zip) == ExtRegistrator.RegistrationExtensionEnum.Zip)
            {
                checkBoxZip.CheckState = CheckState.Checked;
            }
            else
            {
                checkBoxZip.CheckState = CheckState.Unchecked;
            }

            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Rar) == ExtRegistrator.RegistrationExtensionEnum.Rar)
            {
                checkBoxRar.CheckState = CheckState.Checked;
            }
            else
            {
                checkBoxRar.CheckState = CheckState.Unchecked;
            }
            if ((_setState & ExtRegistrator.RegistrationExtensionEnum.Any) == ExtRegistrator.RegistrationExtensionEnum.Any)
            {
                checkBoxAny.CheckState = CheckState.Checked;
            }
            else
            {
                checkBoxAny.CheckState = CheckState.Unchecked;
            }

        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            _setState = ExtRegistrator.RegistrationExtensionEnum.All;
            UpdateAdvancedGUI();
        }

        private void buttonUnSelectAll_Click(object sender, EventArgs e)
        {
            _setState = ExtRegistrator.RegistrationExtensionEnum.None;
            UpdateAdvancedGUI();
        }


        private void checkBoxFb2_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBoxFb2.CheckState)
            {
                case CheckState.Checked:
                    _setState |= ExtRegistrator.RegistrationExtensionEnum.Fb2;
                    break;
                case CheckState.Unchecked:
                    _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Fb2;
                    break;
            }
            UpdateAdvancedGUI();
        }

        private void checkBoxZip_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBoxZip.CheckState)
            {
                case CheckState.Checked:
                    _setState |= ExtRegistrator.RegistrationExtensionEnum.Zip;
                    break;
                case CheckState.Unchecked:
                    _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Zip;
                    break;
            }
            UpdateAdvancedGUI();
        }

        private void checkBoxAny_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBoxAny.CheckState)
            {
                case CheckState.Checked:
                    _setState |= ExtRegistrator.RegistrationExtensionEnum.Any;
                    break;
                case CheckState.Unchecked:
                    _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Any;
                    break;
            }
            UpdateAdvancedGUI();
        }

        private void checkBoxRar_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBoxRar.CheckState)
            {
                case CheckState.Checked:
                    _setState |= ExtRegistrator.RegistrationExtensionEnum.Rar;
                    break;
                case CheckState.Unchecked:
                    _setState &= ~ExtRegistrator.RegistrationExtensionEnum.Rar;
                    break;
            }
            UpdateAdvancedGUI();
        }

        private void buttonRegBrowse_Click(object sender, EventArgs e)
        {
            BrowseForDLLLocation();
            UpdateRegistrationBrowseGUI();
        }

        private void UpdateRegistrationBrowseGUI()
        {
            if (string.IsNullOrEmpty(_location))
            {
                buttonNext.Enabled = false;
                labelRegPath.Text = "???";
            }
            else
            {
                buttonNext.Enabled = true;
                labelRegPath.Text = _location;
            }
        }
    }
}
