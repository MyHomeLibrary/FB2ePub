using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;

namespace RegisterFB2EPub
{
    internal class ExtRegistrator
    {
        [Flags]
        public enum RegistrationExtensionEnum
        {
            None = 0,
            Fb2 =1,
            Zip = 2,
            Rar = 4,
            BaseSet = Fb2 | Zip | Rar,
            Any = 8,
            All = Fb2 | Zip | Rar | Any,
        }
        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        private const string ExtensionGuid = "{5FFF3DF0-E213-4CA6-A37E-0C3BA1FE35FC}";
        private const string DllGuid = "{4A62D35B-DFF9-4901-A0EF-397236523B33}";
        private const string ExtensionName = "Fb2EpubShlExt";
        private const string ExtensionClassName = "FB2Epub Class";
        //private const string FbeExportPluginGuid = "{469E5867-292A-4A8D-B094-5F3597C4B353}";

        /// <summary>
        /// Path to extension DLL file to register
        /// </summary>
        public string RegistrationPath { get; set; }

        /// <summary>
        /// Checks the current registration 
        /// </summary>
        /// <returns></returns>
        public RegistrationExtensionEnum GetRegistrationState()
        {
            RegistrationExtensionEnum state = RegistrationExtensionEnum.None;
            if (!IsRegistered(RegistrationExtensionEnum.None))
            {
                return RegistrationExtensionEnum.None;
            }
            if (IsRegistered(RegistrationExtensionEnum.Fb2))
            {
                state |= RegistrationExtensionEnum.Fb2;
            }
            if (IsRegistered(RegistrationExtensionEnum.Zip))
            {
                state |= RegistrationExtensionEnum.Zip;
            }
            if (IsRegistered(RegistrationExtensionEnum.Rar))
            {
                state |= RegistrationExtensionEnum.Rar;
            }
            if (IsRegistered(RegistrationExtensionEnum.Any))
            {
                state |= RegistrationExtensionEnum.Any;
            }

            return state;
        }

        public bool IsRegistered(RegistrationExtensionEnum regisrationType)
        {
            if (!IsInAprovedList())
            {
                return false;
            }
            if (!IsComRegistered())
            {
                return false;
            }
            if ((regisrationType & RegistrationExtensionEnum.Fb2) == RegistrationExtensionEnum.Fb2 && !IsExtensionRegistered(RegistrationExtensionEnum.Fb2))
            {
                return false;
            }
            if ((regisrationType & RegistrationExtensionEnum.Zip) == RegistrationExtensionEnum.Zip && !IsExtensionRegistered(RegistrationExtensionEnum.Zip))
            {
                return false;
            }
            if ((regisrationType & RegistrationExtensionEnum.Rar) == RegistrationExtensionEnum.Rar && !IsExtensionRegistered(RegistrationExtensionEnum.Rar))
            {
                return false;
            }
            if ((regisrationType & RegistrationExtensionEnum.Any) == RegistrationExtensionEnum.Any && !IsExtensionRegistered(RegistrationExtensionEnum.Any))
            {
                return false;
            }

            return true;
        }

