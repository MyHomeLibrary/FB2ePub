using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Fb2ePubGui
{
    class IniLocations : IEnumerable<string>
    {
        private readonly List<string>  _listOfLocations = new List<string>();


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
          string key, string def, StringBuilder retVal,
          int size, string filePath);

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
            _listOfLocations.Add(defaultPath);
            string iniPath = DetectIniLocation();
            if (iniPath != string.Empty)
            {
                string count = IniReadValue(iniPath, "TARGETS", "TargetsCount");
                int targetsCount;
                if (int.TryParse(count, out targetsCount) && targetsCount > 0)
                {
                    for (int i = 0; i < targetsCount; i++)
                    {
                        string section = string.Format("Target{0}", i + 1);
                        string path = IniReadValue(iniPath, section, "TargetPath");
                        if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                        {
                            //comboBoxDestination.Items.Add(path);
                            _listOfLocations.Add(path);
                        }
                    }
                }
            }
            
        }

        public IEnumerator<string> GetEnumerator()
        {
            return  _listOfLocations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public string IniReadValue(string iniPath, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        private string DetectIniLocation()
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
