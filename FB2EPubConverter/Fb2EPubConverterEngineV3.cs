using System;
using System.Linq;
using EPubLibrary;
using EPubLibrary.XHTML_Items;
using Fb2ePubConverter;
using FB2EPubConverter.ElementConvertersV3;
using Fb2epubSettings;
using FB2Library;
using FB2Library.HeaderItems;
using XHTMLClassLibrary.BaseElements;
using Logger = EPubLibrary.Logger;

namespace FB2EPubConverter
{
    internal class Fb2EPubConverterEngineV3 : Fb2EPubConverterEngineBase
    {
        protected override void ConvertContent(FB2File fb2File,EPubFile epubFile)
        {
            PassHeaderDataFromFb2ToEpub(epubFile, fb2File);
            ConvertAnnotation(fb2File.TitleInfo, epubFile);
            PassCoverImageFromFB2(fb2File.TitleInfo.Cover, epubFile);
            SetupCSS(epubFile);
            SetupFonts(epubFile);
            PassTextFromFb2ToEpub(epubFile, fb2File);
            PassFb2InfoToEpub(epubFile, fb2File);
            UpdateInternalLinks(epubFile, fb2File);
            PassImagesDataFromFb2ToEpub(epubFile, fb2File);
        }

        private void PassImagesDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            Images.ConvertFb2ToEpubImages(fb2File.Images, epubFile.Images);
        }


        private void UpdateInternalLinks(EPubFile epubFile, FB2File fb2File)
        {
            ReferencesManager.RemoveInvalidAnchors();
            ReferencesManager.RemoveInvalidImages(fb2File.Images);
            ReferencesManager.RemapAnchors(epubFile);
        }


        private void PassTextFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Passes FB2 info to the EPub file to be added at the end of the book
        /// </summary>
        /// <param name="epubFile">destination epub object</param>
        /// <param name="fb2File">source fb2 object</param>
        private void PassFb2InfoToEpub(EPubFile epubFile, FB2File fb2File)
        {
            if (!Settings.CommonSettings.Fb2Info)
            {
                return;
            }
            throw new NotImplementedException();
        }

        private void PassHeaderDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            Logger.Log.Debug("Passing header data from FB2 to EPUB");

            if (fb2File.MainBody == null)
            {
                throw new NullReferenceException("MainBody section of the file passed is null");
            }
            var headerDataConverter = new HeaderDataConverterV3(Settings.CommonSettings,Settings.V3Settings);
            headerDataConverter.Convert(epubFile, fb2File);
        }

        private void PassCoverImageFromFB2(CoverPage coverPage, EPubFile epubFile)
        {
            // if we have at least one coverpage image
            if ((coverPage != null) && (coverPage.HasImages()) && (coverPage.CoverpageImages[0].HRef != null))
            {
                // we add just first one 
                epubFile.AddCoverImage(coverPage.CoverpageImages[0].HRef);
                Images.ImageIdUsed(coverPage.CoverpageImages[0].HRef);
            }
        }

        private void PassPublisherInfoFromFB2(FB2File fb2File, EPubFile epubFile)
        {
            if (fb2File.PublishInfo.BookTitle != null)
            {
                var bookTitle = new Title
                {
                    TitleName =
                        epubFile.Transliterator.Translate(fb2File.PublishInfo.BookTitle.Text, epubFile.TranslitMode),
                    Language =
                        !string.IsNullOrEmpty(fb2File.PublishInfo.BookTitle.Language)
                            ? fb2File.PublishInfo.BookTitle.Language
                            : fb2File.TitleInfo.Language
                };
                if ((Settings.CommonSettings.IgnoreTitle != IgnoreInfoSourceOptions.IgnorePublishTitle) && (Settings.CommonSettings.IgnoreTitle != IgnoreInfoSourceOptions.IgnoreMainAndPublish) &&
                    Settings.CommonSettings.IgnoreTitle != IgnoreInfoSourceOptions.IgnoreSourceAndPublish)
                {
                    bookTitle.TitleType = TitleType.PublishInfo;
                    epubFile.Title.BookTitles.Add(bookTitle);
                }
            }


            if (fb2File.PublishInfo.ISBN != null)
            {
                var bookId = new Identifier
                {
                    IdentifierName = "BookISBN",
                    ID = fb2File.PublishInfo.ISBN.Text,
                    Scheme = "ISBN"
                };
                epubFile.Title.Identifiers.Add(bookId);
            }


            if (fb2File.PublishInfo.Publisher != null)
            {
                epubFile.Title.Publisher.PublisherName = epubFile.Transliterator.Translate(fb2File.PublishInfo.Publisher.Text, epubFile.TranslitMode);
            }


            try
            {
                if (fb2File.PublishInfo.Year.HasValue)
                {
                    var date = new DateTime(fb2File.PublishInfo.Year.Value, 1, 1);
                    epubFile.Title.DatePublished = date;
                }
            }
            catch (FormatException ex)
            {
                Logger.Log.DebugFormat("Date reading format exception: {0}", ex);
            }
            catch (Exception exAll)
            {
                Logger.Log.ErrorFormat("Date reading exception: {0}", exAll);
            }
        }


        protected override void ConvertAnnotation(ItemTitleInfo titleInfo, EPubFile epubFile)
        {
            if (titleInfo.Annotation != null)
            {
                epubFile.Title.Description = titleInfo.Annotation.ToString();
                epubFile.AnnotationPage = new AnnotationPageFile(HTMLElementType.HTML5);
                //var converterSettings = new ConverterOptionsV2
                //{
                //    CapitalDrop = Settings.CommonSettings.CapitalDrop,
                //    Images = Images,
                //    MaxSize = MaxSize,
                //    ReferencesManager = ReferencesManager,
                //};
                //var annotationConverter = new AnnotationConverterV2();
                //epubFile.AnnotationPage.BookAnnotation = (Div)annotationConverter.Convert(fb2File.TitleInfo.Annotation,
                //   new AnnotationConverterParams { Settings = converterSettings, Level = 1 });
            }
            throw new NotImplementedException();
        }


        private void PassSeriesData(FB2File fb2File, EPubFile epubFile)
        {
            epubFile.Collections.CollectionMembers.Clear();
            foreach (var seq in fb2File.TitleInfo.Sequences)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    var collectionMember = new CollectionMember
                    {
                        CollectionName = seq.Name,
                        Type = CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    epubFile.Collections.CollectionMembers.Add(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new CollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        epubFile.Collections.CollectionMembers.Add(collectionMember);
                    }
                }
            }
        }

        protected override void Reset()
        {
            base.Reset();
        }
    }
}
