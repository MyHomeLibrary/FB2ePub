using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes
{
    public class LinkRelationAttribute : BaseAttribute
    {
        protected enum RelationValues
        {
            Invalid,
            Alternate,
            Archives,
            Author,
            Bookmark,
            External,
            First,
            Help,
            Icon,
            Last,
            License,
            Next,
            NoFollow,
            NoRefferer,
            Pingback,
            Prefetch,
            Prev,
            Search,
            Sidebar,
            Stylesheet,
            Tag,
            Up,
        }

        private RelationValues _type = RelationValues.Invalid;

        public const string AttributeName = "rel";

        #region Overrides of BaseAttribute

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case RelationValues.Alternate:
                        return "alternate";
                    case RelationValues.Archives:
                        return "archives";
                    case RelationValues.Author:
                        return "author";
                    case RelationValues.Bookmark:
                        return "bookmark";
                    case RelationValues.External:
                        return "external";
                    case RelationValues.First:
                        return "first";
                    case RelationValues.Help:
                        return "help";
                    case RelationValues.Icon:
                        return "icon";
                    case RelationValues.Last:
                        return "last";
                    case RelationValues.License:
                        return "license";
                    case RelationValues.Next:
                        return "next";
                    case RelationValues.NoFollow:
                        return "nofollow";
                    case RelationValues.NoRefferer:
                        return "norefferer";
                    case RelationValues.Pingback:
                        return "pingback";
                    case RelationValues.Prefetch:
                        return "prefetch";
                    case RelationValues.Prev:
                        return "prev";
                    case RelationValues.Search:
                        return "search";
                    case RelationValues.Sidebar:
                        return "sidebar";
                    case RelationValues.Stylesheet:
                        return "stylesheet";
                    case RelationValues.Tag:
                        return "tag";
                    case RelationValues.Up:
                        return "up";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "alternate":
                        _type = RelationValues.Alternate;
                        break;
                    case "archives":
                        _type = RelationValues.Archives;
                        break;
                    case "author":
                        _type = RelationValues.Author;
                        break;
                    case "bookmark":
                        _type = RelationValues.Bookmark;
                        break;
                    case "external":
                        _type = RelationValues.External;
                        break;
                    case "first":
                        _type = RelationValues.First;
                        break;
                    case "help":
                        _type = RelationValues.Help;
                        break;
                    case "icon":
                        _type = RelationValues.Icon;
                        break;
                    case "last":
                        _type = RelationValues.Last;
                        break;
                    case "license":
                        _type = RelationValues.License;
                        break;
                    case "next":
                        _type = RelationValues.Next;
                        break;
                    case "nofollow":
                        _type = RelationValues.NoFollow;
                        break;
                    case "norefferer":
                        _type = RelationValues.NoRefferer;
                        break;
                    case "pingback":
                        _type = RelationValues.Pingback;
                        break;
                    case "prefetch":
                        _type = RelationValues.Prefetch;
                        break;
                    case "prev":
                        _type = RelationValues.Prev;
                        break;
                    case "search":
                        _type = RelationValues.Search;
                        break;
                    case "sidebar":
                        _type = RelationValues.Sidebar;
                        break;
                    case "stylesheet":
                        _type = RelationValues.Stylesheet;
                        break;
                    case "tag":
                        _type = RelationValues.Tag;
                        break;
                    case "up":
                        _type = RelationValues.Up;
                        break;
                    default:
                        _type = RelationValues.Invalid;
                        break;
                }
            }
        }

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion

    }
}
