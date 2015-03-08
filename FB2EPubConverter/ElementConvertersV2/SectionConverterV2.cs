using System;
using System.Collections.Generic;
using System.Linq;
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
        public int RecursionLevel { private get; set;}
        public bool LinkSection { private get; set; }
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
                    documentSize = SplitBlockHTMLItem(titleItem, content, resList, documentSize);
                }
            }

            // Load epigraphs
            documentSize = (from epigraph in sectionItem.Epigraphs let epigraphConverter = new EpigraphConverterV2() select epigraphConverter.Convert(epigraph, new EpigraphConverterParamsV2 {Settings = Settings, Level = RecursionLevel + 1})).Aggregate(documentSize, (current, epigraphItem) => SplitBlockHTMLItem(epigraphItem, content, resList, current));

            // Load section image
            if (Settings.Images.HasRealImages())
            {
                documentSize = sectionItem.SectionImages.Aggregate(documentSize, (current, sectionImage) => ConvertSectionImage(sectionImage, content, resList, current));
            }

            // Load annotations
            if (sectionItem.Annotation != null)
            {
                var annotationConverter = new AnnotationConverterV2();
                IHTMLItem annotationItem = annotationConverter.Convert(sectionItem.Annotation,  new AnnotationConverterParamsV2{ Level = RecursionLevel + 1 ,Settings = Settings});
                documentSize = SplitBlockHTMLItem(annotationItem, content, resList, documentSize);
            }

            // Parse all elements only if section has no sub section
            if (sectionItem.SubSections.Count == 0)
            {
                bool startSection = true;
                sectionItem.Content.Aggregate(documentSize, (current, item) => ConvertSimpleSubItem(item, sectionItem, content, resList, ref startSection, current));
            }

            resList.Add(content);
            return resList;          
        }

        private long ConvertSimpleSubItem(IFb2TextItem item, SectionItem sectionItem, Div content, List<IHTMLItem> resList, ref bool startSection, long documentSize)
        {
            long docSize = documentSize;
            IHTMLItem newItem = null;
            if (item is SubTitleItem)
            {
                var subtitleConverter = new SubtitleConverterV2();
                newItem = subtitleConverter.Convert((SubTitleItem) item, new SubtitleConverterParamsV2 { Settings = Settings });
            }
            else if (item is ParagraphItem)
            {
                var paragraphConverter = new ParagraphConverterV2();
                newItem = paragraphConverter.Convert((ParagraphItem) item,
                    new ParagraphConverterParamsV2 { ResultType = ParagraphConvTargetEnumV2.Paragraph, StartSection = startSection, Settings = Settings });
                startSection = false;
            }
            else if (item is PoemItem)
            {
                var poemConverter = new PoemConverterV2();
                newItem = poemConverter.Convert((PoemItem) item,
                    new PoemConverterParamsV2 { Settings = Settings, Level = RecursionLevel + 1 });
            }
            else if (item is CiteItem)
            {
                var citationConverter = new CitationConverterV2();
                newItem = citationConverter.Convert(item as CiteItem,
                    new CitationConverterParamsV2 { Level = RecursionLevel + 1, Settings = Settings });
            }
            else if (item is EmptyLineItem)
            {
                var emptyLineConverter = new EmptyLineConverterV2();
                newItem = emptyLineConverter.Convert();
            }
            else if (item is TableItem)
            {
                var tableConverter = new TableConverterV2();
                newItem = tableConverter.Convert((TableItem) item,
                    new TableConverterParamsV2 { Settings = Settings });
            }
            else if ((item is ImageItem) && Settings.Images.HasRealImages())
            {
                var fb2Img = (ImageItem) item;
                // if it's not section image and it's used
                if ((sectionItem.SectionImages.Find(x => x == fb2Img) == null) && (fb2Img.HRef != null))
                {
                    if (Settings.Images.IsImageIdReal(fb2Img.HRef))
                    {
                        var enclosing = new Div(HTMLElementType.XHTML11); // we use the enclosing so the user can style center it
                        var imageConverter = new ImageConverterV2();
                        enclosing.Add(imageConverter.Convert(fb2Img, new ImageConverterParamsV2 { Settings = Settings }));
                        SetClassType(enclosing, ElementStylesV2.NormalImage);
                        newItem = enclosing;
                    }
                }
            }
            if (newItem != null)
            {
                docSize = SplitBlockHTMLItem(newItem, content, resList, docSize);
            }
            return docSize;
        }


        private long ConvertSectionImage(ImageItem sectionImage, Div content, IList<IHTMLItem> resList, long documentSize)
        {
            long docSize = documentSize;
            if (sectionImage.HRef != null)
            {
                if (Settings.Images.IsImageIdReal(sectionImage.HRef))
                {
                    var container = new Div(HTMLElementType.HTML5);
                    var sectionImagemage = new Image(HTMLElementType.HTML5);
                    sectionImagemage.Alt.Value = sectionImage.AltText ?? string.Empty;
                    sectionImagemage.Source.Value = Settings.ReferencesManager.AddImageRefferenced(sectionImage, sectionImagemage);
                    sectionImagemage.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(sectionImage.ID,
                                                                            sectionImagemage);
                    if (sectionImage.Title != null)
                    {
                        sectionImagemage.GlobalAttributes.Title.Value = sectionImage.Title;
                    }
                    SetClassType(container, ElementStylesV3.SectionImage);
                    container.Add(sectionImagemage);
                    long itemSize = container.EstimateSize();
                    if (_checker.ExceedSizeLimit(documentSize + itemSize))
                    {
                        var oldContent = content;
                        resList.Add(content);
                        content = new Div(HTMLElementType.HTML5);
                        content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                        content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                        docSize = 0;
                    }
                    docSize += itemSize;
                    content.Add(container);
                    Settings.Images.ImageIdUsed(sectionImage.HRef);
                }
            }
            return docSize;
        }


        private long SplitBlockHTMLItem(IHTMLItem item, Div content, IList<IHTMLItem> resList, long documentSize)
        {
            long docSize = documentSize;
            long itemSize = item.EstimateSize();
            if (_checker.ExceedSizeLimit(documentSize + itemSize))
            {
                var oldContent = content;
                resList.Add(content);
                content = new Div(HTMLElementType.HTML5);
                content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                docSize = 0;
            }
            if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
            {
                docSize += itemSize;
                content.Add(item);
            }
            else
            {
                var div = item as Div;
                if (div != null) // if the item that bigger than max size is Div block
                {
                    foreach (var splitedItem in SplitDiv(div, documentSize))
                    {
                        documentSize = SplitSimpleHTMLItem(splitedItem, content, resList, documentSize);
                    }
                }
            }
            return docSize;
        }

        private long SplitSimpleHTMLItem(IHTMLItem splitedItem, Div content, IList<IHTMLItem> resList, long documentSize)
        {
            long docSize = documentSize;
            long itemSize = splitedItem.EstimateSize();
            if (_checker.ExceedSizeLimit(documentSize + itemSize))
            {
                var oldContent = content;
                resList.Add(content);
                content = new Div(HTMLElementType.HTML5);
                content.GlobalAttributes.Class.Value = oldContent.GlobalAttributes.Class.Value;
                content.GlobalAttributes.Language.Value = oldContent.GlobalAttributes.Language.Value;
                docSize = 0;
            }
            if (!_checker.ExceedSizeLimit(itemSize)) // if we can "fit" element into a max size XHTML document
            {
                docSize += itemSize;
                content.Add(splitedItem);
            }
            return docSize;
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