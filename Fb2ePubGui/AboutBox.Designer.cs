namespace Fb2ePubGui
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxNameAndVersion = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxLogPath = new System.Windows.Forms.TextBox();
            this.textBoxSettingsPath = new System.Windows.Forms.TextBox();
            this.textBoxResourcePath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.linkLabelAbout, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // linkLabelAbout
            // 
            resources.ApplyResources(this.linkLabelAbout, "linkLabelAbout");
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAbout_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNameAndVersion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Fb2ePubGui.Properties.Resources.epub_logo_color_book;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // textBoxNameAndVersion
            // 
            resources.ApplyResources(this.textBoxNameAndVersion, "textBoxNameAndVersion");
            this.textBoxNameAndVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxNameAndVersion, 3);
            this.textBoxNameAndVersion.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxNameAndVersion.Name = "textBoxNameAndVersion";
            this.textBoxNameAndVersion.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBoxLogPath, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxSettingsPath, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxResourcePath, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // textBoxLogPath
            // 
            resources.ApplyResources(this.textBoxLogPath, "textBoxLogPath");
            this.textBoxLogPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.textBoxLogPath, 3);
            this.textBoxLogPath.Name = "textBoxLogPath";
            this.textBoxLogPath.ReadOnly = true;
            // 
            // textBoxSettingsPath
            // 
            resources.ApplyResources(this.textBoxSettingsPath, "textBoxSettingsPath");
            this.textBoxSettingsPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.textBoxSettingsPath, 3);
            this.textBoxSettingsPath.Name = "textBoxSettingsPath";
            this.textBoxSettingsPath.ReadOnly = true;
            // 
            // textBoxResourcePath
            // 
            resources.ApplyResources(this.textBoxResourcePath, "textBoxResourcePath");
            this.tableLayoutPanel2.SetColumnSpan(this.textBoxResourcePath, 3);
            this.textBoxResourcePath.Name = "textBoxResourcePath";
            this.textBoxResourcePath.ReadOnly = true;
            // 
            // AboutBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxNameAndVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxLogPath;
        private System.Windows.Forms.TextBox textBoxSettingsPath;
        private System.Windows.Forms.TextBox textBoxResourcePath;
    }
}
