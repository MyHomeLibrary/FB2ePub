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
            this.listBoxFontNames = new System.Windows.Forms.ListBox();
            this.buttonDeleteFont = new System.Windows.Forms.Button();
            this.buttonAddFont = new System.Windows.Forms.Button();
            this.groupBoxFontProperties = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.listBoxSources = new System.Windows.Forms.ListBox();
            this.buttonAddSource = new System.Windows.Forms.Button();
            this.buttonRemoveSource = new System.Windows.Forms.Button();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.comboBoxSourceType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.buttonBrowseForLocation = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBoxWidth = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.comboBoxStretch = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonVariantSmallCaps = new System.Windows.Forms.RadioButton();
            this.radioButtonVariantNormal = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonStyleOblique = new System.Windows.Forms.RadioButton();
            this.radioButtonStyleItalic = new System.Windows.Forms.RadioButton();
            this.radioButtonStyleNormal = new System.Windows.Forms.RadioButton();
            this.toolTipEditFonts = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxFontProperties.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.errorProvider1.SetError(this.buttonSave, resources.GetString("buttonSave.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonSave.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonSave, ((int)(resources.GetObject("buttonSave.IconPadding"))));
            this.buttonSave.Name = "buttonSave";
            this.toolTipEditFonts.SetToolTip(this.buttonSave, resources.GetString("buttonSave.ToolTip"));
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.toolTipEditFonts.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textBoxFontFamilyName);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // textBoxFontFamilyName
            // 
            resources.ApplyResources(this.textBoxFontFamilyName, "textBoxFontFamilyName");
            this.errorProvider1.SetError(this.textBoxFontFamilyName, resources.GetString("textBoxFontFamilyName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxFontFamilyName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxFontFamilyName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxFontFamilyName, ((int)(resources.GetObject("textBoxFontFamilyName.IconPadding"))));
            this.textBoxFontFamilyName.Name = "textBoxFontFamilyName";
            this.toolTipEditFonts.SetToolTip(this.textBoxFontFamilyName, resources.GetString("textBoxFontFamilyName.ToolTip"));
            this.textBoxFontFamilyName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFontFamilyName_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // buttonReset
            // 
            resources.ApplyResources(this.buttonReset, "buttonReset");
            this.errorProvider1.SetError(this.buttonReset, resources.GetString("buttonReset.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonReset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonReset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonReset, ((int)(resources.GetObject("buttonReset.IconPadding"))));
            this.buttonReset.Name = "buttonReset";
            this.toolTipEditFonts.SetToolTip(this.buttonReset, resources.GetString("buttonReset.ToolTip"));
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.listBoxFontNames);
            this.groupBox2.Controls.Add(this.buttonDeleteFont);
            this.groupBox2.Controls.Add(this.buttonAddFont);
            this.errorProvider1.SetError(this.groupBox2, resources.GetString("groupBox2.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox2, ((int)(resources.GetObject("groupBox2.IconPadding"))));
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // listBoxFontNames
            // 
            resources.ApplyResources(this.listBoxFontNames, "listBoxFontNames");
            this.errorProvider1.SetError(this.listBoxFontNames, resources.GetString("listBoxFontNames.Error"));
            this.listBoxFontNames.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.listBoxFontNames, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listBoxFontNames.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.listBoxFontNames, ((int)(resources.GetObject("listBoxFontNames.IconPadding"))));
            this.listBoxFontNames.Name = "listBoxFontNames";
            this.toolTipEditFonts.SetToolTip(this.listBoxFontNames, resources.GetString("listBoxFontNames.ToolTip"));
            // 
            // buttonDeleteFont
            // 
            resources.ApplyResources(this.buttonDeleteFont, "buttonDeleteFont");
            this.errorProvider1.SetError(this.buttonDeleteFont, resources.GetString("buttonDeleteFont.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonDeleteFont, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonDeleteFont.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonDeleteFont, ((int)(resources.GetObject("buttonDeleteFont.IconPadding"))));
            this.buttonDeleteFont.Name = "buttonDeleteFont";
            this.toolTipEditFonts.SetToolTip(this.buttonDeleteFont, resources.GetString("buttonDeleteFont.ToolTip"));
            this.buttonDeleteFont.UseVisualStyleBackColor = true;
            this.buttonDeleteFont.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddFont
            // 
            resources.ApplyResources(this.buttonAddFont, "buttonAddFont");
            this.errorProvider1.SetError(this.buttonAddFont, resources.GetString("buttonAddFont.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddFont, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddFont.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddFont, ((int)(resources.GetObject("buttonAddFont.IconPadding"))));
            this.buttonAddFont.Name = "buttonAddFont";
            this.toolTipEditFonts.SetToolTip(this.buttonAddFont, resources.GetString("buttonAddFont.ToolTip"));
            this.buttonAddFont.UseVisualStyleBackColor = true;
            this.buttonAddFont.Click += new System.EventHandler(this.buttonAddFont_Click);
            // 
            // groupBoxFontProperties
            // 
            resources.ApplyResources(this.groupBoxFontProperties, "groupBoxFontProperties");
            this.groupBoxFontProperties.Controls.Add(this.groupBox8);
            this.groupBoxFontProperties.Controls.Add(this.groupBox6);
            this.groupBoxFontProperties.Controls.Add(this.groupBox7);
            this.groupBoxFontProperties.Controls.Add(this.groupBox5);
            this.groupBoxFontProperties.Controls.Add(this.groupBox4);
            this.errorProvider1.SetError(this.groupBoxFontProperties, resources.GetString("groupBoxFontProperties.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBoxFontProperties, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxFontProperties.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxFontProperties, ((int)(resources.GetObject("groupBoxFontProperties.IconPadding"))));
            this.groupBoxFontProperties.Name = "groupBoxFontProperties";
            this.groupBoxFontProperties.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBoxFontProperties, resources.GetString("groupBoxFontProperties.ToolTip"));
            // 
            // groupBox8
            // 
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Controls.Add(this.listBoxSources);
            this.groupBox8.Controls.Add(this.buttonAddSource);
            this.groupBox8.Controls.Add(this.buttonRemoveSource);
            this.groupBox8.Controls.Add(this.comboBoxFormat);
            this.groupBox8.Controls.Add(this.labelFormat);
            this.groupBox8.Controls.Add(this.comboBoxSourceType);
            this.groupBox8.Controls.Add(this.labelType);
            this.groupBox8.Controls.Add(this.buttonBrowseForLocation);
            this.groupBox8.Controls.Add(this.textBoxLocation);
            this.groupBox8.Controls.Add(this.labelLocation);
            this.errorProvider1.SetError(this.groupBox8, resources.GetString("groupBox8.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox8.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox8, ((int)(resources.GetObject("groupBox8.IconPadding"))));
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox8, resources.GetString("groupBox8.ToolTip"));
            // 
            // listBoxSources
            // 
            resources.ApplyResources(this.listBoxSources, "listBoxSources");
            this.errorProvider1.SetError(this.listBoxSources, resources.GetString("listBoxSources.Error"));
            this.listBoxSources.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.listBoxSources, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listBoxSources.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.listBoxSources, ((int)(resources.GetObject("listBoxSources.IconPadding"))));
            this.listBoxSources.Name = "listBoxSources";
            this.toolTipEditFonts.SetToolTip(this.listBoxSources, resources.GetString("listBoxSources.ToolTip"));
            // 
            // buttonAddSource
            // 
            resources.ApplyResources(this.buttonAddSource, "buttonAddSource");
            this.errorProvider1.SetError(this.buttonAddSource, resources.GetString("buttonAddSource.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonAddSource, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonAddSource.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonAddSource, ((int)(resources.GetObject("buttonAddSource.IconPadding"))));
            this.buttonAddSource.Name = "buttonAddSource";
            this.toolTipEditFonts.SetToolTip(this.buttonAddSource, resources.GetString("buttonAddSource.ToolTip"));
            this.buttonAddSource.UseVisualStyleBackColor = true;
            this.buttonAddSource.Click += new System.EventHandler(this.buttonAddSource_Click);
            // 
            // buttonRemoveSource
            // 
            resources.ApplyResources(this.buttonRemoveSource, "buttonRemoveSource");
            this.errorProvider1.SetError(this.buttonRemoveSource, resources.GetString("buttonRemoveSource.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonRemoveSource, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonRemoveSource.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonRemoveSource, ((int)(resources.GetObject("buttonRemoveSource.IconPadding"))));
            this.buttonRemoveSource.Name = "buttonRemoveSource";
            this.toolTipEditFonts.SetToolTip(this.buttonRemoveSource, resources.GetString("buttonRemoveSource.ToolTip"));
            this.buttonRemoveSource.UseVisualStyleBackColor = true;
            this.buttonRemoveSource.Click += new System.EventHandler(this.buttonRemoveSource_Click);
            // 
            // comboBoxFormat
            // 
            resources.ApplyResources(this.comboBoxFormat, "comboBoxFormat");
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxFormat, resources.GetString("comboBoxFormat.Error"));
            this.comboBoxFormat.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxFormat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxFormat.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxFormat, ((int)(resources.GetObject("comboBoxFormat.IconPadding"))));
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.toolTipEditFonts.SetToolTip(this.comboBoxFormat, resources.GetString("comboBoxFormat.ToolTip"));
            this.comboBoxFormat.SelectedValueChanged += new System.EventHandler(this.comboBoxFormat_SelectedValueChanged);
            // 
            // labelFormat
            // 
            resources.ApplyResources(this.labelFormat, "labelFormat");
            this.errorProvider1.SetError(this.labelFormat, resources.GetString("labelFormat.Error"));
            this.errorProvider1.SetIconAlignment(this.labelFormat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelFormat.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelFormat, ((int)(resources.GetObject("labelFormat.IconPadding"))));
            this.labelFormat.Name = "labelFormat";
            this.toolTipEditFonts.SetToolTip(this.labelFormat, resources.GetString("labelFormat.ToolTip"));
            // 
            // comboBoxSourceType
            // 
            resources.ApplyResources(this.comboBoxSourceType, "comboBoxSourceType");
            this.comboBoxSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxSourceType, resources.GetString("comboBoxSourceType.Error"));
            this.comboBoxSourceType.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxSourceType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxSourceType.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxSourceType, ((int)(resources.GetObject("comboBoxSourceType.IconPadding"))));
            this.comboBoxSourceType.Name = "comboBoxSourceType";
            this.toolTipEditFonts.SetToolTip(this.comboBoxSourceType, resources.GetString("comboBoxSourceType.ToolTip"));
            this.comboBoxSourceType.SelectedValueChanged += new System.EventHandler(this.comboBoxSourceType_SelectedValueChanged);
            // 
            // labelType
            // 
            resources.ApplyResources(this.labelType, "labelType");
            this.errorProvider1.SetError(this.labelType, resources.GetString("labelType.Error"));
            this.errorProvider1.SetIconAlignment(this.labelType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelType.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelType, ((int)(resources.GetObject("labelType.IconPadding"))));
            this.labelType.Name = "labelType";
            this.toolTipEditFonts.SetToolTip(this.labelType, resources.GetString("labelType.ToolTip"));
            // 
            // buttonBrowseForLocation
            // 
            resources.ApplyResources(this.buttonBrowseForLocation, "buttonBrowseForLocation");
            this.errorProvider1.SetError(this.buttonBrowseForLocation, resources.GetString("buttonBrowseForLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonBrowseForLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonBrowseForLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonBrowseForLocation, ((int)(resources.GetObject("buttonBrowseForLocation.IconPadding"))));
            this.buttonBrowseForLocation.Name = "buttonBrowseForLocation";
            this.toolTipEditFonts.SetToolTip(this.buttonBrowseForLocation, resources.GetString("buttonBrowseForLocation.ToolTip"));
            this.buttonBrowseForLocation.UseVisualStyleBackColor = true;
            this.buttonBrowseForLocation.Click += new System.EventHandler(this.buttonBrowseForLocation_Click);
            // 
            // textBoxLocation
            // 
            resources.ApplyResources(this.textBoxLocation, "textBoxLocation");
            this.errorProvider1.SetError(this.textBoxLocation, resources.GetString("textBoxLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxLocation, ((int)(resources.GetObject("textBoxLocation.IconPadding"))));
            this.textBoxLocation.Name = "textBoxLocation";
            this.toolTipEditFonts.SetToolTip(this.textBoxLocation, resources.GetString("textBoxLocation.ToolTip"));
            this.textBoxLocation.TextChanged += new System.EventHandler(this.textBoxLocation_TextChanged);
            // 
            // labelLocation
            // 
            resources.ApplyResources(this.labelLocation, "labelLocation");
            this.errorProvider1.SetError(this.labelLocation, resources.GetString("labelLocation.Error"));
            this.errorProvider1.SetIconAlignment(this.labelLocation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelLocation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelLocation, ((int)(resources.GetObject("labelLocation.IconPadding"))));
            this.labelLocation.Name = "labelLocation";
            this.toolTipEditFonts.SetToolTip(this.labelLocation, resources.GetString("labelLocation.ToolTip"));
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.comboBoxWidth);
            this.errorProvider1.SetError(this.groupBox6, resources.GetString("groupBox6.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox6, ((int)(resources.GetObject("groupBox6.IconPadding"))));
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox6, resources.GetString("groupBox6.ToolTip"));
            // 
            // comboBoxWidth
            // 
            resources.ApplyResources(this.comboBoxWidth, "comboBoxWidth");
            this.comboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxWidth, resources.GetString("comboBoxWidth.Error"));
            this.comboBoxWidth.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxWidth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxWidth.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxWidth, ((int)(resources.GetObject("comboBoxWidth.IconPadding"))));
            this.comboBoxWidth.Name = "comboBoxWidth";
            this.toolTipEditFonts.SetToolTip(this.comboBoxWidth, resources.GetString("comboBoxWidth.ToolTip"));
            this.comboBoxWidth.SelectedIndexChanged += new System.EventHandler(this.comboBoxWidth_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Controls.Add(this.comboBoxStretch);
            this.errorProvider1.SetError(this.groupBox7, resources.GetString("groupBox7.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox7, ((int)(resources.GetObject("groupBox7.IconPadding"))));
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox7, resources.GetString("groupBox7.ToolTip"));
            // 
            // comboBoxStretch
            // 
            resources.ApplyResources(this.comboBoxStretch, "comboBoxStretch");
            this.comboBoxStretch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxStretch, resources.GetString("comboBoxStretch.Error"));
            this.comboBoxStretch.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxStretch, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxStretch.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxStretch, ((int)(resources.GetObject("comboBoxStretch.IconPadding"))));
            this.comboBoxStretch.Name = "comboBoxStretch";
            this.toolTipEditFonts.SetToolTip(this.comboBoxStretch, resources.GetString("comboBoxStretch.ToolTip"));
            this.comboBoxStretch.SelectedIndexChanged += new System.EventHandler(this.comboBoxStretch_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.radioButtonVariantSmallCaps);
            this.groupBox5.Controls.Add(this.radioButtonVariantNormal);
            this.errorProvider1.SetError(this.groupBox5, resources.GetString("groupBox5.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox5, ((int)(resources.GetObject("groupBox5.IconPadding"))));
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // radioButtonVariantSmallCaps
            // 
            resources.ApplyResources(this.radioButtonVariantSmallCaps, "radioButtonVariantSmallCaps");
            this.errorProvider1.SetError(this.radioButtonVariantSmallCaps, resources.GetString("radioButtonVariantSmallCaps.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonVariantSmallCaps, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonVariantSmallCaps.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonVariantSmallCaps, ((int)(resources.GetObject("radioButtonVariantSmallCaps.IconPadding"))));
            this.radioButtonVariantSmallCaps.Name = "radioButtonVariantSmallCaps";
            this.radioButtonVariantSmallCaps.TabStop = true;
            this.toolTipEditFonts.SetToolTip(this.radioButtonVariantSmallCaps, resources.GetString("radioButtonVariantSmallCaps.ToolTip"));
            this.radioButtonVariantSmallCaps.UseVisualStyleBackColor = true;
            this.radioButtonVariantSmallCaps.CheckedChanged += new System.EventHandler(this.radioButtonVariantSmallCaps_CheckedChanged);
            // 
            // radioButtonVariantNormal
            // 
            resources.ApplyResources(this.radioButtonVariantNormal, "radioButtonVariantNormal");
            this.errorProvider1.SetError(this.radioButtonVariantNormal, resources.GetString("radioButtonVariantNormal.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonVariantNormal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonVariantNormal.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonVariantNormal, ((int)(resources.GetObject("radioButtonVariantNormal.IconPadding"))));
            this.radioButtonVariantNormal.Name = "radioButtonVariantNormal";
            this.radioButtonVariantNormal.TabStop = true;
            this.toolTipEditFonts.SetToolTip(this.radioButtonVariantNormal, resources.GetString("radioButtonVariantNormal.ToolTip"));
            this.radioButtonVariantNormal.UseVisualStyleBackColor = true;
            this.radioButtonVariantNormal.CheckedChanged += new System.EventHandler(this.radioButtonVariantNormal_CheckedChanged);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.radioButtonStyleOblique);
            this.groupBox4.Controls.Add(this.radioButtonStyleItalic);
            this.groupBox4.Controls.Add(this.radioButtonStyleNormal);
            this.errorProvider1.SetError(this.groupBox4, resources.GetString("groupBox4.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox4, ((int)(resources.GetObject("groupBox4.IconPadding"))));
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTipEditFonts.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // radioButtonStyleOblique
            // 
            resources.ApplyResources(this.radioButtonStyleOblique, "radioButtonStyleOblique");
            this.errorProvider1.SetError(this.radioButtonStyleOblique, resources.GetString("radioButtonStyleOblique.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonStyleOblique, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonStyleOblique.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonStyleOblique, ((int)(resources.GetObject("radioButtonStyleOblique.IconPadding"))));
            this.radioButtonStyleOblique.Name = "radioButtonStyleOblique";
            this.radioButtonStyleOblique.TabStop = true;
            this.toolTipEditFonts.SetToolTip(this.radioButtonStyleOblique, resources.GetString("radioButtonStyleOblique.ToolTip"));
            this.radioButtonStyleOblique.UseVisualStyleBackColor = true;
            this.radioButtonStyleOblique.CheckedChanged += new System.EventHandler(this.radioButtonStyleOblique_CheckedChanged);
            // 
            // radioButtonStyleItalic
            // 
            resources.ApplyResources(this.radioButtonStyleItalic, "radioButtonStyleItalic");
            this.errorProvider1.SetError(this.radioButtonStyleItalic, resources.GetString("radioButtonStyleItalic.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonStyleItalic, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonStyleItalic.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonStyleItalic, ((int)(resources.GetObject("radioButtonStyleItalic.IconPadding"))));
            this.radioButtonStyleItalic.Name = "radioButtonStyleItalic";
            this.radioButtonStyleItalic.TabStop = true;
            this.toolTipEditFonts.SetToolTip(this.radioButtonStyleItalic, resources.GetString("radioButtonStyleItalic.ToolTip"));
            this.radioButtonStyleItalic.UseVisualStyleBackColor = true;
            this.radioButtonStyleItalic.CheckedChanged += new System.EventHandler(this.radioButtonStyleItalic_CheckedChanged);
            // 
            // radioButtonStyleNormal
            // 
            resources.ApplyResources(this.radioButtonStyleNormal, "radioButtonStyleNormal");
            this.errorProvider1.SetError(this.radioButtonStyleNormal, resources.GetString("radioButtonStyleNormal.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonStyleNormal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonStyleNormal.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonStyleNormal, ((int)(resources.GetObject("radioButtonStyleNormal.IconPadding"))));
            this.radioButtonStyleNormal.Name = "radioButtonStyleNormal";
            this.radioButtonStyleNormal.TabStop = true;
            this.toolTipEditFonts.SetToolTip(this.radioButtonStyleNormal, resources.GetString("radioButtonStyleNormal.ToolTip"));
            this.radioButtonStyleNormal.UseVisualStyleBackColor = true;
            this.radioButtonStyleNormal.CheckedChanged += new System.EventHandler(this.radioButtonStyleNormal_CheckedChanged);
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
            this.toolTipEditFonts.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBoxFontProperties.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Button buttonDeleteFont;
        private System.Windows.Forms.Button buttonAddFont;
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
        private System.Windows.Forms.ComboBox comboBoxSourceType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Button buttonAddSource;
        private System.Windows.Forms.Button buttonRemoveSource;
        private System.Windows.Forms.ListBox listBoxFontNames;
        private System.Windows.Forms.ListBox listBoxSources;
        private System.Windows.Forms.ToolTip toolTipEditFonts;
    }
}