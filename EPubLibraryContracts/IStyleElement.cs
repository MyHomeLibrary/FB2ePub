using System.IO;

namespace EPubLibraryContracts
{
    public interface IStyleElement : IEPubPath
    {
        void Write(Stream stream);
        EPubCoreMediaType GetMediaType();

    }
}
