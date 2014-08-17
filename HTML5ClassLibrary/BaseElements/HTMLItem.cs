using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using XHTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.MediaEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using XHTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements
{
    /// <summary>
    /// A base class for any HTML item
    /// </summary>
    abstract public class HTMLItem : IHTMLItem
    {
        private readonly List<IBaseAttribute> _attributes = new List<IBaseAttribute>();
        private HTMLElementType _htmlStandard = HTMLElementType.HTML5;

        protected readonly List<IHTMLItem> Subitems  = new List<IHTMLItem>();
        protected ISimpleText TextContent = null;


        // General common attributes and event attributes for any HTML element
        [AttributeBlockAttribute]
        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        [AttributeBlockAttribute]
        private readonly FormEvents _formEvents = new FormEvents();
        [AttributeBlockAttribute]
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        [AttributeBlockAttribute]
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        [AttributeBlockAttribute]
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        [AttributeBlockAttribute]
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();

        protected HTMLItem()
        {
            _globalAttributes.AddAttributes(_attributes);
            _formEvents.AddAttributes(_attributes);
            _keyboardEvents.AddAttributes(_attributes);
            _mediaEvents.AddAttributes(_attributes);
            _mouseEvents.AddAttributes(_attributes);
            _windowEventAttributes.AddAttributes(_attributes);
            RegisterAttributes();
        }

        /// <summary>
        /// Get/set item's standard it should comply to
        /// </summary>
        public HTMLElementType HTMLStandard
        {
            get { return _htmlStandard;}
            set
            {
                if (Subitems.Count > 0 ||
                    TextContent != null)
                {
                    throw new ArgumentException("Standard can't be changed when sub-object already created","value");
                }
                if (ElementFactory.CheckIfValidStandardArgument(value))
                {
                    _htmlStandard = value;
                }
            }
        }


        public static IEnumerable<FieldInfo> GetAllFields(Type t,BindingFlags flags)
        {
            if (t == null)
                return Enumerable.Empty<FieldInfo>();

            //BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
            //                     BindingFlags.Static | BindingFlags.Instance |
            //                     BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.BaseType,flags));
        }


        private void RegisterAttributes()
        {
            _attributes.Clear();
            var fieldInfo = GetAllFields(GetType(),(BindingFlags.NonPublic | BindingFlags.Instance));
            foreach (var field in fieldInfo)
            {
                var attributes =
                    (Attribute[])
                        field.GetCustomAttributes(typeof(AttributeTypeAttributeMember), false);
                if (attributes != null &&
                    attributes.Length > 0)
                {
                    var attributeType = attributes[0] as AttributeTypeAttributeMember;
                    if (attributeType != null &&
                        attributeType.SupportedStandards.HasFlag(_htmlStandard))
                    {
                        RegisterSingleAttribute(field,this);
                    }
                }
                attributes =
                    (Attribute[])
                        field.GetCustomAttributes(typeof(AttributeBlockAttribute), false);
                if (attributes != null &&
                    attributes.Length > 0)
                {
                    var attributeBlock = attributes[0] as AttributeBlockAttribute;
                    if (attributeBlock != null)
                    {
                        RegisterAttributeBlock(field);
                    }
                }


            }
        }

        private void RegisterAttributeBlock(FieldInfo field)
        {
            var blockFields = field.FieldType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var blockField in blockFields)
            {
                var attributes =
                    (Attribute[])
                        blockField.GetCustomAttributes(typeof(AttributeTypeAttributeMember), false);
                if (attributes != null &&
                    attributes.Length > 0)
                {
                    var attributeType = attributes[0] as AttributeTypeAttributeMember;
                    if (attributeType != null &&
                        attributeType.SupportedStandards.HasFlag(_htmlStandard))
                    {
                        var containingMemeber = field.GetValue(this);
                        if (containingMemeber != null)
                        {
                            RegisterSingleAttribute(blockField,containingMemeber);
                        }
                    }
                }
            }
        }


        private void RegisterSingleAttribute(FieldInfo field,object instanceObject)
        {
            var interfaces = field.FieldType.GetInterfaces();
            if (interfaces != null &&
                interfaces.Length > 0)
            {
                if (interfaces.Contains(typeof(IBaseAttribute)))
                {
                    var memberItem = field.GetValue(instanceObject);
                    _attributes.Add((IBaseAttribute)memberItem);
                }
                else
                {
                    throw new ArgumentException(string.Format("The item {0} passed is not implements IBaseAttribute interface",field.Name),"field");
                }
            }
        }

        /// <summary>
        /// Loads an element and it's data from a node
        /// </summary>
        /// <param name="xNode"></param>
        public virtual void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new ArgumentException("xNode is not of element type","xNode");
            }
            string currentObjectElementName = GetObjectElementName();
            var xElement = (XElement)xNode;
            // check that we got a element of matching (to us) type/name
            if (string.CompareOrdinal(xElement.Name.LocalName ,currentObjectElementName) != 0)
            {
                throw new ArgumentException(string.Format("xNode is not {0} element, it's {1}", currentObjectElementName, xElement.Name.LocalName), "xNode");
            }
            // load attributes 
            ReadAttributes(xElement);

            // load sub-items
            Subitems.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTMLItem item = ElementFactory.CreateElement(node, _htmlStandard);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Subitems.Add(item);
                }
            }
            if (TextContent != null) // need to be created in constructor of derived class
            {
                TextContent.Load(xElement);   
            }
        }

        /// <summary>
        /// Return name of the element associated with current HTML object
        /// </summary>
        /// <returns>returns the name</returns>
        private string GetObjectElementName()
        {
            // get current object type
            System.Reflection.MemberInfo inf = GetType();
            // get all attributes of HTMLItemAttribute type
            var attributes = (HTMLItemAttribute[])inf.GetCustomAttributes(typeof(HTMLItemAttribute), false);
            // check if the item has attributes attribute
            if (attributes == null || attributes.Length < 1)
            {
                throw new InvalidDataException(string.Format("The object of type {0} does not have HTMLItemAttribute set", inf));
            }
            //we assume only one property per element , so in any case only first one valid)
            return attributes[0].ElementName;
        }

        /// <summary>
        /// Creates XNode containing all object data
        /// </summary>
        /// <returns></returns>
        public virtual XNode Generate()
        {
            string currentObjectElementName = GetObjectElementName();
            var xElement = new XElement(/* namespace + ?*/currentObjectElementName);

            AddAttributes(xElement);

            foreach (var item in Subitems)
            {
                xElement.Add(item.Generate());
            }

            if (TextContent != null)
            {
                xElement.Add(TextContent.Generate());
            }
            return xElement;

        }

        /// <summary>
        /// Check if item passed is valid to be inserted as a sub-item
        /// Need to be overridden in extensions to provide proper check
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual bool IsValidSubType(IHTMLItem item)
        {
            return false; // by default nothing valid - means nothing added as subtype
        }


        /// <summary>
        /// Register attribute as belong to element
        /// </summary>
        /// <param name="attribute"></param>
        protected void RegisterAttribute(IBaseAttribute attribute)
        {
            _attributes.Add(attribute);
        }

        /// <summary>
        /// Check if element valid 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return false;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public virtual void Add(IHTMLItem item)
        {
            if ((item != null) && IsValidSubType(item))
            {
                Subitems.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException();
            }
        }

        /// <summary>
        /// Remove sub-item 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(IHTMLItem item)
        {
            if (Subitems.Remove(item))
            {
                item.Parent = null;
            }
        }

        /// <summary>
        /// Returns list of sub-items
        /// </summary>
        /// <returns></returns>
        public virtual List<IHTMLItem> SubElements()
        {
            return Subitems;
        }


        /// <summary>
        /// Get/Set item parent in the HTML "tree"
        /// </summary>
        public IHTMLItem Parent { get; set; }

        /// <summary>
        /// Return internal text item
        /// </summary>
        public ISimpleText InternalTextItem
        {
            get { return TextContent; }
        }


        /// <summary>
        /// Global attributes 
        /// </summary>
        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; } }

        /// <summary>
        /// Form related events
        /// </summary>
        public FormEvents FormEvents { get { return _formEvents; } }

        /// <summary>
        /// Keyboard related events
        /// </summary>
        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; } }

        /// <summary>
        /// Media related events (can include few that not 100% media related in all cases)
        /// </summary>
        public MediaEvents MediaEvents { get { return _mediaEvents; } }

        /// <summary>
        /// Mouse global events
        /// </summary>
        public MouseEvents MouseEvents { get { return _mouseEvents; } }

        /// <summary>
        /// Window events , not always working for all types of elements, mostly for windows like "body"
        /// </summary>
        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; } }

        /// <summary>
        /// Used to add attributes to any element when generating it
        /// </summary>
        /// <param name="xElement">XElement to add attributes to </param>
        protected void AddAttributes(XElement xElement)
        {
            foreach (var attribute in _attributes)
            {
                attribute.AddAttribute(xElement);
            }
        }

        /// <summary>
        /// Used to read out attributes from the element passed, when reading/loading it
        /// </summary>
        /// <param name="xElement">XElement to load attributes from</param>
        protected void ReadAttributes(XElement xElement)
        {
            foreach (var attribute in _attributes)
            {
                attribute.ReadAttribute(xElement);
            }
        }
    }
}
