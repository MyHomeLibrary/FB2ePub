using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{

    internal abstract class  BaseElementConverter
    {
        protected void SetClassType(HTMLItem item, string elementClassName)
        {
            item.GlobalAttributes.Class.Value = elementClassName;
        }
    }
}
