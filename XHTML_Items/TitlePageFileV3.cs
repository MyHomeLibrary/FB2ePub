using System.Collections.Generic;
using System.Text;
using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    internal class TitlePageFileV3 : BaseXHTMLFileV3
    {
        private const string TitleTypeAttributeValue = "titlepage";

        private readonly List<string> _authors = new List<string>();
        private readonly List<string> _series = new List<string>();

        public TitlePageFileV3(IBookInformationData titleInformation)
        {
            Authors.AddRange(titleInformation.Authors);
            Series.AddRange(titleInformation.Series);
            BookTitle = titleInformation.BookMainTitle;

            InternalPageTitle = "Title";
            GuideRole = GuideTypeEnum.TitlePage;
            FileName = "title.xhtml";
            FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder);
            Id = "title";
        }

        public string BookTitle { get; set; }

        public List<string> Authors { get { return _authors; } }

        public List<string> Series { get { return _series; } }

        public override void GenerateBody()
        {
            base.GenerateBody();
            BodyElement.CustomAttributes.Add(new CustomAttribute(EPubNamespaces.OpsNamespace + "type", TitleTypeAttributeValue));

            var titlePage = new Div(Compatibility);
            titlePage.GlobalAttributes.Class.Value = "titlepage";
            if (!string.IsNullOrEmpty(BookTitle))
            {
                // try to use FB2 book's title
                var p = new H2(Compatibility);
                p.Add(new SimpleHTML5Text(Compatibility) { Text = BookTitle });
                string itemClass = string.Format("title{0}", 1);
                p.GlobalAttributes.Class.Value = itemClass;
                titlePage.Add(p);
            }
            else
            {
                titlePage.Add(new SimpleHTML5Text(Compatibility) { Text = "Unnamed" });
            }

            titlePage.Add(new EmptyLine(Compatibility));

            var sbSeries = new StringBuilder();
            foreach (var serie in _series)
            {
                if (!string.IsNullOrEmpty(sbSeries.ToString()))
                {
                    sbSeries.Append(" , ");
                }
                sbSeries.Append(serie);
            }
            if (sbSeries.ToString() != string.Empty)
            {
                var seriesItem = new SimpleHTML5Text(Compatibility) { Text = string.Format("( {0} )", sbSeries) };
                var containingText = new EmphasisedText(Compatibility);
                containingText.Add(seriesItem);
                var seriesHeading = new H3(Compatibility);
                seriesHeading.GlobalAttributes.Class.Value = "title_series";
                seriesHeading.Add(containingText);
                titlePage.Add(seriesHeading);
            }

            foreach (var author in _authors)
            {
                var authorsHeading = new H3(Compatibility);
                var authorLine = new SimpleHTML5Text(Compatibility) { Text = author };
                authorsHeading.Add(authorLine);
                authorsHeading.GlobalAttributes.Class.Value = "title_authors";
                titlePage.Add(authorsHeading);
            }


            BodyElement.Add(titlePage);
        }

    }
}
