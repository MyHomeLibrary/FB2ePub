using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
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
