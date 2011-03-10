using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EpigraphAuthorConverter : BaseElementConverter
    {
        public TextAuthorItem Item { get; set; }

        public IBlockElement Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div epigraphAuthor = new Div();
            ParagraphConverter paragraphConverter = new ParagraphConverter
                                                        {
                                                            Settings = Settings,
                                                            Item = Item
                                                        };
            epigraphAuthor.Add(paragraphConverter.Convert(ParagraphConvTargetEnum.Paragraph));
            return epigraphAuthor;
        }

    }
}
