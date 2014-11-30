using System;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class StanzaConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
        public int Level { get; set; }
    }

    internal class StanzaConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 Stanza element into XHTML representation
        /// </summary>
        /// <param name="stanzaItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="stanzaConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(StanzaItem stanzaItem,HTMLElementType compatibility,StanzaConverterParamsV2 stanzaConverterParams)
        {
            if (stanzaItem == null)
            {
                throw new ArgumentNullException("stanzaItem");
            }
            var stanzaSection = new Div(compatibility);

            if (stanzaItem.Title != null)
            {
                var titleConverter = new TitleConverterV2();
                stanzaSection.Add(titleConverter.Convert(stanzaItem.Title, compatibility, new TitleConverterParamsV2 { TitleLevel = stanzaConverterParams.Level, Settings = stanzaConverterParams .Settings}));
            }

            if (stanzaItem.SubTitle != null)
            {
                var subtitleConverter = new SubtitleConverterV2();
                stanzaSection.Add(subtitleConverter.Convert(stanzaItem.SubTitle, compatibility, new SubtitleConverterParamsV2 { Settings = stanzaConverterParams .Settings}));
            }

            foreach (var line in stanzaItem.Lines)
            {
                var vElementConverter = new VElementConverterV2();
                stanzaSection.Add(vElementConverter.Convert(line, compatibility, new VElementConverterParamsV2 { Settings = stanzaConverterParams .Settings}));
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
