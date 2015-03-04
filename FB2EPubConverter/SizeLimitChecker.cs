namespace FB2EPubConverter
{
    internal class SizeLimitChecker
    {
        private readonly long _maxSizeLimit;

        public SizeLimitChecker(long limit)
        {
            _maxSizeLimit = limit;
        }

        public bool ExceedSizeLimit(long itemSize)
        {
            return ((_maxSizeLimit != 0) && (itemSize >= _maxSizeLimit));
        }

    }
}
