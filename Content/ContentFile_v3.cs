using EPubLibrary.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EPubLibrary.Content
{
    public class ContentFileV3 : ContentFile
    {

        /// <summary>
        /// Returns epub version to write into a package
        /// </summary>
        /// <returns></returns>
        protected override string GetEPubVersion()
        {
            return "2.0";
        }

    }
}
