using System.Collections.Generic;
using System.Xml.Linq;

namespace EPubLibrary.Content.Bindings
{
    /// <summary>
    /// The bindings element defines a set of custom handlers for media types not supported by this specification.
    /// Optional fourth or fifth child of package , following spine or guide .
    /// </summary>
    class BindingsV3 : List<BindingsMediaTypeV3>
    {
        public XElement GenerateBindingsElement()
        {
            XElement bindingsElement = null;
            if (Count > 0)
            {

                bindingsElement = new XElement(EPubNamespaces.OpfNameSpace + "bindings");
                foreach (var binding in this)
                {
                    XElement mediatypeElement = binding.GenerateElement();
                    bindingsElement.Add(mediatypeElement);
                }
            }
            return bindingsElement;
        }
    }
}
