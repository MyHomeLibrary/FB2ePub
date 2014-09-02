using System;
using System.Collections.Generic;
using EPubLibrary.ReferenceUtils;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class InternalLinkConverterParams
    {
        public ConverterOptions Settings { get; set; }
        public bool NeedToInsertDrop { get; set; }
    }

    internal class InternalLinkConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 internal link 
        /// </summary>
        /// <param name="internalLinkItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="internalLinkConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public List<IHTMLItem> Convert(InternalLinkItem internalLinkItem, HTMLElementType compatibility,
            InternalLinkConverterParams internalLinkConverterParams)
        {
            if (internalLinkItem == null)
            {
                throw new ArgumentNullException("internalLinkItem");
            }
            var list = new List<IHTMLItem>();
            if (!string.IsNullOrEmpty(internalLinkItem.HRef) && internalLinkItem.HRef != "#")
            {
                var anchor = new Anchor(compatibility);
                bool internalLink = false;
                if (!ReferencesUtils.IsExternalLink(internalLinkItem.HRef))
                {
                    if (internalLinkItem.HRef.StartsWith("#"))
                    {
                        internalLinkItem.HRef = internalLinkItem.HRef.Substring(1);
                    }
                    internalLinkItem.HRef =
                        internalLinkConverterParams.Settings.ReferencesManager.EnsureGoodId(internalLinkItem.HRef);
                    internalLink = true;
                }
                anchor.HRef.Value = internalLinkItem.HRef;
                if (internalLink)
                {
                    internalLinkConverterParams.Settings.ReferencesManager.AddReference(internalLinkItem.HRef, anchor);
                }
                if (internalLinkItem.LinkText != null)
                {
                    var tempConverter = new SimpleTextElementConverter();
                    foreach (var s in tempConverter.Convert(internalLinkItem.LinkText, compatibility,
                        new SimpleTextElementConverterParams
                        {
                            Settings = internalLinkConverterParams.Settings,
                            NeedToInsertDrop = internalLinkConverterParams.NeedToInsertDrop
                        }
                        ))
                    {
                        s.Parent = anchor;
                        anchor.InternalTextItem.Add(s);
                    }
                }
                list.Add(anchor);
                return list;
            }
            var converter = new SimpleTextElementConverter();
            return converter.Convert(internalLinkItem.LinkText, compatibility,
                new SimpleTextElementConverterParams
                {
                    Settings = internalLinkConverterParams.Settings,
                    NeedToInsertDrop = internalLinkConverterParams.NeedToInsertDrop
                });
        }
    }
}
