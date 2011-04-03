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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageTransliteration = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTransliterateAdditional = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateFileName = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateTOC = new System.Windows.Forms.CheckBox();
            this.tabPageSequences = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSequenceFormat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNoSequenceFormat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAuthorFormat = new System.Windows.Forms.TextBox();
            this.labelFileAsFormat = new System.Windows.Forms.Label();
            this.textBoxFileAsFormat = new System.Windows.Forms.TextBox();
            this.tabPageDifferent = new System.Windows.Forms.TabPage();
            this.tabPageFonts = new System.Windows.Forms.TabPage();
            this.checkBoxFb2Info = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertAlphaPNG = new System.Windows.Forms.CheckBox();
            this.checkBoxAddSequences = new System.Windows.Forms.CheckBox();
            this.checkBoxFlatStructure = new System.Windows.Forms.CheckBox();
            this.checkBoxEmbedStyles = new System.Windows.Forms.CheckBox();
            this.checkBoxCapitalize = new System.Windows.Forms.CheckBox();
            this.checkBoxSkipAboutPage = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxFixMode = new System.Windows.Forms.ComboBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageTransliteration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageSequences.SuspendLayout();
            this.tabPageDifferent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlSettings.Controls.Add(this.tabPageTransliteration);
            this.tabControlSettings.Controls.Add(this.tabPageSequences);
            this.tabControlSettings.Controls.Add(this.tabPageDifferent);
            this.tabControlSettings.Controls.Add(this.tabPageFonts);
            this.tabControlSettings.Location = new System.Drawing.Point(12, 12);
            this.tabControlSettings.Multiline = true;
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(378, 211);
            this.tabControlSettings.TabIndex = 0;
            // 
            // tabPageTransliteration
            // 
            this.tabPageTransliteration.Controls.Add(this.groupBox1);
            this.tabPageTransliteration.Location = new System.Drawing.Point(23, 4);
            this.tabPageTransliteration.Name = "tabPageTransliteration";
            this.tabPageTransliteration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTransliteration.Size = new System.Drawing.Size(351, 203);
            this.tabPageTransliteration.TabIndex = 0;
            this.tabPageTransliteration.Text = "Transliteration";
            this.tabPageTransliteration.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTransliterateAdditional);
            this.groupBox1.Controls.Add(this.checkBoxTransliterateFileName);
            this.groupBox1.Controls.Add(this.checkBoxTransliterateTOC);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 197);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transliteration settings";
            // 
            // checkBoxTransliterateAdditional
            // 
            this.checkBoxTransliterateAdditional.AutoSize = true;
            this.checkBoxTransliterateAdditional.Location = new System.Drawing.Point(7, 75);
            this.checkBoxTransliterateAdditional.Name = "checkBoxTransliterateAdditional";
            this.checkBoxTransliterateAdditional.Size = new System.Drawing.Size(156, 17);
            this.checkBoxTransliterateAdditional.TabIndex = 2;
            this.checkBoxTransliterateAdditional.Text = "Transliterate additional data";
            this.checkBoxTransliterateAdditional.UseVisualStyleBackColor = true;
            this.checkBoxTransliterateAdditional.CheckedChanged += new System.EventHandler(this.checkBoxTransliterateAdditional_CheckedChanged);
            // 
            // checkBoxTransliterateFileName
            // 
            this.checkBoxTransliterateFileName.AutoSize = true;
            this.checkBoxTransliterateFileName.Location = new System.Drawing.Point(7, 52);
            this.checkBoxTransliterateFileName.Name = "checkBoxTransliterateFileName";
            this.checkBoxTransliterateFileName.Size = new System.Drawing.Size(129, 17);
            this.checkBoxTransliterateFileName.TabIndex = 1;
            this.checkBoxTransliterateFileName.Text = "Transliterate file name";
            this.checkBoxTransliterateFileName.UseVisualStyleBackColor = true;
            this.checkBoxTransliterateFileName.CheckedChanged += new System.EventHandler(this.checkBoxTransliterateFileName_CheckedChanged);
            // 
            // checkBoxTransliterateTOC
            // 
            this.checkBoxTransliterateTOC.AutoSize = true;
            this.checkBoxTransliterateTOC.Location = new System.Drawing.Point(7, 29);
            this.checkBoxTransliterateTOC.Name = "checkBoxTransliterateTOC";
            this.checkBoxTransliterateTOC.Size = new System.Drawing.Size(166, 17);
            this.checkBoxTransliterateTOC.TabIndex = 0;
            this.checkBoxTransliterateTOC.Text = "Transliterate Table of Content";
            this.checkBoxTransliterateTOC.UseVisualStyleBackColor = true;
            this.checkBoxTransliterateTOC.CheckedChanged += new System.EventHandler(this.checkBoxTransliterateTOC_CheckedChanged);
            // 
            // tabPageSequences
            // 
            this.tabPageSequences.Controls.Add(this.checkBoxAddSequences);
            this.tabPageSequences.Controls.Add(this.textBoxFileAsFormat);
            this.tabPageSequences.Controls.Add(this.labelFileAsFormat);
            this.tabPageSequences.Controls.Add(this.textBoxAuthorFormat);
            this.tabPageSequences.Controls.Add(this.label3);
            this.tabPageSequences.Controls.Add(this.textBoxNoSequenceFormat);
            this.tabPageSequences.Controls.Add(this.label2);
            this.tabPageSequences.Controls.Add(this.textBoxSequenceFormat);
            this.tabPageSequences.Controls.Add(this.label1);
            this.tabPageSequences.Location = new System.Drawing.Point(42, 4);
            this.tabPageSequences.Name = "tabPageSequences";
            this.tabPageSequences.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSequences.Size = new System.Drawing.Size(332, 203);
            this.tabPageSequences.TabIndex = 1;
            this.tabPageSequences.Text = "Sequences";
            this.tabPageSequences.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(397, 13);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(396, 43);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(397, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SequenceFormat :";
            // 
            // textBoxSequenceFormat
            // 
            this.textBoxSequenceFormat.Location = new System.Drawing.Point(181, 6);
            this.textBoxSequenceFormat.Name = "textBoxSequenceFormat";
            this.textBoxSequenceFormat.Size = new System.Drawing.Size(100, 20);
            this.textBoxSequenceFormat.TabIndex = 1;
            this.textBoxSequenceFormat.TextChanged += new System.EventHandler(this.textBoxSequenceFormat_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "NoSequenceFormat :";
            // 
            // textBoxNoSequenceFormat
            // 
            this.textBoxNoSequenceFormat.Location = new System.Drawing.Point(181, 32);
            this.textBoxNoSequenceFormat.Name = "textBoxNoSequenceFormat";
            this.textBoxNoSequenceFormat.Size = new System.Drawing.Size(100, 20);
            this.textBoxNoSequenceFormat.TabIndex = 3;
            this.textBoxNoSequenceFormat.TextChanged += new System.EventHandler(this.textBoxNoSequenceFormat_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "AuthorFormat :";
            // 
            // textBoxAuthorFormat
            // 
            this.textBoxAuthorFormat.Location = new System.Drawing.Point(181, 58);
            this.textBoxAuthorFormat.Name = "textBoxAuthorFormat";
            this.textBoxAuthorFormat.Size = new System.Drawing.Size(100, 20);
            this.textBoxAuthorFormat.TabIndex = 5;
            this.textBoxAuthorFormat.TextChanged += new System.EventHandler(this.textBoxAuthorFormat_TextChanged);
            // 
            // labelFileAsFormat
            // 
            this.labelFileAsFormat.AutoSize = true;
            this.labelFileAsFormat.Location = new System.Drawing.Point(6, 87);
            this.labelFileAsFormat.Name = "labelFileAsFormat";
            this.labelFileAsFormat.Size = new System.Drawing.Size(73, 13);
            this.labelFileAsFormat.TabIndex = 6;
            this.labelFileAsFormat.Text = "FileAsFormat :";
            // 
            // textBoxFileAsFormat
            // 
            this.textBoxFileAsFormat.Location = new System.Drawing.Point(181, 84);
            this.textBoxFileAsFormat.Name = "textBoxFileAsFormat";
            this.textBoxFileAsFormat.Size = new System.Drawing.Size(100, 20);
            this.textBoxFileAsFormat.TabIndex = 7;
            this.textBoxFileAsFormat.TextChanged += new System.EventHandler(this.textBoxFileAsFormat_TextChanged);
            // 
            // tabPageDifferent
            // 
            this.tabPageDifferent.Controls.Add(this.comboBoxFixMode);
            this.tabPageDifferent.Controls.Add(this.label4);
            this.tabPageDifferent.Controls.Add(this.checkBoxSkipAboutPage);
            this.tabPageDifferent.Controls.Add(this.checkBoxCapitalize);
            this.tabPageDifferent.Controls.Add(this.checkBoxEmbedStyles);
            this.tabPageDifferent.Controls.Add(this.checkBoxFlatStructure);
            this.tabPageDifferent.Controls.Add(this.checkBoxConvertAlphaPNG);
            this.tabPageDifferent.Controls.Add(this.checkBoxFb2Info);
            this.tabPageDifferent.Location = new System.Drawing.Point(42, 4);
            this.tabPageDifferent.Name = "tabPageDifferent";
            this.tabPageDifferent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDifferent.Size = new System.Drawing.Size(332, 203);
            this.tabPageDifferent.TabIndex = 2;
            this.tabPageDifferent.Text = "Different";
            this.tabPageDifferent.UseVisualStyleBackColor = true;
            // 
            // tabPageFonts
            // 
            this.tabPageFonts.Location = new System.Drawing.Point(42, 4);
            this.tabPageFonts.Name = "tabPageFonts";
            this.tabPageFonts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFonts.Size = new System.Drawing.Size(332, 203);
            this.tabPageFonts.TabIndex = 3;
            this.tabPageFonts.Text = "Fonts";
            this.tabPageFonts.UseVisualStyleBackColor = true;
            // 
            // checkBoxFb2Info
            // 
            this.checkBoxFb2Info.AutoSize = true;
            this.checkBoxFb2Info.Location = new System.Drawing.Point(7, 7);
            this.checkBoxFb2Info.Name = "checkBoxFb2Info";
            this.checkBoxFb2Info.Size = new System.Drawing.Size(102, 17);
            this.checkBoxFb2Info.TabIndex = 0;
            this.checkBoxFb2Info.Text = "Include Fb2 info";
            this.checkBoxFb2Info.UseVisualStyleBackColor = true;
            this.checkBoxFb2Info.CheckedChanged += new System.EventHandler(this.checkBoxFb2Info_CheckedChanged);
            // 
            // checkBoxConvertAlphaPNG
            // 
            this.checkBoxConvertAlphaPNG.AutoSize = true;
            this.checkBoxConvertAlphaPNG.Location = new System.Drawing.Point(7, 32);
            this.checkBoxConvertAlphaPNG.Name = "checkBoxConvertAlphaPNG";
            this.checkBoxConvertAlphaPNG.Size = new System.Drawing.Size(119, 17);
            this.checkBoxConvertAlphaPNG.TabIndex = 1;
            this.checkBoxConvertAlphaPNG.Text = "Convert Alpha PNG";
            this.checkBoxConvertAlphaPNG.UseVisualStyleBackColor = true;
            this.checkBoxConvertAlphaPNG.CheckedChanged += new System.EventHandler(this.checkBoxConvertAlphaPNG_CheckedChanged);
            // 
            // checkBoxAddSequences
            // 
            this.checkBoxAddSequences.AutoSize = true;
            this.checkBoxAddSequences.Location = new System.Drawing.Point(9, 118);
            this.checkBoxAddSequences.Name = "checkBoxAddSequences";
            this.checkBoxAddSequences.Size = new System.Drawing.Size(99, 17);
            this.checkBoxAddSequences.TabIndex = 8;
            this.checkBoxAddSequences.Text = "AddSequences";
            this.checkBoxAddSequences.UseVisualStyleBackColor = true;
            this.checkBoxAddSequences.CheckedChanged += new System.EventHandler(this.checkBoxAddSequences_CheckedChanged);
            // 
            // checkBoxFlatStructure
            // 
            this.checkBoxFlatStructure.AutoSize = true;
            this.checkBoxFlatStructure.Location = new System.Drawing.Point(7, 56);
            this.checkBoxFlatStructure.Name = "checkBoxFlatStructure";
            this.checkBoxFlatStructure.Size = new System.Drawing.Size(89, 17);
            this.checkBoxFlatStructure.TabIndex = 2;
            this.checkBoxFlatStructure.Text = "Flat Structure";
            this.checkBoxFlatStructure.UseVisualStyleBackColor = true;
            this.checkBoxFlatStructure.CheckedChanged += new System.EventHandler(this.checkBoxFlatStructure_CheckedChanged);
            // 
            // checkBoxEmbedStyles
            // 
            this.checkBoxEmbedStyles.AutoSize = true;
            this.checkBoxEmbedStyles.Location = new System.Drawing.Point(7, 80);
            this.checkBoxEmbedStyles.Name = "checkBoxEmbedStyles";
            this.checkBoxEmbedStyles.Size = new System.Drawing.Size(90, 17);
            this.checkBoxEmbedStyles.TabIndex = 3;
            this.checkBoxEmbedStyles.Text = "Embed Styles";
            this.checkBoxEmbedStyles.UseVisualStyleBackColor = true;
            this.checkBoxEmbedStyles.CheckedChanged += new System.EventHandler(this.checkBoxEmbedStyles_CheckedChanged);
            // 
            // checkBoxCapitalize
            // 
            this.checkBoxCapitalize.AutoSize = true;
            this.checkBoxCapitalize.Location = new System.Drawing.Point(7, 104);
            this.checkBoxCapitalize.Name = "checkBoxCapitalize";
            this.checkBoxCapitalize.Size = new System.Drawing.Size(97, 17);
            this.checkBoxCapitalize.TabIndex = 4;
            this.checkBoxCapitalize.Text = "Capitalize Drop";
            this.checkBoxCapitalize.UseVisualStyleBackColor = true;
            this.checkBoxCapitalize.CheckedChanged += new System.EventHandler(this.checkBoxCapitalize_CheckedChanged);
            // 
            // checkBoxSkipAboutPage
            // 
            this.checkBoxSkipAboutPage.AutoSize = true;
            this.checkBoxSkipAboutPage.Location = new System.Drawing.Point(7, 128);
            this.checkBoxSkipAboutPage.Name = "checkBoxSkipAboutPage";
            this.checkBoxSkipAboutPage.Size = new System.Drawing.Size(105, 17);
            this.checkBoxSkipAboutPage.TabIndex = 5;
            this.checkBoxSkipAboutPage.Text = "Skip About page";
            this.checkBoxSkipAboutPage.UseVisualStyleBackColor = true;
            this.checkBoxSkipAboutPage.CheckedChanged += new System.EventHandler(this.checkBoxSkipAboutPage_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Fix mode :";
            // 
            // comboBoxFixMode
            // 
            this.comboBoxFixMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFixMode.FormattingEnabled = true;
            this.comboBoxFixMode.Location = new System.Drawing.Point(230, 7);
            this.comboBoxFixMode.Name = "comboBoxFixMode";
            this.comboBoxFixMode.Size = new System.Drawing.Size(96, 21);
            this.comboBoxFixMode.TabIndex = 7;
            this.comboBoxFixMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFixMode_SelectedIndexChanged);
            // 
            // ConverterSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "ConverterSettingsForm";
            this.Text = "ConverterSettingsForm";
            this.Load += new System.EventHandler(this.ConverterSettingsForm_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageTransliteration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageSequences.ResumeLayout(false);
            this.tabPageSequences.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNoSequenceFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSequenceFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageDifferent;
        private System.Windows.Forms.TabPage tabPageFonts;
        private System.Windows.Forms.CheckBox checkBoxAddSequences;
        private System.Windows.Forms.CheckBox checkBoxConvertAlphaPNG;
        private System.Windows.Forms.CheckBox checkBoxFb2Info;
        private System.Windows.Forms.CheckBox checkBoxFlatStructure;
        private System.Windows.Forms.CheckBox checkBoxEmbedStyles;
        private System.Windows.Forms.CheckBox checkBoxCapitalize;
        private System.Windows.Forms.CheckBox checkBoxSkipAboutPage;
        private System.Windows.Forms.ComboBox comboBoxFixMode;
        private System.Windows.Forms.Label label4;



    }
}