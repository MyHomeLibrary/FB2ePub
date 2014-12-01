using System;
using EPubLibrary;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class BookIDConverterV2
    {
        public void Convert(FB2File fb2File, EPubFile epubFile)
        {
            // Getting information from FB2 document section
            var bookId = new Identifier
            {
                ID =
                    !string.IsNullOrEmpty(fb2File.DocumentInfo.ID) ? fb2File.DocumentInfo.ID : Guid.NewGuid().ToString(),
                IdentifierName = "FB2BookID",
                Scheme = "URI"
            };
            epubFile.Title.Identifiers.Add(bookId);
            
        }
    }
}