        private static bool IsComRegistered()
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(string.Format("AppID\\{0}",DllGuid)))
            {
                if (key == null)
                {
                    return false;
                }
            }
            using (RegistryKey lmKey = Registry.LocalMachine.OpenSubKey(string.Format("Software\\Classes\\CLSID\\{0}",ExtensionGuid)))
            {
                if (lmKey == null)
                {
                    return false;
                }
                string name = (string)lmKey.GetValue(null);
                if (string.Compare(ExtensionClassName,name,false) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Converts extension type enum to corresponding string
        /// </summary>
        /// <param name="registrationExtensionEnum"></param>
        /// <returns></returns>
        private static string ConvertEnumToString(RegistrationExtensionEnum registrationExtensionEnum)
        {
            string res = string.Empty;
            switch (registrationExtensionEnum)
            {
                case RegistrationExtensionEnum.Fb2:
                    return ".fb2";
                case RegistrationExtensionEnum.Zip:
                    return ".zip";
                case RegistrationExtensionEnum.Rar:
                    return ".rar";
                case RegistrationExtensionEnum.Any:
                    return "*";
            }
            return res;
        }

        /// <summary>
        /// Check if speciffic extensoin registered
        /// </summary>
        /// <param name="registrationExtensionEnum"></param>
        /// <returns></returns>
        private static bool IsExtensionRegistered(RegistrationExtensionEnum registrationExtensionEnum)
        {
            return IsExtensionRegistered(ConvertEnumToString(registrationExtensionEnum),registrationExtensionEnum != RegistrationExtensionEnum.Any);
        }

        private static bool IsExtensionRegistered(string extension, bool followRedirection)
        {
            if (string.IsNullOrEmpty(extension)) // if input invalid - not registered
            {
                return false;
            }
            string subTreeName;
            // Check extension brench
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension))
            {
                if (key == null)
                {
                    return false;
                }
                // read the brench control will be registered to
                subTreeName = (string)key.GetValue(null);
                if (followRedirection &&  string.IsNullOrEmpty(subTreeName))
                {
                    return false;
                }
                if (!followRedirection)
                {
                    subTreeName = extension;
                }
            }
            // check control registration brench
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(string.Format("{0}\\shellex\\ContextMenuHandlers\\{1}", subTreeName, ExtensionName)))
            {
                if (key == null)
                {
                    return false;
                }
                string registeredClass = (string) key.GetValue(null);
                if (string.IsNullOrEmpty(registeredClass))
                {
                    return false;
                }
                if (string.Compare(registeredClass, ExtensionGuid,true) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsInAprovedList()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved", false))
            {
                if (key == null)
                {
                    return false;
                }
                bool detected = false;
                foreach (var name in key.GetValueNames())
                {
                    if (string.Compare(name, ExtensionGuid, true) == 0)
                    {
                        detected = true;
                        break;
                    }
                }
                if (!detected)
                {
                    return false;
                }
            }
            return true;           
        }


        public void Unregister()
        {
            Unregister(RegistrationExtensionEnum.All);
            //UnregisterFbeExtension();

        }

        //private void UnregisterFbeExtension()
        //{
        //    try
        //    {
        //        using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
        //        {
        //            key.DeleteSubKeyTree(string.Format(@"Software\Haali\FBE\Plugins\{0}", FbeExportPluginGuid),false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Log.Error(ex);
        //    }
        //}

        public void Unregister(RegistrationExtensionEnum regType)
        {
            try
            {
                if ((regType & RegistrationExtensionEnum.Any) == RegistrationExtensionEnum.Any)
                {
                    UnregisterFileExtension("*",false);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error unregistering * shell extension: {0}", ex));
            }

            try
            {
                if ((regType & RegistrationExtensionEnum.Fb2) == RegistrationExtensionEnum.Fb2 )
                {
                    UnregisterFileExtension(".fb2",true);                    
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error unregistering .fb2 shell extension: {0}", ex));
            }

            try
            {
                if ((regType & RegistrationExtensionEnum.Zip) == RegistrationExtensionEnum.Zip)
                {
                    UnregisterFileExtension(".zip", true);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error unregistering .zip shell extension: {0}", ex));
            }

            try
            {
                if ((regType & RegistrationExtensionEnum.Rar) == RegistrationExtensionEnum.Rar)
                {
                    UnregisterFileExtension(".rar", true);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error unregistering .rar shell extension: {0}", ex));
            }


            if (regType != RegistrationExtensionEnum.All) // if we not unregistering all nothing more to do
            {
                return;
            }

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved", true))
                {
                    if (key != null)
                    {
                        try
                        {
                            key.DeleteSubKey(ExtensionGuid);
                        }
                        catch (ArgumentException)
                        {}
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("error deleting extension GUID key",ex));
                        }
                    }
                }
            }
            catch (DllNotFoundException)
            {
                Log.ErrorFormat(string.Format("Error unregistering shell extension due to missing 'Fb2EpubExt.DLL' - please ensure it located in program folder "));
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error unregistering shell extension: {0}", ex));
            }


            try
            {
                UnRegisterComServer();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error unregistering COM server : {0}", ex));
            }

        }

        /// <summary>
        /// Unregisters DLL as COM server
        /// </summary>
        private static void UnRegisterComServer()
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("AppID", true))
            {
                if (key != null)
                {
                    try
                    {
                        key.DeleteSubKeyTree(DllGuid);
                    }
                    catch(ArgumentException){}
                }
            }
            using (RegistryKey lmKey = Registry.LocalMachine.OpenSubKey("Software\\Classes\\CLSID",true))
            {
                if (lmKey != null)
                {
                    try
                    {
                        lmKey.DeleteSubKeyTree(ExtensionGuid);
                    }
                    catch(ArgumentException){}
                }
            }
        }

