using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using System.Diagnostics;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class TitleConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
        public int TitleLevel { get; set; }      
    }
    
    
    internal class TitleConverterV3 : BaseElementConverterV3
    {
        private int _level;

        /// <summary>
        /// Converts FB2 Title object to XHTML Title 
        /// </summary>
        /// <param name="titleItem">title item to convert</param>
        /// <param name="titleConverterParams"></param>
        /// <returns></returns>
        public Div Convert(TitleItem titleItem, TitleConverterParamsV3 titleConverterParams)
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
                    var paragraphConverter = new ParagraphConverterV3();
                    title.Add(paragraphConverter.Convert(fb2TextItem as ParagraphItem,
                        new ParagraphConverterParamsV3 { ResultType = paragraphStyle, Settings = titleConverterParams.Settings, StartSection = false }));
                }
                else if (fb2TextItem is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverterV3();
                    title.Add(emptyLineConverter.Convert());
                }
                else
                {
                    Debug.WriteLine("invalid type in Title - {0}", fb2TextItem.GetType());
                }
            }
            SetClassType(title, string.Format("title{0}", _level));
            return title;
        }

        private static ParagraphConvTargetEnumV3 GetParagraphStyleByLevel(int titleLevel)
        {
            var paragraphStyle = ParagraphConvTargetEnumV3.H6;
            switch (titleLevel)
            {
                case 1:
                    paragraphStyle = ParagraphConvTargetEnumV3.H1;
                    break;
                case 2:
                    paragraphStyle = ParagraphConvTargetEnumV3.H2;
                    break;
                case 3:
                    paragraphStyle = ParagraphConvTargetEnumV3.H3;
                    break;
                case 4:
                    paragraphStyle = ParagraphConvTargetEnumV3.H4;
                    break;
                case 5:
                    paragraphStyle = ParagraphConvTargetEnumV3.H5;
                    break;
            }
            return paragraphStyle;
        }

    }
}
