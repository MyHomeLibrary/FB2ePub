using System.Collections.Generic;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using TranslitRuContracts;

namespace EPubLibrary
{
    public interface IEpubFile
    {
        #region Transliteration_common_properties

        /// <summary>
        /// Transliteration mode
        /// </summary>
        ITransliterationSettings TranslitMode { get; set; }

        /// <summary>
        /// Return reference to the list of the contained "book documents" - book content objects
        /// </summary>
        List<BookDocument> BookDocuments { get; }
        #endregion

        #region File creation related properties

        #endregion
        /// <summary>
        /// Writes (generates) file to disk
        /// </summary>
        /// <param name="outFileName"></param>
        void Generate(string outFileName);

        void SetTitlePageInformation(IBookTitleInformation titleInformation);
    }
}
