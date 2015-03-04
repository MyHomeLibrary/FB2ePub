using System;
using System.Collections.Generic;
using EPubLibrary.ReferenceUtils;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class InternalLinkConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }
        public bool NeedToInsertDrop { get; set; }
    }

    internal class InternalLinkConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Convert FB2 internal link 
        /// </summary>
        /// <param name="internalLinkItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="internalLinkConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public List<IHTMLItem> Convert(InternalLinkItem internalLinkItem, 
            InternalLinkConverterParamsV2 internalLinkConverterParams)
        {
            if (internalLinkItem == null)
            {
                throw new ArgumentNullException("internalLinkItem");
            }
            var list = new List<IHTMLItem>();
            if (!string.IsNullOrEmpty(internalLinkItem.HRef) && internalLinkItem.HRef != "#")
            {
                var anchor = new Anchor(HTMLElementType.XHTML11);
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
                    var tempConverter = new SimpleTextElementConverterV2();
                    foreach (var s in tempConverter.Convert(internalLinkItem.LinkText,
                        new SimpleTextElementConverterParamsV2
                        {
                            Settings = internalLinkConverterParams.Settings,
                            NeedToInsertDrop = internalLinkConverterParams.NeedToInsertDrop
                        }
                        ))
                    {
                        s.Parent = anchor;
                        anchor.Add(s);
                    }
                }
                list.Add(anchor);
                return list;
            }
            var converter = new SimpleTextElementConverterV2();
            return converter.Convert(internalLinkItem.LinkText, 
                new SimpleTextElementConverterParamsV2
                {
                    Settings = internalLinkConverterParams.Settings,
                    NeedToInsertDrop = internalLinkConverterParams.NeedToInsertDrop
                });
        }
    }
}
