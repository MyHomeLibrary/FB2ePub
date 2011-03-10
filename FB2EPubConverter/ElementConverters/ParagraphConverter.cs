using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    public enum ParagraphConvTargetEnum
    {
        Paragraph,
        H1,
        H2,
        H3,
        H4,
        H5,
        H6
    }

    internal class ParagraphConverter : BaseElementConverter
    {
        public ParagraphItem Item { get; set;}

        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="resultType">type of the resulting block container in EPUB</param>
        /// <returns></returns>
        public IBlockElement Convert(ParagraphConvTargetEnum resultType)
        {
            return Convert(resultType, false);
        }

        /// <summary>
        /// Converts FB2 Paragraph to EPUB paragraph
        /// </summary>
        /// <param name="resultType">type of the resulting block container in EPUB</param>
        /// <param name="startSection"> if this is a first paragraph in section</param>
        /// <returns></returns>
        public  IBlockElement Convert(ParagraphConvTargetEnum resultType, bool startSection)
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            IBlockElement paragraph = CreateBlock(resultType);
            bool needToInsert = Settings.CapitalDrop && startSection;

            foreach (var item in Item.ParagraphData)
            {
                if (item is SimpleText)
                {
                    SimpleTextElementConverter textConverter = new SimpleTextElementConverter
                                                                   {Item = item, Settings = Settings};
                    foreach (var s in textConverter.Convert(needToInsert))
                    {
                        if (needToInsert)
                        {
                            needToInsert = false;
                            paragraph.Class.Value = "drop";
                        }
                        paragraph.Add(s);
                    }
                }
                else if (item is InlineImageItem)
                {
                    // if no image data - do not insert link
                    if (Settings.Images.HasRealImages())
                    {
                        InlineImageItem inlineItem = item as InlineImageItem;
                        if (Settings.Images.IsImageIdReal(inlineItem.HRef))
                        {
                            InlineImageConverter inlineImageConverter = new InlineImageConverter {Item = inlineItem,Settings = Settings};
                            paragraph.Add(inlineImageConverter.Convert());
                        }
                        Settings.Images.ImageIdUsed(inlineItem.HRef);
                    }
                }
                else if (item is InternalLinkItem)
                {
                    InternalLinkConverter internalLinkConverter = new InternalLinkConverter {Item = item as InternalLinkItem,Settings = Settings};
                    foreach (var s in internalLinkConverter.Convert())
                    {
                        paragraph.Add(s);
                    }
                }
            }

            paragraph.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, paragraph);

            return paragraph;
        }

        /// <summary>
        /// Creates block element based on paragraph type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IBlockElement CreateBlock(ParagraphConvTargetEnum type)
        {
            IBlockElement paragraph;
            switch (type)
            {
                case ParagraphConvTargetEnum.H1:
                    paragraph = new H1();
                    break;
                case ParagraphConvTargetEnum.H2:
                    paragraph = new H2();
                    break;
                case ParagraphConvTargetEnum.H3:
                    paragraph = new H3();
                    break;
                case ParagraphConvTargetEnum.H4:
                    paragraph = new H4();
                    break;
                case ParagraphConvTargetEnum.H5:
                    paragraph = new H5();
                    break;
                case ParagraphConvTargetEnum.H6:
                    paragraph = new H6();
                    break;
                default: // Paragraph or anything else
                    paragraph = new Paragraph();
                    break;

            }
            return paragraph;
        }

    }
}
