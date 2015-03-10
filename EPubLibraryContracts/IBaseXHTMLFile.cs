using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace EPubLibraryContracts
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
        string HRef { get; }
        IEPubInternalPath PathInEPUB { get; }
        IBaseXHTMLFile NavigationParent { get; set; }
        int NavigationLevel { get; }
        ulong MaxSize { get; set; }
    }
}
