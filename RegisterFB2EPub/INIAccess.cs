using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RegisterFB2EPub
{

    class IniAccess
    {
        private const string FilterSection = "Filter";

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, Int32 iDefault, string lpFileName);

        private bool GetStateFromIni(string sectionName,string keyName)
        {
            int  result = (int)GetPrivateProfileInt(sectionName,keyName,-1,_fileName);
            if (result == 0 || result == -1)
            {
                return false;
            }
            return true;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        private void SetStateToIni(string sectionName,string keyName,bool state)
        {
            string stateToSet = state ? "1" : "0";
            WritePrivateProfileString(sectionName, keyName, stateToSet, _fileName);
        }

        private IniAccess()
        {
            // just prevent usage of default constructor
        }

        private string _fileName;

        public string IniFileName 
        {
            get { return _fileName; }

        }

        public  IniAccess(string fileName)
        {
            _fileName = fileName;
        }

        public bool IsAnyAllowed()
        {
            return GetStateFromIni(FilterSection, "AllowAny");
        }

        public bool IsFb2RarOnly()
        {
            if (IsAnyAllowed())
            {
                return false;
            }
            return !GetStateFromIni(FilterSection, "AllowAllRAR");
        }

        public bool IsFb2ZipOnly()
        {
            if (IsAnyAllowed())
            {
                return false;
            }
            return !GetStateFromIni(FilterSection, "AllowAllZIP");
        }

        public void SetAllowAny(bool allow)
        {
            SetStateToIni(FilterSection,"AllowAny",allow);
        }

        public void SetFb2RarOnly(bool rarChecked)
        {
            if ( rarChecked )
            {
                SetAllowAny(false);
            }
            SetStateToIni(FilterSection, "AllowAllRAR", !rarChecked);
        }

        internal void SetFb2ZipOnly(bool zipChecked)
        {
            if (zipChecked)
            {
                SetAllowAny(false);
            }
            SetStateToIni(FilterSection, "AllowAllZIP", !zipChecked);
        }
    }
}
