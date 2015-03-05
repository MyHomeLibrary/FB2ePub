using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    public enum ParagraphConvTargetEnumV2
    {
        Paragraph,
        H1,
        H2,
        H3,
        H4,
        H5,
        H6
    }

    internal class ParagraphConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
        public ParagraphConvTargetEnumV2 ResultType { get; set; }
        public bool StartSection { get; set; }
    }

    internal class ParagraphConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="paragraphConverterParams"></param>
        /// <returns></returns>
        public  HTMLItem Convert(ParagraphItem paragraphItem,ParagraphConverterParamsV2 paragraphConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }
            var paragraph = CreateBlock(paragraphConverterParams.ResultType);
            bool needToInsertDrop = paragraphConverterParams.Settings.CapitalDrop && paragraphConverterParams.StartSection;

            foreach (var item in paragraphItem.ParagraphData)
            {
                if (item is SimpleText)
                {
                    var textConverter = new SimpleTextElementConverterV2();
                    foreach (var s in textConverter.Convert(item, 
                        new SimpleTextElementConverterParamsV2 { Settings = paragraphConverterParams.Settings, NeedToInsertDrop = needToInsertDrop}))
                    {
                        if (needToInsertDrop)
                        {
                            needToInsertDrop = false;
                            SetClassType(paragraph, ElementStylesV2.CapitalDrop);
                        }
                        paragraph.Add(s);
                    }
                }
                else if (item is InlineImageItem)
                {
                    // if no image data - do not insert link
                    if (paragraphConverterParams.Settings.Images.HasRealImages())
                    {
                        var inlineItem = item as InlineImageItem;
                        if (paragraphConverterParams.Settings.Images.IsImageIdReal(inlineItem.HRef))
                        {
                            var inlineImageConverter = new InlineImageConverterV2();
                            paragraph.Add(inlineImageConverter.Convert(inlineItem,new InlineImageConverterParamsV2{ Settings = paragraphConverterParams.Settings }));
                        }
                        paragraphConverterParams.Settings.Images.ImageIdUsed(inlineItem.HRef);
                        if (needToInsertDrop) // in case this is "drop" image - no need to create a drop
                        {
                            needToInsertDrop = false;
                        }
                    }
                }
                else if (item is InternalLinkItem)
                {
                    var internalLinkConverter = new InternalLinkConverterV2();
                    foreach (var s in internalLinkConverter.Convert(item as InternalLinkItem,
                        new InternalLinkConverterParamsV2{ Settings = paragraphConverterParams.Settings,  NeedToInsertDrop = needToInsertDrop}))
                    {
                        if (needToInsertDrop)
                        {
                            needToInsertDrop = false;
                            SetClassType(paragraph, ElementStylesV2.CapitalDrop);
                        }
                        paragraph.Add(s);
                    }
                }
            }

            //SetClassType(paragraph);
            paragraph.GlobalAttributes.ID.Value =
                paragraphConverterParams.Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, paragraph);

            return paragraph;
        }

        /// <summary>
        /// Creates block element based on paragraph type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static HTMLItem CreateBlock(ParagraphConvTargetEnumV2 type)
        {
            HTMLItem paragraph;
            switch (type)
            {
                case ParagraphConvTargetEnumV2.H1:
                    paragraph = new H1(HTMLElementType.XHTML11);
                    break;
                case ParagraphConvTargetEnumV2.H2:
                    paragraph = new H2(HTMLElementType.XHTML11);
                    break;
                case ParagraphConvTargetEnumV2.H3:
                    paragraph = new H3(HTMLElementType.XHTML11);
                    break;
                case ParagraphConvTargetEnumV2.H4:
                    paragraph = new H4(HTMLElementType.XHTML11);
                    break;
                case ParagraphConvTargetEnumV2.H5:
                    paragraph = new H5(HTMLElementType.XHTML11);
                    break;
                case ParagraphConvTargetEnumV2.H6:
                    paragraph = new H6(HTMLElementType.XHTML11);
                    break;
                default: // Paragraph or anything else
                    paragraph = new Paragraph(HTMLElementType.XHTML11);
                    break;

            }
            return paragraph;
        }
    }
}
