using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using EPubLibrary.Content.Guide;
using EPubLibrary.PathUtils;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    internal class AboutPageFile : BaseXHTMLFile
    {
        public AboutPageFile()
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


        internal void Create()
        {
            Div page = new Div();
            page.Class.Value = "about";
            H1 heading = new H1();
            heading.Add(new SimpleEPubText { Text = "About" });
            page.Add(heading);

            foreach (var text in AboutTexts)
            {
                Paragraph p1 = new Paragraph();
                SimpleEPubText text1 = new SimpleEPubText();
                text1.Text = text;
                p1.Add(text1);
                page.Add(p1);
            }

            foreach (var text in AboutLinks)
            {
                Paragraph p1 = new Paragraph();
                Anchor anch = new Anchor();
                anch.HRef.Value = text;
                anch.Title.Value = text;
                SimpleEPubText text3 = new SimpleEPubText();
                text3.Text = text;
                anch.Add(text3);
                p1.Add(anch);
                page.Add(p1);
            }

            BodyElement.Add(page);
        }
    }
}
