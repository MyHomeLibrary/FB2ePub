using System.Collections.Generic;
using EPubLibrary.PathUtils;
using EPubLibraryContracts;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace EPubLibrary.XHTML_Items
{
    internal class AboutPageFileV2 : BaseXHTMLFileV2
    {
        public AboutPageFileV2()
        {
            GuideRole = GuideTypeEnum.CopyrightPage;
            InternalPageTitle = "About";
            Id = "about";
            FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultTextFolder);
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
            Div page = new Div(Compatibility);
            page.GlobalAttributes.Class.Value = "about";
            H1 heading = new H1(Compatibility);
            heading.Add(new SimpleHTML5Text(Compatibility) { Text = "About" });
            page.Add(heading);

            foreach (var text in AboutTexts)
            {
                var p1 = new Paragraph(Compatibility);
                var text1 = new SimpleHTML5Text(Compatibility) {Text = text};
                p1.Add(text1);
                page.Add(p1);
            }

            foreach (var text in AboutLinks)
            {
                var p1 = new Paragraph(Compatibility);
                var anch = new Anchor(Compatibility);
                anch.HRef.Value = text;
                anch.GlobalAttributes.Title.Value = text;
                var text3 = new SimpleHTML5Text(Compatibility) {Text = text};
                anch.Add(text3);
                p1.Add(anch);
                page.Add(p1);
            }

            BodyElement.Add(page);
        }

    }
}
