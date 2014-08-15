using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "output" tag represents the result of a calculation (like one performed by a script).
    /// </summary>
    [HTMLItemAttribute(ElementName = "output", SupportedStandards = HTMLElementType.HTML5)]
    public class Output : HTMLItem, IInlineItem
    {
        public Output()
        {
            RegisterAttribute(_forAttribute);
            RegisterAttribute(_formIdAttribute);
            RegisterAttribute(_nameAttribute);
        }

        private readonly OutputForAttribute _forAttribute = new OutputForAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();


        /// <summary>
        /// Specifies the relationship between the result of the calculation, and the elements used in the calculation
        /// </summary>
        public OutputForAttribute For { get { return _forAttribute; } }

        /// <summary>
        /// Specifies one or more forms the output element belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the output element
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
