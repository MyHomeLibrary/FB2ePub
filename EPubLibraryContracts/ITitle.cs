namespace EPubLibraryContracts
{
    public enum TitleType
    {
        Main,
        SourceInfo,
        PublishInfo,
    }

    public interface ITitle : IDataWithLanguage
    {
        string TitleName { get; set; }
        TitleType TitleType { get; set; }
    }
}
