using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class LinkSectionConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 "notes" and "comments" and other "additional" sections
        /// </summary>
        /// <param name="linkSectionItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(SectionItem linkSectionItem)
        {
            if (linkSectionItem == null)
            {
                throw new ArgumentNullException("linkSectionItem");
            }
            var content = new Div();
            var a = new Anchor();
            var titleConverter = new TitleConverter{ Settings = Settings };
            foreach (var item in titleConverter.Convert(linkSectionItem.Title, 2).SubElements())
            {
                content.Add(item);                    
            }
            string newId = Settings.ReferencesManager.EnsureGoodId(linkSectionItem.ID);
            a.Add(new SimpleHTML5Text { Text = newId });
            a.HRef.Value = string.Format("{0}_back", newId);
            if (((string)a.HRef.Value).StartsWith("_back") == false)
            {
                a.GlobalAttributes.Class.Value = "note_anchor";
                Settings.ReferencesManager.AddBackReference((string)a.HRef.Value, a);
                //content.Add(a);
            }
            SetClassType(content);
            return content;

        }

    
        public override string GetElementType()
        {
            return "note_section";
        }
    }
}
