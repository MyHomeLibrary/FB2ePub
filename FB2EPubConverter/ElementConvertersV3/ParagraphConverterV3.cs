using FB2Library.Elements;
using System;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    public enum ParagraphConvTargetEnumV3
    {
        Paragraph,
        H1,
        H2,
        H3,
        H4,
        H5,
        H6
    }

    internal class ParagraphConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
        public ParagraphConvTargetEnumV3 ResultType { get; set; }
        public bool StartSection { get; set; }
    }


    internal class ParagraphConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="paragraphConverterParams"></param>
        /// <returns></returns>
        public HTMLItem Convert(ParagraphItem paragraphItem, ParagraphConverterParamsV3 paragraphConverterParams)
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
                    var textConverter = new SimpleTextElementConverterV3();
                    foreach (var s in textConverter.Convert(item,
                        new SimpleTextElementConverterParamsV3 { Settings = paragraphConverterParams.Settings, NeedToInsertDrop = needToInsertDrop }))
                    {
                        if (needToInsertDrop)
                        {
                            needToInsertDrop = false;
                            paragraph.GlobalAttributes.Class.Value = "drop";
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
                            var inlineImageConverter = new InlineImageConverterV3();
                            paragraph.Add(inlineImageConverter.Convert(inlineItem, new InlineImageConverterParamsV3 { Settings = paragraphConverterParams.Settings }));
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
                    var internalLinkConverter = new InternalLinkConverterV3();
                    foreach (var s in internalLinkConverter.Convert(item as InternalLinkItem,
                        new InternalLinkConverterParamsV3 { Settings = paragraphConverterParams.Settings, NeedToInsertDrop = needToInsertDrop }))
                    {
                        if (needToInsertDrop)
                        {
                            needToInsertDrop = false;
                            paragraph.GlobalAttributes.Class.Value = "drop";
                        }
                        paragraph.Add(s);
                    }
                }
            }

            //SetClassType(paragraph);
            paragraph.GlobalAttributes.ID.Value = paragraphConverterParams.Settings.ReferencesManager.AddIdUsed(paragraphItem.ID, paragraph);

            return paragraph;
        }

        /// <summary>
        /// Creates block element based on paragraph type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static HTMLItem CreateBlock(ParagraphConvTargetEnumV3 type)
        {
            HTMLItem paragraph;
            switch (type)
            {
                case ParagraphConvTargetEnumV3.H1:
                    paragraph = new H1(HTMLElementType.HTML5);
                    break;
                case ParagraphConvTargetEnumV3.H2:
                    paragraph = new H2(HTMLElementType.HTML5);
                    break;
                case ParagraphConvTargetEnumV3.H3:
                    paragraph = new H3(HTMLElementType.HTML5);
                    break;
                case ParagraphConvTargetEnumV3.H4:
                    paragraph = new H4(HTMLElementType.HTML5);
                    break;
                case ParagraphConvTargetEnumV3.H5:
                    paragraph = new H5(HTMLElementType.HTML5);
                    break;
                case ParagraphConvTargetEnumV3.H6:
                    paragraph = new H6(HTMLElementType.HTML5);
                    break;
                default: // Paragraph or anything else
                    paragraph = new Paragraph(HTMLElementType.HTML5);
                    break;

            }
            return paragraph;
        }

    }
}
