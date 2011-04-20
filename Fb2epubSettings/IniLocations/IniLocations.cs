using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Fb2epubSettings.IniLocations
{
    public class IniLocations : IEnumerable<Location>
    {
        private readonly List<Location> _listOfLocations = new List<Location>();


        public IniLocations()
        {
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                              "My Books\\");
            if (!Directory.Exists(defaultPath))
            {
                try
                {
                    Directory.CreateDirectory(defaultPath);
                }
                catch (Exception)
                {

                }
            }
            _listOfLocations.Add(new Location {Path = defaultPath,ShowInGui = true});
            string iniPath = DetectIniLocation();
            if (iniPath != string.Empty)
            {
                string count = IniAccessFunctions.IniReadValue(iniPath, "TARGETS", "TargetsCount");
                int targetsCount;
                if (int.TryParse(count, out targetsCount) && targetsCount > 0)
                {
                    for (int i = 0; i < targetsCount; i++)
                    {
                        Location location = new Location();
                        location.ReadLocation(iniPath,i);
                        if (!string.IsNullOrEmpty(location.Path) && Directory.Exists(location.Path))
                        {
                            _listOfLocations.Add(location);
                        }
                    }
                }
            }
            
        }

        public IEnumerator<Location> GetEnumerator()
        {
            return  _listOfLocations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<Location> GetGuiLocations()
        {
            return _listOfLocations.FindAll(x =>(x.ShowInGui == true));
        }

        public List<Location> GetShellLocations()
        {
            return _listOfLocations.FindAll(x => (x.ShowInShell== true));
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
