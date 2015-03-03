using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ConverterContracts.Settings;

namespace Fb2epubSettings
{
    public class FB2ImportSettings : IFB2ImportSettings 
    {
        #region private_members
        private FixOptions _fixMode = FixOptions.DoNotFix;
        private bool _convertAlphaPng;
        #endregion


        #region constants

        public const string FB2ImportSettingsElementName = "Fb2ImportSettings";

        private const string Fb2FixModeElementName = "Fb2FixMode";
        private const string ConverAlphaChennelElementName = "ConvertAlphaChannelPNGImages";

        #endregion 

        public void CopyFrom(IFB2ImportSettings temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("temp");
            }
            if (temp == this)
            {
                return;
            }
            _fixMode = temp.FixMode;
            _convertAlphaPng = temp.ConvertAlphaPng;
        }

        public void SetupDefaults()
        {
            _fixMode = FixOptions.UseFb2Fix;
            _convertAlphaPng = true;
        }

        /// <summary>
        /// Get/Set FB2 fix mode
        /// </summary>
        public FixOptions FixMode
        {
            get { return _fixMode; }
            set { _fixMode = value; }
        }

        /// <summary>
        /// Set/Get if alpha channel palette bitmaps need to be converted
        /// </summary>
        public bool ConvertAlphaPng
        {
            get { return _convertAlphaPng; }
            set { _convertAlphaPng = value; }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case Fb2FixModeElementName:
                            FixOptions fixMode;
                            string elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out fixMode))
                            {
                                throw new InvalidDataException(string.Format("Invalid fix mode: {0}", elementContent));
                            }
                            _fixMode = fixMode;
                            continue;
                        case ConverAlphaChennelElementName:
                            _convertAlphaPng = reader.ReadElementContentAsBoolean();
                            continue;
                    }
                }
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(FB2ImportSettingsElementName);

            writer.WriteStartElement(Fb2FixModeElementName);
            writer.WriteValue(_fixMode.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(ConverAlphaChennelElementName);
            writer.WriteValue(_convertAlphaPng);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
