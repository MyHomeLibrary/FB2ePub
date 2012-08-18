using System.Runtime.InteropServices;
using System.Xml;

namespace FB2EPubConverter.Interfaces
{
    [Guid("9B6D89BC-0C32-46E0-B06C-360DA401FE9A"),ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEPubConverterInterface
    {
        void ConvertPath(string inputPath, string outputFolder, IProgressUpdateInterface progress);
        void ConvertList(string[] files, string outputFolder, IProgressUpdateInterface progress);
        void ConvertXml(XmlDocument doc, string outFileName, IProgressUpdateInterface progress);
    }
}
