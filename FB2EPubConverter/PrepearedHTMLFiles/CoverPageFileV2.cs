using ConverterContracts.ConversionElementsStyles;
using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.PrepearedHTMLFiles
{
    internal class CoverPageFileV2 : BaseXHTMLFileV2
    {
        private readonly Image _coverImage;

        public CoverPageFileV2(InlineImageItem item, HRefManagerV2 refManagerV2)
        {
            _coverImage = CreateCoverImage(item, refManagerV2);
            InternalPageTitle = "Cover";
            GuideRole = GuideTypeEnum.Cover;
            Id = "cover";
            FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder);
            FileName = "cover.xhtml";
        }

        private Image CreateCoverImage(InlineImageItem item, HRefManagerV2 refManagerV2)
        {
            var coverImage = new Image(Compatibility);
            coverImage.GlobalAttributes.Class.Value = ElementStylesV2.CoverImage;
            coverImage.Source.Value = refManagerV2.AddImageRefferenced(item, coverImage);
            coverImage.Alt.Value = "Cover";
            return coverImage;

        }

        public override void GenerateBody()
        {
            base.GenerateBody();

            var coverPage = new Div(Compatibility);
            coverPage.GlobalAttributes.Class.Value = ElementStylesV2.CoverPage;
            coverPage.Add(_coverImage);
            BodyElement.Add(coverPage);

        }

    }
}
