using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary.Container;
using EPubLibrary.Content;
using ICSharpCode.SharpZipLib.Zip;

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


        protected override void AddContentFile(ZipOutputStream stream)
        {
            stream.SetLevel(9);
            CreateFileEntryInZip(stream, _content);
            _content.Title = _title;
            _content.CalibreData = _calibreMetadata;
            _content.Write(stream);
        }

    }
}
