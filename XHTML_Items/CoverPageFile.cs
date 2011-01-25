using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EPubLibrary.Content.Guide;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace EPubLibrary.XHTML_Items
{
    internal class CoverPageFile : BaseXHTMLFile
    {
       public string CoverFileName { get; set; }
        
        public CoverPageFile()
        {
            pageTitle = "Cover";
            DocumentType = GuideTypeEnum.Cover;
        }

        override public XDocument Generate()
        {
            Div coverPage = new Div();
            coverPage.Class.Value = "coverpage";
//            coverPage.Style.Value = "text-align: center; page-break-after: always;";

            Image coverImage = new Image();
            coverImage.Class.Value = "coverimage";
            coverImage.Source.Value = CoverFileName;
            coverImage.Alt.Value = "Cover";
            //coverImage.Style.Value = "max-width: 100%;";
            coverPage.Add(coverImage);

            bodyElement.Add(coverPage);


            //return document;
            return base.Generate();
        }
    }
}
