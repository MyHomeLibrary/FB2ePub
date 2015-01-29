using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal abstract class BaseElementConverterV3
    {
        protected void SetClassType(HTMLItem item, string elementClassName)
        {
            item.GlobalAttributes.Class.Value = elementClassName;
        }

    }
}
