using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2
{

    internal abstract class  BaseElementConverterV2
    {
        protected void SetClassType(HTMLItem item, string elementClassName)
        {
            item.GlobalAttributes.Class.Value = elementClassName;
        }
    }
}
