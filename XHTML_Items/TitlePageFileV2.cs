using System.Collections.Generic;
using System.Text;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using EPubLibrary.Content.Guide;
using EPubLibraryContracts;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    internal class TitlePageFileV2 : BaseXHTMLFile
    {
        private readonly List<string> _authors = new List<string>();
        private readonly List<string> _series = new List<string>();

        public TitlePageFileV2(IBookTitleInformation titleInformation)
            : base(HTMLElementType.XHTML11)
        {
            Authors.AddRange(titleInformation.Authors);
            Series.AddRange(titleInformation.Series);
            BookTitle = titleInformation.BookMainTitle;

            InternalPageTitle = "Title";
            DocumentType = GuideTypeEnum.TitlePage;
            FileName = "title.xhtml";
            FileEPubInternalPath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
            Id = "title";
        }

        public string BookTitle { get; set; }

        public List<string> Authors { get { return _authors; } }

        public List<string> Series { get { return _series; } }

        public override void GenerateBody()
        {
            base.GenerateBody();
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
