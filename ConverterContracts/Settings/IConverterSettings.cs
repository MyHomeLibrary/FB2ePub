namespace ConverterContracts.Settings
{
    public interface IConverterSettings
    {
        void SetupDefaults();
        IFB2ImportSettings Fb2ImportSettings { get; set; }
        IEPubCommonSettings CommonSettings { get; set; }
        IEPubV2Settings V2Settings { get; set; }
        IEPubV3Settings V3Settings { get; set; }
    }
}
