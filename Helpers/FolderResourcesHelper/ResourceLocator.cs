using System;
using System.IO;
using System.Reflection;

namespace FolderResourcesHelper
{
    public class ResourceLocator
    {
        public static readonly ResourceLocator Instance = new ResourceLocator();

        /// <summary>
        /// Return path to default resources
        /// </summary>
        /// <returns></returns>
        public string GetResourcesPath()
        {
            string detectionFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Lord_KiRon\FB2ePub";
            if (Directory.Exists(detectionFolder))
            {
                return detectionFolder;
            }
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }


    }
}
