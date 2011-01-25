using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RegisterFB2EPub
{
    static class LocationDetector
    {
        public static string DetectLocation(string file)
        {
            // Check current folder
            if (!string.IsNullOrEmpty(Directory.GetCurrentDirectory()))
            {
                string temppath = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), file);
                if (File.Exists(temppath))
                {
                    return temppath;
                }
            }

            // Check the location we run from 
            if (Assembly.GetExecutingAssembly().Location != null)
            {
                string path = string.Empty;
                string assemblyLocation = Path.GetFullPath(Assembly.GetExecutingAssembly().Location);
                if (!string.IsNullOrEmpty(assemblyLocation))
                {
                    path = Path.GetDirectoryName(assemblyLocation);
                }
                if (!string.IsNullOrEmpty(path))
                {
                    string temppath = string.Format("{0}\\{1}", path, file);
                    if (File.Exists(temppath))
                    {
                        return temppath;
                    }
                }
                }

            return string.Empty;
        }

    }
}
