using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary.PathUtils;

namespace EPubLibrary.Content.NavigationDocument
{
    class NavigationDocumentFile : IEPubPath
    {
        public static readonly EPubInternalPath NAVFilePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/nav.xhtml");

        public EPubInternalPath PathInEPUB
        {
            get { return NAVFilePath; }
        }
    }
}
