namespace FB2EPubConverter.ElementConvertersV3
{
    class ConverterOptionsV3
    {
        public HRefManagerV3 ReferencesManager { get; set; }
        public ImageManager Images { get; set; }
        public bool CapitalDrop { get; set; }
        public long MaxSize;
    }
}
