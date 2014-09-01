using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.Events
{
    /// <summary>
    /// Defines base class for event attribute
    /// </summary>
    public class OnEventAttribute : SimpleSingleTypeAttribute<ScriptType>
    {
        public OnEventAttribute(string name)
            : base(name)
        {
        }
    }
}