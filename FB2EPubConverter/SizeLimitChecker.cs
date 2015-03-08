namespace FB2EPubConverter
{
    internal class SizeLimitChecker
    {
        public ulong MaxSizeLimit { get; set; }

        public bool ExceedSizeLimit(ulong itemSize)
        {
            return ((MaxSizeLimit != 0) && (itemSize >= MaxSizeLimit));
        }

    }
}