        /// <summary>
        /// Unregisters specific extension
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="followRedirection"></param>
        private static void UnregisterFileExtension(string extension,bool followRedirection)
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                if (key == null)
                {
                    return;
                }
                string progId = (string)key.GetValue(null);
                if (followRedirection && string.IsNullOrEmpty(progId))
                {
                    return;
                }
                if (followRedirection)
                {
                    UnregisterContextMenuExtension(progId);                    
                }
                else
                {
                    UnregisterContextMenuExtension(extension);
                }
            }
        }

        /// <summary>
        /// Helper function used in unregistering extensions
        /// </summary>
        /// <param name="id"></param>
        private static void UnregisterContextMenuExtension(string id)
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(id, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                if (key == null)
                {
                    return;
                }
                using (RegistryKey contentKey = key.OpenSubKey("ShellEx\\ContextMenuHandlers", RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (contentKey == null)
                    {
                        return;
                    }
                    try
                    {
                        contentKey.DeleteSubKeyTree(ExtensionName);
                    }
                    catch (ArgumentException){}
                }
            }
        }

        /// <summary>
        /// Register specific file extension
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="registrationName"></param>
        private static void RegisterFileExtension(string extension, string registrationName)
        {
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(extension, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                if (key != null)
                {
                    string extRegName = (string)key.GetValue(null);
                    if (string.IsNullOrEmpty(extRegName))
                    {
                        if (string.IsNullOrEmpty(registrationName))
                        {
                            extRegName = extension; // for cases like .*
                        }
                        else
                        {
                            key.SetValue(null, registrationName);
                            extRegName = registrationName;
                        }
                    }
                    RegisterContextMenuExtension(extRegName);
                }
                else
                {
                    throw new Exception(string.Format("error accessing/creating {0} registry key", extension));
                }
            }

        }

        /// <summary>
        /// Helper function used to register context menu part of extension registration
        /// </summary>
        /// <param name="name"></param>
        private static void RegisterContextMenuExtension(string name)
        {
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                if (key != null)
                {
                    using (RegistryKey shellKey = key.CreateSubKey("ShellEx", RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        if (shellKey != null)
                        {
                            using (RegistryKey contextKey = shellKey.CreateSubKey("ContextMenuHandlers", RegistryKeyPermissionCheck.ReadWriteSubTree))
                            {
                                if (contextKey != null)
                                {
                                    using (RegistryKey progKey = contextKey.CreateSubKey(ExtensionName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        if (progKey != null)
                                        {
                                            progKey.SetValue(null, ExtensionGuid);
                                        }
                                        else
                                        {
                                            throw new Exception("error setting Extension GUID value");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("error creating/accessing ContextMenuHandlers brench");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("error creating/accessing ShellEx brench");
                        }
                    }
                }
                else
                {
                    throw new Exception(string.Format("error creating/accessing {0} brench", name));
                }
            }
        }



        /// <summary>
        /// Register shell extension and specific file extension
        /// </summary>
        /// <param name="regType">file extension type to register</param>
        public  void Register(RegistrationExtensionEnum regType)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved", true))
                {
                    if (key != null)
                    {
                        key.SetValue(ExtensionGuid, string.Format("{0} extension", ExtensionName));
                    }
                }

                RegisterComServer();

                //RegisterFbeExtension();

                if ((regType & RegistrationExtensionEnum.Any) == RegistrationExtensionEnum.Any)
                {
                    RegisterFileExtension("*", "");
                }
                else
                {
                    UnregisterFileExtension("*", false);
                }

                if ((regType & RegistrationExtensionEnum.Fb2) == RegistrationExtensionEnum.Fb2)
                {
                    RegisterFileExtension(".fb2", "FictionBook.2");                    
                }
                else
                {
                    UnregisterFileExtension(".fb2", true);
                }

                if ((regType & RegistrationExtensionEnum.Zip) == RegistrationExtensionEnum.Zip)
                {
                    RegisterFileExtension(".zip", "CompressedFolder");
                }
                else
                {
                    UnregisterFileExtension(".zip", true);
                }

                if ((regType & RegistrationExtensionEnum.Rar) == RegistrationExtensionEnum.Rar)
                {
                    RegisterFileExtension(".rar", "WinRAR");
                }
                else
                {
                    UnregisterFileExtension(".rar", true);
                }


            }
            catch (DllNotFoundException)
            {
                Log.ErrorFormat(string.Format("Error registering shell extension due to missing 'Fb2EpubExt.DLL' - please ensure it located in program folder "));
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved", true);
                if (key != null)
                {
                    try
                    {
                        key.DeleteSubKey(ExtensionGuid);
                    }
                    catch (ArgumentException)
                    { }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat(string.Format("Error registering shell extension: {0}", ex));
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved", true);
                if (key != null)
                {
                    try
                    {
                        key.DeleteSubKey(ExtensionGuid);
                    }
                    catch (ArgumentException)
                    { }
                }
            }
        }

        //private void RegisterFbeExtension()
        //{
        //    try
        //    {
        //        string iconFile = Path.GetDirectoryName(RegistrationPath);
        //        iconFile= Path.Combine(iconFile, "FBE2EpubPlugin.dll");
        //        using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
        //        {
        //            if (key != null)
        //            {
        //                RegistryKey topKey = key.CreateSubKey(string.Format(@"Software\Haali\FBE\Plugins\{0}", FbeExportPluginGuid), RegistryKeyPermissionCheck.ReadWriteSubTree);
        //                if (topKey != null)
        //                {
        //                    topKey.SetValue(string.Empty, "Export Fb2 to ePub");
        //                    topKey.SetValue("Menu", "To ePub");
        //                    topKey.SetValue("Type", "Export");
        //                    topKey.SetValue("Icon", iconFile);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Log.Error(ex);
        //    }

        //}

        /// <summary>
        /// Registers DLL as COM server
        /// </summary>
        private void RegisterComServer()
        {
            if (string.IsNullOrEmpty(RegistrationPath))
            {
                throw new NullReferenceException("Path can not be empty");
            }
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("AppID", true))
            {
                if (key == null)
                {
                    throw new Exception("Unable to open AppID brench");
                }
                using (RegistryKey appSubKey = key.CreateSubKey(DllGuid, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (appSubKey == null)
                    {
                        throw new Exception("Error creating App key");
                    }
                    key.SetValue(null, "Fb2EpubExt");
                }
            }
            using (RegistryKey lmKey = Registry.LocalMachine.OpenSubKey("Software\\Classes\\CLSID",true))
            {
                if (lmKey == null)
                {
                    throw new Exception("Unable to open Software\\Classes\\CLSID brench");
                }
                using (RegistryKey appSubKey = lmKey.CreateSubKey(ExtensionGuid, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (appSubKey == null)
                    {
                        throw new Exception(string.Format("Unable to create {0} registration brench",ExtensionGuid));
                    }
                    appSubKey.SetValue(null,ExtensionClassName);
                    using (RegistryKey apartmentKey = appSubKey.CreateSubKey("InprocServer32", RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        if (apartmentKey == null)
                        {
                            throw new Exception("Unable to create apartment key brench");
                        }
                        apartmentKey.SetValue(null,RegistrationPath);
                        apartmentKey.SetValue("ThreadingModel", "Apartment");
                    }
                }
            }
        }


        public string GetRegistredLocation()
        {
            RegistrationExtensionEnum state = GetRegistrationState();
            if (state != RegistrationExtensionEnum.None)
            {
                using (RegistryKey lmKey = Registry.LocalMachine.OpenSubKey(string.Format("Software\\Classes\\CLSID\\{0}\\InprocServer32", ExtensionGuid), false))
                {
                    if (lmKey != null)
                    {
                        return (string)lmKey.GetValue(null);
                    }
                }
            }

            return string.Empty;
        }
    }
}
