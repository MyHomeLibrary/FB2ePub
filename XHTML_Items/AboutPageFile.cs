using System.Collections.Generic;
using EPubLibrary.Content.Guide;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    internal class AboutPageFile : BaseXHTMLFile
    {
        public AboutPageFile(HTMLElementType compatibility)
            : base(compatibility)
        {
            DocumentType = GuideTypeEnum.CopyrightPage;
            pageTitle = "About";
            Id = "about";
            FileEPubInternalPath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/text/");
            FileName = "about.xhtml";
        }

        /// <summary>
        /// Strings added to about page
        /// </summary>
        public List<string> AboutTexts { get; set; }

        /// <summary>
        /// Links added to about page
        /// </summary>
        public List<string> AboutLinks{get; set;}


        public override void GenerateBody()
        {
            base.GenerateBody();
            Div page = new Div();
            page.GlobalAttributes.Class.Value = "about";
            H1 heading = new H1();
            heading.Add(new SimpleHTML5Text { Text = "About" });
            page.Add(heading);

            foreach (var text in AboutTexts)
            {
                Paragraph p1 = new Paragraph();
                SimpleHTML5Text text1 = new SimpleHTML5Text();
                text1.Text = text;
                p1.Add(text1);
                page.Add(p1);
            }

            foreach (var text in AboutLinks)
            {
                var p1 = new Paragraph();
                var anch = new Anchor();
                anch.HRef.Value = text;
                anch.GlobalAttributes.Title.Value = text;
                var text3 = new SimpleHTML5Text();
                text3.Text = text;
                anch.Add(text3);
                p1.Add(anch);
                page.Add(p1);
            }

            BodyElement.Add(page);
        }

    }
}
