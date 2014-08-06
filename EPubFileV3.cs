using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary.Container;
using EPubLibrary.Content;

namespace EPubLibrary
{
    /// <summary>
    /// Implements v3 of ePub standard
    /// </summary>
    public class EPubFileV3 : EPubFile
    {
        public EPubFileV3()
        {
            _content = new ContentFileV3();
        }


        protected override void CreateContainer(out ContainerFile container)
        {
            container = new ContainerFileV3 { FlatStructure = _flatStructure, ContentFilePath = _content };
        }

    }
}
