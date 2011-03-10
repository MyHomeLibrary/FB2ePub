using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fb2ePubConverter;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class SectionConverter : BaseElementConverter
    {
        public int RecursionLevel { get; set;}
        public bool LinkSection { get; set; }

        /// <summary>
        /// Converts FB2 section element
        /// </summary>
        /// <param name="sectionItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public List<IXHTMLItem> Convert(SectionItem sectionItem)
        {
            if (sectionItem == null)
            {
                throw new ArgumentNullException("sectionItem");
            }
            List<IXHTMLItem> resList = new List<IXHTMLItem>();
            Logger.Log.Debug("Convering section");

            IBlockElement content = new Div();
            long documentSize = 0;

            content.Class.Value = string.Format("section{0}", RecursionLevel);

            content.ID.Value = Settings.ReferencesManager.AddIdUsed(sectionItem.ID, content);

            if (sectionItem.Lang != null)
            {
                content.Language.Value = sectionItem.Lang;
            }


            // Load Title
            if (sectionItem.Title != null)
            {
                IXHTMLItem titleItem = null;
                if (!LinkSection)
                {
                    TitleConverter titleConverter = new TitleConverter {Settings = Settings};
                    titleItem = titleConverter.Convert(sectionItem.Title, RecursionLevel + 1);
                }
                else
                {
                    LinkSectionConverter linkSectionConverter = new LinkSectionConverter {Settings = Settings};
                    titleItem = linkSectionConverter.Convert(sectionItem);
                }
                if (titleItem != null)
                {
                    long itemSize = titleItem.EstimateSize();
                    if (documentSize + itemSize >= Settings.MaxSize)
                    {
                        IBlockElement oldContent = content;
                        resList.Add(content);
                        content = new Div();
                        content.Class.Value = oldContent.Class.Value;
                        content.Language.Value = oldContent.Language.Value;
                        documentSize = 0;
                    }
                    if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                    {
                        documentSize += itemSize;
                        content.Add(titleItem);
                    }
                    else
                    {
                        if (titleItem is Div) // if the item that bigger than max size is Div block
                        {
                            foreach (var splitedItem in SplitDiv(titleItem as Div, documentSize))
                            {
                                itemSize = splitedItem.EstimateSize();
                                if (documentSize + itemSize >= Settings.MaxSize)
                                {
                                    IBlockElement oldContent = content;
                                    resList.Add(content);
                                    content = new Div();
                                    content.Class.Value = oldContent.Class.Value;
                                    content.Language.Value = oldContent.Language.Value;
                                    documentSize = 0;
                                }
                                if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                                {
                                    documentSize += itemSize;
                                    content.Add(splitedItem);
                                }
                            }
                        }
                    }
                }
            }

            // Load epigraphs
            foreach (var epigraph in sectionItem.Epigraphs)
            {

                EpigraphConverter epigraphConverter = new EpigraphConverter {Settings = Settings};
                IXHTMLItem epigraphItem = epigraphConverter.Convert(epigraph,RecursionLevel + 1, false);
                long itemSize = epigraphItem.EstimateSize();
                if (documentSize + itemSize >= Settings.MaxSize)
                {
                    IBlockElement oldContent = content;
                    resList.Add(content);
                    content = new Div();
                    content.Class.Value = oldContent.Class.Value;
                    content.Language.Value = oldContent.Language.Value;
                    documentSize = 0;
                }
                if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                {
                    documentSize += itemSize;
                    content.Add(epigraphItem);
                }
                else
                {
                    if (epigraphItem is Div) // if the item that bigger than max size is Div block
                    {
                        foreach (var splitedItem in SplitDiv(epigraphItem as Div, documentSize))
                        {
                            itemSize = splitedItem.EstimateSize();
                            if (documentSize + itemSize >= Settings.MaxSize)
                            {
                                IBlockElement oldContent = content;
                                resList.Add(content);
                                content = new Div();
                                content.Class.Value = oldContent.Class.Value;
                                content.Language.Value = oldContent.Language.Value;
                                documentSize = 0;
                            }
                            if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                            {
                                documentSize += itemSize;
                                content.Add(splitedItem);
                            }
                        }
                    }
                }
            }

            // Load section image
            if (Settings.Images.HasRealImages())
            {
                foreach (var sectionImage in sectionItem.SectionImages)
                {
                    if (sectionImage.HRef != null)
                    {
                        if (Settings.Images.IsImageIdReal(sectionImage.HRef))
                        {
                            Div container = new Div();
                            Image sectionImagemage = new Image();
                            if (sectionImage.AltText != null)
                            {
                                sectionImagemage.Alt.Value = sectionImage.AltText;
                            }
                            sectionImagemage.Source.Value = Settings.ReferencesManager.AddImageRefferenced(sectionImage, sectionImagemage);
                            sectionImagemage.ID.Value = Settings.ReferencesManager.AddIdUsed(sectionImage.ID,
                                                                                    sectionImagemage);
                            if (sectionImage.Title != null)
                            {
                                sectionImagemage.Title.Value = sectionImage.Title;
                            }
                            container.Class.Value = "section_image";
                            container.Add(sectionImagemage);
                            long itemSize = container.EstimateSize();
                            if (documentSize + itemSize >= Settings.MaxSize)
                            {
                                IBlockElement oldContent = content;
                                resList.Add(content);
                                content = new Div();
                                content.Class.Value = oldContent.Class.Value;
                                content.Language.Value = oldContent.Language.Value;
                                documentSize = 0;
                            }
                            documentSize += itemSize;
                            content.Add(container);
                            Settings.Images.ImageIdUsed(sectionImage.HRef);
                        }
                    }

                }
            }

            // Load annotations
            if (sectionItem.Annotation != null)
            {
                AnnotationConverter annotationConverter = new AnnotationConverter {Settings = Settings};
                IXHTMLItem annotationItem = annotationConverter.Convert(sectionItem.Annotation,RecursionLevel + 1);
                long itemSize = annotationItem.EstimateSize();
                if (documentSize + itemSize >= Settings.MaxSize)
                {
                    IBlockElement oldContent = content;
                    resList.Add(content);
                    content = new Div();
                    content.Class.Value = oldContent.Class.Value;
                    content.Language.Value = oldContent.Language.Value;
                    documentSize = 0;
                }
                if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                {
                    documentSize += itemSize;
                    content.Add(annotationItem);
                }
                else
                {
                    if (annotationItem is Div) // if the item that bigger than max size is Div block
                    {
                        foreach (var splitedItem in SplitDiv(annotationItem as Div, documentSize))
                        {
                            itemSize = splitedItem.EstimateSize();
                            if (documentSize + itemSize >= Settings.MaxSize)
                            {
                                IBlockElement oldContent = content;
                                resList.Add(content);
                                content = new Div();
                                content.Class.Value = oldContent.Class.Value;
                                content.Language.Value = oldContent.Language.Value;
                                documentSize = 0;
                            }
                            if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                            {
                                documentSize += itemSize;
                                content.Add(splitedItem);
                            }
                        }
                    }
                }
            }

            // Parse all elements only if section has no sub section
            if (sectionItem.SubSections.Count == 0)
            {
                bool startSection = true;
                foreach (var item in sectionItem.Content)
                {
                    IXHTMLItem newItem = null;
                    if (item is SubTitleItem)
                    {
                        SubtitleConverter subtitleConverter = new SubtitleConverter {Settings = Settings};
                        newItem = subtitleConverter.Convert(item as SubTitleItem);
                    }
                    else if (item is ParagraphItem)
                    {
                        ParagraphConverter paragraphConverter = new ParagraphConverter {Settings = Settings};
                        newItem = paragraphConverter.Convert(item as ParagraphItem,ParagraphConvTargetEnum.Paragraph, startSection);
                        startSection = false;
                    }
                    else if (item is PoemItem)
                    {
                        PoemConverter poemConverter = new PoemConverter {Settings = Settings};
                        newItem = poemConverter.Convert(item as PoemItem,RecursionLevel + 1);
                    }
                    else if (item is CiteItem)
                    {
                        CitationConverter citationConverter = new CitationConverter {Settings = Settings};
                        newItem = citationConverter.Convert(item as CiteItem,RecursionLevel + 1);
                    }
                    else if (item is EmptyLineItem)
                    {
                        EmptyLineConverter emptyLineConverter = new EmptyLineConverter();
                        newItem = emptyLineConverter.Convert();
                    }
                    else if (item is TableItem)
                    {
                        TableConverter tableConverter = new TableConverter {Settings = Settings};
                        newItem = tableConverter.Convert(item as TableItem);
                    }
                    else if ((item is ImageItem) && Settings.Images.HasRealImages())
                    {
                        ImageItem fb2Img = item as ImageItem;
                        // if it's not section image and it's used
                        if ((sectionItem.SectionImages.Find(x => x == fb2Img) == null) && (fb2Img.HRef != null))
                        {
                            if (Settings.Images.IsImageIdReal(fb2Img.HRef))
                            {
                                Div enclosing = new Div(); // we use the enclosing so the user can style center it
                                ImageConverter imageConverter = new ImageConverter {Settings = Settings};
                                enclosing.Add(imageConverter.Convert(fb2Img));
                                enclosing.Class.Value = "normal_image";
                                newItem = enclosing;
                            }
                        }
                    }
                    if (newItem != null)
                    {
                        long itemSize = newItem.EstimateSize();
                        if (documentSize + itemSize >= Settings.MaxSize)
                        {
                            IBlockElement oldContent = content;
                            resList.Add(content);
                            content = new Div();
                            content.Class.Value = oldContent.Class.Value;
                            content.Language.Value = oldContent.Language.Value;
                            documentSize = 0;
                        }
                        if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                        {
                            documentSize += itemSize;
                            content.Add(newItem);
                        }
                        else
                        {
                            if (newItem is Div) // if the item that bigger than max size is Div block
                            {
                                foreach (var splitedItem in SplitDiv(newItem as Div, documentSize))
                                {
                                    itemSize = splitedItem.EstimateSize();
                                    if (documentSize + itemSize >= Settings.MaxSize)
                                    {
                                        IBlockElement oldContent = content;
                                        resList.Add(content);
                                        content = new Div();
                                        content.Class.Value = oldContent.Class.Value;
                                        content.Language.Value = oldContent.Language.Value;
                                        documentSize = 0;
                                    }
                                    if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                                    {
                                        documentSize += itemSize;
                                        content.Add(splitedItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            resList.Add(content);
            return resList;          
        }

        private List<IXHTMLItem> SplitDiv(Div div, long documentSize)
        {
            List<IXHTMLItem> resList = new List<IXHTMLItem>();
            long newDocumentSize = documentSize;
            Div container = new Div();
            container.ID.Value = div.ID.Value;
            foreach (var element in div.SubElements())
            {
                long elementSize = element.EstimateSize();
                if (elementSize + newDocumentSize >= Settings.MaxSize)
                {
                    resList.Add(container);
                    container = new Div();
                    container.Class.Value = div.Class.Value;
                    container.Language.Value = div.Language.Value;
                    newDocumentSize = 0;
                }
                if (elementSize < Settings.MaxSize)
                {
                    container.Add(element);
                    newDocumentSize += elementSize;
                }
            }
            resList.Add(container);
            return resList;
        }

    }
}