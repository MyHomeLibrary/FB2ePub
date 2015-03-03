using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ConverterContracts.ComInterfaces
{
    [Guid("9B6D89BC-0C32-46E0-B06C-360DA401FE9A"),ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEPubConverterInterface
    {
        void ConvertPath(string inputPath, string outputFolder, IProgressUpdateInterface progress);
        void ConvertList(string[] files, string outputFolder, IProgressUpdateInterface progress);
        void ConvertXml(XDocument doc, string outFileName, IProgressUpdateInterface progress);
        void ConvertSingleFile(string inputPath, string outputName, IProgressUpdateInterface progress);
        void AbortConversion();
        bool ShowSettingsDialog(IntPtr parent);
    }
}
