using System;
using System.Collections.Generic;
using Fb2ePubConverter;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class SectionConverter 
    {
        public int RecursionLevel { get; set;}
        public bool LinkSection { get; set; }
        public ConverterOptions Settings { get; set; }

        /// <summary>
        /// Converts FB2 section element
        /// </summary>
        /// <param name="sectionItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <returns>XHTML representation</returns>
        public List<IHTMLItem> Convert(SectionItem sectionItem,HTMLElementType compatibility)
        {
            if (sectionItem == null)
            {
                throw new ArgumentNullException("sectionItem");
            }
            var resList = new List<IHTMLItem>();
            Logger.Log.Debug("Convering section");

            var content = new Div(compatibility);
            long documentSize = 0;

            content.GlobalAttributes.Class.Value = string.Format("section{0}", RecursionLevel);

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
                    var titleConverter = new TitleConverter();
                    titleItem = titleConverter.Convert(sectionItem.Title,compatibility,
                        new TitleConverterParams { Settings = Settings, TitleLevel = RecursionLevel + 1 });
                }
                else
                {
                    var linkSectionConverter = new LinkSectionConverter();
                    titleItem = linkSectionConverter.Convert(sectionItem,compatibility,
                        new LinkSectionConverterParams{Settings = Settings});
                }
                if (titleItem != null)
                {
                    long itemSize = titleItem.EstimateSize();
                    if (documentSize + itemSize >= Settings.MaxSize)
                    {
                        var oldContent = content;
                        resList.Add(content);
                        content = new Div(compatibility);
                        content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                        content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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
                            foreach (var splitedItem in SplitDiv(titleItem as Div, documentSize,compatibility))
                            {
                                itemSize = splitedItem.EstimateSize();
                                if (documentSize + itemSize >= Settings.MaxSize)
                                {
                                    var oldContent = content;
                                    resList.Add(content);
                                    content = new Div(compatibility);
                                    content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                    content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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

                var epigraphConverter = new EpigraphConverter();
                var epigraphItem = epigraphConverter.Convert(epigraph,compatibility,
                    new EpigraphConverterParams { Settings = Settings, Level = RecursionLevel + 1 });
                long itemSize = epigraphItem.EstimateSize();
                if (documentSize + itemSize >= Settings.MaxSize)
                {
                    var oldContent = content;
                    resList.Add(content);
                    content = new Div(compatibility);
                    content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                    content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                    documentSize = 0;
                }
                if (itemSize < Settings.MaxSize) // if we can "fit" element into a max size XHTML document
                {
                    documentSize += itemSize;
                    content.Add(epigraphItem);
                }
                else
                {
                    foreach (var splitedItem in SplitDiv(epigraphItem, documentSize, compatibility))
                    {
                        itemSize = splitedItem.EstimateSize();
                        if (documentSize + itemSize >= Settings.MaxSize)
                        {
                            var oldContent = content;
                            resList.Add(content);
                            content = new Div(compatibility);
                            content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                            content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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

            // Load section image
            if (Settings.Images.HasRealImages())
            {
                foreach (var sectionImage in sectionItem.SectionImages)
                {
                    if (sectionImage.HRef != null)
                    {
                        if (Settings.Images.IsImageIdReal(sectionImage.HRef))
                        {
                            var container = new Div(compatibility);
                            var sectionImagemage = new Image(compatibility);
                            if (sectionImage.AltText != null)
                            {
                                sectionImagemage.Alt.Value = sectionImage.AltText;
                            }
                            sectionImagemage.Source.Value = Settings.ReferencesManager.AddImageRefferenced(sectionImage, sectionImagemage);
                            sectionImagemage.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(sectionImage.ID,
                                                                                    sectionImagemage);
                            if (sectionImage.Title != null)
                            {
                                sectionImagemage.GlobalAttributes.Title.Value = sectionImage.Title;
                            }
                            container.GlobalAttributes.Class.Value = "section_image";
                            container.Add(sectionImagemage);
                            long itemSize = container.EstimateSize();
                            if (documentSize + itemSize >= Settings.MaxSize)
                            {
                                var oldContent = content;
                                resList.Add(content);
                                content = new Div(compatibility);
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
                var annotationConverter = new AnnotationConverter();
                IHTMLItem annotationItem = annotationConverter.Convert(sectionItem.Annotation, compatibility, new AnnotationConverterParams{ Level = RecursionLevel + 1 ,Settings = Settings});
                long itemSize = annotationItem.EstimateSize();
                if (documentSize + itemSize >= Settings.MaxSize)
                {
                    var oldContent = content;
                    resList.Add(content);
                    content = new Div(compatibility);
                    content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                    content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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
                        foreach (var splitedItem in SplitDiv(annotationItem as Div, documentSize,compatibility))
                        {
                            itemSize = splitedItem.EstimateSize();
                            if (documentSize + itemSize >= Settings.MaxSize)
                            {
                                var oldContent = content;
                                resList.Add(content);
                                content = new Div(compatibility);
                                content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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
                    IHTMLItem newItem = null;
                    if (item is SubTitleItem)
                    {
                        var subtitleConverter = new SubtitleConverter();
                        newItem = subtitleConverter.Convert(item as SubTitleItem,compatibility,new SubtitleConverterParams{ Settings = Settings});
                    }
                    else if (item is ParagraphItem)
                    {
                        var paragraphConverter = new ParagraphConverter();
                        newItem = paragraphConverter.Convert(item as ParagraphItem,compatibility,
                            new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, StartSection =startSection,Settings = Settings});
                        startSection = false;
                    }
                    else if (item is PoemItem)
                    {
                        var poemConverter = new PoemConverter();
                        newItem = poemConverter.Convert(item as PoemItem,compatibility,
                            new PoemConverterParams { Settings = Settings, Level = RecursionLevel + 1 });
                    }
                    else if (item is CiteItem)
                    {
                        var citationConverter = new CitationConverter();
                        newItem = citationConverter.Convert(item as CiteItem,compatibility,
                            new CitationConverterParams { Level = RecursionLevel + 1 , Settings = Settings});
                    }
                    else if (item is EmptyLineItem)
                    {
                        var emptyLineConverter = new EmptyLineConverter();
                        newItem = emptyLineConverter.Convert(compatibility);
                    }
                    else if (item is TableItem)
                    {
                        var tableConverter = new TableConverter();
                        newItem = tableConverter.Convert(item as TableItem,compatibility,
                            new TableConverterParams { Settings = Settings});
                    }
                    else if ((item is ImageItem) && Settings.Images.HasRealImages())
                    {
                        var fb2Img = item as ImageItem;
                        // if it's not section image and it's used
                        if ((sectionItem.SectionImages.Find(x => x == fb2Img) == null) && (fb2Img.HRef != null))
                        {
                            if (Settings.Images.IsImageIdReal(fb2Img.HRef))
                            {
                                var enclosing = new Div(compatibility); // we use the enclosing so the user can style center it
                                var imageConverter = new ImageConverter();
                                enclosing.Add(imageConverter.Convert(fb2Img,compatibility,new ImageConverterParams{Settings = Settings}));
                                enclosing.GlobalAttributes.Class.Value = "normal_image";
                                newItem = enclosing;
                            }
                        }
                    }
                    if (newItem != null)
                    {
                        long itemSize = newItem.EstimateSize();
                        if (documentSize + itemSize >= Settings.MaxSize)
                        {
                            var oldContent = content;
                            resList.Add(content);
                            content = new Div(compatibility);
                            content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                            content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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
                                foreach (var splitedItem in SplitDiv(newItem as Div, documentSize,compatibility))
                                {
                                    itemSize = splitedItem.EstimateSize();
                                    if (documentSize + itemSize >= Settings.MaxSize)
                                    {
                                        var oldContent = content;
                                        resList.Add(content);
                                        content = new Div(compatibility);
                                        content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                                        content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
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

        private IEnumerable<IHTMLItem> SplitDiv(Div div, long documentSize, HTMLElementType compatibility)
        {
            var resList = new List<IHTMLItem>();
            long newDocumentSize = documentSize;
            var container = new Div(compatibility);
            container.GlobalAttributes.ID.Value = div.GlobalAttributes.ID.Value;
            foreach (var element in div.SubElements())
            {
                long elementSize = element.EstimateSize();
                if (elementSize + newDocumentSize >= Settings.MaxSize)
                {
                    resList.Add(container);
                    container = new Div(compatibility);
                    container.GlobalAttributes.Class.Value = div.GlobalAttributes.Class.Value;
                    container.GlobalAttributes.Language.Value = div.GlobalAttributes.Language.Value;
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