using ConverterContracts;
using ConverterContracts.ComInterfaces;
using ConverterContracts.Settings;
using Fb2epubSettings;

namespace FB2EPubConverter
{

    internal class ConvertProcessorSettings : IConvertProcessorSettings
    {

        private PathSearchOptions _searchMask = PathSearchOptions.Fb2WithArchives;

        private readonly IConverterSettings _settings = new ConverterSettings();
        /// <summary>
        /// Settings for the converter
        /// </summary>
        public IConverterSettings Settings { get { return _settings; } }

        /// <summary>
        /// Get/Set if source file (Fb2/zip/etc...) need to be deleted
        /// </summary>
        public bool DeleteSource { get; set; }

        /// <summary>
        /// Set/Get if processor should "look" in subfolders for a source files
        /// </summary>
        public bool LookInSubFolders { get; set; }

        /// <summary>
        /// Returns search mask
        /// </summary>
        public PathSearchOptions SearchMask { get { return _searchMask; } set { _searchMask = value; } }

        /// <summary>
        /// Get/Set reference to progress callback interface
        /// </summary>
        public IProgressUpdateInterface ProgressCallbacks { get; set; }

        /// <summary>
        /// Returns the name of the settings file to use, null if we need to use default file
        /// </summary>
        public string SettingsFileToUse { get; set; }

        /// <summary>
        /// Set if we converting single file and single file output expected
        /// in such case we return error if not converted
        /// </summary>
        public bool SingleFile { get; set; }

    }
}
