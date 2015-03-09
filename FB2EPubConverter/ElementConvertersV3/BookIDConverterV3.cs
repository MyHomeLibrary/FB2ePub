using System;
using EPubLibraryContracts;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class BookIDConverterV3
    {
        public void Convert(FB2File fb2File, IBookInformationData titleInformation)
        {
            // Getting information from FB2 document section
            var bookId = new Identifier
            {
                ID =
                    !string.IsNullOrEmpty(fb2File.DocumentInfo.ID) ? fb2File.DocumentInfo.ID : Guid.NewGuid().ToString(),
                IdentifierName = "FB2BookID",
                Scheme = "URI"
            };
            titleInformation.Identifiers.Add(bookId);
            
        }
    }
}
