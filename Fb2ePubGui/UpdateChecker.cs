using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Fb2ePubGui.Properties;

namespace Fb2ePubGui
{
    internal enum UpdateCheckResult
    {
        CheckError,
        UpdatePresent,
        CurrentVersion,
    }

    internal class UpdateChecker
    {
        private readonly Version _assemblyVersion = null;
        private readonly Form _baseWindow = null;

        private string _urlVersionUpdatePath;
        private Version _serverVersion;

        public string UpdatePath
        {
            get { return _urlVersionUpdatePath; }
        }

        public Version CurrentVersion
        {
            get { return _assemblyVersion; }
        }

        public Version UpdateVersion
        {
            get { return _serverVersion; }
        }


        public UpdateChecker(Form baseWindow)
        {
            _baseWindow = baseWindow;
            Assembly asm = Assembly.GetAssembly(GetType());
            _serverVersion = _assemblyVersion = asm.GetName().Version;
            _urlVersionUpdatePath =  Resources.UpdateURL;
                    
        }

        public UpdateCheckResult CheckForUpdate(bool showErrorMessages)
        {
            const string updateFilePath = @"http://www.fb2epub.net/files/latest_ver.xml";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            string fileName = string.Format(@"{0}\fb2epub_latest_{1}.xml", path, DateTime.Now.Ticks);

            try
            {
                using (var client = new WebClient())
                {

                    client.DownloadFile(updateFilePath,
                        fileName);
                }
            }
            catch (WebException webEx)
            {
                Program.log.Error("Error obtaining version file", webEx);
                if (showErrorMessages)
                {
                    MessageBox.Show(_baseWindow, webEx.Message,
                        Resources.FormGUI_checkForUpdateToolStripMenuItem_Click_Error_downloading_version_file,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return UpdateCheckResult.CheckError;
            }
            catch (Exception ex)
            {
                Program.log.Error("Error obtaining version file", ex);
                if (showErrorMessages)
                {
                    MessageBox.Show(_baseWindow, ex.Message,
                        Resources.FormGUI_checkForUpdateToolStripMenuItem_Click_Error_downloading_version_file,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return UpdateCheckResult.CheckError;
            }
            try
            {
                XDocument versionDocument = XDocument.Load(fileName);
                if (versionDocument.Root == null)
                {
                    throw new Exception("Document's root is NULL (empty document passed)");
                }
                XNamespace fileNameSpace = versionDocument.Root.GetDefaultNamespace();
                XElement xVersion = versionDocument.Element(fileNameSpace + "version");
                if (xVersion == null)
                {
                    throw  new Exception("Invalid version file format - \"version\" element not present");
                }
                int versionMajorValue = 0;
                GetElementValue(xVersion,fileNameSpace,"major",out versionMajorValue);
                int versionMinorValue = 0;
                GetElementValue(xVersion, fileNameSpace, "minor", out versionMinorValue);
                int versionBuildValue = 0;
                GetElementValue(xVersion, fileNameSpace, "build", out versionBuildValue);
                int versionRevisionValue = 0;
                GetElementValue(xVersion, fileNameSpace, "revision", out versionRevisionValue);

                _serverVersion = new Version(versionMajorValue,versionMinorValue,versionBuildValue,versionRevisionValue);
                if (_serverVersion.CompareTo(_assemblyVersion) == 0)
                {
                    return UpdateCheckResult.CurrentVersion;
                }
                GetUpdatePath(xVersion, fileNameSpace);
                return UpdateCheckResult.UpdatePresent;
            }
            catch (Exception ex)
            {
                Program.log.Error("Error loading downloaded version file", ex);
                if (showErrorMessages)
                {
                    MessageBox.Show(_baseWindow, ex.Message,
                        Resources.UpdateChecker_CheckForUpdate_Error_loading_downloaded_version_file,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return UpdateCheckResult.CheckError;
            }
        }

        private void GetUpdatePath(XElement xVersion,XNamespace fileNameSpace)
        {
            if (xVersion == null)
            {
                return;
            }
            XElement xPathElement = xVersion.Element(fileNameSpace + "paths");
            if (xPathElement == null)
            {
                return;
            }
            IEnumerable<XElement> xPaths = xPathElement.Elements(fileNameSpace + "update_path");
            foreach (var xPath in xPaths)
            {
                string lang;
                string path;
                LoadUpdatePathFromElement(xPath, fileNameSpace, out lang, out path);
                if (!string.IsNullOrEmpty(path))
                {
                    if (string.CompareOrdinal(lang, "eng") == 0 || string.IsNullOrEmpty(lang)) // English we load anyway
                    {
                        _urlVersionUpdatePath = path; //set but continue in case we have a proper lang.
                    }
                    // if proper language detected
                    if (string.CompareOrdinal(lang, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName) == 0 ) // English we load anyway
                    {
                        _urlVersionUpdatePath = path; // set and return
                        return;
                    }
                }
            }
        }

        private void LoadUpdatePathFromElement(XElement xPath, XNamespace fileNameSpace, out string lang, out string path)
        {
            lang = string.Empty;
            path = _urlVersionUpdatePath; // make sure it's not changed
            if (xPath == null)
            {
                return;
            }
            XElement xLang = xPath.Element(fileNameSpace + "lang");
            if (xLang != null)
            {
                lang = xLang.Value;
            }
            XElement xUrl = xPath.Element(fileNameSpace + "path");
            if (xUrl != null)
            {
                path = xUrl.Value;
            }
        }

        private void GetElementValue(XElement xVersion, XNamespace fileNameSpace, string elementName, out int versionValue)
        {
            XElement xNodeElement = xVersion.Element(fileNameSpace + elementName);
            if (xNodeElement == null)
            {
                throw new Exception(string.Format("Invalid version file format - \"{0}\" element not present",elementName));
            }
            if (string.IsNullOrEmpty(xNodeElement.Value))
            {
                throw new Exception(string.Format("Invalid version file format - \"{0}\" element is empty",elementName));
            }
            if (!int.TryParse(xNodeElement.Value, out versionValue))
            {
                throw new Exception(string.Format("Invalid version file format - \"{1}\" element contains invalid value of {0}", xNodeElement.Value,elementName));
            }
        }
    }
}
