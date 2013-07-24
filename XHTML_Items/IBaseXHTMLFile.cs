using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EPubLibrary.Content.Guide;
using EPubLibrary.CSS_Items;

namespace EPubLibrary.XHTML_Items
{
    interface IBaseXHTMLFile
    {
        void Write(Stream stream);
        XDocument Generate();
        void GenerateBody();
        void GenerateHead();

        GuideTypeEnum DocumentType { get; set; }
        bool NotPartOfNavigation { get; set; }
        bool FlatStructure { get; set; }
        string Id { get; set; }
        string FileName { get; set; }
        bool EmbedStyles { get; set; }
        string PageTitle { get; set; }
        List<StyleElement> StyleFiles { get;  }
    }
}
