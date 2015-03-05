using System;
using System.Diagnostics;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2.Poem
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
                        new ParagraphConverterParamsV2 { ResultType = paragraphStyle, Settings = titleConverterParams.Settings, StartSection = false }));
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
            SetClassType(title, ElementStylesV2.PoemTitle);
            return title;
        }

        private static ParagraphConvTargetEnumV2 GetParagraphStyleByLevel(int titleLevel)
        {
            var paragraphStyle = ParagraphConvTargetEnumV2.H6;
            switch (titleLevel)
            {
                case 1:
                    paragraphStyle = ParagraphConvTargetEnumV2.H1;
                    break;
                case 2:
                    paragraphStyle = ParagraphConvTargetEnumV2.H2;
                    break;
                case 3:
                    paragraphStyle = ParagraphConvTargetEnumV2.H3;
                    break;
                case 4:
                    paragraphStyle = ParagraphConvTargetEnumV2.H4;
                    break;
                case 5:
                    paragraphStyle = ParagraphConvTargetEnumV2.H5;
                    break;
            }
            return paragraphStyle;
        }

    }
}
