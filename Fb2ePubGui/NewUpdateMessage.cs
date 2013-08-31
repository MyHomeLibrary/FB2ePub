using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fb2ePubGui
{
    public partial class NewUpdateMessage : Form
    {
        public Version VersionNow { get; set; }

        public Version VersionServer { get; set; }

        public string UpdateURl { get; set; }

        public NewUpdateMessage()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            textBoxCurrentVersion.Text = VersionNow.ToString();
            textBoxServerVersion.Text = VersionServer.ToString();
            linkLabelPath.Text = UpdateURl;
            linkLabelPath.Links.Add(0, linkLabelPath.Text.Length, UpdateURl);
            base.OnLoad(e);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabelPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((string)e.Link.LinkData); 
        }
    }
}
