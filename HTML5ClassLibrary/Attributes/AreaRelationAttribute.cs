using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class AreaRelationAttribute : BaseAttribute
    {
        protected enum RelationValues
        {
            Invalid,
            Alternate,
            Author,
            Bookmark,
            Help,
            License,
            Next,
            NoFollow,
            NoRefferer,
            Prefetch,
            Prev,
            Search,
            Tag,
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
                    case RelationValues.Author:
                        return "author";
                    case RelationValues.Bookmark:
                        return "bookmark";
                    case RelationValues.Help:
                        return "help";
                    case RelationValues.License:
                        return "license";
                    case RelationValues.Next:
                        return "next";
                    case RelationValues.NoFollow:
                        return "nofollow";
                    case RelationValues.NoRefferer:
                        return "norefferer";
                    case RelationValues.Prefetch:
                        return "prefetch";
                    case RelationValues.Prev:
                        return "prev";
                    case RelationValues.Search:
                        return "search";
                    case RelationValues.Tag:
                        return "tag";
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
                    case "author":
                        _type = RelationValues.Author;
                        break;
                    case "bookmark":
                        _type = RelationValues.Bookmark;
                        break;
                    case "help":
                        _type = RelationValues.Help;
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
                    case "prefetch":
                        _type = RelationValues.Prefetch;
                        break;
                    case "prev":
                        _type = RelationValues.Prev;
                        break;
                    case "search":
                        _type = RelationValues.Search;
                        break;
                    case "tag":
                        _type = RelationValues.Tag;
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
