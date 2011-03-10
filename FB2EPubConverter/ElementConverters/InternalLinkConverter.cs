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
        public InternalLinkItem Item { get; set; }

        /// <summary>
        /// Convert FB2 internal link 
        /// </summary>
        /// <returns></returns>
        public List<IXHTMLItem> Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            List<IXHTMLItem> list = new List<IXHTMLItem>();
            if (!string.IsNullOrEmpty(Item.HRef))
            {
                Anchor anchor = new Anchor();
                bool internalLink = false;
                if (!ReferencesUtils.IsExternalLink(Item.HRef))
                {
                    if (Item.HRef.StartsWith("#"))
                    {
                        Item.HRef = Item.HRef.Substring(1);
                    }
                    Item.HRef = Settings.ReferencesManager.EnsureGoodID(Item.HRef);
                    internalLink = true;
                }
                anchor.HRef.Value = Item.HRef;
                if (internalLink)
                {
                    Settings.ReferencesManager.AddReference(Item.HRef, anchor);
                }
                if (Item.LinkText != null)
                {
                    SimpleTextElementConverter tempConverter = new SimpleTextElementConverter
                                                                   {
                                                                       Item = Item.LinkText,
                                                                       Settings = Settings
                                                                   };
                    foreach (var s in tempConverter.Convert())
                    {
                        s.Parent = anchor;
                        anchor.Content.Add(s);
                    }
                }
                list.Add(anchor);
                return list;
            }
            SimpleTextElementConverter converter = new SimpleTextElementConverter
                                                       {Item = Item.LinkText, Settings = Settings};
            return converter.Convert();
        }


    }
}
