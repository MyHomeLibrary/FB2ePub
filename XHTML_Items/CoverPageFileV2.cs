using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    internal class CoverPageFileV2 : BaseXHTMLFile
    {
        public ImageOnStorage CoverFileName { get; set; }

        public CoverPageFileV2()
            : base(HTMLElementType.XHTML11)
        {
            InternalPageTitle = "Cover";
            GuideRole = GuideTypeEnum.Cover;
            Id = "cover";
            FileEPubInternalPath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
            FileName = "cover.xhtml";
        }

        public override void GenerateBody()
        {
            base.GenerateBody();
            var coverPage = new Div(Compatibility);
            coverPage.GlobalAttributes.Class.Value = "coverpage";
            //            coverPage.Style.Value = "text-align: center; page-break-after: always;";

            var coverImage = new Image(Compatibility);
            coverImage.GlobalAttributes.Class.Value = "coverimage";
            coverImage.Source.Value = CoverFileName.PathInEPUB.GetRelativePath(FileEPubInternalPath, FlatStructure);
            coverImage.Alt.Value = "Cover";
            //coverImage.Style.Value = "max-width: 100%;";
            coverPage.Add(coverImage);

            BodyElement.Add(coverPage);

        }

    }
}
