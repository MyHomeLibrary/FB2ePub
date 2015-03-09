namespace EPubLibraryContracts
{
    public interface IPublisher : IDataWithLanguage
    {
        string PublisherName { get; set; }
    }
}
