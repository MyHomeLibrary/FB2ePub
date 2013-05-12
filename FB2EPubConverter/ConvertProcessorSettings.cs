using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2EPubConverter.Interfaces;
using Fb2epubSettings;

namespace FB2EPubConverter
{
    public enum PathSearchOptions
    {
        Fb2Only,
        Fb2WithArchives,
        WithAllArchives,
        All,
    }

    public class ConvertProcessorSettings
    {

        private PathSearchOptions _searchMask = PathSearchOptions.Fb2WithArchives;

        private readonly ConverterSettings _settings = new ConverterSettings();
        /// <summary>
        /// Settings for the converter
        /// </summary>
        public ConverterSettings Settings { get { return _settings; } }

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

    }
}
