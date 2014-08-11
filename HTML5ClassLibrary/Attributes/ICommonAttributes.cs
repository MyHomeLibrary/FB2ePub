using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;

namespace HTML5ClassLibrary.Attributes
{
    /// <summary>
    /// Attributes that can be part of any HTML5 element
    /// </summary>
    public interface ICommonAttributes
    {
        ClassAttr Class { get; }
        IdAttribute ID { get; }
        TitleAttribute Title { get; }
        StyleAttribute Style { get; }
    }

}
