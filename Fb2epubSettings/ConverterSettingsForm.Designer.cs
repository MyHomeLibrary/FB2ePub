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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageTransliteration = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTransliterateAdditional = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateFileName = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateTOC = new System.Windows.Forms.CheckBox();
            this.tabPageSequences = new System.Windows.Forms.TabPage();
            this.groupBoxSequences = new System.Windows.Forms.GroupBox();
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
            this.tabPageDifferent = new System.Windows.Forms.TabPage();
            this.comboBoxFixMode = new System.Windows.Forms.ComboBox();
            this.labelFixMode = new System.Windows.Forms.Label();
            this.checkBoxSkipAboutPage = new System.Windows.Forms.CheckBox();
            this.checkBoxCapitalize = new System.Windows.Forms.CheckBox();
            this.checkBoxEmbedStyles = new System.Windows.Forms.CheckBox();
            this.checkBoxFlatStructure = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertAlphaPNG = new System.Windows.Forms.CheckBox();
            this.checkBoxFb2Info = new System.Windows.Forms.CheckBox();
            this.tabPageFonts = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlSettings.SuspendLayout();
            this.tabPageTransliteration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageSequences.SuspendLayout();
            this.groupBoxSequences.SuspendLayout();
            this.tabPageDifferent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageTransliteration);
            this.tabControlSettings.Controls.Add(this.tabPageSequences);
            this.tabControlSettings.Controls.Add(this.tabPageDifferent);
            this.tabControlSettings.Controls.Add(this.tabPageFonts);
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
            // tabPageDifferent
            // 
            resources.ApplyResources(this.tabPageDifferent, "tabPageDifferent");
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
            // tabPageFonts
            // 
            resources.ApplyResources(this.tabPageFonts, "tabPageFonts");
            this.tabPageFonts.Name = "tabPageFonts";
            this.toolTipControl.SetToolTip(this.tabPageFonts, resources.GetString("tabPageFonts.ToolTip"));
            this.tabPageFonts.UseVisualStyleBackColor = true;
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
            // ConverterSettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConverterSettingsForm";
            this.toolTipControl.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ConverterSettingsForm_Load);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
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



    }
}