namespace FontsSettings
{
    partial class FontEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontEditControl));
            this.buttonRemoveFont = new System.Windows.Forms.Button();
            this.buttonEditFont = new System.Windows.Forms.Button();
            this.buttonAddFont = new System.Windows.Forms.Button();
            this.listViewFonts = new System.Windows.Forms.ListView();
            this.columnHeaderFont = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonRemoveFont
            // 
            resources.ApplyResources(this.buttonRemoveFont, "buttonRemoveFont");
            this.buttonRemoveFont.Name = "buttonRemoveFont";
            this.buttonRemoveFont.UseVisualStyleBackColor = true;
            this.buttonRemoveFont.Click += new System.EventHandler(this.ButtonRemoveFontClick);
            // 
            // buttonEditFont
            // 
            resources.ApplyResources(this.buttonEditFont, "buttonEditFont");
            this.buttonEditFont.Name = "buttonEditFont";
            this.buttonEditFont.UseVisualStyleBackColor = true;
            this.buttonEditFont.Click += new System.EventHandler(this.ButtonEditFontClick);
            // 
            // buttonAddFont
            // 
            resources.ApplyResources(this.buttonAddFont, "buttonAddFont");
            this.buttonAddFont.Name = "buttonAddFont";
            this.buttonAddFont.UseVisualStyleBackColor = true;
            this.buttonAddFont.Click += new System.EventHandler(this.ButtonAddFontClick);
            // 
            // listViewFonts
            // 
            this.listViewFonts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFont});
            this.listViewFonts.FullRowSelect = true;
            resources.ApplyResources(this.listViewFonts, "listViewFonts");
            this.listViewFonts.MultiSelect = false;
            this.listViewFonts.Name = "listViewFonts";
            this.listViewFonts.ShowGroups = false;
            this.listViewFonts.ShowItemToolTips = true;
            this.listViewFonts.UseCompatibleStateImageBehavior = false;
            this.listViewFonts.View = System.Windows.Forms.View.Details;
            this.listViewFonts.SelectedIndexChanged += new System.EventHandler(this.ListViewFontsSelectedIndexChanged);
            // 
            // columnHeaderFont
            // 
            resources.ApplyResources(this.columnHeaderFont, "columnHeaderFont");
            // 
            // FontEditControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRemoveFont);
            this.Controls.Add(this.buttonEditFont);
            this.Controls.Add(this.buttonAddFont);
            this.Controls.Add(this.listViewFonts);
            this.Name = "FontEditControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRemoveFont;
        private System.Windows.Forms.Button buttonEditFont;
        private System.Windows.Forms.Button buttonAddFont;
        private System.Windows.Forms.ListView listViewFonts;
        private System.Windows.Forms.ColumnHeader columnHeaderFont;
    }
}
