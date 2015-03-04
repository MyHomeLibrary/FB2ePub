using System.Collections.Generic;
using EPubLibrary.XHTML_Items;
using TranslitRu;

namespace EPubLibrary
{
    public interface IEpubFile
    {
        #region Transliteration_common_properties

        /// <summary>
        /// Transliteration mode
        /// </summary>
        TransliterationSettings TranslitMode { get; set; }

        /// <summary>
        /// Set/get it Table of Content (TOC) entries should be transliterated
        /// </summary>
        bool TranliterateToc { set; get; }

        /// <summary>
        /// Return reference to the list of the contained "book documents" - book content objects
        /// </summary>
        List<BookDocument> BookDocuments { get; }
        #endregion

        #region File creation related properties

        /// <summary>
        /// Max size of content (xhtml) file, 0 means no limit
        /// </summary>
        ulong ContentFileLimit { get; set; }

        #endregion
        /// <summary>
        /// Writes (generates) file to disk
        /// </summary>
        /// <param name="outFileName"></param>
        void Generate(string outFileName);
    }
}
