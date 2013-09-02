namespace Fb2ePubGui
{
    partial class NewUpdateMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewUpdateMessage));
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLink = new System.Windows.Forms.Label();
            this.textBoxCurrentVersion = new System.Windows.Forms.TextBox();
            this.textBoxServerVersion = new System.Windows.Forms.TextBox();
            this.linkLabelPath = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.toolTip1.SetToolTip(this.buttonClose, resources.GetString("buttonClose.ToolTip"));
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelCurrent
            // 
            resources.ApplyResources(this.labelCurrent, "labelCurrent");
            this.labelCurrent.Name = "labelCurrent";
            this.toolTip1.SetToolTip(this.labelCurrent, resources.GetString("labelCurrent.ToolTip"));
            // 
            // labelServer
            // 
            resources.ApplyResources(this.labelServer, "labelServer");
            this.labelServer.Name = "labelServer";
            this.toolTip1.SetToolTip(this.labelServer, resources.GetString("labelServer.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // labelLink
            // 
            resources.ApplyResources(this.labelLink, "labelLink");
            this.labelLink.Name = "labelLink";
            this.toolTip1.SetToolTip(this.labelLink, resources.GetString("labelLink.ToolTip"));
            // 
            // textBoxCurrentVersion
            // 
            resources.ApplyResources(this.textBoxCurrentVersion, "textBoxCurrentVersion");
            this.textBoxCurrentVersion.Name = "textBoxCurrentVersion";
            this.textBoxCurrentVersion.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxCurrentVersion, resources.GetString("textBoxCurrentVersion.ToolTip"));
            // 
            // textBoxServerVersion
            // 
            resources.ApplyResources(this.textBoxServerVersion, "textBoxServerVersion");
            this.textBoxServerVersion.Name = "textBoxServerVersion";
            this.textBoxServerVersion.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxServerVersion, resources.GetString("textBoxServerVersion.ToolTip"));
            // 
            // linkLabelPath
            // 
            resources.ApplyResources(this.linkLabelPath, "linkLabelPath");
            this.linkLabelPath.Name = "linkLabelPath";
            this.linkLabelPath.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelPath, resources.GetString("linkLabelPath.ToolTip"));
            this.linkLabelPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelPath_LinkClicked);
            // 
            // NewUpdateMessage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelPath);
            this.Controls.Add(this.textBoxServerVersion);
            this.Controls.Add(this.textBoxCurrentVersion);
            this.Controls.Add(this.labelLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.buttonClose);
            this.Name = "NewUpdateMessage";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLink;
        private System.Windows.Forms.TextBox textBoxCurrentVersion;
        private System.Windows.Forms.TextBox textBoxServerVersion;
        private System.Windows.Forms.LinkLabel linkLabelPath;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}