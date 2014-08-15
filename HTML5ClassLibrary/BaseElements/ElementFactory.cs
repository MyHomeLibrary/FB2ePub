using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace HTMLClassLibrary.BaseElements
{
    public static class ElementFactory
    {
        private static readonly Dictionary<HTMLElementType,ElementConvertor>  Converters = new Dictionary<HTMLElementType, ElementConvertor>();


        static ElementFactory()
        {
            // Create converters 
            Converters.Add(HTMLElementType.FrameSet,new ElementConvertor());
            Converters.Add(HTMLElementType.HTML5, new ElementConvertor());
            Converters.Add(HTMLElementType.Strict, new ElementConvertor());
            Converters.Add(HTMLElementType.Transitional, new ElementConvertor());
            Converters.Add(HTMLElementType.XHTML11, new ElementConvertor());
            // initialize converters
            Assembly currentAssembly = Assembly.GetAssembly(typeof(ElementFactory));
            Type[] types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterface(typeof(IHTMLItem).ToString()) != null)
                {
                    var attributes = (HTMLItemAttribute[])type.GetCustomAttributes(typeof(HTMLItemAttribute), false);
                    if (attributes != null &&
                        attributes.Length > 0)
                    {
                        HTMLElementType compatibilityMask = attributes[0].SupportedStandards;
                        if (compatibilityMask.HasFlag(HTMLElementType.FrameSet))
                        {
                            Converters[HTMLElementType.FrameSet].AddElement(attributes[0].ElementName, type);
                        }

                        if (compatibilityMask.HasFlag(HTMLElementType.HTML5))
                        {
                            Converters[HTMLElementType.HTML5].AddElement(attributes[0].ElementName, type);
                        }

                        if (compatibilityMask.HasFlag(HTMLElementType.Strict))
                        {
                            Converters[HTMLElementType.Strict].AddElement(attributes[0].ElementName, type);
                        }

                        if (compatibilityMask.HasFlag(HTMLElementType.Transitional))
                        {
                            Converters[HTMLElementType.Transitional].AddElement(attributes[0].ElementName, type);
                        }

                        if (compatibilityMask.HasFlag(HTMLElementType.XHTML11))
                        {
                            Converters[HTMLElementType.XHTML11].AddElement(attributes[0].ElementName, type);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if passed standard  is valid 
        /// we need this as HTMLElementType is enumeration of [Flags] type and here we need only single value, not bitmask
        /// </summary>
        /// <param name="standardType">standard to create element according to</param>
        /// <returns>if this is valid standard</returns>
        public static bool CheckIfValidStandardArgument(HTMLElementType standardType)
        {
            if (standardType == HTMLElementType.FrameSet ||
                standardType == HTMLElementType.HTML5 ||
                standardType == HTMLElementType.Strict ||
                standardType == HTMLElementType.Transitional ||
                standardType == HTMLElementType.XHTML11)
            {
                return true;
            }
            return false;
        }


        public static IHTMLItem CreateElement(XNode xNode,HTMLElementType standardType)
        {
            if (!CheckIfValidStandardArgument(standardType))
            {
                return null;
            }
            if (xNode.NodeType == XmlNodeType.Element)
            {
                var element = (XElement) xNode;
                return Converters[standardType].CreateHTMLItem(element.Name.LocalName);

            }
            else if (xNode.NodeType == XmlNodeType.Text)
            {
                return new SimpleHTML5Text();
            }
            return null;
        }


    }
}
