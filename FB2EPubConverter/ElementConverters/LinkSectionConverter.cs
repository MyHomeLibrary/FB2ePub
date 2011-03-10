using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class LinkSectionConverter : BaseElementConverter
    {
        public SectionItem Item { get; set; }

        public IXHTMLItem Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            IBlockElement content = new Paragraph();
            Anchor a = new Anchor();
            TitleConverter titleConverter = new TitleConverter
                                                {
                                                    Settings = Settings,
                                                    Item = Item.Title
                                                };
            foreach (var item in titleConverter.Convert(7).SubElements())
            {
                if (item is IInlineItem)
                {
                    a.Add(item);
                }
                else if (item is SimpleEPubText)
                {
                    a.Add(item);
                }
                else
                {
                    foreach (var subElement in item.SubElements())
                    {
                        a.Add(subElement);
                    }
                }
            }
            string newId = Settings.ReferencesManager.EnsureGoodID(Item.ID);
            a.HRef.Value = string.Format("{0}_back", newId);
            if (a.HRef.Value.StartsWith("_back") == false)
            {
                //a.ID.Value = newId; // no need since the Id usualy located on section level
                a.Class.Value = "note_anchor";
                Settings.ReferencesManager.AddBackReference(a.HRef.Value, a);
                content.Add(a);
            }
            content.Class.Value = "note_section";
            return content;

        }

    }
}
