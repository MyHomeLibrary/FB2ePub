using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    internal class CoverPageFileV3 : BaseXHTMLFile
    {
        private const string CoverTypeAttributeValue = "cover";

        public ImageOnStorage CoverFileName { get; set; }

        public CoverPageFileV3()
            : base(HTMLElementType.HTML5)
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
            BodyElement.CustomAttributes.Add(new CustomAttribute(EPubNamespaces.OpsNamespace+ "type", CoverTypeAttributeValue));

            var coverPage = new Div(Compatibility);
            coverPage.GlobalAttributes.Class.Value = "coverpage";
            //            coverPage.Style.Value = "text-align: center; page-break-after: always;";

            var coverImage = new Image(Compatibility);
            coverImage.GlobalAttributes.Class.Value = "coverimage";
            coverImage.Source.Value = CoverFileName.PathInEPUB.GetRelativePath(FileEPubInternalPath, FlatStructure);
            coverImage.Alt.Value = "Cover";
            coverPage.Add(coverImage);

            BodyElement.Add(coverPage);

        }

    }
}
