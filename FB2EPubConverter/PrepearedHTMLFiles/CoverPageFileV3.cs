using ConverterContracts.ConversionElementsStyles;
using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.PrepearedHTMLFiles
{
    internal class CoverPageFileV3 : BaseXHTMLFileV3
    {
        private readonly Image _coverImage;

        public CoverPageFileV3(InlineImageItem item,HRefManagerV3 refManagerV3)
        {
            _coverImage = CreateCoverImage(item,refManagerV3);
            InternalPageTitle = "Cover";
            GuideRole = GuideTypeEnum.Cover;
            Id = "cover";
            FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder);
            FileName = "cover.xhtml";
            SetDocumentEpubType(EpubV3Vocabulary.Cover);
        }

        private Image CreateCoverImage(InlineImageItem item, HRefManagerV3 refManagerV3)
        {
            var coverImage = new Image(Compatibility);
            coverImage.GlobalAttributes.Class.Value = ElementStylesV3.CoverImage;
            coverImage.Source.Value = refManagerV3.AddImageRefferenced(item, coverImage);
            coverImage.Alt.Value = "Cover";
            return coverImage;
        }

        public override void GenerateBody()
        {

            base.GenerateBody();

            var coverPage = new Div(Compatibility);
            coverPage.GlobalAttributes.Class.Value = ElementStylesV3.CoverPage;
            coverPage.Add(_coverImage);
            BodyElement.Add(coverPage);

        }

    }
}
