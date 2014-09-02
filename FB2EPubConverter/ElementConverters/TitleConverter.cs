using System;
using System.Diagnostics;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class TitleConverterParams
    {
        public ConverterOptions Settings { get; set; }  
        public int TitleLevel { get; set; }
    }

    internal class TitleConverter : BaseElementConverter
    {
        private int _level;

        /// <summary>
        /// Converts FB2 Title object to XHTML Title 
        /// </summary>
        /// <param name="titleItem">title item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="titleConverterParams"></param>
        /// <returns></returns>
        public Div Convert(TitleItem titleItem,HTMLElementType compatibility,TitleConverterParams titleConverterParams)
        {
            if (titleItem == null)
            {
                throw new ArgumentNullException("titleItem");
            }
            _level = titleConverterParams.TitleLevel;
            var title = new Div(compatibility);
            foreach (var fb2TextItem in titleItem.TitleData)
            {
                if (fb2TextItem is ParagraphItem)
                {
                    var paragraphStyle = GetParagraphStyleByLevel(_level);
                    var paragraphConverter = new ParagraphConverter();
                    title.Add(paragraphConverter.Convert(fb2TextItem as ParagraphItem, compatibility,
                        new ParagraphConverterParams { ResultType = paragraphStyle, Settings = titleConverterParams.Settings, StartSection = false}));
                }
                else if (fb2TextItem is EmptyLineItem)
                {
                    var emptyLineConverter = new EmptyLineConverter();
                    title.Add(emptyLineConverter.Convert(compatibility));
                }
                else
                {
                    Debug.WriteLine("invalid type in Title - {0}", fb2TextItem.GetType());
                }
            }
            SetClassType(title, string.Format("title{0}", _level));
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
