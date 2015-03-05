using System;
using System.Reflection;
using System.Windows.Forms;
using Fb2ePubGui.Properties;
using Fb2epubSettings;
using FolderResourcesHelper;
using log4net;

namespace Fb2ePubGui
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            Text = String.Format("About {0}", AssemblyTitle);
            linkLabelAbout.Links.Add(0, linkLabelAbout.Text.Length, @"http://www.fb2epub.net");
            Assembly asm = Assembly.GetAssembly(GetType());
            string version = "???";
            if (asm != null)
            {
                version = asm.GetName().Version.ToString();
            }
            textBoxNameAndVersion.Text = string.Format("FB2ePub GUI converter v{0}", version);
            textBoxLogPath.Text = string.Format(Resources.Log_Folder, GlobalContext.Properties["LogName"]);
            string settingsLocation;
            DefaultSettingsLocatorHelper.SettingsLocation detected;
            DefaultSettingsLocatorHelper.LocateDefaultSettings(out settingsLocation, out detected);
            textBoxSettingsPath.Text = detected != DefaultSettingsLocatorHelper.SettingsLocation.NotDetected ? string.Format(Resources.Default_Settings_File, settingsLocation, detected) : Resources.AboutBox_AboutBox_Default_settings_file_not_found;
            textBoxResourcePath.Text = string.Format(Resources.Using_Resources_From, (ResourceLocator.Instance.GetResourcesPath()));
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((string)e.Link.LinkData); 
        }
    }
}
