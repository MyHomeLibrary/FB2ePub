using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class TitleConverter : BaseElementConverter
    {
        public TitleItem Item { get; set; }

        /// <summary>
        /// Converts FB2 Title to EPub Title page
        /// </summary>
        /// <param name="titleLevel"></param>
        /// <returns></returns>
        public Div Convert(int titleLevel)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Div title = new Div();
            foreach (var fb2TextItem in Item.TitleData)
            {
                if (fb2TextItem is ParagraphItem)
                {
                    ParagraphConvTargetEnum paragraphStyle = GetParagraphStyleByLevel(titleLevel);
                    ParagraphConverter paragraphConverter = new ParagraphConverter
                                                                {
                                                                    Item = fb2TextItem as ParagraphItem,
                                                                    Settings = Settings
                                                                };
                    title.Add(paragraphConverter.Convert(paragraphStyle));
                }
                else if (fb2TextItem is EmptyLineItem)
                {
                    EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                    title.Add(emptyLineConverter.Convert());
                }
                else
                {
                    Debug.WriteLine(string.Format("invalid type in Title - {0}", fb2TextItem.GetType()));
                }
            }
            string itemClass = string.Format("title{0}", titleLevel);
            title.Class.Value = itemClass;
            return title;
        }

        private static ParagraphConvTargetEnum GetParagraphStyleByLevel(int titleLevel)
        {
            ParagraphConvTargetEnum paragraphStyle = ParagraphConvTargetEnum.H6;
            switch (titleLevel)
            {
                case 1:
                    paragraphStyle = ParagraphConvTargetEnum.H1;
                    break;
                case 2:
                    paragraphStyle = ParagraphConvTargetEnum.H2;
                    break;
                case 3:
                    paragraphStyle = ParagraphConvTargetEnum.H3;
                    break;
                case 4:
                    paragraphStyle = ParagraphConvTargetEnum.H4;
                    break;
                case 5:
                    paragraphStyle = ParagraphConvTargetEnum.H5;
                    break;
            }
            return paragraphStyle;
        }


    }
}
