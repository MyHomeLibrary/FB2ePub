namespace EPubLibraryContracts
{
    public interface ISource : IDataWithLanguage
    {
        string SourceData { get; set; }
    }
}
