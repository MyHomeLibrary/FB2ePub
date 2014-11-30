using System;
using System.Collections.Generic;
using System.Linq;
using EPubLibrary;
using EPubLibrary.XHTML_Items;
using Fb2ePubConverter;
using Fb2epubSettings;
using FB2Library;
using XHTMLClassLibrary.BaseElements;
using Logger = EPubLibrary.Logger;

namespace FB2EPubConverter
{
    internal class Fb2EPubConverterEngineV3 : Fb2EPubConverterEngineBase
    {
        protected override void ConvertContent(FB2File fb2File,EPubFile epubFile)
        {
            PassHeaderDataFromFb2ToEpub(epubFile, fb2File);
            throw new NotImplementedException();
        }

        private void PassHeaderDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            epubFile.Title.Languages.Clear();
            epubFile.Title.Creators.Clear();
            epubFile.Title.Contributors.Clear();
            epubFile.Title.Subjects.Clear();
            epubFile.Title.Identifiers.Clear();

            if (fb2File.MainBody == null)
            {
                throw new NullReferenceException("MainBody section of the file passed is null");
            }

            Logger.Log.Debug("Passing header data from FB2 to EPUB");


            PassTitleInfoFromFB2EPub(fb2File,epubFile);

            // Getting information from FB2 document section
            var bookId = new Identifier
            {
                ID =
                    !string.IsNullOrEmpty(fb2File.DocumentInfo.ID) ? fb2File.DocumentInfo.ID : Guid.NewGuid().ToString(),
                IdentifierName = "FB2BookID",
                Scheme = "URI"
            };
            epubFile.Title.Identifiers.Add(bookId);

            if ((fb2File.DocumentInfo.SourceOCR != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.SourceOCR.Text))
            {
                epubFile.Title.Source = new Source { SourceData = fb2File.DocumentInfo.SourceOCR.Text };
            }

            foreach (var docAuthor in fb2File.DocumentInfo.DocumentAuthors)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        epubFile.Transliterator.Translate(GenerateAuthorString(docAuthor), epubFile.TranslitMode),
                    FileAs = GenerateFileAsString(docAuthor),
                    Role = RolesEnum.Adapter
                };
                if (fb2File.TitleInfo != null)
                {
                    person.Language = fb2File.TitleInfo.Language;
                }
                epubFile.Title.Contributors.Add(person);
            }

            // Getting information from FB2 Source Title Info section
            if ((fb2File.SourceTitleInfo.BookTitle != null) && !string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Text))
            {
                var bookTitle = new Title
                {
                    TitleName =
                        epubFile.Transliterator.Translate(fb2File.SourceTitleInfo.BookTitle.Text, epubFile.TranslitMode),
                    Language =
                        string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Language) && (fb2File.TitleInfo != null)
                            ? fb2File.TitleInfo.Language
                            : fb2File.SourceTitleInfo.BookTitle.Language
                };
                if ((Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreSourceTitle) && (Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreMainAndSource)
                    && Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreSourceAndPublish)
                {
                    bookTitle.TitleType = TitleType.SourceInfo;
                    epubFile.Title.BookTitles.Add(bookTitle);
                }

                epubFile.Title.Languages.Add(fb2File.SourceTitleInfo.Language);
            }

            // add authors
            foreach (var author in fb2File.SourceTitleInfo.BookAuthors)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        epubFile.Transliterator.Translate(
                            string.Format("{0} {1} {2}", author.FirstName, author.MiddleName, author.LastName),
                            epubFile.TranslitMode),
                    FileAs = GenerateFileAsString(author),
                    Role = RolesEnum.Author,
                    Language = fb2File.SourceTitleInfo.Language
                };
                epubFile.Title.Creators.Add(person);
            }

            foreach (var translator in fb2File.SourceTitleInfo.Translators)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        epubFile.Transliterator.Translate(
                            string.Format("{0} {1} {2}", translator.FirstName, translator.MiddleName,
                                translator.LastName), epubFile.TranslitMode),
                    FileAs = GenerateFileAsString(translator),
                    Role = RolesEnum.Translator,
                    Language = fb2File.SourceTitleInfo.Language
                };
                epubFile.Title.Contributors.Add(person);
            }


            foreach (var genre in fb2File.SourceTitleInfo.Genres)
            {
                var item = new Subject
                {
                    SubjectInfo =
                        epubFile.Transliterator.Translate(Fb2GenreToDescription(genre.Genre), epubFile.TranslitMode)
                };
                if (epubFile.Title.Subjects.Contains(item))
                {
                    epubFile.Title.Subjects.Add(item);
                }

            }


            if (fb2File.PublishInfo.BookName != null)
            {
                var bookTitle = new Title
                {
                    TitleName =
                        epubFile.Transliterator.Translate(fb2File.PublishInfo.BookName.Text, epubFile.TranslitMode),
                    Language =
                        !string.IsNullOrEmpty(fb2File.PublishInfo.BookName.Language)
                            ? fb2File.PublishInfo.BookName.Language
                            : fb2File.TitleInfo.Language
                };
                if ((Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnorePublishTitle) && (Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreMainAndPublish) &&
                    Settings.CommonSettings.IgnoreTitle != IgnoreTitleOptions.IgnoreSourceAndPublish)
                {
                    bookTitle.TitleType = TitleType.PublishInfo;
                    epubFile.Title.BookTitles.Add(bookTitle);
                }
            }


            if (fb2File.PublishInfo.ISBN != null)
            {
                bookId = new Identifier
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

            // if we have at least one coverpage image
            if ((fb2File.TitleInfo.Cover != null) && (fb2File.TitleInfo.Cover.HasImages()) && (fb2File.TitleInfo.Cover.CoverpageImages[0].HRef != null))
            {
                // we add just first one 
                epubFile.AddCoverImage(fb2File.TitleInfo.Cover.CoverpageImages[0].HRef);
                Images.ImageIdUsed(fb2File.TitleInfo.Cover.CoverpageImages[0].HRef);
            }

            epubFile.Title.DateFileCreation = DateTime.Now;

            PassSeriesData(fb2File, epubFile);
            
        }

        private void PassTitleInfoFromFB2EPub(FB2File fb2File, EPubFile epubFile)
        {
            // cReate new Title page
            epubFile.TitlePage = new TitlePageFile(HTMLElementType.HTML5);

            // in case main body title is not defined (empty) 
            if ((fb2File.TitleInfo != null) && (fb2File.TitleInfo.BookTitle != null))
            {
                epubFile.TitlePage.BookTitle = fb2File.TitleInfo.BookTitle.Text;
            }

            epubFile.AllSequences.Clear();

            if (fb2File.TitleInfo != null)
            {
                // Pass all sequences
                ConvertSequences(fb2File, epubFile);

                // Getting information from FB2 Title section
                ConvertMainTitle(fb2File, epubFile);

                ConvertAnnotation(fb2File, epubFile);

                // add authors
                ConvertAuthors(fb2File,epubFile);

                // add translators
                ConvertTranslators(fb2File, epubFile);

                // genres
                ConvertGenres(fb2File, epubFile);
            }
        }

        protected override void ConvertAnnotation(FB2File fb2File, EPubFile epubFile)
        {
            if (fb2File.TitleInfo.Annotation != null)
            {
                epubFile.Title.Description = fb2File.TitleInfo.Annotation.ToString();
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
