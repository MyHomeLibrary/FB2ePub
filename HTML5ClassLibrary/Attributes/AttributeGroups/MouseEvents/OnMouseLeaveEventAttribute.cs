using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
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
