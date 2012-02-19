using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace Fb2epubSettings
{
    public class NonStyledTabs : TabControl
    {
        [DllImport("uxtheme.dll")]
        private static extern int SetWindowTheme(
            [In] IntPtr hwnd,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSubAppName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList
            );
        protected override void OnHandleCreated(EventArgs e)
        {
            if (Environment.OSVersion.Version.Major <= 5) // fix forXP  not displaying vertical tab if visual styles enabled
            {
                SetWindowTheme(this.Handle, "", "");                
            }
            base.OnHandleCreated(e);
        }

    }
}
