namespace EPubLibraryContracts.Settings
{
    public enum EPubV3SubStandard
    {
        V30,
        V301,
    }

    public interface IEPubV3Settings
    {
        void SetupDefaults();
        void CopyFrom(IEPubV3Settings temp);

        EPubV3SubStandard V3SubStandard { get; set; }

    }
}
