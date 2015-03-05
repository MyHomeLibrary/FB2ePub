using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class LinkSectionConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }


    internal class LinkSectionConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Convert FB2 "notes" and "comments" and other "additional" sections
        /// </summary>
        /// <param name="linkSectionItem">item to convert</param>
        /// <param name="linkSectionConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(SectionItem linkSectionItem, LinkSectionConverterParamsV3 linkSectionConverterParams)
        {
            if (linkSectionItem == null)
            {
                throw new ArgumentNullException("linkSectionItem");
            }
            var content = new Div(HTMLElementType.XHTML11);
            var a = new Anchor(HTMLElementType.XHTML11);
            var titleConverter = new TitleConverterV3();
            foreach (var item in titleConverter.Convert(linkSectionItem.Title, new TitleConverterParamsV3 { Settings = linkSectionConverterParams.Settings, TitleLevel = 2 }).SubElements())
            {
                content.Add(item);
            }
            string newId = linkSectionConverterParams.Settings.ReferencesManager.EnsureGoodId(linkSectionItem.ID);
            a.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = newId });
            a.HRef.Value = string.Format("{0}_back", newId);
            if (((string)a.HRef.Value).StartsWith("_back") == false)
            {
                SetClassType(a, ElementStylesV3.NoteAnchor);
                linkSectionConverterParams.Settings.ReferencesManager.AddBackReference((string)a.HRef.Value, a);
                //content.Add(a);
            }
            SetClassType(content, ElementStylesV3.NoteSection);
            return content;

        }
    }
}
