using Fb2ePubGui.UpdateSettingsControl;

namespace Fb2ePubGui
{
    partial class FB2epubGUISettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FB2epubGUISettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageUpdate = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.updateSettings = new UpdateSettings();
            this.groupBox1.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControlSettings);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageUpdate);
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControlSettings, resources.GetString("tabControlSettings.ToolTip"));
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.Controls.Add(this.updateSettings);
            resources.ApplyResources(this.tabPageUpdate, "tabPageUpdate");
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.toolTip1.SetToolTip(this.tabPageUpdate, resources.GetString("tabPageUpdate.ToolTip"));
            this.tabPageUpdate.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.toolTip1.SetToolTip(this.buttonSave, resources.GetString("buttonSave.ToolTip"));
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // updteSettings1
            // 
            resources.ApplyResources(this.updateSettings, "updteSettings1");
            this.updateSettings.Name = "updteSettings1";
            // 
            // FB2epubGUISettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FB2epubGUISettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.groupBox1.ResumeLayout(false);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageUpdate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageUpdate;
        private System.Windows.Forms.ToolTip toolTip1;
        private UpdateSettings updateSettings;
    }
}