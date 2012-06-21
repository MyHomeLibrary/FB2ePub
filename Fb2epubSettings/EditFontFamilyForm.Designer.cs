namespace Fb2epubSettings
{
    partial class EditFontFamilyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFontFamilyForm));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxFontFamilyName = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxFonts = new System.Windows.Forms.ListBox();
            this.buttonAddFont = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.groupBoxFontProperties = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonVariantNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonVariantSmallCaps = new System.Windows.Forms.RadioButton();
            this.radioButtonStyleNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonStyleItalic = new System.Windows.Forms.RadioButton();
            this.radioButtonStyleOblique = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.comboBoxWidth = new System.Windows.Forms.ComboBox();
            this.comboBoxStretch = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.listBoxSources = new System.Windows.Forms.ListBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.buttonBrowseForLocation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSourceType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.buttonRemoveSource = new System.Windows.Forms.Button();
            this.buttonAddSource = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxFontProperties.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxFontFamilyName);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBoxFontFamilyName
            // 
            resources.ApplyResources(this.textBoxFontFamilyName, "textBoxFontFamilyName");
            this.textBoxFontFamilyName.Name = "textBoxFontFamilyName";
            this.textBoxFontFamilyName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFontFamilyName_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonReset
            // 
            resources.ApplyResources(this.buttonReset, "buttonReset");
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonAddFont);
            this.groupBox2.Controls.Add(this.listBoxFonts);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // listBoxFonts
            // 
            this.listBoxFonts.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxFonts, "listBoxFonts");
            this.listBoxFonts.Name = "listBoxFonts";
            this.listBoxFonts.SelectedIndexChanged += new System.EventHandler(this.listBoxFonts_SelectedIndexChanged);
            // 
            // buttonAddFont
            // 
            resources.ApplyResources(this.buttonAddFont, "buttonAddFont");
            this.buttonAddFont.Name = "buttonAddFont";
            this.buttonAddFont.UseVisualStyleBackColor = true;
            this.buttonAddFont.Click += new System.EventHandler(this.buttonAddFont_Click);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // groupBoxFontProperties
            // 
            this.groupBoxFontProperties.Controls.Add(this.groupBox8);
            this.groupBoxFontProperties.Controls.Add(this.groupBox6);
            this.groupBoxFontProperties.Controls.Add(this.groupBox7);
            this.groupBoxFontProperties.Controls.Add(this.groupBox5);
            this.groupBoxFontProperties.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.groupBoxFontProperties, "groupBoxFontProperties");
            this.groupBoxFontProperties.Name = "groupBoxFontProperties";
            this.groupBoxFontProperties.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonStyleOblique);
            this.groupBox4.Controls.Add(this.radioButtonStyleItalic);
            this.groupBox4.Controls.Add(this.radioButtonStyleNormal);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButtonVariantSmallCaps);
            this.groupBox5.Controls.Add(this.radioButtonVariantNormal);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // radioButtonVariantNormal
            // 
            resources.ApplyResources(this.radioButtonVariantNormal, "radioButtonVariantNormal");
            this.radioButtonVariantNormal.Name = "radioButtonVariantNormal";
            this.radioButtonVariantNormal.TabStop = true;
            this.radioButtonVariantNormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonVariantSmallCaps
            // 
            resources.ApplyResources(this.radioButtonVariantSmallCaps, "radioButtonVariantSmallCaps");
            this.radioButtonVariantSmallCaps.Name = "radioButtonVariantSmallCaps";
            this.radioButtonVariantSmallCaps.TabStop = true;
            this.radioButtonVariantSmallCaps.UseVisualStyleBackColor = true;
            // 
            // radioButtonStyleNormal
            // 
            resources.ApplyResources(this.radioButtonStyleNormal, "radioButtonStyleNormal");
            this.radioButtonStyleNormal.Name = "radioButtonStyleNormal";
            this.radioButtonStyleNormal.TabStop = true;
            this.radioButtonStyleNormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonStyleItalic
            // 
            resources.ApplyResources(this.radioButtonStyleItalic, "radioButtonStyleItalic");
            this.radioButtonStyleItalic.Name = "radioButtonStyleItalic";
            this.radioButtonStyleItalic.TabStop = true;
            this.radioButtonStyleItalic.UseVisualStyleBackColor = true;
            // 
            // radioButtonStyleOblique
            // 
            resources.ApplyResources(this.radioButtonStyleOblique, "radioButtonStyleOblique");
            this.radioButtonStyleOblique.Name = "radioButtonStyleOblique";
            this.radioButtonStyleOblique.TabStop = true;
            this.radioButtonStyleOblique.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBoxWidth);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.comboBoxStretch);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // comboBoxWidth
            // 
            this.comboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWidth.FormattingEnabled = true;
            this.comboBoxWidth.Items.AddRange(new object[] {
            resources.GetString("comboBoxWidth.Items"),
            resources.GetString("comboBoxWidth.Items1"),
            resources.GetString("comboBoxWidth.Items2"),
            resources.GetString("comboBoxWidth.Items3"),
            resources.GetString("comboBoxWidth.Items4"),
            resources.GetString("comboBoxWidth.Items5"),
            resources.GetString("comboBoxWidth.Items6"),
            resources.GetString("comboBoxWidth.Items7"),
            resources.GetString("comboBoxWidth.Items8"),
            resources.GetString("comboBoxWidth.Items9"),
            resources.GetString("comboBoxWidth.Items10")});
            resources.ApplyResources(this.comboBoxWidth, "comboBoxWidth");
            this.comboBoxWidth.Name = "comboBoxWidth";
            // 
            // comboBoxStretch
            // 
            this.comboBoxStretch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStretch.FormattingEnabled = true;
            this.comboBoxStretch.Items.AddRange(new object[] {
            resources.GetString("comboBoxStretch.Items"),
            resources.GetString("comboBoxStretch.Items1"),
            resources.GetString("comboBoxStretch.Items2"),
            resources.GetString("comboBoxStretch.Items3"),
            resources.GetString("comboBoxStretch.Items4"),
            resources.GetString("comboBoxStretch.Items5"),
            resources.GetString("comboBoxStretch.Items6"),
            resources.GetString("comboBoxStretch.Items7"),
            resources.GetString("comboBoxStretch.Items8")});
            resources.ApplyResources(this.comboBoxStretch, "comboBoxStretch");
            this.comboBoxStretch.Name = "comboBoxStretch";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.buttonAddSource);
            this.groupBox8.Controls.Add(this.buttonRemoveSource);
            this.groupBox8.Controls.Add(this.comboBoxFormat);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.comboBoxSourceType);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.buttonBrowseForLocation);
            this.groupBox8.Controls.Add(this.textBoxLocation);
            this.groupBox8.Controls.Add(this.labelLocation);
            this.groupBox8.Controls.Add(this.listBoxSources);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // listBoxSources
            // 
            this.listBoxSources.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxSources, "listBoxSources");
            this.listBoxSources.Name = "listBoxSources";
            // 
            // labelLocation
            // 
            resources.ApplyResources(this.labelLocation, "labelLocation");
            this.labelLocation.Name = "labelLocation";
            // 
            // textBoxLocation
            // 
            resources.ApplyResources(this.textBoxLocation, "textBoxLocation");
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.TextChanged += new System.EventHandler(this.textBoxLocation_TextChanged);
            // 
            // buttonBrowseForLocation
            // 
            resources.ApplyResources(this.buttonBrowseForLocation, "buttonBrowseForLocation");
            this.buttonBrowseForLocation.Name = "buttonBrowseForLocation";
            this.buttonBrowseForLocation.UseVisualStyleBackColor = true;
            this.buttonBrowseForLocation.Click += new System.EventHandler(this.buttonBrowseForLocation_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBoxSourceType
            // 
            this.comboBoxSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceType.FormattingEnabled = true;
            this.comboBoxSourceType.Items.AddRange(new object[] {
            resources.GetString("comboBoxSourceType.Items"),
            resources.GetString("comboBoxSourceType.Items1"),
            resources.GetString("comboBoxSourceType.Items2")});
            resources.ApplyResources(this.comboBoxSourceType, "comboBoxSourceType");
            this.comboBoxSourceType.Name = "comboBoxSourceType";
            this.comboBoxSourceType.SelectedValueChanged += new System.EventHandler(this.comboBoxSourceType_SelectedValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Items.AddRange(new object[] {
            resources.GetString("comboBoxFormat.Items"),
            resources.GetString("comboBoxFormat.Items1"),
            resources.GetString("comboBoxFormat.Items2"),
            resources.GetString("comboBoxFormat.Items3"),
            resources.GetString("comboBoxFormat.Items4"),
            resources.GetString("comboBoxFormat.Items5")});
            resources.ApplyResources(this.comboBoxFormat, "comboBoxFormat");
            this.comboBoxFormat.Name = "comboBoxFormat";
            // 
            // buttonRemoveSource
            // 
            resources.ApplyResources(this.buttonRemoveSource, "buttonRemoveSource");
            this.buttonRemoveSource.Name = "buttonRemoveSource";
            this.buttonRemoveSource.UseVisualStyleBackColor = true;
            // 
            // buttonAddSource
            // 
            resources.ApplyResources(this.buttonAddSource, "buttonAddSource");
            this.buttonAddSource.Name = "buttonAddSource";
            this.buttonAddSource.UseVisualStyleBackColor = true;
            // 
            // EditFontFamilyForm
            // 
            this.AcceptButton = this.buttonSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxFontProperties);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditFontFamilyForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBoxFontProperties.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxFontFamilyName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAddFont;
        private System.Windows.Forms.ListBox listBoxFonts;
        private System.Windows.Forms.GroupBox groupBoxFontProperties;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonVariantSmallCaps;
        private System.Windows.Forms.RadioButton radioButtonVariantNormal;
        private System.Windows.Forms.RadioButton radioButtonStyleNormal;
        private System.Windows.Forms.RadioButton radioButtonStyleOblique;
        private System.Windows.Forms.RadioButton radioButtonStyleItalic;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox comboBoxWidth;
        private System.Windows.Forms.ComboBox comboBoxStretch;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button buttonBrowseForLocation;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.ListBox listBoxSources;
        private System.Windows.Forms.ComboBox comboBoxSourceType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Button buttonAddSource;
        private System.Windows.Forms.Button buttonRemoveSource;
    }
}