using System.Xml.Linq;

namespace EPubLibraryContracts
{
    public interface ICalibreMetadata
    {
        string SeriesName { get; set; }
        int SeriesIndex { get; set; }
        string TitleForSort { get; set; }

        void InjectData(XElement metadata);
        void InjectNamespace(XElement metadata);
    }
}
