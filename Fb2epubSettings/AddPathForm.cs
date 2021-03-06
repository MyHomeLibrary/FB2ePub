﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fb2epubSettings
{
    public partial class AddPathForm : Form
    {
        public AddPathForm()
        {
            InitializeComponent();
        }

        public string PathName { get; set; }

        public string PathFolder { get; set; }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            PathName = textBoxAddName.Text;
            PathFolder = textBoxAddPath.Text;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = Resources.Fb2epubSettings.AddPathForm_buttonSelect_Click_Please_select_a_destination_folder;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = true;
            if ( folderBrowserDialog.ShowDialog(this) == DialogResult.OK )
            {
                textBoxAddPath.Text = folderBrowserDialog.SelectedPath;
                buttonOK.Enabled = !string.IsNullOrEmpty(textBoxAddPath.Text);
            }
        }


        private void AddPath_Load(object sender, EventArgs e)
        {
            textBoxAddName.Text = PathName;
            textBoxAddPath.Text = PathFolder;
            buttonOK.Enabled = !string.IsNullOrEmpty(textBoxAddPath.Text);
        }
    }
}
