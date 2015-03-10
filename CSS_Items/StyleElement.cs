using System.IO;
using EPubLibrary.Content;
using EPubLibrary.PathUtils;
using EPubLibraryContracts;

namespace EPubLibrary.CSS_Items
{
    public interface IStyleElement : IEPubPath
    {
        void Write(Stream stream);
        EPubCoreMediaType GetMediaType();

    }
}
