using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Fb2epubSettings.IniLocations
{
    public class IniLocations : List<Location>
    {
        //private readonly List<Location> _listOfLocations = new List<Location>();
        private string _iniPath = string.Empty;


        public bool Init()
        {
            _iniPath = DetectIniLocation();
            if (_iniPath != string.Empty)
            {
                return true;
            }
            return false;
        }

        public void Load()
        {
            if (!string.IsNullOrEmpty(_iniPath))
            {
                string count = IniAccessFunctions.IniReadValue(_iniPath, "TARGETS", "TargetsCount");
                int targetsCount;
                if (int.TryParse(count, out targetsCount) && targetsCount > 0)
                {
                    for (int i = 0; i < targetsCount; i++)
                    {
                        Location location = new Location();
                        location.ReadLocation(_iniPath, i);
                        if (!string.IsNullOrEmpty(location.Path) && Directory.Exists(location.Path))
                        {
                            Add(location);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_iniPath))
            {
                IniAccessFunctions.IniWriteValue(_iniPath, "TARGETS", "TargetsCount", Count.ToString());
                int i = 0;
                foreach (var location in this)
                {
                    location.WriteLocation(_iniPath,i++);
                }
            }
        }

  

        public List<Location> GetGuiLocations()
        {
            return FindAll(x =>x.ShowInGui);
        }

        public List<Location> GetShellLocations()
        {
            return FindAll(x => x.ShowInShell);
        }

        private static string DetectIniLocation()
        {
            string path2Exe = Assembly.GetEntryAssembly().Location;
            string folderPath = Path.GetDirectoryName(path2Exe);
            if (folderPath == null)
            {
                folderPath = string.Empty;
            }
            string path2INI = Path.Combine(folderPath, "FB2EPUBExt.INI");
            if (File.Exists(path2INI))
            {
                return path2INI;
            }

            return string.Empty;
        }


    }
}
