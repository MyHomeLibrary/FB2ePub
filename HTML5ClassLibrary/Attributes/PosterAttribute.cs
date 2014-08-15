using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes
{
    /// <summary>
    /// The poster attribute specifies an image to be shown while the video is downloading, or until the user hits the play button. 
    /// If this is not included, the first frame of the video will be used instead.
    /// </summary>
    public class PosterAttribute : BaseAttribute
    {
        private URI _attrObject = new URI();

        private const string AttributeName = "poster";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new URI { Value = xObject.Value };
                AttributeHasValue = true;
            }

        }

        public override string Value
        {
            get { return _attrObject.Value; }
            set
            {
                _attrObject.Value = value;
                AttributeHasValue = (value != string.Empty);
            }
        }
        #endregion

    }
}
