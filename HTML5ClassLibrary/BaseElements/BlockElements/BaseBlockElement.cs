using System.Xml.Linq;


namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// Base class for all "block" elements , meaning element containers, object that according
    /// to HTML5 rules can contain other elements
    /// </summary>
    public abstract class BaseBlockElement : BaseContainingElement, IBlockElement
    {
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";
    }
}
