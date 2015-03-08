namespace FB2EPubConverter.ElementConvertersV2
{
    internal class ConverterOptionsV2
    {
        public HRefManagerV2 ReferencesManager { get; set; }
        public ImageManager Images { get; set; }
        public bool CapitalDrop { get; set; }
        public ulong MaxSize;
    }
}
