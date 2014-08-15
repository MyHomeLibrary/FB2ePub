using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class AutoPlayAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "autoplay";

        public override string GetElementName()
        {
            return AttributeName;
        }
    }
}
