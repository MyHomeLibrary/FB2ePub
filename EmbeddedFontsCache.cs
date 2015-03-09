using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace EPubLibrary
{
    public interface IEmbeddedFontsCache
    {
        void WriteFontToStream(string fontPath, Stream outputStream);
    }

    internal class EmbeddedFontsCache : IEmbeddedFontsCache, IDisposable
    {
        public static readonly IEmbeddedFontsCache Instance = new EmbeddedFontsCache();

        private readonly Dictionary<string, MemoryStream> _cachedFonts = new Dictionary<string, MemoryStream>();
        private readonly object _accesslock = new object();
        private bool _disposed = false;

        public void WriteFontToStream(string fontPath, Stream outputStream)
        {
            lock (_accesslock)
            {
                int iCount;
                var buffer = new Byte[2048];
                if (!_cachedFonts.ContainsKey(fontPath))
                {
                    var tempStream = new MemoryStream();
                    using (var reader = new BinaryReader(File.OpenRead(fontPath)))
                    {
                        while ((iCount = reader.Read(buffer, 0, 2048)) != 0)
                        {
                            tempStream.Write(buffer, 0, iCount);
                        }
                    }
                    _cachedFonts.Add(fontPath,tempStream);
                }
                MemoryStream fontStream = _cachedFonts[fontPath];
                fontStream.Seek(0, SeekOrigin.Begin);
                while ((iCount = fontStream.Read(buffer, 0, 2048)) != 0)
                {
                    outputStream.Write(buffer,0, iCount);
                }
                
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                lock (_accesslock)
                {
                    foreach (var fontBinaries in _cachedFonts.Values)
                    {
                        {
                            fontBinaries.Dispose();
                        }
                    }
                    _cachedFonts.Clear();
                }
            }
            _disposed = true;
        }

        ~EmbeddedFontsCache()
        {
            Dispose(false);
        }
    }
}
