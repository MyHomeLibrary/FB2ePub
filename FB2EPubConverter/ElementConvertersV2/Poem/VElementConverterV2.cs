using System;
using FB2Library.Elements.Poem;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2.Poem
{
    internal class VElementConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class VElementConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts "v" (poem) sub-element
        /// </summary>
        /// <returns>XHTML formatted representation</returns>
        public IHTMLItem Convert(VPoemParagraph paragraphItem,VElementConverterParamsV2 vElementConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var paragraphConverter = new ParagraphConverterV2();
            var item = paragraphConverter.Convert(paragraphItem,
                new ParagraphConverterParamsV2
                {
                    ResultType =
                        ParagraphConvTargetEnumV2.Paragraph,
                    Settings = vElementConverterParams.Settings,
                    StartSection = false,
                });

            SetClassType(item, "v");

            //item.ID.Value = Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, item);

            return item;
        }
    }
}
