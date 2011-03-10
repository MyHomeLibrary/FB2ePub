using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class VElementConverter : BaseElementConverter
    {
        public VPoemParagraph Item { get; set; }

        public IXHTMLItem Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            ParagraphConverter paragraphConverter = new ParagraphConverter
                                                        {
                                                            Settings = Settings,
                                                            Item = Item
                                                        };
            IBlockElement item = paragraphConverter.Convert(ParagraphConvTargetEnum.Paragraph);

            item.Class.Value = "v";

            item.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, item);

            return item;
        }

    }
}
