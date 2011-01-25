namespace RegisterFB2EPub
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabWizard = new RegisterFB2EPub.WizardPagesControl();
            this.tabPageInitial = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonChange = new System.Windows.Forms.RadioButton();
            this.radioButtonRegister = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonUnregister = new System.Windows.Forms.RadioButton();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxAny = new System.Windows.Forms.CheckBox();
            this.buttonUnSelectAll = new System.Windows.Forms.Button();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.checkBoxRar = new System.Windows.Forms.CheckBox();
            this.checkBoxZip = new System.Windows.Forms.CheckBox();
            this.checkBoxFb2 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dllPath = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.tabPageUnregisterFinish = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageRegisterBrowse = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelRegPath = new System.Windows.Forms.Label();
            this.buttonRegBrowse = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageRegisterFb2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonNotRegisterFb2 = new System.Windows.Forms.RadioButton();
            this.radioButtonFb2Register = new System.Windows.Forms.RadioButton();
            this.tabPageRegisterZip = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.checkBoxFb2ZipOnly = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButtonNotRegisterZip = new System.Windows.Forms.RadioButton();
            this.radioButtonZipRegister = new System.Windows.Forms.RadioButton();
            this.tabPageRegisterRar = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.checkBoxFb2RarOnly = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonNotRegisterRar = new System.Windows.Forms.RadioButton();
            this.radioButtonRarRegister = new System.Windows.Forms.RadioButton();
            this.tabPageRegisterFinish = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tabWizard.SuspendLayout();
            this.tabPageInitial.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageUnregisterFinish.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPageRegisterBrowse.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPageRegisterFb2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPageRegisterZip.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabPageRegisterRar.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tabPageRegisterFinish.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.tabWizard, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // tabWizard
            // 
            this.tabWizard.AccessibleDescription = null;
            this.tabWizard.AccessibleName = null;
            resources.ApplyResources(this.tabWizard, "tabWizard");
            this.tabWizard.BackgroundImage = null;
            this.tabWizard.Controls.Add(this.tabPageInitial);
            this.tabWizard.Controls.Add(this.tabPageAdvanced);
            this.tabWizard.Controls.Add(this.tabPageUnregisterFinish);
            this.tabWizard.Controls.Add(this.tabPageRegisterBrowse);
            this.tabWizard.Controls.Add(this.tabPageRegisterFb2);
            this.tabWizard.Controls.Add(this.tabPageRegisterZip);
            this.tabWizard.Controls.Add(this.tabPageRegisterRar);
            this.tabWizard.Controls.Add(this.tabPageRegisterFinish);
            this.tabWizard.Font = null;
            this.tabWizard.Name = "tabWizard";
            this.tabWizard.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabWizard, resources.GetString("tabWizard.ToolTip"));
            // 
            // tabPageInitial
            // 
            this.tabPageInitial.AccessibleDescription = null;
            this.tabPageInitial.AccessibleName = null;
            resources.ApplyResources(this.tabPageInitial, "tabPageInitial");
            this.tabPageInitial.BackgroundImage = null;
            this.tabPageInitial.Controls.Add(this.groupBox6);
            this.tabPageInitial.Font = null;
            this.tabPageInitial.Name = "tabPageInitial";
            this.toolTip1.SetToolTip(this.tabPageInitial, resources.GetString("tabPageInitial.ToolTip"));
            this.tabPageInitial.UseVisualStyleBackColor = true;
            this.tabPageInitial.Enter += new System.EventHandler(this.tabPageInitial_Enter);
            // 
            // groupBox6
            // 
            this.groupBox6.AccessibleDescription = null;
            this.groupBox6.AccessibleName = null;
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.BackgroundImage = null;
            this.groupBox6.Controls.Add(this.radioButtonChange);
            this.groupBox6.Controls.Add(this.radioButtonRegister);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.radioButtonUnregister);
            this.groupBox6.Controls.Add(this.radioButtonCustom);
            this.groupBox6.Font = null;
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox6, resources.GetString("groupBox6.ToolTip"));
            // 
            // radioButtonChange
            // 
            this.radioButtonChange.AccessibleDescription = null;
            this.radioButtonChange.AccessibleName = null;
            resources.ApplyResources(this.radioButtonChange, "radioButtonChange");
            this.radioButtonChange.BackgroundImage = null;
            this.radioButtonChange.Font = null;
            this.radioButtonChange.Name = "radioButtonChange";
            this.radioButtonChange.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonChange, resources.GetString("radioButtonChange.ToolTip"));
            this.radioButtonChange.UseVisualStyleBackColor = true;
            // 
            // radioButtonRegister
            // 
            this.radioButtonRegister.AccessibleDescription = null;
            this.radioButtonRegister.AccessibleName = null;
            resources.ApplyResources(this.radioButtonRegister, "radioButtonRegister");
            this.radioButtonRegister.BackgroundImage = null;
            this.radioButtonRegister.Checked = true;
            this.radioButtonRegister.Font = null;
            this.radioButtonRegister.Name = "radioButtonRegister";
            this.radioButtonRegister.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonRegister, resources.GetString("radioButtonRegister.ToolTip"));
            this.radioButtonRegister.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // radioButtonUnregister
            // 
            this.radioButtonUnregister.AccessibleDescription = null;
            this.radioButtonUnregister.AccessibleName = null;
            resources.ApplyResources(this.radioButtonUnregister, "radioButtonUnregister");
            this.radioButtonUnregister.BackgroundImage = null;
            this.radioButtonUnregister.Font = null;
            this.radioButtonUnregister.Name = "radioButtonUnregister";
            this.toolTip1.SetToolTip(this.radioButtonUnregister, resources.GetString("radioButtonUnregister.ToolTip"));
            this.radioButtonUnregister.UseVisualStyleBackColor = true;
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.AccessibleDescription = null;
            this.radioButtonCustom.AccessibleName = null;
            resources.ApplyResources(this.radioButtonCustom, "radioButtonCustom");
            this.radioButtonCustom.BackgroundImage = null;
            this.radioButtonCustom.Font = null;
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.toolTip1.SetToolTip(this.radioButtonCustom, resources.GetString("radioButtonCustom.ToolTip"));
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.AccessibleDescription = null;
            this.tabPageAdvanced.AccessibleName = null;
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.BackgroundImage = null;
            this.tabPageAdvanced.Controls.Add(this.panel1);
            this.tabPageAdvanced.Font = null;
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.toolTip1.SetToolTip(this.tabPageAdvanced, resources.GetString("tabPageAdvanced.ToolTip"));
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = null;
            this.groupBox2.AccessibleName = null;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackgroundImage = null;
            this.groupBox2.Controls.Add(this.checkBoxAny);
            this.groupBox2.Controls.Add(this.buttonUnSelectAll);
            this.groupBox2.Controls.Add(this.buttonSelectAll);
            this.groupBox2.Controls.Add(this.checkBoxRar);
            this.groupBox2.Controls.Add(this.checkBoxZip);
            this.groupBox2.Controls.Add(this.checkBoxFb2);
            this.groupBox2.Font = null;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // checkBoxAny
            // 
            this.checkBoxAny.AccessibleDescription = null;
            this.checkBoxAny.AccessibleName = null;
            resources.ApplyResources(this.checkBoxAny, "checkBoxAny");
            this.checkBoxAny.BackgroundImage = null;
            this.checkBoxAny.Font = null;
            this.checkBoxAny.ForeColor = System.Drawing.Color.DarkRed;
            this.checkBoxAny.Name = "checkBoxAny";
            this.toolTip1.SetToolTip(this.checkBoxAny, resources.GetString("checkBoxAny.ToolTip"));
            this.checkBoxAny.UseVisualStyleBackColor = true;
            this.checkBoxAny.CheckedChanged += new System.EventHandler(this.checkBoxAny_CheckedChanged);
            // 
            // buttonUnSelectAll
            // 
            this.buttonUnSelectAll.AccessibleDescription = null;
            this.buttonUnSelectAll.AccessibleName = null;
            resources.ApplyResources(this.buttonUnSelectAll, "buttonUnSelectAll");
            this.buttonUnSelectAll.BackgroundImage = null;
            this.buttonUnSelectAll.Font = null;
            this.buttonUnSelectAll.Name = "buttonUnSelectAll";
            this.toolTip1.SetToolTip(this.buttonUnSelectAll, resources.GetString("buttonUnSelectAll.ToolTip"));
            this.buttonUnSelectAll.UseVisualStyleBackColor = true;
            this.buttonUnSelectAll.Click += new System.EventHandler(this.buttonUnSelectAll_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.AccessibleDescription = null;
            this.buttonSelectAll.AccessibleName = null;
            resources.ApplyResources(this.buttonSelectAll, "buttonSelectAll");
            this.buttonSelectAll.BackgroundImage = null;
            this.buttonSelectAll.Font = null;
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.toolTip1.SetToolTip(this.buttonSelectAll, resources.GetString("buttonSelectAll.ToolTip"));
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // checkBoxRar
            // 
            this.checkBoxRar.AccessibleDescription = null;
            this.checkBoxRar.AccessibleName = null;
            resources.ApplyResources(this.checkBoxRar, "checkBoxRar");
            this.checkBoxRar.BackgroundImage = null;
            this.checkBoxRar.Font = null;
            this.checkBoxRar.Name = "checkBoxRar";
            this.toolTip1.SetToolTip(this.checkBoxRar, resources.GetString("checkBoxRar.ToolTip"));
            this.checkBoxRar.UseVisualStyleBackColor = true;
            this.checkBoxRar.CheckedChanged += new System.EventHandler(this.checkBoxRar_CheckedChanged);
            // 
            // checkBoxZip
            // 
            this.checkBoxZip.AccessibleDescription = null;
            this.checkBoxZip.AccessibleName = null;
            resources.ApplyResources(this.checkBoxZip, "checkBoxZip");
            this.checkBoxZip.BackgroundImage = null;
            this.checkBoxZip.Font = null;
            this.checkBoxZip.Name = "checkBoxZip";
            this.toolTip1.SetToolTip(this.checkBoxZip, resources.GetString("checkBoxZip.ToolTip"));
            this.checkBoxZip.UseVisualStyleBackColor = true;
            this.checkBoxZip.CheckedChanged += new System.EventHandler(this.checkBoxZip_CheckedChanged);
            // 
            // checkBoxFb2
            // 
            this.checkBoxFb2.AccessibleDescription = null;
            this.checkBoxFb2.AccessibleName = null;
            resources.ApplyResources(this.checkBoxFb2, "checkBoxFb2");
            this.checkBoxFb2.BackgroundImage = null;
            this.checkBoxFb2.Font = null;
            this.checkBoxFb2.Name = "checkBoxFb2";
            this.toolTip1.SetToolTip(this.checkBoxFb2, resources.GetString("checkBoxFb2.ToolTip"));
            this.checkBoxFb2.UseVisualStyleBackColor = true;
            this.checkBoxFb2.CheckedChanged += new System.EventHandler(this.checkBoxFb2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.dllPath);
            this.groupBox1.Controls.Add(this.buttonBrowse);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // dllPath
            // 
            this.dllPath.AccessibleDescription = null;
            this.dllPath.AccessibleName = null;
            resources.ApplyResources(this.dllPath, "dllPath");
            this.dllPath.AutoEllipsis = true;
            this.dllPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dllPath.Font = null;
            this.dllPath.Name = "dllPath";
            this.toolTip1.SetToolTip(this.dllPath, resources.GetString("dllPath.ToolTip"));
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.AccessibleDescription = null;
            this.buttonBrowse.AccessibleName = null;
            resources.ApplyResources(this.buttonBrowse, "buttonBrowse");
            this.buttonBrowse.BackgroundImage = null;
            this.buttonBrowse.Font = null;
            this.buttonBrowse.Name = "buttonBrowse";
            this.toolTip1.SetToolTip(this.buttonBrowse, resources.GetString("buttonBrowse.ToolTip"));
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // tabPageUnregisterFinish
            // 
            this.tabPageUnregisterFinish.AccessibleDescription = null;
            this.tabPageUnregisterFinish.AccessibleName = null;
            resources.ApplyResources(this.tabPageUnregisterFinish, "tabPageUnregisterFinish");
            this.tabPageUnregisterFinish.BackgroundImage = null;
            this.tabPageUnregisterFinish.Controls.Add(this.groupBox5);
            this.tabPageUnregisterFinish.Font = null;
            this.tabPageUnregisterFinish.Name = "tabPageUnregisterFinish";
            this.toolTip1.SetToolTip(this.tabPageUnregisterFinish, resources.GetString("tabPageUnregisterFinish.ToolTip"));
            this.tabPageUnregisterFinish.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleDescription = null;
            this.groupBox5.AccessibleName = null;
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.BackgroundImage = null;
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Font = null;
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // tabPageRegisterBrowse
            // 
            this.tabPageRegisterBrowse.AccessibleDescription = null;
            this.tabPageRegisterBrowse.AccessibleName = null;
            resources.ApplyResources(this.tabPageRegisterBrowse, "tabPageRegisterBrowse");
            this.tabPageRegisterBrowse.BackgroundImage = null;
            this.tabPageRegisterBrowse.Controls.Add(this.tableLayoutPanel3);
            this.tabPageRegisterBrowse.Font = null;
            this.tabPageRegisterBrowse.Name = "tabPageRegisterBrowse";
            this.toolTip1.SetToolTip(this.tabPageRegisterBrowse, resources.GetString("tabPageRegisterBrowse.ToolTip"));
            this.tabPageRegisterBrowse.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AccessibleDescription = null;
            this.tableLayoutPanel3.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.BackgroundImage = null;
            this.tableLayoutPanel3.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel3.Font = null;
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.toolTip1.SetToolTip(this.tableLayoutPanel3, resources.GetString("tableLayoutPanel3.ToolTip"));
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = null;
            this.groupBox3.AccessibleName = null;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackgroundImage = null;
            this.groupBox3.Controls.Add(this.labelRegPath);
            this.groupBox3.Controls.Add(this.buttonRegBrowse);
            this.groupBox3.Font = null;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // labelRegPath
            // 
            this.labelRegPath.AccessibleDescription = null;
            this.labelRegPath.AccessibleName = null;
            resources.ApplyResources(this.labelRegPath, "labelRegPath");
            this.labelRegPath.AutoEllipsis = true;
            this.labelRegPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelRegPath.Font = null;
            this.labelRegPath.Name = "labelRegPath";
            this.toolTip1.SetToolTip(this.labelRegPath, resources.GetString("labelRegPath.ToolTip"));
            // 
            // buttonRegBrowse
            // 
            this.buttonRegBrowse.AccessibleDescription = null;
            this.buttonRegBrowse.AccessibleName = null;
            resources.ApplyResources(this.buttonRegBrowse, "buttonRegBrowse");
            this.buttonRegBrowse.BackgroundImage = null;
            this.buttonRegBrowse.Font = null;
            this.buttonRegBrowse.Name = "buttonRegBrowse";
            this.toolTip1.SetToolTip(this.buttonRegBrowse, resources.GetString("buttonRegBrowse.ToolTip"));
            this.buttonRegBrowse.UseVisualStyleBackColor = true;
            this.buttonRegBrowse.Click += new System.EventHandler(this.buttonRegBrowse_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = null;
            this.groupBox4.AccessibleName = null;
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.BackgroundImage = null;
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Font = null;
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // tabPageRegisterFb2
            // 
            this.tabPageRegisterFb2.AccessibleDescription = null;
            this.tabPageRegisterFb2.AccessibleName = null;
            resources.ApplyResources(this.tabPageRegisterFb2, "tabPageRegisterFb2");
            this.tabPageRegisterFb2.BackgroundImage = null;
            this.tabPageRegisterFb2.Controls.Add(this.groupBox7);
            this.tabPageRegisterFb2.Font = null;
            this.tabPageRegisterFb2.Name = "tabPageRegisterFb2";
            this.toolTip1.SetToolTip(this.tabPageRegisterFb2, resources.GetString("tabPageRegisterFb2.ToolTip"));
            this.tabPageRegisterFb2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.AccessibleDescription = null;
            this.groupBox7.AccessibleName = null;
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.BackgroundImage = null;
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.radioButtonNotRegisterFb2);
            this.groupBox7.Controls.Add(this.radioButtonFb2Register);
            this.groupBox7.Font = null;
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox7, resources.GetString("groupBox7.ToolTip"));
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = null;
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // radioButtonNotRegisterFb2
            // 
            this.radioButtonNotRegisterFb2.AccessibleDescription = null;
            this.radioButtonNotRegisterFb2.AccessibleName = null;
            resources.ApplyResources(this.radioButtonNotRegisterFb2, "radioButtonNotRegisterFb2");
            this.radioButtonNotRegisterFb2.BackgroundImage = null;
            this.radioButtonNotRegisterFb2.Font = null;
            this.radioButtonNotRegisterFb2.Name = "radioButtonNotRegisterFb2";
            this.radioButtonNotRegisterFb2.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonNotRegisterFb2, resources.GetString("radioButtonNotRegisterFb2.ToolTip"));
            this.radioButtonNotRegisterFb2.UseVisualStyleBackColor = true;
            // 
            // radioButtonFb2Register
            // 
            this.radioButtonFb2Register.AccessibleDescription = null;
            this.radioButtonFb2Register.AccessibleName = null;
            resources.ApplyResources(this.radioButtonFb2Register, "radioButtonFb2Register");
            this.radioButtonFb2Register.BackgroundImage = null;
            this.radioButtonFb2Register.Font = null;
            this.radioButtonFb2Register.Name = "radioButtonFb2Register";
            this.radioButtonFb2Register.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonFb2Register, resources.GetString("radioButtonFb2Register.ToolTip"));
            this.radioButtonFb2Register.UseVisualStyleBackColor = true;
            // 
            // tabPageRegisterZip
            // 
            this.tabPageRegisterZip.AccessibleDescription = null;
            this.tabPageRegisterZip.AccessibleName = null;
            resources.ApplyResources(this.tabPageRegisterZip, "tabPageRegisterZip");
            this.tabPageRegisterZip.BackgroundImage = null;
            this.tabPageRegisterZip.Controls.Add(this.groupBox8);
            this.tabPageRegisterZip.Font = null;
            this.tabPageRegisterZip.Name = "tabPageRegisterZip";
            this.toolTip1.SetToolTip(this.tabPageRegisterZip, resources.GetString("tabPageRegisterZip.ToolTip"));
            this.tabPageRegisterZip.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.AccessibleDescription = null;
            this.groupBox8.AccessibleName = null;
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.BackgroundImage = null;
            this.groupBox8.Controls.Add(this.checkBoxFb2ZipOnly);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.radioButtonNotRegisterZip);
            this.groupBox8.Controls.Add(this.radioButtonZipRegister);
            this.groupBox8.Font = null;
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox8, resources.GetString("groupBox8.ToolTip"));
            // 
            // checkBoxFb2ZipOnly
            // 
            this.checkBoxFb2ZipOnly.AccessibleDescription = null;
            this.checkBoxFb2ZipOnly.AccessibleName = null;
            resources.ApplyResources(this.checkBoxFb2ZipOnly, "checkBoxFb2ZipOnly");
            this.checkBoxFb2ZipOnly.BackgroundImage = null;
            this.checkBoxFb2ZipOnly.Checked = true;
            this.checkBoxFb2ZipOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFb2ZipOnly.Font = null;
            this.checkBoxFb2ZipOnly.Name = "checkBoxFb2ZipOnly";
            this.toolTip1.SetToolTip(this.checkBoxFb2ZipOnly, resources.GetString("checkBoxFb2ZipOnly.ToolTip"));
            this.checkBoxFb2ZipOnly.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = null;
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // radioButtonNotRegisterZip
            // 
            this.radioButtonNotRegisterZip.AccessibleDescription = null;
            this.radioButtonNotRegisterZip.AccessibleName = null;
            resources.ApplyResources(this.radioButtonNotRegisterZip, "radioButtonNotRegisterZip");
            this.radioButtonNotRegisterZip.BackgroundImage = null;
            this.radioButtonNotRegisterZip.Font = null;
            this.radioButtonNotRegisterZip.Name = "radioButtonNotRegisterZip";
            this.radioButtonNotRegisterZip.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonNotRegisterZip, resources.GetString("radioButtonNotRegisterZip.ToolTip"));
            this.radioButtonNotRegisterZip.UseVisualStyleBackColor = true;
            // 
            // radioButtonZipRegister
            // 
            this.radioButtonZipRegister.AccessibleDescription = null;
            this.radioButtonZipRegister.AccessibleName = null;
            resources.ApplyResources(this.radioButtonZipRegister, "radioButtonZipRegister");
            this.radioButtonZipRegister.BackgroundImage = null;
            this.radioButtonZipRegister.Font = null;
            this.radioButtonZipRegister.Name = "radioButtonZipRegister";
            this.radioButtonZipRegister.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonZipRegister, resources.GetString("radioButtonZipRegister.ToolTip"));
            this.radioButtonZipRegister.UseVisualStyleBackColor = true;
            // 
            // tabPageRegisterRar
            // 
            this.tabPageRegisterRar.AccessibleDescription = null;
            this.tabPageRegisterRar.AccessibleName = null;
            resources.ApplyResources(this.tabPageRegisterRar, "tabPageRegisterRar");
            this.tabPageRegisterRar.BackgroundImage = null;
            this.tabPageRegisterRar.Controls.Add(this.groupBox9);
            this.tabPageRegisterRar.Font = null;
            this.tabPageRegisterRar.Name = "tabPageRegisterRar";
            this.toolTip1.SetToolTip(this.tabPageRegisterRar, resources.GetString("tabPageRegisterRar.ToolTip"));
            this.tabPageRegisterRar.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.AccessibleDescription = null;
            this.groupBox9.AccessibleName = null;
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.BackgroundImage = null;
            this.groupBox9.Controls.Add(this.checkBoxFb2RarOnly);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.radioButtonNotRegisterRar);
            this.groupBox9.Controls.Add(this.radioButtonRarRegister);
            this.groupBox9.Font = null;
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox9, resources.GetString("groupBox9.ToolTip"));
            // 
            // checkBoxFb2RarOnly
            // 
            this.checkBoxFb2RarOnly.AccessibleDescription = null;
            this.checkBoxFb2RarOnly.AccessibleName = null;
            resources.ApplyResources(this.checkBoxFb2RarOnly, "checkBoxFb2RarOnly");
            this.checkBoxFb2RarOnly.BackgroundImage = null;
            this.checkBoxFb2RarOnly.Checked = true;
            this.checkBoxFb2RarOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFb2RarOnly.Font = null;
            this.checkBoxFb2RarOnly.Name = "checkBoxFb2RarOnly";
            this.toolTip1.SetToolTip(this.checkBoxFb2RarOnly, resources.GetString("checkBoxFb2RarOnly.ToolTip"));
            this.checkBoxFb2RarOnly.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = null;
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // radioButtonNotRegisterRar
            // 
            this.radioButtonNotRegisterRar.AccessibleDescription = null;
            this.radioButtonNotRegisterRar.AccessibleName = null;
            resources.ApplyResources(this.radioButtonNotRegisterRar, "radioButtonNotRegisterRar");
            this.radioButtonNotRegisterRar.BackgroundImage = null;
            this.radioButtonNotRegisterRar.Font = null;
            this.radioButtonNotRegisterRar.Name = "radioButtonNotRegisterRar";
            this.radioButtonNotRegisterRar.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonNotRegisterRar, resources.GetString("radioButtonNotRegisterRar.ToolTip"));
            this.radioButtonNotRegisterRar.UseVisualStyleBackColor = true;
            // 
            // radioButtonRarRegister
            // 
            this.radioButtonRarRegister.AccessibleDescription = null;
            this.radioButtonRarRegister.AccessibleName = null;
            resources.ApplyResources(this.radioButtonRarRegister, "radioButtonRarRegister");
            this.radioButtonRarRegister.BackgroundImage = null;
            this.radioButtonRarRegister.Font = null;
            this.radioButtonRarRegister.Name = "radioButtonRarRegister";
            this.radioButtonRarRegister.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonRarRegister, resources.GetString("radioButtonRarRegister.ToolTip"));
            this.radioButtonRarRegister.UseVisualStyleBackColor = true;
            // 
            // tabPageRegisterFinish
            // 
            this.tabPageRegisterFinish.AccessibleDescription = null;
            this.tabPageRegisterFinish.AccessibleName = null;
            resources.ApplyResources(this.tabPageRegisterFinish, "tabPageRegisterFinish");
            this.tabPageRegisterFinish.BackgroundImage = null;
            this.tabPageRegisterFinish.Controls.Add(this.groupBox10);
            this.tabPageRegisterFinish.Font = null;
            this.tabPageRegisterFinish.Name = "tabPageRegisterFinish";
            this.toolTip1.SetToolTip(this.tabPageRegisterFinish, resources.GetString("tabPageRegisterFinish.ToolTip"));
            this.tabPageRegisterFinish.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.AccessibleDescription = null;
            this.groupBox10.AccessibleName = null;
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.BackgroundImage = null;
            this.groupBox10.Controls.Add(this.label7);
            this.groupBox10.Controls.Add(this.textBoxResults);
            this.groupBox10.Font = null;
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox10, resources.GetString("groupBox10.ToolTip"));
            // 
            // label7
            // 
            this.label7.AccessibleDescription = null;
            this.label7.AccessibleName = null;
            resources.ApplyResources(this.label7, "label7");
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = null;
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // textBoxResults
            // 
            this.textBoxResults.AcceptsReturn = true;
            this.textBoxResults.AccessibleDescription = null;
            this.textBoxResults.AccessibleName = null;
            resources.ApplyResources(this.textBoxResults, "textBoxResults");
            this.textBoxResults.BackgroundImage = null;
            this.textBoxResults.Font = null;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxResults, resources.GetString("textBoxResults.ToolTip"));
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AccessibleDescription = null;
            this.tableLayoutPanel2.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.BackgroundImage = null;
            this.tableLayoutPanel2.Controls.Add(this.buttonPrev, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonNext, 1, 0);
            this.tableLayoutPanel2.Font = null;
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(0, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.toolTip1.SetToolTip(this.tableLayoutPanel2, resources.GetString("tableLayoutPanel2.ToolTip"));
            // 
            // buttonPrev
            // 
            this.buttonPrev.AccessibleDescription = null;
            this.buttonPrev.AccessibleName = null;
            resources.ApplyResources(this.buttonPrev, "buttonPrev");
            this.buttonPrev.BackgroundImage = null;
            this.buttonPrev.Font = null;
            this.buttonPrev.Name = "buttonPrev";
            this.toolTip1.SetToolTip(this.buttonPrev, resources.GetString("buttonPrev.ToolTip"));
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.AccessibleDescription = null;
            this.buttonNext.AccessibleName = null;
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.BackgroundImage = null;
            this.buttonNext.Font = null;
            this.buttonNext.Name = "buttonNext";
            this.toolTip1.SetToolTip(this.buttonNext, resources.GetString("buttonNext.ToolTip"));
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // MainForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = null;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabWizard.ResumeLayout(false);
            this.tabPageInitial.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPageUnregisterFinish.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tabPageRegisterBrowse.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabPageRegisterFb2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPageRegisterZip.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPageRegisterRar.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tabPageRegisterFinish.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WizardPagesControl tabWizard;
        private System.Windows.Forms.TabPage tabPageInitial;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.RadioButton radioButtonUnregister;
        private System.Windows.Forms.RadioButton radioButtonRegister;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxAny;
        private System.Windows.Forms.Button buttonUnSelectAll;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.CheckBox checkBoxRar;
        private System.Windows.Forms.CheckBox checkBoxZip;
        private System.Windows.Forms.CheckBox checkBoxFb2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label dllPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TabPage tabPageUnregisterFinish;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageRegisterBrowse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelRegPath;
        private System.Windows.Forms.Button buttonRegBrowse;
        private System.Windows.Forms.TabPage tabPageRegisterFb2;
        private System.Windows.Forms.TabPage tabPageRegisterZip;
        private System.Windows.Forms.TabPage tabPageRegisterRar;
        private System.Windows.Forms.TabPage tabPageRegisterFinish;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton radioButtonNotRegisterFb2;
        private System.Windows.Forms.RadioButton radioButtonFb2Register;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButtonNotRegisterZip;
        private System.Windows.Forms.RadioButton radioButtonZipRegister;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButtonNotRegisterRar;
        private System.Windows.Forms.RadioButton radioButtonRarRegister;
        private System.Windows.Forms.RadioButton radioButtonChange;
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxFb2ZipOnly;
        private System.Windows.Forms.CheckBox checkBoxFb2RarOnly;


    }
}