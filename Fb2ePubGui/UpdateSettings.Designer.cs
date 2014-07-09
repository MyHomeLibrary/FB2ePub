namespace Fb2ePubGui
{
    partial class UpdateSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxUpdateFreq = new System.Windows.Forms.ComboBox();
            this.checkBoxAutomaticUpdate = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxUpdateFreq);
            this.groupBox1.Controls.Add(this.checkBoxAutomaticUpdate);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBoxUpdateFreq
            // 
            this.comboBoxUpdateFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpdateFreq.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxUpdateFreq, "comboBoxUpdateFreq");
            this.comboBoxUpdateFreq.Name = "comboBoxUpdateFreq";
            // 
            // checkBoxAutomaticUpdate
            // 
            resources.ApplyResources(this.checkBoxAutomaticUpdate, "checkBoxAutomaticUpdate");
            this.checkBoxAutomaticUpdate.Name = "checkBoxAutomaticUpdate";
            this.checkBoxAutomaticUpdate.UseVisualStyleBackColor = true;
            // 
            // UpdateSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UpdateSettings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxAutomaticUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxUpdateFreq;
    }
}
