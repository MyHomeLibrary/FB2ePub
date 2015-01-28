namespace ConverterContracts.Settings
{
    /// <summary>
    /// Fix mode options
    /// </summary>
    public enum FixOptions
    {
        DoNotFix, // do not attempt to fix
        MinimalFix, // try minimal (internal) fix
        UseFb2Fix, // use Fb2Fix if prev. failed
        Fb2FixAlways, // always use Fb2Fix
    }

    public interface IFB2ImportSettings
    {
        void CopyFrom(IFB2ImportSettings temp);
        void SetupDefaults();
        FixOptions FixMode { get; set; }
        bool ConvertAlphaPng { get; set; }
    }
}
