using System.Collections.Generic;
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

        #endregion


        IBookInformationData BookInformation { get; }


        /// <summary>
        /// Writes (generates) file to disk
        /// </summary>
        /// <param name="outFileName"></param>
        void Generate(string outFileName);
    }
}
