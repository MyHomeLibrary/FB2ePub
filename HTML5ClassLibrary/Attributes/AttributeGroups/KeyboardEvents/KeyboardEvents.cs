using System.Collections.Generic;
using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents
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
        public void AddAttributes(List<IBaseAttribute> attributesList)
        {
            attributesList.Add(_onKeyDownEventAttribute);
            attributesList.Add(_onKeyPressEventAttribute);
            attributesList.Add(_onKeyUpEventAttribute);
        }

    }
}
