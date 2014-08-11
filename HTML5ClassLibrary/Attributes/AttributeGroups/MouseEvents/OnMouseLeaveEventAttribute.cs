using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class OnMouseLeaveEventAttribute : OnEventAttribute
    {
        protected override string GetAttributeName()
        {
            return "mouseleave";
        }

    }
}
