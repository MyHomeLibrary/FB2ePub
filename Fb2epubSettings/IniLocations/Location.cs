using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Fb2epubSettings.IniLocations
{
    public class Location
    {

        public string Path { set; get; }
        public bool ShowInGui { set; get; }
        public bool ShowInShell { get; set; }
        public string Name { get; set; }

        public void ReadLocation(string iniPath,int locationNumber)
        {
            string section = string.Format("Target{0}", locationNumber + 1);
            string visability = IniAccessFunctions.IniReadValue(iniPath, section, "ShowInGUI");
            bool bAdd = true;
            if (bool.TryParse(visability, out bAdd))
            {
                ShowInGui = bAdd;
            }
            visability = IniAccessFunctions.IniReadValue(iniPath, section, "ShowInShell");
            if (bool.TryParse(visability, out bAdd))
            {
                ShowInShell = bAdd;
            }
            IniAccessFunctions.IniReadValue(iniPath, section, "TargetPath");
            Path = IniAccessFunctions.IniReadValue(iniPath, section, "TargetPath");
            Name = IniAccessFunctions.IniReadValue(iniPath, section, "TargetName");
            
        }

        public override string ToString()
        {
            return Path;
        }

    }

}
