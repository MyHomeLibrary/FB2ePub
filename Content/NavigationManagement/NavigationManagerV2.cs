using EPubLibrary.Content.Guide;
using EPubLibrary.XHTML_Items;

namespace EPubLibrary.Content.NavigationManagement
{
    internal class NavigationManagerV2
    {
        private readonly GuideSection _guide = new GuideSection();

        /// <summary>
        /// Add new document to be a part of navigation
        /// </summary>
        /// <param name="baseXhtmlFile"></param>
        public void AddDocumentToNavigation(BaseXHTMLFile baseXhtmlFile)
        {
            _guide.AddGuideItem(baseXhtmlFile.HRef, baseXhtmlFile.Id, baseXhtmlFile.DocumentType);
        }


        /// <summary>
        /// Add all the relevant navigation documents to content document
        /// </summary>
        /// <param name="document"></param>
        public void WriteNavigationItemsToContentDocumentElement(System.Xml.Linq.XElement document)
        {
            // if guide has data
            if (_guide.HasData())
            {
                // write guide to content
                document.Add(_guide.GenerateGuide());
            }
        }

    }
}
