using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class SimpleTextElementConverter : BaseElementConverter
    {
        public StyleType Item { get; set; }

        /// <summary>
        /// Converts FB2 simple text 
        /// ( simple text is normal text or text with one of the "styles")
        /// </summary>
        /// <returns></returns>
        public List<IXHTMLItem> Convert()
        {
            return Convert(false);
        }

        /// <summary>
        /// Converts FB2 simple text 
        /// ( simple text is normal text or text with one of the "styles")
        /// </summary>
        /// <param name="needToInsert"></param>
        /// <returns></returns>
        public List<IXHTMLItem> Convert(bool needToInsert)
        {

            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }

            List<IXHTMLItem> list = new List<IXHTMLItem>();

            if (Item is SimpleText)
            {
                SimpleText text = Item as SimpleText;
                switch (text.Style)
                {
                    case FB2Library.Elements.TextStyles.Normal:
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                SimpleTextElementConverter converter = new SimpleTextElementConverter{ Item = child};
                                foreach (var item in converter.Convert())
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (needToInsert && text.Text.Length > 0)
                            {
                                var span1 = new Span();
                                span1.Class.Value = "drop";
                                int dropEnd = 0;
                                // "pad" the white spaces so drop starts from visible character
                                while (UnicodeHelpers.IsSpaceLike(text.Text[dropEnd]) && dropEnd < text.Text.Length)
                                {
                                    dropEnd++;
                                }
                                // calculate the initial drop part
                                string dropPart = text.Text.Substring(0, dropEnd + 1);
                                // non-drop part starts from the next character
                                int nondropPosition = dropEnd + 1;
                                // If first character is dash/hyphen like we need to add character to 
                                // capital drop so it looks better with next character
                                if (UnicodeHelpers.IsNeedToBeJoinInDrop(dropPart[dropEnd]))
                                {
                                    // we need to add to capital drop all spaces if any
                                    while (nondropPosition < text.Text.Length && UnicodeHelpers.IsSpaceLike(text.Text[nondropPosition]))
                                    {
                                        nondropPosition++;
                                    }
                                    // we need to advance to include one following nonspace character , unless
                                    // we already at last character of the text
                                    if (nondropPosition - dropEnd < text.Text.Length)
                                    {
                                        nondropPosition++;
                                    }
                                    // update drop part with the "string" we calculated
                                    dropPart += text.Text.Substring(dropEnd + 1, nondropPosition - dropEnd - 1);
                                }
                                span1.Add(new SimpleEPubText { Text = dropPart });
                                list.Add(span1);
                                string substring = text.Text.Substring(nondropPosition);
                                if (substring.Length > 0)
                                {
                                    list.Add(new SimpleEPubText { Text = substring });
                                }
                            }
                            else
                            {
                                list.Add(new SimpleEPubText { Text = text.Text });
                            }
                        }
                        break;
                    case FB2Library.Elements.TextStyles.Code:
                        CodeText code = new CodeText();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                SimpleTextElementConverter converter = new SimpleTextElementConverter { Item = child };
                                foreach (var item in converter.Convert())
                                {
                                    code.Add(item);
                                }

                            }
                        }
                        else
                        {
                            code.Add(new SimpleEPubText() { Text = text.Text });
                        }
                        list.Add(code);
                        break;
                    case FB2Library.Elements.TextStyles.Emphasis:
                        EmphasisedText emph = new EmphasisedText();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                SimpleTextElementConverter converter = new SimpleTextElementConverter { Item = child };
                                foreach (var item in converter.Convert())
                                {
                                    emph.Add(item);
                                }
                            }
                        }
                        else
                        {
                            emph.Add(new SimpleEPubText() { Text = text.Text });
                        }
                        list.Add(emph);
                        break;
                    case FB2Library.Elements.TextStyles.Strong:
                        Strong str = new Strong();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                SimpleTextElementConverter converter = new SimpleTextElementConverter { Item = child };
                                foreach (var item in converter.Convert())
                                {
                                    str.Add(item);
                                }
                            }
                        }
                        else
                        {
                            str.Add(new SimpleEPubText() { Text = text.Text });
                        }
                        list.Add(str);
                        break;
                    case FB2Library.Elements.TextStyles.Sub:
                        Sub sub = new Sub();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                SimpleTextElementConverter converter = new SimpleTextElementConverter { Item = child };
                                foreach (var item in converter.Convert())
                                {
                                    sub.Add(item);
                                }
                            }
                        }
                        else
                        {
                            sub.Add(new SimpleEPubText() { Text = text.Text });
                        }
                        list.Add(sub);
                        break;
                    case FB2Library.Elements.TextStyles.Sup:
                        Sup sup = new Sup();
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                SimpleTextElementConverter converter = new SimpleTextElementConverter { Item = child };
                                foreach (var item in converter.Convert())
                                {
                                    sup.Add(item);
                                }
                            }
                        }
                        else
                        {
                            sup.Add(new SimpleEPubText() { Text = text.Text });
                        }
                        list.Add(sup);
                        break;
                }
            }
            else if (Item is InternalLinkItem)
            {
                InternalLinkConverter linkConverter = new InternalLinkConverter{Item = Item as InternalLinkItem,Settings = Settings};
                foreach (var item in linkConverter.Convert())
                {
                    list.Add(item);
                }
            }
            else if (Item is InlineImageItem)
            {
                InlineImageConverter inlineImageConverter = new InlineImageConverter
                                                                {Item = Item as InlineImageItem,
                                                                Settings = Settings};
                list.Add(inlineImageConverter.Convert());
            }

            return list;
        }

    }
}
