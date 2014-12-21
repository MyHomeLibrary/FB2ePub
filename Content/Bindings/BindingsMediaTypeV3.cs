using System;
using System.Xml.Linq;

namespace EPubLibrary.Content.Bindings
{
    /// <summary>
    /// The mediaType element associates a Foreign Resource media type with a handler XHTML Content Document. 
    /// </summary>
    class BindingsMediaTypeV3
    {
        /// <summary>
        /// A media type [RFC2046] that specifies the type and format of the resource to be handled.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// An IDREF [XML] that identifies the manifest XHTML Content Document to be invoked to handle content of the type specified in this element
        /// </summary>
        public string Handler { get; set; }

        public XElement GenerateElement()
        {
            if (string.IsNullOrEmpty(MediaType))
            {
                throw new InvalidOperationException("Media Type attribute need to be set for an BindingsMediaTypeV3 element");
            }
            if (string.IsNullOrEmpty(Handler))
            {
                throw new InvalidOperationException("Media Type attribute need to be set for an BindingsMediaTypeV3 element");
            }
            var bindingsElement = new XElement(EPubNamespaces.OpfNameSpace + "mediaType");
            bindingsElement.Add(new XAttribute("media-type", MediaType));
            bindingsElement.Add(new XAttribute("handler", Handler));
            return bindingsElement;
        }
    }
}
