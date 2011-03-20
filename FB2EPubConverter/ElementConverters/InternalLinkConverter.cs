using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary.ReferenceUtils;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class InternalLinkConverter : BaseElementConverter
    {

        /// <summary>
        /// Convert FB2 internal link 
        /// </summary>
        /// <param name="internalLinkItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public List<IXHTMLItem> Convert(InternalLinkItem internalLinkItem)
        {
            if (internalLinkItem == null)
            {
                throw new ArgumentNullException("internalLinkItem");
            }
            List<IXHTMLItem> list = new List<IXHTMLItem>();
            if (!string.IsNullOrEmpty(internalLinkItem.HRef))
            {
                Anchor anchor = new Anchor();
                bool internalLink = false;
                if (!ReferencesUtils.IsExternalLink(internalLinkItem.HRef))
                {
                    if (internalLinkItem.HRef.StartsWith("#"))
                    {
                        internalLinkItem.HRef = internalLinkItem.HRef.Substring(1);
                    }
                    internalLinkItem.HRef = Settings.ReferencesManager.EnsureGoodId(internalLinkItem.HRef);
                    internalLink = true;
                }
                anchor.HRef.Value = internalLinkItem.HRef;
                if (internalLink)
                {
                    Settings.ReferencesManager.AddReference(internalLinkItem.HRef, anchor);
                }
                if (internalLinkItem.LinkText != null)
                {
                    SimpleTextElementConverter tempConverter = new SimpleTextElementConverter {Settings = Settings};
                    foreach (var s in tempConverter.Convert(internalLinkItem.LinkText))
                    {
                        s.Parent = anchor;
                        anchor.Content.Add(s);
                    }
                }
                list.Add(anchor);
                return list;
            }
            SimpleTextElementConverter converter = new SimpleTextElementConverter {Settings = Settings};
            return converter.Convert(internalLinkItem.LinkText);
        }


        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
