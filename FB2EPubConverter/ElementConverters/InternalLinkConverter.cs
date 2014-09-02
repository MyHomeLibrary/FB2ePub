using System;
using System.Collections.Generic;
using EPubLibrary.ReferenceUtils;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class InternalLinkConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 internal link 
        /// </summary>
        /// <param name="internalLinkItem">item to convert</param>
        /// <param name="needToInsertDrop"></param>
        /// <returns>XHTML representation</returns>
        public List<IHTMLItem> Convert(InternalLinkItem internalLinkItem, bool needToInsertDrop)
        {
            if (internalLinkItem == null)
            {
                throw new ArgumentNullException("internalLinkItem");
            }
            var list = new List<IHTMLItem>();
            if (!string.IsNullOrEmpty(internalLinkItem.HRef) && internalLinkItem.HRef != "#")
            {
                var anchor = new Anchor();
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
                    var tempConverter = new SimpleTextElementConverter {Settings = Settings};
                    foreach (var s in tempConverter.Convert(internalLinkItem.LinkText,needToInsertDrop))
                    {
                        s.Parent = anchor;
                        anchor.InternalTextItem.Add(s);
                    }
                }
                list.Add(anchor);
                return list;
            }
            var converter = new SimpleTextElementConverter {Settings = Settings};
            return converter.Convert(internalLinkItem.LinkText,needToInsertDrop);
        }


        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
