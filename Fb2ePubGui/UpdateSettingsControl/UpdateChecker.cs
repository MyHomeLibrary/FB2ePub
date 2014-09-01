using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Fb2ePubGui.Properties;

namespace Fb2ePubGui.UpdateSettingsControl
{
    /// <summary>
    /// Contains possible result of new version detection
    /// </summary>
    internal enum UpdateCheckResult
    {
        CheckError, // Error during version detection
        UpdatePresent, // New version present 
        CurrentVersion, // the version on sever is the same as the running one
    }


    /// <summary>
    /// This class used to check for updated version presence on the server
    /// </summary>
    internal class UpdateChecker
    {
        /// <summary>
        /// Path to default location where the file describing update located (to download from)
        /// </summary>
        const string UpdateFilePath = @"http://www.fb2epub.net/files/latest_ver.xml";

        private readonly Version _assemblyVersion = null;
        private readonly Form _baseWindow = null;

        private string _urlVersionUpdatePath;
        private Version _serverVersion;

        /// <summary>
        /// Returns path to the version update page
        /// </summary>
        public string UpdatePath
        {
            get { return _urlVersionUpdatePath; }
        }

        /// <summary>
        /// Returns current assembly version
        /// </summary>
        public Version CurrentVersion
        {
            get { return _assemblyVersion; }
        }

        /// <summary>
        /// Returns version detected on server
        /// </summary>
        public Version UpdateVersion
        {
            get { return _serverVersion; }
        }


        public UpdateChecker(Form baseWindow)
        {
            // store parent window
            _baseWindow = baseWindow;
            // detect current assembly version
            Assembly asm = Assembly.GetAssembly(GetType());
            _serverVersion = _assemblyVersion = asm.GetName().Version;
            // load default update page based on current language
            _urlVersionUpdatePath =  Resources.UpdateURL;
                    
        }

