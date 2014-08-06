using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary.Container;

namespace EPubLibrary
{
    /// <summary>
    /// Implements v3 of ePub standard
    /// </summary>
    public class EPubFileV3 : EPubFile
    {
        protected virtual void CreateContainer(out ContainerFile container)
        {
            container = new ContainerFileV3 { FlatStructure = _flatStructure, ContentFilePath = _content };
        }

    }
}
