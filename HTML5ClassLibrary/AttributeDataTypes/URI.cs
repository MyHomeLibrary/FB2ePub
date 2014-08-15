using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A Uniform Resource Identifier reference. URI can be an Internet address. 
    /// For example: http://xhtml.com/ or images/file.gif. 
    /// URI can also be an identification of a resource without an Internet location. 
    /// For example: urn:ISBN 88-7633-000-3.
    /// </summary>
    public class URI
    {
        public string Value { get; set; }
    }
}
