using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using EPubLibrary.Content.Guide;

namespace EPubLibrary.XHTML_Items
{
    public class TitlePageFile : BaseXHTMLFile
    {
        private readonly List<string> _authors = new List<string>();
        private readonly List<string> _series = new List<string>();

        public TitlePageFile(XHTMRulesEnum compatibility)
            : base(compatibility)
        {
            pageTitle = "Title";
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
            Div titlePage = new Div();
            titlePage.Class.Value = "titlepage";
            if (!string.IsNullOrEmpty(BookTitle))
            {
                // try to use FB2 book's title
                IBlockElement p = new H2();
                p.Add(new SimpleEPubText { Text = BookTitle });
                string itemClass = string.Format("title{0}", 1);
                p.Class.Value = itemClass;
                titlePage.Add(p);
            }
            else
            {
                titlePage.Add(new SimpleEPubText { Text = "Unnamed" });
            }

            titlePage.Add(new EmptyLine());

            StringBuilder sbSeries = new StringBuilder();
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
                SimpleEPubText seriesItem = new SimpleEPubText { Text = string.Format("( {0} )", sbSeries) };
                EmphasisedText containingText = new EmphasisedText();
                containingText.Add(seriesItem);
                H3 seriesHeading = new H3();
                seriesHeading.Class.Value = "title_series";
                seriesHeading.Add(containingText);
                titlePage.Add(seriesHeading);
            }

            foreach (var author in _authors)
            {
                H3 authorsHeading = new H3();
                SimpleEPubText authorLine = new SimpleEPubText { Text = author };
                authorsHeading.Add(authorLine);
                authorsHeading.Class.Value = "title_authors";
                titlePage.Add(authorsHeading);
            }


            BodyElement.Add(titlePage);
        }

    }
}
