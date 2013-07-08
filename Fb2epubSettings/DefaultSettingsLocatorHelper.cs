using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fb2epubSettings
{
    public static class DefaultSettingsLocatorHelper
    {
        public enum SettingsLocation
        {
            NotDetected,
            UserAppDataFolder,
            AppDataFolder,
            ProgramFolder,
        };

        /// <summary>
        /// Returns path to the properly located default settings file, if it not there copies it or if nowhere to find - creates a new one
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="defaultSettings"></param>
        public static void EnsureDefaultSettingsFilePresent(out string fileLocation,ConverterSettings defaultSettings)
        {
            SettingsLocation locationDetected = SettingsLocation.NotDetected;
            if (LocateDefaultSettings(out fileLocation,out locationDetected))
            {
                switch (locationDetected)
                {
                    case SettingsLocation.UserAppDataFolder:
                        return;
                    case SettingsLocation.AppDataFolder:
                    case SettingsLocation.ProgramFolder:
                        CopySettingsFileToUserAppDataFolder(fileLocation);
                        return;
                };
            }
            fileLocation    =   GetProperSettingsLocation();
            ConverterSettingsFile settingsFile = new ConverterSettingsFile();
            settingsFile.Settings.CopyFrom(defaultSettings);
            Directory.CreateDirectory(Path.GetDirectoryName(fileLocation));
            settingsFile.Save(fileLocation);
        }

        /// <summary>
        /// Copies settings file to the user path location
        /// </summary>
        /// <param name="fileLocation"></param>
        private static void CopySettingsFileToUserAppDataFolder(string fileLocation)
        {
            string newLocation = GetProperSettingsLocation();
            string folder = Path.GetDirectoryName(newLocation);
            if (folder != null && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.Copy(fileLocation, newLocation);
        }

        public static string GetProperSettingsLocation()
        {
            string detectionFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileLocation = AttachFileSubpath(detectionFolder);
            return fileLocation;
        }

        /// <summary>
        /// Locates default settings file from one of the locations based on search order
        /// </summary>
        /// <param name="fileLocation">location of the file to return</param>
        /// <param name="locationDetected">where file was located</param>
        /// <returns>true if file found, false if not</returns>
        public static bool LocateDefaultSettings(out string fileLocation, out SettingsLocation locationDetected)
        {
            fileLocation = string.Empty;
            locationDetected = SettingsLocation.NotDetected;

            // First look into user profile folder
            fileLocation = GetProperSettingsLocation();
            if (File.Exists(fileLocation))
            {
                locationDetected = SettingsLocation.UserAppDataFolder;
                return true;
            }

            // then if not found - try to detect in Program data folder
            string detectionFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            fileLocation = AttachFileSubpath(detectionFolder);
            if (File.Exists(fileLocation))
            {
                locationDetected = SettingsLocation.AppDataFolder;
                return true;
            }

            detectionFolder = Assembly.GetEntryAssembly().Location;
            detectionFolder = Path.GetDirectoryName(detectionFolder);
            if (detectionFolder != null)
            {
                fileLocation = Path.Combine(detectionFolder, "defsettings.xml");
                if (File.Exists(fileLocation))
                {
                    locationDetected = SettingsLocation.ProgramFolder;
                    return true;
                }
            }


            return false;
        }

        private static string AttachFileSubpath(string rootPath)
        {
            return Path.Combine(rootPath, @"Lord_KiRon\Fb2ePub", "defsettings.xml");
        }
    }
}
