namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// Arbitrary textual data, likely meant to be human-readable.
    /// </summary>
    public class Text : IAttributeDataType
    {
        public string Value { set; get; }
    }
}
