using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using EPubLibrary.CSS_Items;
using EPubLibraryContracts;

namespace EPubLibrary.XHTML_Items
{
    public interface IBaseXHTMLFile
    {
        void Write(Stream stream);
        XDocument Generate();
        void GenerateBody();
        void GenerateHead();

        GuideTypeEnum GuideRole { get; set; }
        bool NotPartOfNavigation { get; set; }
        bool FlatStructure { get; set; }
        string Id { get; set; }
        string FileName { get; set; }
        bool EmbedStyles { get; set; }
        string PageTitle { get; set; }
        List<IStyleElement> StyleFiles { get;  }
    }
}
