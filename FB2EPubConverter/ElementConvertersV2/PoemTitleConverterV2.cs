using System;
using System.Diagnostics;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class PoemTitleConverterV2 : BaseElementConverterV2
    {
        private int _level;

        /// <summary>
        /// Converts FB2 Title object to XHTML Title 
        /// </summary>
        /// <param name="titleItem">title item to convert</param>
        /// <param name="titleConverterParams"></param>
        /// <returns></returns>
        public Div Convert(TitleItem titleItem,  TitleConverterParamsV2 titleConverterParams)
        {
            if (titleItem == null)
            {
                throw new ArgumentNullException("titleItem");
            }
            _level = titleConverterParams.TitleLevel;
            var title = new Div(HTMLElementType.XHTML11);
            foreach (var fb2TextItem in titleItem.TitleData)
            {
                if (fb2TextItem is ParagraphItem)
                {
                    var paragraphStyle = GetParagraphStyleByLevel(_level);
                    var paragraphConverter = new ParagraphConverterV2();
                    title.Add(paragraphConverter.Convert(fb2TextItem as ParagraphItem, 
                        new ParagraphConverterParams { ResultType = paragraphStyle, Settings = titleConverterParams.Settings, StartSection = false }));
                }
                else if (fb2TextItem is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV2();
                    title.Add(emptyLineConverter.Convert());
                }
                else
                {
                    Debug.WriteLine("invalid type in Title - {0}", fb2TextItem.GetType());
                }
            }
            SetClassType(title, "poem_title");
            return title;
        }

        private static ParagraphConvTargetEnum GetParagraphStyleByLevel(int titleLevel)
        {
            var paragraphStyle = ParagraphConvTargetEnum.H6;
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
