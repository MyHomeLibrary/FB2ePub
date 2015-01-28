namespace EPubLibraryContracts.Settings
{
    public interface IEPubV2Settings
    {
        void SetupDefaults();
        bool AddCalibreMetadata { get; set; }
        bool EnableAdobeTemplate { get; set; }
        IAppleConverterePub2Settings AppleConverterEPubSettings { get; set; }
        string AdobeTemplatePath { get; set; }

        void CopyFrom(IEPubV2Settings temp);
    }
}
