using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class LinkSectionConverterParams
    {
        public ConverterOptions Settings { get; set; }
    }

    internal class LinkSectionConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 "notes" and "comments" and other "additional" sections
        /// </summary>
        /// <param name="linkSectionItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="linkSectionConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(SectionItem linkSectionItem,HTMLElementType compatibility,LinkSectionConverterParams linkSectionConverterParams)
        {
            if (linkSectionItem == null)
            {
                throw new ArgumentNullException("linkSectionItem");
            }
            var content = new Div(compatibility);
            var a = new Anchor(compatibility);
            var titleConverter = new TitleConverter();
            foreach (var item in titleConverter.Convert(linkSectionItem.Title,compatibility,new TitleConverterParams{Settings = linkSectionConverterParams.Settings,TitleLevel = 2}).SubElements())
            {
                content.Add(item);                    
            }
            string newId = linkSectionConverterParams.Settings.ReferencesManager.EnsureGoodId(linkSectionItem.ID);
            a.Add(new SimpleHTML5Text(compatibility) { Text = newId });
            a.HRef.Value = string.Format("{0}_back", newId);
            if (((string)a.HRef.Value).StartsWith("_back") == false)
            {
                SetClassType(a,"note_anchor");
                linkSectionConverterParams.Settings.ReferencesManager.AddBackReference((string)a.HRef.Value, a);
                //content.Add(a);
            }
            SetClassType(content,"note_section");
            return content;

        }
   
    }
}
