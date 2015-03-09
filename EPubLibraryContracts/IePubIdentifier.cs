namespace EPubLibraryContracts
{
    public interface IEPubIdentifier
    {
        string IdentifierName { get; set; }
        string ID { get; set; }
        string Scheme { get; set; }
    }
}
