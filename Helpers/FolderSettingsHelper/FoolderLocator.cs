using System;
using Microsoft.Win32;

namespace FolderSettingsHelper
{
    static public class FolderLocator
    {
        static public string GetLocalAppDataFolder()
        {
            // first try to get folder "Standard way" as it works in Vista and up
            string vistaAndUpFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (!string.IsNullOrEmpty(vistaAndUpFolder))
            {
                return vistaAndUpFolder;
            }
            // if standard way failed - try to read registry
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders",RegistryKeyPermissionCheck.ReadSubTree))
            {
                if (key != null)
                {
                    string folder = (string)key.GetValue("Local AppData");
                    if (!string.IsNullOrEmpty(folder))
                    {
                        return folder;
                    }
                }  
            }
            return Environment.GetEnvironmentVariable("TEMP");
        }
    }
}
