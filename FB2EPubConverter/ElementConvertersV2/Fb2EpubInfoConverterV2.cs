using System;
using System.Linq;
using EPubLibrary.ReferenceUtils;
using FB2Library;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;
using XHTMLClassLibrary.BaseElements.ListElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    /// <summary>
    /// Used to convert FB2 information section into EPUB document content to generate FB2Info page(s)
    /// </summary>
    internal class Fb2EpubInfoConverterV2 : BaseElementConverterV2
    {
        public HTMLItem Convert(FB2File fb2File, ConverterOptionsV2 settings)
        {
            if (fb2File == null)
            {
                throw new ArgumentNullException("fb2File");
            }
            var info = new Div(HTMLElementType.XHTML11);
            var header = new H3(HTMLElementType.XHTML11);
            header.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = "FB2 document info" });
            info.Add(header);
            if (fb2File.DocumentInfo != null)
            {
                if ( !string.IsNullOrEmpty(fb2File.DocumentInfo.ID) )
                {
                    var p = new Paragraph(HTMLElementType.XHTML11);
                    p.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = string.Format("Document ID:  {0}", fb2File.DocumentInfo.ID) });
                    info.Add(p);
                }
                if (fb2File.DocumentInfo.DocumentVersion.HasValue)
                {
                    var p = new Paragraph(HTMLElementType.XHTML11);
                    p.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = string.Format("Document version:  {0}", fb2File.DocumentInfo.DocumentVersion) });
                    info.Add(p);                    
                }
                if ((fb2File.DocumentInfo.DocumentDate != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.DocumentDate.Text))
                {
                    var p = new Paragraph(HTMLElementType.XHTML11);
                    p.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = string.Format("Document creation date:  {0}", fb2File.DocumentInfo.DocumentDate.Text) });
                    info.Add(p);
                }
                if ( (fb2File.DocumentInfo.ProgramUsed2Create != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.ProgramUsed2Create.Text) )
                {
                    var p = new Paragraph(HTMLElementType.XHTML11);
                    p.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = string.Format("Created using:  {0} software", fb2File.DocumentInfo.ProgramUsed2Create.Text) });
                    info.Add(p);                    
                }
                if ((fb2File.DocumentInfo.SourceOCR != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.SourceOCR.Text))
                {
                    var p = new Paragraph(HTMLElementType.XHTML11);
                    p.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = string.Format("OCR Source:  {0}", fb2File.DocumentInfo.SourceOCR.Text) });
                    info.Add(p);                                        
                }
                if ((fb2File.DocumentInfo.DocumentAuthors != null) && (fb2File.DocumentInfo.DocumentAuthors.Count > 0))
                {
                    var heading = new H4(HTMLElementType.XHTML11);
                    heading.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = "Document authors :" });
                    info.Add(heading);
                    var authors = new UnorderedList(HTMLElementType.XHTML11) ;
                    foreach (var author in fb2File.DocumentInfo.DocumentAuthors)
                    {
                        var li = new ListItem(HTMLElementType.XHTML11);
                        li.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = DescriptionConverters.GetAuthorAsSting(author) });
                        authors.Add(li);
                    }
                    info.Add(authors);
                }
                if ((fb2File.DocumentInfo.DocumentPublishers != null) && (fb2File.DocumentInfo.DocumentPublishers.Count > 0))
                {
                    var heading = new H4(HTMLElementType.XHTML11);
                    heading.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = "Document publishers :" });
                    info.Add(heading);

                    var publishers = new UnorderedList(HTMLElementType.XHTML11);
                    foreach (var publisher in fb2File.DocumentInfo.DocumentPublishers)
                    {
                        var li = new ListItem(HTMLElementType.XHTML11);
                        li.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = DescriptionConverters.GetAuthorAsSting(publisher) });
                        publishers.Add(li);
                    }
                    info.Add(publishers);
                }

                if ((fb2File.DocumentInfo.SourceURLs != null) && (fb2File.DocumentInfo.SourceURLs.Any()))
                {
                    var heading = new H4(HTMLElementType.XHTML11);
                    heading.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = "Source URLs :" });
                    info.Add(heading);

                    var urls = new UnorderedList(HTMLElementType.XHTML11);
                    foreach (var url in fb2File.DocumentInfo.SourceURLs)
                    {
                        var li = new ListItem(HTMLElementType.XHTML11);
                        if (ReferencesUtils.IsExternalLink(url))
                        {
                            var link = new Anchor(HTMLElementType.XHTML11);
                            link.HRef.Value = url;
                            link.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) {Text = url});
                            li.Add(link);
                        }
                        else
                        {
                            li.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = url });
                        }
                        urls.Add(li);
                    }
                    info.Add(urls);
                }

                if (fb2File.DocumentInfo.History != null)
                {
                    var heading = new H4(HTMLElementType.XHTML11);
                    heading.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = "Document history:" });
                    info.Add(heading);
                    var annotationConverter = new AnnotationConverterV2();
                    info.Add(annotationConverter.Convert(fb2File.DocumentInfo.History,new AnnotationConverterParamsV2 { Level = 1, Settings = settings }));
                    //Paragraph p = new Paragraph();
                    //p.Add(new SimpleHTML5Text() { Text = fb2File.DocumentInfo.History.ToString() });
                    //info.Add(p);                                                            
                }
            }

            // in case there is no elements - no need for a header
            if (info.SubElements().Count <= 1)
            {
                info.Remove(header);
            }

            info.GlobalAttributes.Class.Value = "fb2_info";
            return info;
            
        }
    }
}
