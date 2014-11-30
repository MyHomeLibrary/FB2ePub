using System;
using System.Xml.Serialization;

namespace Fb2epubSettings
{
    public class FB2ImportSettings
    {
        #region private_members
        private FixOptions _fixMode = FixOptions.DoNotFix;
        private bool _convertAlphaPng;
        #endregion


        public void CopyFrom(FB2ImportSettings temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("temp");
            }
            if (temp == this)
            {
                return;
            }
            _fixMode = temp._fixMode;
            _convertAlphaPng = temp._convertAlphaPng;
        }

        public void SetupDefaults()
        {
            _fixMode = FixOptions.UseFb2Fix;
            _convertAlphaPng = true;
        }

        /// <summary>
        /// Get/Set FB2 fix mode
        /// </summary>
        [XmlElement(ElementName = "Fb2FixMode")]
        public FixOptions FixMode
        {
            get { return _fixMode; }
            set { _fixMode = value; }
        }

        /// <summary>
        /// Set/Get if alpha channel palette bitmaps need to be converted
        /// </summary>
        [XmlElement(ElementName = "ConvertAlphaChannelPNGImages")]
        public bool ConvertAlphaPng
        {
            get { return _convertAlphaPng; }
            set { _convertAlphaPng = value; }
        }

    }
}
