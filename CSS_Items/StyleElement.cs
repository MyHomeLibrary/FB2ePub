using System.IO;
using EPubLibrary.PathUtils;

namespace EPubLibrary.CSS_Items
{
    public abstract class StyleElement : IEPubPath
    {
        abstract public void Write(Stream stream);
        public abstract EPubInternalPath PathInEPUB { get; }
        public abstract string GetMediaType();

    }
}
