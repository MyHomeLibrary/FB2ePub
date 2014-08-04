using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IEFb2ePubPlugin
{
    [System.Runtime.InteropServices.ComVisible(true)]
    [System.Runtime.InteropServices.Guid("bdb9c34c-d0ca-448e-b497-8de62e709744")]
    public class IEFb2ePubPlugin : NativeMethods.IDownloadManager
    {
        /// <summary>
        /// Return S_OK (0) so that IE will stop to download the file itself. 
        /// Else the default download user interface is used.
        /// </summary>
        public int Download(IMoniker pmk, IBindCtx pbc, uint dwBindVerb, int grfBINDF, 
            IntPtr pBindInfo, string pszHeaders, string pszRedir, uint uiCP)
        {

            // Get the display name of the pointer to an IMoniker interface that specifies
            // the object to be downloaded.
            string name = string.Empty;
            pmk.GetDisplayName(pbc, null, out name);

            if (!string.IsNullOrEmpty(name))
            {
                Uri url = null;
                bool result = Uri.TryCreate(name, UriKind.Absolute, out url);

                if (result)
                {
                    MessageBox.Show(url.ToString());
                    //pmk.BindToStorage(pbc,null,);

                    //// Launch CSWebDownloader.exe to download the file. 
                    //FileInfo assemblyFile = 
                    //    new FileInfo(Assembly.GetExecutingAssembly().Location);
                    //ProcessStartInfo start = new ProcessStartInfo
                    //{
                    //    Arguments = name,
                    //    FileName =
                    //    string.Format("{0}\\CSWebDownloader.exe", assemblyFile.DirectoryName)
                    //};
                    //Process.Start(start);
                    return 0;
                }              
            }
            return 1;
        }



        #region ComRegister Functions

        private const string IeKeyPath = @"SOFTWARE\Microsoft\Internet Explorer\";

        /// <summary>
        /// Called when derived class is registered as a COM server.
        /// </summary>
        [System.Runtime.InteropServices.ComRegisterFunction]
        public static void Register(Type t)
        {
            using (RegistryKey ieKey = Registry.CurrentUser.CreateSubKey(IeKeyPath))
            {
                if (ieKey != null)
                {
                    ieKey.SetValue("DownloadUI", t.GUID.ToString("B"));
                }
            }
        }

        /// <summary>
        /// Called when derived class is unregistered as a COM server.
        /// </summary>
        [System.Runtime.InteropServices.ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            using (RegistryKey ieKey = Registry.CurrentUser.OpenSubKey(IeKeyPath, true))
            {
                if (ieKey != null )
                {
                    if (System.String.Compare(ieKey.GetValue("DownloadUI").ToString(), t.GUID.ToString("B"), System.StringComparison.InvariantCulture) == 0)
                    {
                        ieKey.DeleteValue("DownloadUI");
                    }
                }
            }
        }

        #endregion
    }
}
