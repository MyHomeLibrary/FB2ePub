using System.Xml.Linq;

namespace ConverterContracts
{
    public interface IFb2EPubConverterEngine
    {
        void LoadFB2FileFromXML(XDocument fb2Document);
        void Save(string outFileName);
        bool LoadAndCheckFB2Files(string fileName);
    }
}
