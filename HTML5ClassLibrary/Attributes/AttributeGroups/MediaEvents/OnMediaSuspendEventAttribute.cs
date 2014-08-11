﻿using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when fetching the media data is stopped before it is completely loaded for whatever reason
    /// </summary>
    public class OnMediaSuspendEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onsuspend";
        }

        #endregion
    }
}
