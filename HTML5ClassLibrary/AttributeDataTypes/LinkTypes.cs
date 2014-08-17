using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A predefined classification of hyperlinks. 
    /// The following is a list of predefined values: Alternate, Stylesheet, Start, Next, Prev, Contents, Index, Glossary, Copyright, Chapter, Section, Subsection, Appendix, Help and Bookmark. These values are case-insensitive.
    /// </summary>
    public class LinkTypes
    {
        private enum LinkTypesEnum
        {
            Invalid,
            Alternate,
            Stylesheet,
            Start,
            Next,
            Prev,
            Contents,
            Index,
            Glossary,
            Copyright,
            Chapter,
            Section,
            Subsection,
            Appendix,
            Help,
            Bookmark,
        }

        private LinkTypesEnum linkType = LinkTypesEnum.Invalid;

        public string Value
        {
            get
            {
                switch (linkType)
                {
                    case LinkTypesEnum.Alternate:
                        return "Alternate";
                    case LinkTypesEnum.Stylesheet:
                        return "Stylesheet";
                    case LinkTypesEnum.Start:
                        return "Start";
                    case LinkTypesEnum.Next:
                        return "Next";
                    case LinkTypesEnum.Prev:
                        return "Prev";
                    case LinkTypesEnum.Contents:
                        return "Contents";
                    case LinkTypesEnum.Index:return "Index";
                    case LinkTypesEnum.Glossary:
                        return "Glossary";
                    case LinkTypesEnum.Copyright:
                        return "Copyright";
                    case LinkTypesEnum.Chapter:
                        return "Chapter";
                    case LinkTypesEnum.Section:
                        return "Section";
                    case LinkTypesEnum.Subsection:
                        return "Subsection";
                    case LinkTypesEnum.Appendix:
                        return "Appendix";
                    case LinkTypesEnum.Help:
                        return "Help";
                    case LinkTypesEnum.Bookmark:
                        return "Bookmark";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "alternate":
                        linkType =  LinkTypesEnum.Alternate;
                        break;
                    case "stylesheet":
                        linkType = LinkTypesEnum.Stylesheet;
                        break;
                    case "start":
                        linkType = LinkTypesEnum.Start;
                        break;
                    case "next":
                        linkType = LinkTypesEnum.Next;
                        break;
                    case "prev":
                        linkType = LinkTypesEnum.Prev;
                        break;
                    case "contents" :
                        linkType = LinkTypesEnum.Contents;
                        break;
                    case "index":
                        linkType = LinkTypesEnum.Index;
                        break;
                    case "glossary":
                        linkType = LinkTypesEnum.Glossary;
                        break;
                    case "copyright":
                        linkType = LinkTypesEnum.Copyright;
                        break;
                    case "chapter":
                        linkType = LinkTypesEnum.Chapter;
                        break;
                    case "section":
                        linkType = LinkTypesEnum.Section;
                        break;
                    case "subsection":
                        linkType = LinkTypesEnum.Subsection;
                        break;
                    case "appendix":
                        linkType = LinkTypesEnum.Appendix;
                        break;
                    case "help":
                        linkType = LinkTypesEnum.Help;
                        break;
                    case "bookmark":
                        linkType = LinkTypesEnum.Bookmark;
                        break;
                    default:
                        linkType = LinkTypesEnum.Invalid;
                        break;
                }
                
            }
        }
    }
}
