﻿using EPubLibrary.PathUtils;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.PrepearedHTMLFiles
{
    internal sealed class LicenseFileV3 : BaseXHTMLFileV3
    {
        internal LicenseFileV3()
        {
            FileEPubInternalPath = EPubInternalPath.GetDefaultLocation(DefaultLocations.DefaultLicenseFolder);
            Id = "license";
            FileName = "license.xhtml";
            GuideRole = GuideTypeEnum.Ignore;
            InternalPageTitle = "License";
            NotPartOfNavigation = true;
            SetDocumentEpubType(EpubV3Vocabulary.Imprint);
            SetDocumentEpubType(EpubV3Vocabulary.CopyRightPage);
            AddContent();
        }

        private void AddContent()
        {
            var pageData = new Div(Compatibility);
            var heading = new H1(Compatibility);
            heading.Add(new SimpleHTML5Text(Compatibility) { Text = "Converter use license" });
            pageData.Add(heading);

            var p1 = new Paragraph(Compatibility);

            p1.Add(new SimpleHTML5Text(Compatibility) { Text = "This file was generated by Lord KiRon's FB2EPUB converter." });
            p1.Add(new SimpleHTML5Text(Compatibility) { Text = "(This book might contain copyrighted material, author of the converter bears no responsibility for it's usage)" });
            pageData.Add(p1);

            var anch = new Anchor(Compatibility);
            p1 = new Paragraph(Compatibility);
            anch.Add(new SimpleHTML5Text(Compatibility) { Text = @"http://www.fb2epub.net" });
            anch.HRef.Value = @"http://www.fb2epub.net";
            p1.Add(anch);
            pageData.Add(p1);


            anch = new Anchor(Compatibility);
            p1 = new Paragraph(Compatibility);
            anch.Add(new SimpleHTML5Text(Compatibility) { Text = @"https://code.google.com/p/fb2epub/" });
            anch.HRef.Value = @"https://code.google.com/p/fb2epub/";
            p1.Add(anch);
            pageData.Add(p1);
            Content = pageData;
        }
    }
}
