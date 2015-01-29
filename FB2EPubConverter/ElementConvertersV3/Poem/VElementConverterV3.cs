using System;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV3.Poem
{
    internal class VElementConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }

    internal class VElementConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts "v" (poem) sub-element
        /// </summary>
        /// <returns>XHTML formatted representation</returns>
        public IHTMLItem Convert(VPoemParagraph paragraphItem, VElementConverterParamsV3 vElementConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var paragraphConverter = new ParagraphConverterV3();
            var item = paragraphConverter.Convert(paragraphItem,
                new ParagraphConverterParamsV3
                {
                    ResultType =
                        ParagraphConvTargetEnumV3.Paragraph,
                    Settings = vElementConverterParams.Settings,
                    StartSection = false,
                });

            SetClassType(item, "v");

            //item.ID.Value = Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, item);

            return item;
        }

    }
}