        /// <summary>
        /// Performs a check for update 
        /// </summary>
        /// <param name="showErrorMessages">show or not error messages</param>
        /// <returns>if update present</returns>
        public UpdateCheckResult CheckForUpdate(bool showErrorMessages)
        {
            // we going to download into Internet cache folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            // make sure file name unique
            string fileName = string.Format(@"{0}\fb2epub_latest_{1}.xml", path, DateTime.Now.Ticks);

            try
            {
                using (var client = new WebClient())
                {

                    // download version file from server
                    client.DownloadFile(UpdateFilePath,
                        fileName);
                }
            }
            catch (WebException webEx)
            {
                Program.Log.Error("Error obtaining version file", webEx);
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
                Program.Log.Error("Error obtaining version file", ex);
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
                // update last time we checked for version
                // only update here if internet was available and file downloaded
                Settings.Default.LastUpdateTime = DateTime.Now;
                Settings.Default.Save();
                // try to read and parse downloaded version file
                XDocument versionDocument = XDocument.Load(fileName);
                if (versionDocument.Root == null)
                {
                    throw new Exception("Document's root is NULL (empty document passed)");
                }
                // read top  level version node (everything should be defined inside "version")
                XElement xVersion = versionDocument.Element(versionDocument.Root.GetDefaultNamespace() + "version");
                if (xVersion == null)
                {
                    throw  new Exception("Invalid version file format - \"version\" element not present");
                }
                int versionMajorValue = 0;
                // readout major version
                GetElementValue(xVersion,"major",out versionMajorValue);
                int versionMinorValue = 0;
                // readout minor version
                GetElementValue(xVersion,  "minor", out versionMinorValue);
                int versionBuildValue = 0;
                // readout build
                GetElementValue(xVersion,  "build", out versionBuildValue);
                int versionRevisionValue = 0;
                // readout revision
                GetElementValue(xVersion,  "revision", out versionRevisionValue);
                // generate version object out of values we read from file
                _serverVersion = new Version(versionMajorValue,versionMinorValue,versionBuildValue,versionRevisionValue);
                if (_serverVersion.CompareTo(_assemblyVersion) == 0) // if same version as of current assembly
                {
                    return UpdateCheckResult.CurrentVersion; // return that version is same
                }
                // if version different
                GetUpdatePath(xVersion); // try to read update page path from XML file we downloaded
                return UpdateCheckResult.UpdatePresent;
            }
            catch (Exception ex)
            {
                Program.Log.Error("Error loading downloaded version file", ex);
                if (showErrorMessages)
                {
                    MessageBox.Show(_baseWindow, ex.Message,
                        Resources.UpdateChecker_CheckForUpdate_Error_loading_downloaded_version_file,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return UpdateCheckResult.CheckError;
            }
        }

        /// <summary>
        /// Attempts to load update path from downloaded update XML file
        /// if not present - default stay
        /// </summary>
        /// <param name="xVersion">"version" top level nodde</param>
        private void GetUpdatePath(XElement xVersion)
        {
            if (xVersion == null)
            {
                return;
            }
            // get "paths" node containing all paths for all languges
            XElement xPathElement = xVersion.Element(xVersion.GetDefaultNamespace() + "paths");
            if (xPathElement == null)
            {
                return;
            }
            // iterate over all "update_path" nodes (each path defines one path for one language)
            IEnumerable<XElement> xPaths = xPathElement.Elements(xVersion.GetDefaultNamespace() + "update_path");
            foreach (var xPath in xPaths)
            {
                string lang;
                string path;
                // reads path and language
                LoadUpdatePathFromElement(xPath,  out lang, out path);
                if (!string.IsNullOrEmpty(path)) // if path not empty
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

        /// <summary>
        /// Loads (Reads) path and language from one "upadate_path" node
        /// </summary>
        /// <param name="xPath">"update_path" node</param>
        /// <param name="lang">out - returned language</param>
        /// <param name="path">out - returned path</param>
        private void LoadUpdatePathFromElement(XElement xPath, out string lang, out string path)
        {
            lang = string.Empty;
            path = _urlVersionUpdatePath; // make sure it's not changed (by top level function call) in case we exit 
            if (xPath == null)
            {
                return;
            }
            XElement xLang = xPath.Element(xPath.GetDefaultNamespace() + "lang"); // read out language
            if (xLang != null)
            {
                lang = xLang.Value;
            }
            XElement xUrl = xPath.Element(xPath.GetDefaultNamespace() + "path"); // read out path
            if (xUrl != null)
            {
                path = xUrl.Value;
            }
        }

        /// <summary>
        /// Reads integer value from the element's sub-node
        /// </summary>
        /// <param name="xVersion"></param>
        /// <param name="elementName"></param>
        /// <param name="versionValue"></param>
        private void GetElementValue(XElement xVersion, string elementName, out int versionValue)
        {
            // retrieve element
            XElement xNodeElement = xVersion.Element(xVersion.GetDefaultNamespace() + elementName);
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

        /// <summary>
        /// Used to decide , based one settings , if update needed
        /// </summary>
        /// <returns>if we need to perfor autoupdate</returns>
        public static bool IsNeedToUpdate()
        {
            if (!Settings.Default.PerformAutoupdate) // if autoupdate disabled
            {
                return false;
            }
            bool needToAutoUpdate = false; // default value
            switch (Settings.Default.AutoUpdateFreqCheck) // check how often we need to update
            {
                case AutoUpdateFreqCheckTimeSlice.EveryRun: // if every run then we do need to perform autoupdate
                    needToAutoUpdate = true;
                    break;
                case AutoUpdateFreqCheckTimeSlice.OnceADay: // if once a day
                    if (Settings.Default.LastUpdateTime.Date != DateTime.Now.Date) // just check that date is different
                    {
                        needToAutoUpdate = true;
                    }
                    break;
                case AutoUpdateFreqCheckTimeSlice.OnceAWeek: // if once a week 
                    TimeSpan dif = DateTime.Now - Settings.Default.LastUpdateTime;
                    if (dif.Days >= 7) // 7 days ( aweek) passed
                    {
                        needToAutoUpdate = true;
                    }
                    break;
                case AutoUpdateFreqCheckTimeSlice.OnceAMonths: // if once a month
                    if (Settings.Default.LastUpdateTime.Month != DateTime.Now.Month) // if different month number then enough
                    {
                        needToAutoUpdate = true;
                    }
                    break;
            }
            return needToAutoUpdate;
        }
    }
}
