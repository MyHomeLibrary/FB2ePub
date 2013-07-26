namespace Fb2ePubGui
{
    partial class LogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
            this.richTextBoxLogData = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxLogData
            // 
            this.richTextBoxLogData.CausesValidation = false;
            this.richTextBoxLogData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLogData.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxLogData.Name = "richTextBoxLogData";
            this.richTextBoxLogData.ReadOnly = true;
            this.richTextBoxLogData.Size = new System.Drawing.Size(909, 582);
            this.richTextBoxLogData.TabIndex = 0;
            this.richTextBoxLogData.Text = "";
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(909, 582);
            this.Controls.Add(this.richTextBoxLogData);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 300);
            this.Name = "LogWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogWindow";
            this.Load += new System.EventHandler(this.LogWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLogData;
    }
}