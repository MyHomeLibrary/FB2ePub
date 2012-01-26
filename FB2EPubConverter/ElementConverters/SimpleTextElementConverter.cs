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

        /// <summary>
        /// Converts FB2 simple text 
        /// ( simple text is normal text or text with one of the "styles")
        /// </summary>
        /// <param name="styletypeItem">item to convert</param>
        /// <returns></returns>
        public List<IXHTMLItem> Convert(StyleType styletypeItem)
        {
            return Convert(styletypeItem,false);
        }

        /// <summary>
        /// Converts FB2 simple text 
        /// ( simple text is normal text or text with one of the "styles")
        /// </summary>
        /// <param name="styletypeItem">item to convert</param>
        /// <param name="needToInsert">set to true if we want to create a "capital drop" part</param>
        /// <returns></returns>
        public List<IXHTMLItem> Convert(StyleType styletypeItem,bool needToInsert)
        {

            if (styletypeItem == null)
            {
                throw new ArgumentNullException("styletypeItem");
            }

            List<IXHTMLItem> list = new List<IXHTMLItem>();

            if (styletypeItem is SimpleText)
            {
                SimpleText text = styletypeItem as SimpleText;
                switch (text.Style)
                {
                    case FB2Library.Elements.TextStyles.Normal:
                        if (text.HasChildren)
                        {
                            foreach (var child in text.Children)
                            {
                                foreach (var item in Convert(child))
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (needToInsert && text.Text.Length > 0)
                            {
                                AddAsDrop(list,text);
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
                                foreach (var item in Convert(child))
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
                                foreach (var item in Convert(child))
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
                                foreach (var item in Convert(child))
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
                                foreach (var item in Convert(child))
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
                                foreach (var item in Convert(child))
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
            else if (styletypeItem is InternalLinkItem)
            {
                InternalLinkConverter linkConverter = new InternalLinkConverter { Settings = Settings };
                foreach (var item in linkConverter.Convert(styletypeItem as InternalLinkItem))
                {
                    list.Add(item);
                }
            }
            else if (styletypeItem is InlineImageItem)
            {
                InlineImageConverter inlineImageConverter = new InlineImageConverter {Settings = Settings};
                list.Add(inlineImageConverter.Convert(styletypeItem as InlineImageItem));
            }

            return list;
        }

        /// <summary>
        /// Create a text with Capital Drop
        /// </summary>
        /// <param name="parent">parent container insert into</param>
        /// <param name="text">text to insert</param>
        private static void AddAsDrop(List<IXHTMLItem> parent, SimpleText text)
        {
            var span1 = new Span();
            span1.Class.Value = "drop";
            int dropEnd = 0;
            // "pad" the white spaces so drop starts from visible character
            while (dropEnd < text.Text.Length && UnicodeHelpers.IsSpaceLike(text.Text[dropEnd]) )
            {
                dropEnd++;
            }
            if (dropEnd >= text.Text.Length) // in case the text is too short for drop
            {
                parent.Add(new SimpleEPubText { Text = text.Text});
                return;
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
                if (nondropPosition < text.Text.Length)
                {
                    nondropPosition++;
                }
                // update drop part with the "string" we calculated
                dropPart += text.Text.Substring(dropEnd + 1, nondropPosition - dropEnd - 1);
            }
            span1.Add(new SimpleEPubText { Text = dropPart });
            parent.Add(span1);
            string substring = text.Text.Substring(nondropPosition);
            if (substring.Length > 0)
            {
                parent.Add(new SimpleEPubText { Text = substring });
            }

        }

        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
