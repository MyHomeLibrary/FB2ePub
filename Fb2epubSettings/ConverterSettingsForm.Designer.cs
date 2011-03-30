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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTransliterateTOC = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateFileName = new System.Windows.Forms.CheckBox();
            this.checkBoxTransliterateAdditional = new System.Windows.Forms.CheckBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageTransliteration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlSettings.Controls.Add(this.tabPageTransliteration);
            this.tabControlSettings.Controls.Add(this.tabPage2);
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(23, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(456, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageTransliteration;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxTransliterateTOC;
        private System.Windows.Forms.CheckBox checkBoxTransliterateFileName;
        private System.Windows.Forms.CheckBox checkBoxTransliterateAdditional;



    }
}