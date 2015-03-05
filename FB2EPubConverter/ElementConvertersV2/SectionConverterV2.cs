using System;
using System.Collections.Generic;
using FB2EPubConverter.ElementConvertersV2.Epigraph;
using FB2EPubConverter.ElementConvertersV2.Poem;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using FB2EPubConverter.ElementConvertersV2.Tables;
using ConverterContracts.ConversionElementsStyles;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class SectionConverterV2 : BaseElementConverterV2
    {
        public int RecursionLevel { get; set;}
        public bool LinkSection { get; set; }
        public ConverterOptionsV2 Settings { get; set; }

        private readonly SizeLimitChecker _checker = new SizeLimitChecker();

        /// <summary>
        /// Converts FB2 section element
        /// </summary>
        /// <param name="sectionItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public List<IHTMLItem> Convert(SectionItem sectionItem)
        {
            if (sectionItem == null)
            {
                throw new ArgumentNullException("sectionItem");
            }
            var resList = new List<IHTMLItem>();
            Logger.Log.Debug("Converting section");

            var content = new Div(HTMLElementType.XHTML11);
            long documentSize = 0;
            _checker.MaxSizeLimit = Settings.MaxSize;

            SetClassType(content, string.Format(ElementStylesV2.SectionItemFormat, RecursionLevel));

            content.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(sectionItem.ID, content);

            if (sectionItem.Lang != null)
            {
                content.GlobalAttributes.Language.Value = sectionItem.Lang;
            }


            // Load Title
            if (sectionItem.Title != null)
            {
                IHTMLItem titleItem;
                if (!LinkSection)
                {
                    var titleConverter = new TitleConverterV2();
                    titleItem = titleConverter.Convert(sectionItem.Title,
                        new TitleConverterParamsV2 { Settings = Settings, TitleLevel = RecursionLevel + 1 });
                }
                else
                {
                    var linkSectionConverter = new LinkSectionConverterV2();
                    titleItem = linkSectionConverter.Convert(sectionItem,
                        new LinkSectionConverterParamsV2{Settings = Settings});
                }
                if (titleItem != null)
                {
                    long itemSize = titleItem.EstimateSize();
                    if (_checker.ExceedSizeLimit(documentSize + itemSize))
                    {
                        var oldContent = content;
                        resList.Add(content);
                        content = new Div(HTMLElementType.XHTML11);
                        content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                        content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                        documentSize = 0;
                    }
                    if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
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
                                if (_checker.ExceedSizeLimit(documentSize + itemSize))
                                {
                                    var oldContent = content;
                                    resList.Add(content);
                                    content = new Div(HTMLElementType.XHTML11);
                                    content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                    content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                                    documentSize = 0;
                                }
                                if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
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

                var epigraphConverter = new EpigraphConverterV2();
                var epigraphItem = epigraphConverter.Convert(epigraph,
                    new EpigraphConverterParamsV2 { Settings = Settings, Level = RecursionLevel + 1 });
                long itemSize = epigraphItem.EstimateSize();
                if (_checker.ExceedSizeLimit(documentSize + itemSize))
                {
                    var oldContent = content;
                    resList.Add(content);
                    content = new Div(HTMLElementType.XHTML11);
                    content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                    content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                    documentSize = 0;
                }
                if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
                {
                    documentSize += itemSize;
                    content.Add(epigraphItem);
                }
                else
                {
                    foreach (var splitedItem in SplitDiv(epigraphItem, documentSize))
                    {
                        itemSize = splitedItem.EstimateSize();
                        if (_checker.ExceedSizeLimit(documentSize + itemSize))
                        {
                            var oldContent = content;
                            resList.Add(content);
                            content = new Div(HTMLElementType.XHTML11);
                            content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                            content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                            documentSize = 0;
                        }
                        if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
                        {
                            documentSize += itemSize;
                            content.Add(splitedItem);
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
                            var container = new Div(HTMLElementType.XHTML11);
                            var sectionImagemage = new Image(HTMLElementType.XHTML11);
                            sectionImagemage.Alt.Value = sectionImage.AltText ?? string.Empty;
                            sectionImagemage.Source.Value = Settings.ReferencesManager.AddImageRefferenced(sectionImage, sectionImagemage);
                            sectionImagemage.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(sectionImage.ID,
                                                                                    sectionImagemage);
                            if (sectionImage.Title != null)
                            {
                                sectionImagemage.GlobalAttributes.Title.Value = sectionImage.Title;
                            }
                            SetClassType(container, ElementStylesV2.SectionImage);
                            container.Add(sectionImagemage);
                            long itemSize = container.EstimateSize();
                            if (_checker.ExceedSizeLimit(documentSize + itemSize))
                            {
                                var oldContent = content;
                                resList.Add(content);
                                content = new Div(HTMLElementType.XHTML11);
                                content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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
                var annotationConverter = new AnnotationConverterV2();
                IHTMLItem annotationItem = annotationConverter.Convert(sectionItem.Annotation,  new AnnotationConverterParamsV2{ Level = RecursionLevel + 1 ,Settings = Settings});
                long itemSize = annotationItem.EstimateSize();
                if (_checker.ExceedSizeLimit(documentSize + itemSize))
                {
                    var oldContent = content;
                    resList.Add(content);
                    content = new Div(HTMLElementType.XHTML11);
                    content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                    content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                    documentSize = 0;
                }
                if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
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
                            if (_checker.ExceedSizeLimit(documentSize + itemSize))
                            {
                                var oldContent = content;
                                resList.Add(content);
                                content = new Div(HTMLElementType.XHTML11);
                                content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                                documentSize = 0;
                            }
                            if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
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
                    IHTMLItem newItem = null;
                    if (item is SubTitleItem)
                    {
                        var subtitleConverter = new SubtitleConverterV2();
                        newItem = subtitleConverter.Convert(item as SubTitleItem,new SubtitleConverterParamsV2{ Settings = Settings});
                    }
                    else if (item is ParagraphItem)
                    {
                        var paragraphConverter = new ParagraphConverterV2();
                        newItem = paragraphConverter.Convert(item as ParagraphItem,
                            new ParagraphConverterParamsV2 { ResultType = ParagraphConvTargetEnumV2.Paragraph, StartSection =startSection,Settings = Settings});
                        startSection = false;
                    }
                    else if (item is PoemItem)
                    {
                        var poemConverter = new PoemConverterV2();
                        newItem = poemConverter.Convert(item as PoemItem,
                            new PoemConverterParamsV2 { Settings = Settings, Level = RecursionLevel + 1 });
                    }
                    else if (item is CiteItem)
                    {
                        var citationConverter = new CitationConverterV2();
                        newItem = citationConverter.Convert(item as CiteItem,
                            new CitationConverterParamsV2 { Level = RecursionLevel + 1 , Settings = Settings});
                    }
                    else if (item is EmptyLineItem)
                    {
                        var emptyLineConverter = new EmptyLineConverterV2();
                        newItem = emptyLineConverter.Convert();
                    }
                    else if (item is TableItem)
                    {
                        var tableConverter = new TableConverterV2();
                        newItem = tableConverter.Convert(item as TableItem,
                            new TableConverterParamsV2 { Settings = Settings});
                    }
                    else if ((item is ImageItem) && Settings.Images.HasRealImages())
                    {
                        var fb2Img = item as ImageItem;
                        // if it's not section image and it's used
                        if ((sectionItem.SectionImages.Find(x => x == fb2Img) == null) && (fb2Img.HRef != null))
                        {
                            if (Settings.Images.IsImageIdReal(fb2Img.HRef))
                            {
                                var enclosing = new Div(HTMLElementType.XHTML11); // we use the enclosing so the user can style center it
                                var imageConverter = new ImageConverterV2();
                                enclosing.Add(imageConverter.Convert(fb2Img,new ImageConverterParamsV2{Settings = Settings}));
                                SetClassType(enclosing, ElementStylesV2.NormalImage);
                                newItem = enclosing;
                            }
                        }
                    }
                    if (newItem != null)
                    {
                        long itemSize = newItem.EstimateSize();
                        if (_checker.ExceedSizeLimit(documentSize + itemSize))
                        {
                            var oldContent = content;
                            resList.Add(content);
                            content = new Div(HTMLElementType.XHTML11);
                            content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                            content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                            documentSize = 0;
                        }
                        if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
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
                                    if (_checker.ExceedSizeLimit(documentSize + itemSize))
                                    {
                                        var oldContent = content;
                                        resList.Add(content);
                                        content = new Div(HTMLElementType.XHTML11);
                                        content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                        content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                                        documentSize = 0;
                                    }
                                    if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
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


        private IEnumerable<IHTMLItem> SplitDiv(Div div, long documentSize)
        {
            var resList = new List<IHTMLItem>();
            long newDocumentSize = documentSize;
            var container = new Div(HTMLElementType.XHTML11);
            container.GlobalAttributes.ID.Value = div.GlobalAttributes.ID.Value;
            foreach (var element in div.SubElements())
            {
                long elementSize = element.EstimateSize();
                if (_checker.ExceedSizeLimit(elementSize + newDocumentSize))
                {
                    resList.Add(container);
                    container = new Div(HTMLElementType.XHTML11);
                    container.GlobalAttributes.Class.Value = div.GlobalAttributes.Class.Value;
                    container.GlobalAttributes.Language.Value = div.GlobalAttributes.Language.Value;
                    newDocumentSize = 0;
                }
                if (!_checker.ExceedSizeLimit(elementSize))
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