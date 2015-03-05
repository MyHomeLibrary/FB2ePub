using System;
using System.Collections.Generic;
using ConverterContracts.ConversionElementsStyles;
using FB2EPubConverter.ElementConvertersV3.Epigraph;
using FB2EPubConverter.ElementConvertersV3.Poem;
using FB2EPubConverter.ElementConvertersV3.Tables;
using FB2Library.Elements;
using FB2Library.Elements.Poem;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class SectionConverterV3 : BaseElementConverterV3
    {
        private readonly SizeLimitChecker _checker = new SizeLimitChecker();

        public int RecursionLevel { private get; set; }
        public bool LinkSection { private get; set; }
        public ConverterOptionsV3 Settings { private get; set; }

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

            var content = new Div(HTMLElementType.HTML5);
            long documentSize = 0;
            _checker.MaxSizeLimit = Settings.MaxSize;

            SetClassType(content, string.Format(ElementStylesV3.SectionItemFormat, RecursionLevel));

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
                    var titleConverter = new TitleConverterV3();
                    titleItem = titleConverter.Convert(sectionItem.Title,
                        new TitleConverterParamsV3 { Settings = Settings, TitleLevel = RecursionLevel + 1 });
                }
                else
                {
                    var linkSectionConverter = new LinkSectionConverterV3();
                    titleItem = linkSectionConverter.Convert(sectionItem,
                        new LinkSectionConverterParamsV3 { Settings = Settings });
                }
                if (titleItem != null)
                {
                    documentSize = SplitBlockHTMLItem(titleItem,content,resList,documentSize);
                }
            }

            // Load epigraphs
            foreach (var epigraph in sectionItem.Epigraphs)
            {
                var epigraphConverter = new EpigraphConverterV3();
                var epigraphItem = epigraphConverter.Convert(epigraph,
                    new EpigraphConverterParamsV3 { Settings = Settings, Level = RecursionLevel + 1 });
                documentSize = SplitBlockHTMLItem(epigraphItem, content, resList, documentSize);
            }

            // Load section image
            if (Settings.Images.HasRealImages())
            {
                foreach (var sectionImage in sectionItem.SectionImages)
                {
                    documentSize = ConvertSectionImage(sectionImage, content,resList,documentSize);
                }
            }

            // Load annotations
            if (sectionItem.Annotation != null)
            {
                var annotationConverter = new AnnotationConverterV3();
                IHTMLItem annotationItem = annotationConverter.Convert(sectionItem.Annotation, new AnnotationConverterParamsV3 { Level = RecursionLevel + 1, Settings = Settings });
                documentSize = SplitBlockHTMLItem(annotationItem, content, resList, documentSize);
            }

            // Parse all elements only if section has no sub section
            if (sectionItem.SubSections.Count == 0)
            {
                bool startSection = true;
                foreach (var item in sectionItem.Content)
                {
                    documentSize = ConvertSimpleSubItem(item, sectionItem,content, resList, ref startSection, documentSize);
                }
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
                var subtitleConverter = new SubtitleConverterV3();
                newItem = subtitleConverter.Convert(item as SubTitleItem, new SubtitleConverterParamsV3 { Settings = Settings });
            }
            else if (item is ParagraphItem)
            {
                var paragraphConverter = new ParagraphConverterV3();
                newItem = paragraphConverter.Convert(item as ParagraphItem,
                    new ParagraphConverterParamsV3 { ResultType = ParagraphConvTargetEnumV3.Paragraph, StartSection = startSection, Settings = Settings });
                startSection = false;
            }
            else if (item is PoemItem)
            {
                var poemConverter = new PoemConverterV3();
                newItem = poemConverter.Convert(item as PoemItem,
                    new PoemConverterParamsV3 { Settings = Settings, Level = RecursionLevel + 1 });
            }
            else if (item is CiteItem)
            {
                var citationConverter = new CitationConverterV3();
                newItem = citationConverter.Convert(item as CiteItem,
                    new CitationConverterParamsV3 { Level = RecursionLevel + 1, Settings = Settings });
            }
            else if (item is EmptyLineItem)
            {
                var emptyLineConverter = new EmptyLineConverterV3();
                newItem = emptyLineConverter.Convert();
            }
            else if (item is TableItem)
            {
                var tableConverter = new TableConverterV3();
                newItem = tableConverter.Convert(item as TableItem,
                    new TableConverterParamsV3 { Settings = Settings });
            }
            else if ((item is ImageItem) && Settings.Images.HasRealImages())
            {
                var fb2Img = item as ImageItem;
                // if it's not section image and it's used
                if ((sectionItem.SectionImages.Find(x => x == fb2Img) == null) && (fb2Img.HRef != null))
                {
                    if (Settings.Images.IsImageIdReal(fb2Img.HRef))
                    {
                        var enclosing = new Div(HTMLElementType.HTML5); // we use the enclosing so the user can style center it
                        var imageConverter = new ImageConverterV3();
                        enclosing.Add(imageConverter.Convert(fb2Img, new ImageConverterParamsV3 { Settings = Settings }));
                        SetClassType(enclosing, ElementStylesV3.NormalImage);
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
            var container = new Div(HTMLElementType.HTML5);
            container.GlobalAttributes.ID.Value = div.GlobalAttributes.ID.Value;
            foreach (var element in div.SubElements())
            {
                long elementSize = element.EstimateSize();
                if (_checker.ExceedSizeLimit(elementSize + newDocumentSize))
                {
                    resList.Add(container);
                    container = new Div(HTMLElementType.HTML5);
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
