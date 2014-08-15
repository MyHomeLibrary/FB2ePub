using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{

    public class AutoFocusAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "autofocus";

        public override string GetElementName()
        {
            return AttributeName;
        }
    }
}
