using System.Linq;
using System.Text;
using EPubLibrary.ReferenceUtils;
using FB2Library;
using FB2Library.HeaderItems;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.ListElements;

namespace FB2EPubConverter.ElementConverters
{
    /// <summary>
    /// Used to convert FB2 information section into EPUB document content to generate FB2Info page(s)
    /// </summary>
    internal class Fb2EpubInfoConverter : BaseElementConverter
    {
        public IXHTMLItem ConvertInfo(FB2File fb2File)
        {
            Div info = new Div();
            H3 header = new H3();
            header.Add(new SimpleEPubText() { Text = "FB2 document info"});
            info.Add(header);
            if (fb2File.DocumentInfo != null)
            {
                if ( !string.IsNullOrEmpty(fb2File.DocumentInfo.ID) )
                {
                    Paragraph p = new Paragraph();
                    p.Add(new SimpleEPubText() { Text = string.Format("Document ID:  {0}", fb2File.DocumentInfo.ID) });
                    info.Add(p);
                }
                if (fb2File.DocumentInfo.DocumentVersion.HasValue)
                {
                    Paragraph p = new Paragraph();
                    p.Add(new SimpleEPubText() { Text = string.Format("Document version:  {0}", fb2File.DocumentInfo.DocumentVersion) });
                    info.Add(p);                    
                }
                if ((fb2File.DocumentInfo.DocumentDate != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.DocumentDate.Text))
                {
                    Paragraph p = new Paragraph();
                    p.Add(new SimpleEPubText() { Text = string.Format("Document creation date:  {0}", fb2File.DocumentInfo.DocumentDate.Text) });
                    info.Add(p);
                }
                if ( (fb2File.DocumentInfo.ProgramUsed2Create != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.ProgramUsed2Create.Text) )
                {
                    Paragraph p = new Paragraph();
                    p.Add(new SimpleEPubText() { Text = string.Format("Created using:  {0} software", fb2File.DocumentInfo.ProgramUsed2Create.Text) });
                    info.Add(p);                    
                }
                if ((fb2File.DocumentInfo.SourceOCR != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.SourceOCR.Text))
                {
                    Paragraph p = new Paragraph();
                    p.Add(new SimpleEPubText() { Text = string.Format("OCR Source:  {0}", fb2File.DocumentInfo.SourceOCR.Text) });
                    info.Add(p);                                        
                }
                if ((fb2File.DocumentInfo.DocumentAuthors != null) && (fb2File.DocumentInfo.DocumentAuthors.Count > 0))
                {
                    H4 heading = new H4();
                    heading.Add(new SimpleEPubText() { Text = "Document authors :" });
                    info.Add(heading);
                    UnorderedList authors = new UnorderedList() ;
                    foreach (var author in fb2File.DocumentInfo.DocumentAuthors)
                    {
                        ListItem li = new ListItem();
                        li.Add(new SimpleEPubText() { Text = GetAuthorAsSting(author)});
                        authors.Add(li);
                    }
                    info.Add(authors);
                }
                if ((fb2File.DocumentInfo.DocumentPublishers != null) && (fb2File.DocumentInfo.DocumentPublishers.Count > 0))
                {
                    H4 heading = new H4();
                    heading.Add(new SimpleEPubText() { Text = "Document publishers :" });
                    info.Add(heading);

                    UnorderedList publishers = new UnorderedList();
                    foreach (var publisher in fb2File.DocumentInfo.DocumentPublishers)
                    {
                        ListItem li = new ListItem();
                        li.Add(new SimpleEPubText() { Text = GetAuthorAsSting(publisher) });
                        publishers.Add(li);
                    }
                    info.Add(publishers);
                }

                if ((fb2File.DocumentInfo.SourceURLs != null) && (fb2File.DocumentInfo.SourceURLs.Count() > 0))
                {
                    H4 heading = new H4();
                    heading.Add(new SimpleEPubText() { Text = "Source URLs :" });
                    info.Add(heading);

                    UnorderedList urls = new UnorderedList();
                    foreach (var url in fb2File.DocumentInfo.SourceURLs)
                    {
                        ListItem li = new ListItem();
                        if (ReferencesUtils.IsExternalLink(url))
                        {
                            Anchor link = new Anchor();
                            link.HRef.Value = url;
                            link.Add(new SimpleEPubText() {Text = url});
                            li.Add(link);
                        }
                        else
                        {
                            li.Add(new SimpleEPubText() { Text = url });
                        }
                        urls.Add(li);
                    }
                    info.Add(urls);
                }

                if (fb2File.DocumentInfo.History != null)
                {
                    H4 heading = new H4();
                    heading.Add(new SimpleEPubText() { Text = "Document history:" });
                    info.Add(heading);
                    AnnotationConverter annotationConverter = new AnnotationConverter {Settings = Settings};
                    info.Add(annotationConverter.Convert(fb2File.DocumentInfo.History,1));
                    //Paragraph p = new Paragraph();
                    //p.Add(new SimpleEPubText() { Text = fb2File.DocumentInfo.History.ToString() });
                    //info.Add(p);                                                            
                }
            }

            // in case there is no elements - no need for a header
            if (info.SubElements().Count <= 1)
            {
                info.Remove(header);
            }

            info.Class.Value = "fb2_info";
            return info;
        }

        private static string GetAuthorAsSting(AuthorType author)
        {
            StringBuilder sb = new StringBuilder();
            if ((author.FirstName != null) && !string.IsNullOrEmpty(author.FirstName.Text))
            {
                sb.AppendFormat("{0} ", author.FirstName.Text);
            }

            if ((author.MiddleName != null) && !string.IsNullOrEmpty(author.MiddleName.Text)) 
            {
                sb.AppendFormat("{0} ", author.MiddleName.Text);
            }

            if ((author.LastName != null) && !string.IsNullOrEmpty(author.LastName.Text))
            {
                sb.AppendFormat("{0} ", author.LastName.Text);
            }

            if ((author.NickName != null) && !string.IsNullOrEmpty(author.NickName.Text))
            {
                if (sb.Length == 0)
                {
                    sb.AppendFormat("{0} ", author.NickName.Text);
                }
                else
                {
                    sb.AppendFormat("({0}) ", author.NickName.Text);
                }
            }

            if ((author.UID != null) && !string.IsNullOrEmpty(author.UID.Text))
            {
                sb.AppendFormat(": {0}", author.UID.Text);
            }
            return sb.ToString();
        }

        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
