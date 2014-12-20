using TranslitRu;

namespace EPubLibrary
{
    public interface IEpubFile
    {
        #region Transliteration_common_properties
        /// <summary>
        /// Return transliteration rule object
        /// </summary>
        Rus2Lat Transliterator { get; }

        /// <summary>
        /// Transliteration mode
        /// </summary>
        TranslitModeEnum TranslitMode { get; set; }

        /// <summary>
        /// Set/get it Table of Content (TOC) entries should be transliterated
        /// </summary>
        bool TranliterateToc { set; get; }
        #endregion

        /// <summary>
        /// Writes (generates) file to disk
        /// </summary>
        /// <param name="outFileName"></param>
        void Generate(string outFileName);
    }
}
