namespace EPubLibrary.AppleEPubV2Extensions
{
    public enum PlatformType
    {
        All,
        iPad,
        iPhone,
    }


    public enum OrientationLock
    {
        Off,
        LandscapeOnly,
        PortraitOnly,
    }

    public class AppleTargetPlatform
    {
        private PlatformType _type = PlatformType.All;
        private OrientationLock _lockType = OrientationLock.Off;
        private bool _allowCustomFonts = true;
        private bool _openToSpread = false;
        private bool _fixedLayout = false;


        public PlatformType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public OrientationLock OrientationLockType
        {
            get { return _lockType; }
            set { _lockType = value; }
        }

        public bool CustomFontsAllowed
        {
            get { return _allowCustomFonts; }
            set { _allowCustomFonts = value; }
        }

        public bool OpenToSpread
        {
            get { return _openToSpread; }
            set { _openToSpread = value; }
        }

        public bool FixedLayout
        {
            get { return _fixedLayout; }
            set { _fixedLayout = value; }
        }

        public static string ConvertTypeToString(PlatformType type)
        {
            if (type == PlatformType.iPad)
            {
                return "ipad";
            }
            if (type == PlatformType.iPhone)
            {
                return "iphone";
            }
            return "*";
        }


    }
}
