using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardEvents
    {
        private readonly OnKeyDownEventAttribute _onKeyDownEventAttribute = new OnKeyDownEventAttribute(); // global
        private readonly OnKeyPressEventAttribute _onKeyPressEventAttribute = new OnKeyPressEventAttribute(); // global
        private readonly OnKeyUpEventAttribute _onKeyUpEventAttribute = new OnKeyUpEventAttribute(); // global



        /// <summary>
        /// Fires when a user is pressing a key
        /// </summary>
        public OnKeyDownEventAttribute OnKeyDownEvent { get { return _onKeyDownEventAttribute; }}

        /// <summary>
        /// Fires when a user presses a key
        /// </summary>
        public OnKeyPressEventAttribute OnKeyPressEvent { get { return _onKeyPressEventAttribute; }}


        /// <summary>
        /// Fires when a user releases a key
        /// </summary>
        public OnKeyUpEventAttribute OnKeyUpEvent   { get { return _onKeyUpEventAttribute; }}




        /// <summary>
        /// Add all attributes set to specified xElement
        /// </summary>
        /// <param name="xElement">element to store/write attributes to</param>
        public void AddAttributes(XElement xElement)
        {
            _onKeyDownEventAttribute.AddAttribute(xElement);
            _onKeyPressEventAttribute.AddAttribute(xElement);
            _onKeyUpEventAttribute.AddAttribute(xElement);
        }

        /// <summary>
        /// Loads all attributes from provided xElement
        /// </summary>
        /// <param name="xElement">element to load attributes from</param>
        public void ReadAttributes(XElement xElement)
        {
            _onKeyDownEventAttribute.ReadAttribute(xElement);
            _onKeyPressEventAttribute.ReadAttribute(xElement);
            _onKeyUpEventAttribute.ReadAttribute(xElement);
        }

    }
}
