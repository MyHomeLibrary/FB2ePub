namespace Fb2epubSettings
{
    partial class ConverterSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverterSettingsForm));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxTransliterateAdditional = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateFileName = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateTOC = new System.Windows.Forms.CheckBox();
            this.textBoxNoSeriesFormat = new System.Windows.Forms.TextBox();
            this.labelNoSeries = new System.Windows.Forms.Label();
            this.checkBoxAddSequences = new System.Windows.Forms.CheckBox();
            this.textBoxNoSequenceFormat = new System.Windows.Forms.TextBox();
            this.labelNoSeqFormat = new System.Windows.Forms.Label();
            this.textBoxSequenceFormat = new System.Windows.Forms.TextBox();
            this.labelSeqFormat = new System.Windows.Forms.Label();
            this.textBoxFileAsFormat = new System.Windows.Forms.TextBox();
            this.labelFileAsFormat = new System.Windows.Forms.Label();
            this.textBoxAuthorFormat = new System.Windows.Forms.TextBox();
            this.labelAuthorFormat = new System.Windows.Forms.Label();
            this.comboBoxFixMode = new System.Windows.Forms.ComboBox();
            this.labelFixMode = new System.Windows.Forms.Label();
            this.checkBoxSkipAboutPage = new System.Windows.Forms.CheckBox();
            this.checkBoxCapitalize = new System.Windows.Forms.CheckBox();
            this.checkBoxEmbedStyles = new System.Windows.Forms.CheckBox();
            this.checkBoxFlatStructure = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertAlphaPNG = new System.Windows.Forms.CheckBox();
            this.checkBoxFb2Info = new System.Windows.Forms.CheckBox();
            this.comboBoxSDValue = new System.Windows.Forms.ComboBox();
            this.radioButtonSDEnabled = new System.Windows.Forms.RadioButton();
            this.radioButtonSDDisabled = new System.Windows.Forms.RadioButton();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonPathNew = new System.Windows.Forms.Button();
            this.checkBoxVisibleInGUI = new System.Windows.Forms.CheckBox();
            this.checkBoxVisibleInExt = new System.Windows.Forms.CheckBox();
            this.buttonDownPath = new System.Windows.Forms.Button();
            this.buttonUpPath = new System.Windows.Forms.Button();
            this.buttonDeletePath = new System.Windows.Forms.Button();
            this.buttonBrowseForXPGT = new System.Windows.Forms.Button();
            this.buttonXPGTClear = new System.Windows.Forms.Button();
            this.textBoxTemplatePath = new System.Windows.Forms.TextBox();
            this.checkBoxUseXPGT = new System.Windows.Forms.CheckBox();
            this.buttonRemoveFont = new System.Windows.Forms.Button();
            this.buttonEditFont = new System.Windows.Forms.Button();
            this.buttonAddFont = new System.Windows.Forms.Button();
            this.listViewFonts = new System.Windows.Forms.ListView();
            this.columnHeaderFont = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBoxCSSFonts = new System.Windows.Forms.ListBox();
            this.buttonDeleteCSSFont = new System.Windows.Forms.Button();
            this.buttonAddCSSFont = new System.Windows.Forms.Button();
            this.listBoxCSSElements = new System.Windows.Forms.ListBox();
            this.buttonRemoveCSS = new System.Windows.Forms.Button();
            this.buttonAddCSS = new System.Windows.Forms.Button();
            this.groupBoxAssignedFonts = new System.Windows.Forms.GroupBox();
            this.groupBoxCSS = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxIgnoreTitle = new System.Windows.Forms.ComboBox();
            this.buttonImportExport = new System.Windows.Forms.Button();
            this.tabControlSettings = new Fb2epubSettings.NonStyledTabs();
            this.tabPageTransliteration = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPageSequences = new System.Windows.Forms.TabPage();
            this.groupBoxSequences = new System.Windows.Forms.GroupBox();
            this.tabPageDifferent = new System.Windows.Forms.TabPage();
            this.tabPageFonts = new System.Windows.Forms.TabPage();
            this.tabPagePaths = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxPaths = new System.Windows.Forms.ListBox();
            this.tabPageXPGT = new System.Windows.Forms.TabPage();
            this.groupBoxXPGTPath = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageCSSElements = new System.Windows.Forms.TabPage();
            this.groupBoxAssignedFonts.SuspendLayout();
            this.groupBoxCSS.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageTransliteration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageSequences.SuspendLayout();
            this.groupBoxSequences.SuspendLayout();
            this.tabPageDifferent.SuspendLayout();
            this.tabPageFonts.SuspendLayout();
            this.tabPagePaths.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageXPGT.SuspendLayout();
            this.groupBoxXPGTPath.SuspendLayout();
            this.tabPageCSSElements.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.toolTipControl.SetToolTip(this.buttonSave, resources.GetString("buttonSave.ToolTip"));
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonReset
            // 
            resources.ApplyResources(this.buttonReset, "buttonReset");
            this.buttonReset.Name = "buttonReset";
            this.toolTipControl.SetToolTip(this.buttonReset, resources.GetString("buttonReset.ToolTip"));
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTipControl.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // toolTipControl
            // 
            this.toolTipControl.AutoPopDelay = 50000;
            this.toolTipControl.InitialDelay = 500;
            this.toolTipControl.IsBalloon = true;
            this.toolTipControl.ReshowDelay = 100;
            // 
            // checkBoxTransliterateAdditional
            // 
            resources.ApplyResources(this.checkBoxTransliterateAdditional, "checkBoxTransliterateAdditional");
            this.checkBoxTransliterateAdditional.Name = "checkBoxTransliterateAdditional";
            this.toolTipControl.SetToolTip(this.checkBoxTransliterateAdditional, resources.GetString("checkBoxTransliterateAdditional.ToolTip"));
            this.checkBoxTransliterateAdditional.UseVisualStyleBackColor = true;
            this.checkBoxTransliterateAdditional.CheckedChanged += new System.EventHandler(this.checkBoxTransliterateAdditional_CheckedChanged);
            // 
            // checkBoxTransliterateFileName
            // 
            resources.ApplyResources(this.checkBoxTransliterateFileName, "checkBoxTransliterateFileName");
            this.checkBoxTransliterateFileName.Name = "checkBoxTransliterateFileName";
            this.toolTipControl.SetToolTip(this.checkBoxTransliterateFileName, resources.GetString("checkBoxTransliterateFileName.ToolTip"));
            this.checkBoxTransliterateFileName.UseVisualStyleBackColor = true;
            this.checkBoxTransliterateFileName.CheckedChanged += new System.EventHandler(this.checkBoxTransliterateFileName_CheckedChanged);
            // 
            // checkBoxTransliterateTOC
            // 
            resources.ApplyResources(this.checkBoxTransliterateTOC, "checkBoxTransliterateTOC");
            this.checkBoxTransliterateTOC.Name = "checkBoxTransliterateTOC";
            this.toolTipControl.SetToolTip(this.checkBoxTransliterateTOC, resources.GetString("checkBoxTransliterateTOC.ToolTip"));
            this.checkBoxTransliterateTOC.UseVisualStyleBackColor = true;
            this.checkBoxTransliterateTOC.CheckedChanged += new System.EventHandler(this.checkBoxTransliterateTOC_CheckedChanged);
            // 
            // textBoxNoSeriesFormat
            // 
            resources.ApplyResources(this.textBoxNoSeriesFormat, "textBoxNoSeriesFormat");
            this.textBoxNoSeriesFormat.Name = "textBoxNoSeriesFormat";
            this.toolTipControl.SetToolTip(this.textBoxNoSeriesFormat, resources.GetString("textBoxNoSeriesFormat.ToolTip"));
            this.textBoxNoSeriesFormat.TextChanged += new System.EventHandler(this.textBoxNoSeriesFormat_TextChanged);
            // 
            // labelNoSeries
            // 
            resources.ApplyResources(this.labelNoSeries, "labelNoSeries");
            this.labelNoSeries.Name = "labelNoSeries";
            this.toolTipControl.SetToolTip(this.labelNoSeries, resources.GetString("labelNoSeries.ToolTip"));
            // 
            // checkBoxAddSequences
            // 
            resources.ApplyResources(this.checkBoxAddSequences, "checkBoxAddSequences");
            this.checkBoxAddSequences.Name = "checkBoxAddSequences";
            this.toolTipControl.SetToolTip(this.checkBoxAddSequences, resources.GetString("checkBoxAddSequences.ToolTip"));
            this.checkBoxAddSequences.UseVisualStyleBackColor = true;
            this.checkBoxAddSequences.CheckedChanged += new System.EventHandler(this.checkBoxAddSequences_CheckedChanged);
            // 
            // textBoxNoSequenceFormat
            // 
            resources.ApplyResources(this.textBoxNoSequenceFormat, "textBoxNoSequenceFormat");
            this.textBoxNoSequenceFormat.Name = "textBoxNoSequenceFormat";
            this.toolTipControl.SetToolTip(this.textBoxNoSequenceFormat, resources.GetString("textBoxNoSequenceFormat.ToolTip"));
            this.textBoxNoSequenceFormat.TextChanged += new System.EventHandler(this.textBoxNoSequenceFormat_TextChanged);
            // 
            // labelNoSeqFormat
            // 
            resources.ApplyResources(this.labelNoSeqFormat, "labelNoSeqFormat");
            this.labelNoSeqFormat.Name = "labelNoSeqFormat";
            this.toolTipControl.SetToolTip(this.labelNoSeqFormat, resources.GetString("labelNoSeqFormat.ToolTip"));
            // 
            // textBoxSequenceFormat
            // 
            resources.ApplyResources(this.textBoxSequenceFormat, "textBoxSequenceFormat");
            this.textBoxSequenceFormat.Name = "textBoxSequenceFormat";
            this.toolTipControl.SetToolTip(this.textBoxSequenceFormat, resources.GetString("textBoxSequenceFormat.ToolTip"));
            this.textBoxSequenceFormat.TextChanged += new System.EventHandler(this.textBoxSequenceFormat_TextChanged);
            // 
            // labelSeqFormat
            // 
            resources.ApplyResources(this.labelSeqFormat, "labelSeqFormat");
            this.labelSeqFormat.Name = "labelSeqFormat";
            this.toolTipControl.SetToolTip(this.labelSeqFormat, resources.GetString("labelSeqFormat.ToolTip"));
            // 
            // textBoxFileAsFormat
            // 
            resources.ApplyResources(this.textBoxFileAsFormat, "textBoxFileAsFormat");
            this.textBoxFileAsFormat.Name = "textBoxFileAsFormat";
            this.toolTipControl.SetToolTip(this.textBoxFileAsFormat, resources.GetString("textBoxFileAsFormat.ToolTip"));
            this.textBoxFileAsFormat.TextChanged += new System.EventHandler(this.textBoxFileAsFormat_TextChanged);
            // 
            // labelFileAsFormat
            // 
            resources.ApplyResources(this.labelFileAsFormat, "labelFileAsFormat");
            this.labelFileAsFormat.Name = "labelFileAsFormat";
            this.toolTipControl.SetToolTip(this.labelFileAsFormat, resources.GetString("labelFileAsFormat.ToolTip"));
            // 
            // textBoxAuthorFormat
            // 
            resources.ApplyResources(this.textBoxAuthorFormat, "textBoxAuthorFormat");
            this.textBoxAuthorFormat.Name = "textBoxAuthorFormat";
            this.toolTipControl.SetToolTip(this.textBoxAuthorFormat, resources.GetString("textBoxAuthorFormat.ToolTip"));
            this.textBoxAuthorFormat.TextChanged += new System.EventHandler(this.textBoxAuthorFormat_TextChanged);
            // 
            // labelAuthorFormat
            // 
            resources.ApplyResources(this.labelAuthorFormat, "labelAuthorFormat");
            this.labelAuthorFormat.Name = "labelAuthorFormat";
            this.toolTipControl.SetToolTip(this.labelAuthorFormat, resources.GetString("labelAuthorFormat.ToolTip"));
            // 
            // comboBoxFixMode
            // 
            resources.ApplyResources(this.comboBoxFixMode, "comboBoxFixMode");
            this.comboBoxFixMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFixMode.FormattingEnabled = true;
            this.comboBoxFixMode.Name = "comboBoxFixMode";
            this.toolTipControl.SetToolTip(this.comboBoxFixMode, resources.GetString("comboBoxFixMode.ToolTip"));
            this.comboBoxFixMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFixMode_SelectedIndexChanged);
            // 
            // labelFixMode
            // 
            resources.ApplyResources(this.labelFixMode, "labelFixMode");
            this.labelFixMode.Name = "labelFixMode";
            this.toolTipControl.SetToolTip(this.labelFixMode, resources.GetString("labelFixMode.ToolTip"));
            // 
            // checkBoxSkipAboutPage
            // 
            resources.ApplyResources(this.checkBoxSkipAboutPage, "checkBoxSkipAboutPage");
            this.checkBoxSkipAboutPage.Name = "checkBoxSkipAboutPage";
            this.toolTipControl.SetToolTip(this.checkBoxSkipAboutPage, resources.GetString("checkBoxSkipAboutPage.ToolTip"));
            this.checkBoxSkipAboutPage.UseVisualStyleBackColor = true;
            this.checkBoxSkipAboutPage.CheckedChanged += new System.EventHandler(this.checkBoxSkipAboutPage_CheckedChanged);
            // 
            // checkBoxCapitalize
            // 
            resources.ApplyResources(this.checkBoxCapitalize, "checkBoxCapitalize");
            this.checkBoxCapitalize.Name = "checkBoxCapitalize";
            this.toolTipControl.SetToolTip(this.checkBoxCapitalize, resources.GetString("checkBoxCapitalize.ToolTip"));
            this.checkBoxCapitalize.UseVisualStyleBackColor = true;
            this.checkBoxCapitalize.CheckedChanged += new System.EventHandler(this.checkBoxCapitalize_CheckedChanged);
            // 
            // checkBoxEmbedStyles
            // 
            resources.ApplyResources(this.checkBoxEmbedStyles, "checkBoxEmbedStyles");
            this.checkBoxEmbedStyles.Name = "checkBoxEmbedStyles";
            this.toolTipControl.SetToolTip(this.checkBoxEmbedStyles, resources.GetString("checkBoxEmbedStyles.ToolTip"));
            this.checkBoxEmbedStyles.UseVisualStyleBackColor = true;
            this.checkBoxEmbedStyles.CheckedChanged += new System.EventHandler(this.checkBoxEmbedStyles_CheckedChanged);
            // 
            // checkBoxFlatStructure
            // 
            resources.ApplyResources(this.checkBoxFlatStructure, "checkBoxFlatStructure");
            this.checkBoxFlatStructure.Name = "checkBoxFlatStructure";
            this.toolTipControl.SetToolTip(this.checkBoxFlatStructure, resources.GetString("checkBoxFlatStructure.ToolTip"));
            this.checkBoxFlatStructure.UseVisualStyleBackColor = true;
            this.checkBoxFlatStructure.CheckedChanged += new System.EventHandler(this.checkBoxFlatStructure_CheckedChanged);
            // 
            // checkBoxConvertAlphaPNG
            // 
            resources.ApplyResources(this.checkBoxConvertAlphaPNG, "checkBoxConvertAlphaPNG");
            this.checkBoxConvertAlphaPNG.Name = "checkBoxConvertAlphaPNG";
            this.toolTipControl.SetToolTip(this.checkBoxConvertAlphaPNG, resources.GetString("checkBoxConvertAlphaPNG.ToolTip"));
            this.checkBoxConvertAlphaPNG.UseVisualStyleBackColor = true;
            this.checkBoxConvertAlphaPNG.CheckedChanged += new System.EventHandler(this.checkBoxConvertAlphaPNG_CheckedChanged);
            // 
            // checkBoxFb2Info
            // 
            resources.ApplyResources(this.checkBoxFb2Info, "checkBoxFb2Info");
            this.checkBoxFb2Info.Name = "checkBoxFb2Info";
            this.toolTipControl.SetToolTip(this.checkBoxFb2Info, resources.GetString("checkBoxFb2Info.ToolTip"));
            this.checkBoxFb2Info.UseVisualStyleBackColor = true;
            this.checkBoxFb2Info.CheckedChanged += new System.EventHandler(this.checkBoxFb2Info_CheckedChanged);
            // 
            // comboBoxSDValue
            // 
            resources.ApplyResources(this.comboBoxSDValue, "comboBoxSDValue");
            this.comboBoxSDValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSDValue.FormattingEnabled = true;
            this.comboBoxSDValue.Name = "comboBoxSDValue";
            this.toolTipControl.SetToolTip(this.comboBoxSDValue, resources.GetString("comboBoxSDValue.ToolTip"));
            this.comboBoxSDValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxSDValue_SelectedIndexChanged);
            // 
            // radioButtonSDEnabled
            // 
            resources.ApplyResources(this.radioButtonSDEnabled, "radioButtonSDEnabled");
            this.radioButtonSDEnabled.Name = "radioButtonSDEnabled";
            this.radioButtonSDEnabled.TabStop = true;
            this.toolTipControl.SetToolTip(this.radioButtonSDEnabled, resources.GetString("radioButtonSDEnabled.ToolTip"));
            this.radioButtonSDEnabled.UseVisualStyleBackColor = true;
            this.radioButtonSDEnabled.CheckedChanged += new System.EventHandler(this.radioButtonSDEnabled_CheckedChanged);
            // 
            // radioButtonSDDisabled
            // 
            resources.ApplyResources(this.radioButtonSDDisabled, "radioButtonSDDisabled");
            this.radioButtonSDDisabled.Name = "radioButtonSDDisabled";
            this.radioButtonSDDisabled.TabStop = true;
            this.toolTipControl.SetToolTip(this.radioButtonSDDisabled, resources.GetString("radioButtonSDDisabled.ToolTip"));
            this.radioButtonSDDisabled.UseVisualStyleBackColor = true;
            this.radioButtonSDDisabled.CheckedChanged += new System.EventHandler(this.radioButtonSDDisabled_CheckedChanged);
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.Name = "buttonEdit";
            this.toolTipControl.SetToolTip(this.buttonEdit, resources.GetString("buttonEdit.ToolTip"));
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonPathNew
            // 
            resources.ApplyResources(this.buttonPathNew, "buttonPathNew");
            this.buttonPathNew.Name = "buttonPathNew";
            this.toolTipControl.SetToolTip(this.buttonPathNew, resources.GetString("buttonPathNew.ToolTip"));
            this.buttonPathNew.UseVisualStyleBackColor = true;
            this.buttonPathNew.Click += new System.EventHandler(this.buttonPathNew_Click);
            // 
            // checkBoxVisibleInGUI
            // 
            resources.ApplyResources(this.checkBoxVisibleInGUI, "checkBoxVisibleInGUI");
            this.checkBoxVisibleInGUI.Name = "checkBoxVisibleInGUI";
            this.toolTipControl.SetToolTip(this.checkBoxVisibleInGUI, resources.GetString("checkBoxVisibleInGUI.ToolTip"));
            this.checkBoxVisibleInGUI.UseVisualStyleBackColor = true;
            this.checkBoxVisibleInGUI.CheckedChanged += new System.EventHandler(this.checkBoxVisibleInGUI_CheckedChanged);
            // 
            // checkBoxVisibleInExt
            // 
            resources.ApplyResources(this.checkBoxVisibleInExt, "checkBoxVisibleInExt");
            this.checkBoxVisibleInExt.Name = "checkBoxVisibleInExt";
            this.toolTipControl.SetToolTip(this.checkBoxVisibleInExt, resources.GetString("checkBoxVisibleInExt.ToolTip"));
            this.checkBoxVisibleInExt.UseVisualStyleBackColor = true;
            this.checkBoxVisibleInExt.CheckedChanged += new System.EventHandler(this.checkBoxVisibleInExt_CheckedChanged);
            // 
            // buttonDownPath
            // 
            resources.ApplyResources(this.buttonDownPath, "buttonDownPath");
            this.buttonDownPath.Name = "buttonDownPath";
            this.toolTipControl.SetToolTip(this.buttonDownPath, resources.GetString("buttonDownPath.ToolTip"));
            this.buttonDownPath.UseVisualStyleBackColor = true;
            this.buttonDownPath.Click += new System.EventHandler(this.buttonDownPath_Click);
            // 
            // buttonUpPath
            // 
            resources.ApplyResources(this.buttonUpPath, "buttonUpPath");
            this.buttonUpPath.Name = "buttonUpPath";
            this.toolTipControl.SetToolTip(this.buttonUpPath, resources.GetString("buttonUpPath.ToolTip"));
            this.buttonUpPath.UseVisualStyleBackColor = true;
            this.buttonUpPath.Click += new System.EventHandler(this.buttonUpPath_Click);
            // 
            // buttonDeletePath
            // 
            resources.ApplyResources(this.buttonDeletePath, "buttonDeletePath");
            this.buttonDeletePath.Name = "buttonDeletePath";
            this.toolTipControl.SetToolTip(this.buttonDeletePath, resources.GetString("buttonDeletePath.ToolTip"));
            this.buttonDeletePath.UseVisualStyleBackColor = true;
            this.buttonDeletePath.Click += new System.EventHandler(this.buttonDeletePath_Click);
            // 
            // buttonBrowseForXPGT
            // 
            resources.ApplyResources(this.buttonBrowseForXPGT, "buttonBrowseForXPGT");
            this.buttonBrowseForXPGT.Name = "buttonBrowseForXPGT";
            this.toolTipControl.SetToolTip(this.buttonBrowseForXPGT, resources.GetString("buttonBrowseForXPGT.ToolTip"));
            this.buttonBrowseForXPGT.UseVisualStyleBackColor = true;
            this.buttonBrowseForXPGT.Click += new System.EventHandler(this.buttonBrowseForXPGT_Click);
            // 
            // buttonXPGTClear
            // 
            resources.ApplyResources(this.buttonXPGTClear, "buttonXPGTClear");
            this.buttonXPGTClear.Name = "buttonXPGTClear";
            this.toolTipControl.SetToolTip(this.buttonXPGTClear, resources.GetString("buttonXPGTClear.ToolTip"));
            this.buttonXPGTClear.UseVisualStyleBackColor = true;
            this.buttonXPGTClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxTemplatePath
            // 
            resources.ApplyResources(this.textBoxTemplatePath, "textBoxTemplatePath");
            this.textBoxTemplatePath.Name = "textBoxTemplatePath";
            this.textBoxTemplatePath.ReadOnly = true;
            this.toolTipControl.SetToolTip(this.textBoxTemplatePath, resources.GetString("textBoxTemplatePath.ToolTip"));
            // 
            // checkBoxUseXPGT
            // 
            resources.ApplyResources(this.checkBoxUseXPGT, "checkBoxUseXPGT");
            this.checkBoxUseXPGT.Name = "checkBoxUseXPGT";
            this.toolTipControl.SetToolTip(this.checkBoxUseXPGT, resources.GetString("checkBoxUseXPGT.ToolTip"));
            this.checkBoxUseXPGT.UseVisualStyleBackColor = true;
            this.checkBoxUseXPGT.CheckedChanged += new System.EventHandler(this.checkBoxUseXPGT_CheckedChanged);
            // 
            // buttonRemoveFont
            // 
            resources.ApplyResources(this.buttonRemoveFont, "buttonRemoveFont");
            this.buttonRemoveFont.Name = "buttonRemoveFont";
            this.toolTipControl.SetToolTip(this.buttonRemoveFont, resources.GetString("buttonRemoveFont.ToolTip"));
            this.buttonRemoveFont.UseVisualStyleBackColor = true;
            this.buttonRemoveFont.Click += new System.EventHandler(this.buttonRemoveFont_Click);
            // 
            // buttonEditFont
            // 
            resources.ApplyResources(this.buttonEditFont, "buttonEditFont");
            this.buttonEditFont.Name = "buttonEditFont";
            this.toolTipControl.SetToolTip(this.buttonEditFont, resources.GetString("buttonEditFont.ToolTip"));
            this.buttonEditFont.UseVisualStyleBackColor = true;
            this.buttonEditFont.Click += new System.EventHandler(this.buttonEditFont_Click);
            // 
            // buttonAddFont
            // 
            resources.ApplyResources(this.buttonAddFont, "buttonAddFont");
            this.buttonAddFont.Name = "buttonAddFont";
            this.toolTipControl.SetToolTip(this.buttonAddFont, resources.GetString("buttonAddFont.ToolTip"));
            this.buttonAddFont.UseVisualStyleBackColor = true;
            this.buttonAddFont.Click += new System.EventHandler(this.buttonAddFont_Click);
            // 
            // listViewFonts
            // 
            resources.ApplyResources(this.listViewFonts, "listViewFonts");
            this.listViewFonts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFont});
            this.listViewFonts.FullRowSelect = true;
            this.listViewFonts.MultiSelect = false;
            this.listViewFonts.Name = "listViewFonts";
            this.toolTipControl.SetToolTip(this.listViewFonts, resources.GetString("listViewFonts.ToolTip"));
            this.listViewFonts.UseCompatibleStateImageBehavior = false;
            this.listViewFonts.View = System.Windows.Forms.View.Details;
            this.listViewFonts.SelectedIndexChanged += new System.EventHandler(this.listViewFonts_SelectedIndexChanged);
            // 
            // columnHeaderFont
            // 
            resources.ApplyResources(this.columnHeaderFont, "columnHeaderFont");
            // 
            // listBoxCSSFonts
            // 
            resources.ApplyResources(this.listBoxCSSFonts, "listBoxCSSFonts");
            this.listBoxCSSFonts.FormattingEnabled = true;
            this.listBoxCSSFonts.Name = "listBoxCSSFonts";
            this.toolTipControl.SetToolTip(this.listBoxCSSFonts, resources.GetString("listBoxCSSFonts.ToolTip"));
            // 
            // buttonDeleteCSSFont
            // 
            resources.ApplyResources(this.buttonDeleteCSSFont, "buttonDeleteCSSFont");
            this.buttonDeleteCSSFont.Name = "buttonDeleteCSSFont";
            this.toolTipControl.SetToolTip(this.buttonDeleteCSSFont, resources.GetString("buttonDeleteCSSFont.ToolTip"));
            this.buttonDeleteCSSFont.UseVisualStyleBackColor = true;
            this.buttonDeleteCSSFont.Click += new System.EventHandler(this.buttonDeleteCSSFont_Click);
            // 
            // buttonAddCSSFont
            // 
            resources.ApplyResources(this.buttonAddCSSFont, "buttonAddCSSFont");
            this.buttonAddCSSFont.Name = "buttonAddCSSFont";
            this.toolTipControl.SetToolTip(this.buttonAddCSSFont, resources.GetString("buttonAddCSSFont.ToolTip"));
            this.buttonAddCSSFont.UseVisualStyleBackColor = true;
            this.buttonAddCSSFont.Click += new System.EventHandler(this.buttonAddCSSFont_Click);
            // 
            // listBoxCSSElements
            // 
            resources.ApplyResources(this.listBoxCSSElements, "listBoxCSSElements");
            this.listBoxCSSElements.FormattingEnabled = true;
            this.listBoxCSSElements.Name = "listBoxCSSElements";
            this.toolTipControl.SetToolTip(this.listBoxCSSElements, resources.GetString("listBoxCSSElements.ToolTip"));
            // 
            // buttonRemoveCSS
            // 
            resources.ApplyResources(this.buttonRemoveCSS, "buttonRemoveCSS");
            this.buttonRemoveCSS.Name = "buttonRemoveCSS";
            this.toolTipControl.SetToolTip(this.buttonRemoveCSS, resources.GetString("buttonRemoveCSS.ToolTip"));
            this.buttonRemoveCSS.UseVisualStyleBackColor = true;
            this.buttonRemoveCSS.Click += new System.EventHandler(this.buttonRemoveCSS_Click);
            // 
            // buttonAddCSS
            // 
            resources.ApplyResources(this.buttonAddCSS, "buttonAddCSS");
            this.buttonAddCSS.Name = "buttonAddCSS";
            this.toolTipControl.SetToolTip(this.buttonAddCSS, resources.GetString("buttonAddCSS.ToolTip"));
            this.buttonAddCSS.UseVisualStyleBackColor = true;
            this.buttonAddCSS.Click += new System.EventHandler(this.buttonAddCSS_Click);
            // 
            // groupBoxAssignedFonts
            // 
            resources.ApplyResources(this.groupBoxAssignedFonts, "groupBoxAssignedFonts");
            this.groupBoxAssignedFonts.Controls.Add(this.listBoxCSSFonts);
            this.groupBoxAssignedFonts.Controls.Add(this.buttonDeleteCSSFont);
            this.groupBoxAssignedFonts.Controls.Add(this.buttonAddCSSFont);
            this.groupBoxAssignedFonts.Name = "groupBoxAssignedFonts";
            this.groupBoxAssignedFonts.TabStop = false;
            this.toolTipControl.SetToolTip(this.groupBoxAssignedFonts, resources.GetString("groupBoxAssignedFonts.ToolTip"));
            // 
            // groupBoxCSS
            // 
            resources.ApplyResources(this.groupBoxCSS, "groupBoxCSS");
            this.groupBoxCSS.Controls.Add(this.listBoxCSSElements);
            this.groupBoxCSS.Controls.Add(this.buttonRemoveCSS);
            this.groupBoxCSS.Controls.Add(this.buttonAddCSS);
            this.groupBoxCSS.Name = "groupBoxCSS";
            this.groupBoxCSS.TabStop = false;
            this.toolTipControl.SetToolTip(this.groupBoxCSS, resources.GetString("groupBoxCSS.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTipControl.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // comboBoxIgnoreTitle
            // 
            resources.ApplyResources(this.comboBoxIgnoreTitle, "comboBoxIgnoreTitle");
            this.comboBoxIgnoreTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIgnoreTitle.FormattingEnabled = true;
            this.comboBoxIgnoreTitle.Items.AddRange(new object[] {
            resources.GetString("comboBoxIgnoreTitle.Items"),
            resources.GetString("comboBoxIgnoreTitle.Items1"),
            resources.GetString("comboBoxIgnoreTitle.Items2"),
            resources.GetString("comboBoxIgnoreTitle.Items3"),
            resources.GetString("comboBoxIgnoreTitle.Items4"),
            resources.GetString("comboBoxIgnoreTitle.Items5"),
            resources.GetString("comboBoxIgnoreTitle.Items6")});
            this.comboBoxIgnoreTitle.Name = "comboBoxIgnoreTitle";
            this.toolTipControl.SetToolTip(this.comboBoxIgnoreTitle, resources.GetString("comboBoxIgnoreTitle.ToolTip"));
            this.comboBoxIgnoreTitle.SelectedIndexChanged += new System.EventHandler(this.comboBoxIgnoreTitle_SelectedIndexChanged);
            // 
            // buttonImportExport
            // 
            resources.ApplyResources(this.buttonImportExport, "buttonImportExport");
            this.buttonImportExport.Name = "buttonImportExport";
            this.toolTipControl.SetToolTip(this.buttonImportExport, resources.GetString("buttonImportExport.ToolTip"));
            this.buttonImportExport.UseVisualStyleBackColor = true;
            this.buttonImportExport.Click += new System.EventHandler(this.buttonImportExport_Click);
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageTransliteration);
            this.tabControlSettings.Controls.Add(this.tabPageSequences);
            this.tabControlSettings.Controls.Add(this.tabPageDifferent);
            this.tabControlSettings.Controls.Add(this.tabPageFonts);
            this.tabControlSettings.Controls.Add(this.tabPagePaths);
            this.tabControlSettings.Controls.Add(this.tabPageXPGT);
            this.tabControlSettings.Controls.Add(this.tabPageCSSElements);
            this.tabControlSettings.Multiline = true;
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.toolTipControl.SetToolTip(this.tabControlSettings, resources.GetString("tabControlSettings.ToolTip"));
            // 
            // tabPageTransliteration
            // 
            resources.ApplyResources(this.tabPageTransliteration, "tabPageTransliteration");
            this.tabPageTransliteration.Controls.Add(this.groupBox1);
            this.tabPageTransliteration.Name = "tabPageTransliteration";
            this.toolTipControl.SetToolTip(this.tabPageTransliteration, resources.GetString("tabPageTransliteration.ToolTip"));
            this.tabPageTransliteration.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.checkBoxTransliterateAdditional);
            this.groupBox1.Controls.Add(this.checkBoxTransliterateFileName);
            this.groupBox1.Controls.Add(this.checkBoxTransliterateTOC);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTipControl.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // tabPageSequences
            // 
            resources.ApplyResources(this.tabPageSequences, "tabPageSequences");
            this.tabPageSequences.Controls.Add(this.groupBoxSequences);
            this.tabPageSequences.Controls.Add(this.textBoxFileAsFormat);
            this.tabPageSequences.Controls.Add(this.labelFileAsFormat);
            this.tabPageSequences.Controls.Add(this.textBoxAuthorFormat);
            this.tabPageSequences.Controls.Add(this.labelAuthorFormat);
            this.tabPageSequences.Name = "tabPageSequences";
            this.toolTipControl.SetToolTip(this.tabPageSequences, resources.GetString("tabPageSequences.ToolTip"));
            this.tabPageSequences.UseVisualStyleBackColor = true;
            // 
            // groupBoxSequences
            // 
            resources.ApplyResources(this.groupBoxSequences, "groupBoxSequences");
            this.groupBoxSequences.Controls.Add(this.textBoxNoSeriesFormat);
            this.groupBoxSequences.Controls.Add(this.labelNoSeries);
            this.groupBoxSequences.Controls.Add(this.checkBoxAddSequences);
            this.groupBoxSequences.Controls.Add(this.textBoxNoSequenceFormat);
            this.groupBoxSequences.Controls.Add(this.labelNoSeqFormat);
            this.groupBoxSequences.Controls.Add(this.textBoxSequenceFormat);
            this.groupBoxSequences.Controls.Add(this.labelSeqFormat);
            this.groupBoxSequences.Name = "groupBoxSequences";
            this.groupBoxSequences.TabStop = false;
            this.toolTipControl.SetToolTip(this.groupBoxSequences, resources.GetString("groupBoxSequences.ToolTip"));
            // 
            // tabPageDifferent
            // 
            resources.ApplyResources(this.tabPageDifferent, "tabPageDifferent");
            this.tabPageDifferent.Controls.Add(this.comboBoxIgnoreTitle);
            this.tabPageDifferent.Controls.Add(this.label2);
            this.tabPageDifferent.Controls.Add(this.comboBoxFixMode);
            this.tabPageDifferent.Controls.Add(this.labelFixMode);
            this.tabPageDifferent.Controls.Add(this.checkBoxSkipAboutPage);
            this.tabPageDifferent.Controls.Add(this.checkBoxCapitalize);
            this.tabPageDifferent.Controls.Add(this.checkBoxEmbedStyles);
            this.tabPageDifferent.Controls.Add(this.checkBoxFlatStructure);
            this.tabPageDifferent.Controls.Add(this.checkBoxConvertAlphaPNG);
            this.tabPageDifferent.Controls.Add(this.checkBoxFb2Info);
            this.tabPageDifferent.Name = "tabPageDifferent";
            this.toolTipControl.SetToolTip(this.tabPageDifferent, resources.GetString("tabPageDifferent.ToolTip"));
            this.tabPageDifferent.UseVisualStyleBackColor = true;
            // 
            // tabPageFonts
            // 
            resources.ApplyResources(this.tabPageFonts, "tabPageFonts");
            this.tabPageFonts.Controls.Add(this.buttonRemoveFont);
            this.tabPageFonts.Controls.Add(this.buttonEditFont);
            this.tabPageFonts.Controls.Add(this.buttonAddFont);
            this.tabPageFonts.Controls.Add(this.listViewFonts);
            this.tabPageFonts.Name = "tabPageFonts";
            this.toolTipControl.SetToolTip(this.tabPageFonts, resources.GetString("tabPageFonts.ToolTip"));
            this.tabPageFonts.UseVisualStyleBackColor = true;
            this.tabPageFonts.Click += new System.EventHandler(this.tabPageFonts_Click);
            // 
            // tabPagePaths
            // 
            resources.ApplyResources(this.tabPagePaths, "tabPagePaths");
            this.tabPagePaths.Controls.Add(this.groupBox2);
            this.tabPagePaths.Controls.Add(this.buttonEdit);
            this.tabPagePaths.Controls.Add(this.buttonPathNew);
            this.tabPagePaths.Controls.Add(this.checkBoxVisibleInGUI);
            this.tabPagePaths.Controls.Add(this.checkBoxVisibleInExt);
            this.tabPagePaths.Controls.Add(this.buttonDownPath);
            this.tabPagePaths.Controls.Add(this.buttonUpPath);
            this.tabPagePaths.Controls.Add(this.buttonDeletePath);
            this.tabPagePaths.Controls.Add(this.listBoxPaths);
            this.tabPagePaths.Name = "tabPagePaths";
            this.toolTipControl.SetToolTip(this.tabPagePaths, resources.GetString("tabPagePaths.ToolTip"));
            this.tabPagePaths.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.comboBoxSDValue);
            this.groupBox2.Controls.Add(this.radioButtonSDEnabled);
            this.groupBox2.Controls.Add(this.radioButtonSDDisabled);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTipControl.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // listBoxPaths
            // 
            resources.ApplyResources(this.listBoxPaths, "listBoxPaths");
            this.listBoxPaths.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxPaths.FormattingEnabled = true;
            this.listBoxPaths.Name = "listBoxPaths";
            this.toolTipControl.SetToolTip(this.listBoxPaths, resources.GetString("listBoxPaths.ToolTip"));
            this.listBoxPaths.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxPaths_DrawItem);
            this.listBoxPaths.SelectedIndexChanged += new System.EventHandler(this.listBoxPaths_SelectedIndexChanged);
            // 
            // tabPageXPGT
            // 
            resources.ApplyResources(this.tabPageXPGT, "tabPageXPGT");
            this.tabPageXPGT.Controls.Add(this.groupBoxXPGTPath);
            this.tabPageXPGT.Controls.Add(this.checkBoxUseXPGT);
            this.tabPageXPGT.Name = "tabPageXPGT";
            this.toolTipControl.SetToolTip(this.tabPageXPGT, resources.GetString("tabPageXPGT.ToolTip"));
            this.tabPageXPGT.UseVisualStyleBackColor = true;
            // 
            // groupBoxXPGTPath
            // 
            resources.ApplyResources(this.groupBoxXPGTPath, "groupBoxXPGTPath");
            this.groupBoxXPGTPath.Controls.Add(this.buttonBrowseForXPGT);
            this.groupBoxXPGTPath.Controls.Add(this.buttonXPGTClear);
            this.groupBoxXPGTPath.Controls.Add(this.textBoxTemplatePath);
            this.groupBoxXPGTPath.Controls.Add(this.label1);
            this.groupBoxXPGTPath.Name = "groupBoxXPGTPath";
            this.groupBoxXPGTPath.TabStop = false;
            this.toolTipControl.SetToolTip(this.groupBoxXPGTPath, resources.GetString("groupBoxXPGTPath.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTipControl.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // tabPageCSSElements
            // 
            resources.ApplyResources(this.tabPageCSSElements, "tabPageCSSElements");
            this.tabPageCSSElements.Controls.Add(this.groupBoxAssignedFonts);
            this.tabPageCSSElements.Controls.Add(this.groupBoxCSS);
            this.tabPageCSSElements.Name = "tabPageCSSElements";
            this.toolTipControl.SetToolTip(this.tabPageCSSElements, resources.GetString("tabPageCSSElements.ToolTip"));
            this.tabPageCSSElements.UseVisualStyleBackColor = true;
            // 
            // ConverterSettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonImportExport);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConverterSettingsForm";
            this.ShowInTaskbar = false;
            this.toolTipControl.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ConverterSettingsForm_Load);
            this.groupBoxAssignedFonts.ResumeLayout(false);
            this.groupBoxCSS.ResumeLayout(false);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageTransliteration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageSequences.ResumeLayout(false);
            this.tabPageSequences.PerformLayout();
            this.groupBoxSequences.ResumeLayout(false);
            this.groupBoxSequences.PerformLayout();
            this.tabPageDifferent.ResumeLayout(false);
            this.tabPageDifferent.PerformLayout();
            this.tabPageFonts.ResumeLayout(false);
            this.tabPagePaths.ResumeLayout(false);
            this.tabPagePaths.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageXPGT.ResumeLayout(false);
            this.tabPageXPGT.PerformLayout();
            this.groupBoxXPGTPath.ResumeLayout(false);
            this.groupBoxXPGTPath.PerformLayout();
            this.tabPageCSSElements.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageTransliteration;
        private System.Windows.Forms.TabPage tabPageSequences;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxTransliterateTOC;
        private System.Windows.Forms.CheckBox checkBoxTransliterateFileName;
        private System.Windows.Forms.CheckBox checkBoxTransliterateAdditional;
        private System.Windows.Forms.TextBox textBoxFileAsFormat;
        private System.Windows.Forms.Label labelFileAsFormat;
        private System.Windows.Forms.TextBox textBoxAuthorFormat;
        private System.Windows.Forms.Label labelAuthorFormat;
        private System.Windows.Forms.TabPage tabPageDifferent;
        private System.Windows.Forms.TabPage tabPageFonts;
        private System.Windows.Forms.CheckBox checkBoxConvertAlphaPNG;
        private System.Windows.Forms.CheckBox checkBoxFb2Info;
        private System.Windows.Forms.CheckBox checkBoxFlatStructure;
        private System.Windows.Forms.CheckBox checkBoxEmbedStyles;
        private System.Windows.Forms.CheckBox checkBoxCapitalize;
        private System.Windows.Forms.CheckBox checkBoxSkipAboutPage;
        private System.Windows.Forms.ComboBox comboBoxFixMode;
        private System.Windows.Forms.Label labelFixMode;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.GroupBox groupBoxSequences;
        private System.Windows.Forms.CheckBox checkBoxAddSequences;
        private System.Windows.Forms.TextBox textBoxNoSequenceFormat;
        private System.Windows.Forms.Label labelNoSeqFormat;
        private System.Windows.Forms.TextBox textBoxSequenceFormat;
        private System.Windows.Forms.Label labelSeqFormat;
        private System.Windows.Forms.TextBox textBoxNoSeriesFormat;
        private System.Windows.Forms.Label labelNoSeries;
        private System.Windows.Forms.TabPage tabPagePaths;
        private System.Windows.Forms.Button buttonDownPath;
        private System.Windows.Forms.Button buttonUpPath;
        private System.Windows.Forms.Button buttonDeletePath;
        private System.Windows.Forms.ListBox listBoxPaths;
        private System.Windows.Forms.CheckBox checkBoxVisibleInGUI;
        private System.Windows.Forms.CheckBox checkBoxVisibleInExt;
        private System.Windows.Forms.Button buttonPathNew;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonSDEnabled;
        private System.Windows.Forms.RadioButton radioButtonSDDisabled;
        private System.Windows.Forms.ComboBox comboBoxSDValue;
        private System.Windows.Forms.TabPage tabPageXPGT;
        private NonStyledTabs tabControlSettings;
        private System.Windows.Forms.CheckBox checkBoxUseXPGT;
        private System.Windows.Forms.GroupBox groupBoxXPGTPath;
        private System.Windows.Forms.Button buttonBrowseForXPGT;
        private System.Windows.Forms.Button buttonXPGTClear;
        private System.Windows.Forms.TextBox textBoxTemplatePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewFonts;
        private System.Windows.Forms.Button buttonRemoveFont;
        private System.Windows.Forms.Button buttonEditFont;
        private System.Windows.Forms.Button buttonAddFont;
        private System.Windows.Forms.ColumnHeader columnHeaderFont;
        private System.Windows.Forms.TabPage tabPageCSSElements;
        private System.Windows.Forms.GroupBox groupBoxAssignedFonts;
        private System.Windows.Forms.GroupBox groupBoxCSS;
        private System.Windows.Forms.Button buttonRemoveCSS;
        private System.Windows.Forms.Button buttonAddCSS;
        private System.Windows.Forms.Button buttonDeleteCSSFont;
        private System.Windows.Forms.Button buttonAddCSSFont;
        private System.Windows.Forms.ListBox listBoxCSSFonts;
        private System.Windows.Forms.ListBox listBoxCSSElements;
        private System.Windows.Forms.ComboBox comboBoxIgnoreTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonImportExport;



    }
}