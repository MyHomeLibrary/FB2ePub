using System;
using EPubLibrary;
using Fb2ePubConverter;
using FB2Library;
using FB2Library.HeaderItems;
using XHTMLClassLibrary.BaseElements;
using EPubLibrary.AppleEPubV2Extensions;
using EPubLibrary.Content.Guide;
using EPubLibrary.XHTML_Items;
using FB2EPubConverter.ElementConvertersV2;
using XHTMLClassLibrary.BaseElements.BlockElements;


namespace FB2EPubConverter
{
    internal class Fb2EPubConverterEngineV2 : Fb2EPubConverterEngineBase
    {
        private long _maxSize = 245 * 1024;

        /// <summary>
        /// Get/Set max document size in bytes
        /// </summary>
        public long MaxSize
        {
            get { return _maxSize; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("value");
                }
                _maxSize = value;
            }
        }

        protected override void ConvertContent(FB2File fb2File, EPubFile epubFile)
        {
            ReferencesManager = new HRefManagerV2();
            SetAdobeOptions(epubFile);
            PassHeaderDataFromFb2ToEpub(epubFile, fb2File);
            ConvertAnnotation(fb2File.TitleInfo,epubFile);
            PassCoverImageFromFB2(fb2File.TitleInfo.Cover, epubFile);
            SetupCSS(epubFile);
            SetupFonts(epubFile);
            SetupAppleSettings(epubFile);
            PassTextFromFb2ToEpub(epubFile, fb2File);
            PassFb2InfoToEpub(epubFile, fb2File);
            UpdateInternalLinks(epubFile, fb2File);
            PassImagesDataFromFb2ToEpub(epubFile, fb2File);
        }

        private void SetAdobeOptions(EPubFile epubFile)
        {
            epubFile.AdobeTemplatePath = string.IsNullOrEmpty(Settings.V2Settings.AdobeTemplatePath) ? @".\Template\template.xpgt" : Settings.V2Settings.AdobeTemplatePath;
            epubFile.UseAdobeTemplate = Settings.V2Settings.EnableAdobeTemplate;
        }

        private void SetupAppleSettings(EPubFile epubFile)
        {
            if (epubFile == null)
            {
                throw new ArgumentNullException("epubFile");
            }
            // setup epub2 options
            epubFile.AppleOptions.Reset();
            foreach (var platform in Settings.V2Settings.AppleConverterEPubSettings.V2Settings.Platforms)
            {
                var targetPlatform = new AppleTargetPlatform();
                switch (platform.Name)
                {
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleTargetPlatform.All:
                        targetPlatform.Type = PlatformType.All;
                        break;
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleTargetPlatform.iPad:
                        targetPlatform.Type = PlatformType.iPad;
                        break;
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleTargetPlatform.iPhone:
                        targetPlatform.Type = PlatformType.iPhone;
                        break;
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleTargetPlatform.NotSet: // we not going to add if type not set
                        Logger.Log.Error("SetupAppleSettings() - passed apple platform of type NotSet");
                        continue;
                }
                targetPlatform.FixedLayout = platform.FixedLayout;
                targetPlatform.OpenToSpread = platform.OpenToSpread;
                targetPlatform.CustomFontsAllowed = platform.UseCustomFonts;
                switch (platform.OrientationLock)
                {
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleOrientationLock.None:
                        targetPlatform.OrientationLockType = OrientationLock.Off;
                        break;
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleOrientationLock.LandscapeOnly:
                        targetPlatform.OrientationLockType = OrientationLock.LandscapeOnly;
                        break;
                    case Fb2epubSettings.AppleSettings.ePub_v2.AppleOrientationLock.PortraitOnly:
                        targetPlatform.OrientationLockType = OrientationLock.PortraitOnly;
                        break;
                }
                epubFile.AppleOptions.AddPlatform(targetPlatform);
            }

        }

        private void UpdateInternalLinks(EPubFile epubFile, FB2File fb2File)
        {
            ReferencesManager.RemoveInvalidAnchors();
            ReferencesManager.RemoveInvalidImages(fb2File.Images);
            ReferencesManager.RemapAnchors(epubFile);
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
            BookDocument infoDocument = epubFile.AddDocument("FB2 Info");
            var converterSettings = new ConverterOptionsV2
            {
                CapitalDrop = false,
                Images = Images,
                MaxSize = MaxSize,
                ReferencesManager = (HRefManagerV2)ReferencesManager,
            };
            var infoConverter = new Fb2EpubInfoConverterV2();
            infoDocument.Content = infoConverter.Convert(fb2File, converterSettings);
            infoDocument.FileName = "fb2info.xhtml";
            infoDocument.DocumentType = GuideTypeEnum.Notes;
            infoDocument.Type = SectionTypeEnum.Text;
            infoDocument.NotPartOfNavigation = true;
        }




        private void PassTextFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            var converter = new Fb2EPubTextConverterV2(Settings.CommonSettings, Images, (HRefManagerV2)ReferencesManager, _maxSize);
            converter.Convert(epubFile,fb2File);
        }


        private void PassImagesDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            Images.ConvertFb2ToEpubImages(fb2File.Images, epubFile.Images);
        }

        private void PassHeaderDataFromFb2ToEpub(EPubFile epubFile, FB2File fb2File)
        {
            Logger.Log.Debug("Passing header data from FB2 to EPUB");

            if (fb2File.MainBody == null)
            {
                throw new NullReferenceException("MainBody section of the file passed is null");
            }

            var headerDataConverter = new HeaderDataConverterV2(Settings.CommonSettings,Settings.V2Settings);
            headerDataConverter.Convert(epubFile,fb2File);
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




        private void ConvertAnnotation(ItemTitleInfo titleInfo, EPubFile epubFile)
        {
            if (titleInfo.Annotation != null)
            {
                epubFile.Title.Description = titleInfo.Annotation.ToString();
                epubFile.AnnotationPage = new AnnotationPageFile(HTMLElementType.XHTML11);
                var converterSettings = new ConverterOptionsV2
                {
                    CapitalDrop = Settings.CommonSettings.CapitalDrop,
                    Images = Images,
                    MaxSize = MaxSize,
                    ReferencesManager = (HRefManagerV2)ReferencesManager,
                };
                var annotationConverter = new AnnotationConverterV2();
                epubFile.AnnotationPage.BookAnnotation = (Div)annotationConverter.Convert(titleInfo.Annotation,
                   new AnnotationConverterParams { Settings = converterSettings, Level = 1 });
            }
        }


        protected override EPubFile CreateEpub()
        {
            return new EPubFile { FlatStructure = Settings.CommonSettings.Flat, EmbedStyles = Settings.CommonSettings.EmbedStyles };
        }


    }
}
