using ConverterContracts.ComInterfaces;
using ConverterContracts.Settings;

namespace ConverterContracts
{
    public enum PathSearchOptions
    {
        Fb2Only,
        Fb2WithArchives,
        WithAllArchives,
        All,
    }

    public interface IConvertProcessorSettings
    {
        IConverterSettings Settings { get; }
        bool DeleteSource { get; set; }
        bool LookInSubFolders { get; set; }
        PathSearchOptions SearchMask { get; set; }
        IProgressUpdateInterface ProgressCallbacks { get; set; }
        string SettingsFileToUse { get; set; }
        bool SingleFile { get; set; }
    }
}
