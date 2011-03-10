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
        /// <summary>
        /// Converts FB2 subtitle element into XHTML representation
        /// </summary>
        /// <param name="subtitleItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IBlockElement Convert(SubTitleItem subtitleItem)
        {
            if (subtitleItem == null)
            {
                throw new ArgumentNullException("subtitleItem");
            }
            //Div subtitle = new Div();
            ParagraphConverter paragraphConverter = new ParagraphConverter {Settings = Settings};
            IBlockElement internalData = paragraphConverter.Convert(subtitleItem,ParagraphConvTargetEnum.Paragraph);
            SetClassType(internalData);
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }


        public override string GetElementType()
        {
            return "subtitle";
        }
    }
}
