using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EPubLibrary
{
    public abstract class StyleElement
    {
        abstract public void Write(Stream stream);
        abstract public string GetFilePathExt();
        public abstract string GetMediaType();

    }
}
