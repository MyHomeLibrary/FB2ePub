using System;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class VElementConverterParams
    {
        public ConverterOptions Settings { get; set; }  
    }

    internal class VElementConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts "v" (poem) sub-element
        /// </summary>
        /// <returns>XHTML formated representation</returns>
        public IHTMLItem Convert(VPoemParagraph paragraphItem,HTMLElementType compatibility,VElementConverterParams vElementConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var paragraphConverter = new ParagraphConverter();
            var item = paragraphConverter.Convert(paragraphItem,compatibility,
                new ParagraphConverterParams
                {
                    ResultType =
                        ParagraphConvTargetEnum.Paragraph,
                    Settings = vElementConverterParams.Settings,
                    StartSection = false,
                });

            SetClassType(item, "v");

            //item.ID.Value = Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, item);

            return item;
        }
    }
}
