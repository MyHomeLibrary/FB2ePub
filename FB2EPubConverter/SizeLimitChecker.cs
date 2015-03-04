namespace FB2EPubConverter
{
    internal class SizeLimitChecker
    {
        public long MaxSizeLimit { get; set; }

        public bool ExceedSizeLimit(long itemSize)
        {
            return ((MaxSizeLimit != 0) && (itemSize >= MaxSizeLimit));
        }

    }
}
