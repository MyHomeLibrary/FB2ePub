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
            int bAdd = 0;
            if (int.TryParse(visability, out bAdd))
            {
                ShowInGui = (bAdd!=0);
            }
            visability = IniAccessFunctions.IniReadValue(iniPath, section, "ShowInShell");
            if (int.TryParse(visability, out bAdd))
            {
                ShowInShell = (bAdd!=0);
            }
            IniAccessFunctions.IniReadValue(iniPath, section, "TargetPath");
            Path = IniAccessFunctions.IniReadValue(iniPath, section, "TargetPath");
            Name = IniAccessFunctions.IniReadValue(iniPath, section, "TargetName");
            
        }

        public void WriteLocation(string iniPath,int locationNumber)
        {
            string section = string.Format("Target{0}", locationNumber + 1);
            IniAccessFunctions.IniWriteValue(iniPath,section,"TargetPath",Path);
            IniAccessFunctions.IniWriteValue(iniPath, section, "TargetName", Name);
            IniAccessFunctions.IniWriteValue(iniPath, section, "ShowInGUI", ShowInGui?"1":"0");
            IniAccessFunctions.IniWriteValue(iniPath, section, "ShowInShell", ShowInShell? "1" : "0");
        }


        public override string ToString()
        {
            return Path;
        }

    }

}
