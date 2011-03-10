using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class SubtitleConverter : BaseElementConverter
    {
        public SubTitleItem Item { get; set;}

        public Div Convert(int p)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div subtitle = new Div();
            ParagraphConverter paragraphConverter = new ParagraphConverter
                                                        {
                                                            Item = Item,
                                                            Settings = Settings
                                                        };
            IBlockElement internalData = paragraphConverter.Convert(ParagraphConvTargetEnum.Paragraph);
            internalData.Class.Value = "subtitle";
            subtitle.Add(internalData);
            subtitle.Class.Value = "subtitle";
            return subtitle;
        }


    }
}
