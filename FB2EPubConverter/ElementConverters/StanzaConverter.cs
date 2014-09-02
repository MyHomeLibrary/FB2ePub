using System;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class StanzaConverterParams
    {
        public ConverterOptions Settings { get; set; }  
        public int Level { get; set; }
    }

    internal class StanzaConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 Stanza element into XHTML representation
        /// </summary>
        /// <param name="stanzaItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="stanzaConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(StanzaItem stanzaItem,HTMLElementType compatibility,StanzaConverterParams stanzaConverterParams)
        {
            if (stanzaItem == null)
            {
                throw new ArgumentNullException("stanzaItem");
            }
            var stanzaSection = new Div(compatibility);

            if (stanzaItem.Title != null)
            {
                var titleConverter = new TitleConverter();
                stanzaSection.Add(titleConverter.Convert(stanzaItem.Title, compatibility, new TitleConverterParams { TitleLevel = stanzaConverterParams.Level, Settings = stanzaConverterParams .Settings}));
            }

            if (stanzaItem.SubTitle != null)
            {
                var subtitleConverter = new SubtitleConverter();
                stanzaSection.Add(subtitleConverter.Convert(stanzaItem.SubTitle, compatibility, new SubtitleConverterParams { Settings = stanzaConverterParams .Settings}));
            }

            foreach (var line in stanzaItem.Lines)
            {
                var vElementConverter = new VElementConverter();
                stanzaSection.Add(vElementConverter.Convert(line, compatibility, new VElementConverterParams { Settings = stanzaConverterParams .Settings}));
            }

            if (stanzaItem.Lang != null)
            {
                stanzaSection.GlobalAttributes.Language.Value = stanzaItem.Lang;
            }

            SetClassType(stanzaSection,"stanza");
            return stanzaSection;
        }
    }
}
