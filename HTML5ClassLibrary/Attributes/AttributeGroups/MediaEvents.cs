using XHTMLClassLibrary.Attributes.Events;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups
{
    public class MediaEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onAbortEventAttribute = new OnEventAttribute("onabort");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onCanPlayEventAttribute = new OnEventAttribute("oncanplay");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onCanPlayThroughEventAttribute = new OnEventAttribute("oncanplaythrough");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onDurationChangeEventAttribute = new OnEventAttribute("ondurationchange");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onEmptiedEventAttribute = new OnEventAttribute("onemptied");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onEndedEventAttribute = new OnEventAttribute("onended");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaErrorEventAttribute = new OnEventAttribute("onerror");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaLoadedDataEventAttribute = new OnEventAttribute("onloadeddata");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaLoadedMetadateEventAttribute = new OnEventAttribute("onloadedmetadata");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaLoadStartEventAttribute = new OnEventAttribute("onloadstart");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaPauseEventAttribute = new OnEventAttribute("onpause");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaPlayEventAtrribute = new OnEventAttribute("onplay");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaPlayingEventAttribute = new OnEventAttribute("onplaying");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaProgressEventAttribute = new OnEventAttribute("onprogress");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaRateChangeEventAttribute = new OnEventAttribute("onratechange");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaReadyStateChangeEventAttribute = new OnEventAttribute("onreadystatechange"); 

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaSeekedEventAttribute = new OnEventAttribute("onseeked");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaSeekingEventAttribute = new OnEventAttribute("onseeking");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaStalledEventAttribute = new OnEventAttribute("onstalled");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaSuspendEventAttribute = new OnEventAttribute("onsuspend");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaTimeUpdateEventAttribute = new OnEventAttribute("ontimeupdate");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaVolumeChangeEventAttribute = new OnEventAttribute("onvolumechange");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaWaitingEventAttribute = new OnEventAttribute("onwaiting");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaCueChangeEventAttribute = new OnEventAttribute("oncuechange");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMediaCancelEventAttribute = new OnEventAttribute("oncancel"); 



        /// <summary>
        /// Script to be run on abort
        /// </summary>
        public OnEventAttribute OnAbortEvent { get { return _onAbortEventAttribute; }}

        /// <summary>
        /// Script to be run when a file is ready to start playing (when it has buffered enough to begin)
        /// </summary>
        public OnEventAttribute OnCanPlayEvent { get { return _onCanPlayEventAttribute; }}


        /// <summary>
        /// Script to be run when a file can be played all the way to the end without pausing for buffering
        /// </summary>
        public OnEventAttribute OnCanPlayThroughEvent { get { return _onCanPlayThroughEventAttribute; }}


        /// <summary>
        /// Script to be run when the length of the media changes
        /// </summary>
        public OnEventAttribute OnDurationChangeEvent { get { return _onDurationChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when something bad happens and the file is suddenly unavailable (like unexpectedly disconnects)
        /// </summary>
        public OnEventAttribute OnEmptiedEvent { get { return _onEmptiedEventAttribute; }}


        /// <summary>
        /// Script to be run when the media has reach the end (a useful event for messages like "thanks for listening")
        /// </summary>
        public OnEventAttribute OnEndedEvent { get { return _onEndedEventAttribute; }}


        /// <summary>
        /// Script to be run when an error occurs when the file is being loaded 
        /// </summary>
        public OnEventAttribute OnMediaErrorEvent { get { return _onMediaErrorEventAttribute; }}


        /// <summary>
        /// Script to be run when media data is loaded
        /// </summary>
        public OnEventAttribute OnMediaLoadedDataEvent { get { return _onMediaLoadedDataEventAttribute; }}


        /// <summary>
        /// Script to be run when meta data (like dimensions and duration) are loaded
        /// </summary>
        public OnEventAttribute OnMediaLoadedMetadateEvent { get { return _onMediaLoadedMetadateEventAttribute; }}


        /// <summary>
        /// Script to be run just as the file begins to load before anything is actually loaded
        /// </summary>
        public OnEventAttribute OnMediaLoadStartEvent { get { return _onMediaLoadStartEventAttribute; }}


        /// <summary>
        /// Script to be run when the media is paused either by the user or programmatically
        /// </summary>
        public OnEventAttribute OnMediaPauseEvent { get { return _onMediaPauseEventAttribute; }}


        /// <summary>
        /// Script to be run when the media is ready to start playing
        /// </summary>
        public OnEventAttribute OnMediaPlayEvent { get { return _onMediaPlayEventAtrribute; }}


        /// <summary>
        /// Script to be run when the media actually has started playing
        /// </summary>
        public OnEventAttribute OnMediaPlayingEvent { get { return _onMediaPlayingEventAttribute; }}


        /// <summary>
        /// Script to be run when the browser is in the process of getting the media data
        /// </summary>
        public OnEventAttribute OnMediaProgressEvent {get { return _onMediaProgressEventAttribute; }}


        /// <summary>
        /// Script to be run each time the playback rate changes (like when a user switches to a slow motion or fast forward mode)
        /// </summary>
        public OnEventAttribute OnMediaRateChangeEvent { get { return _onMediaRateChangeEventAttribute; }}


        /// <summary>
        /// Script to be run each time the ready state changes (the ready state tracks the state of the media data)
        /// </summary>
        public OnEventAttribute OnMediaReadyStateChangeEvent { get { return _onMediaReadyStateChangeEventAttribute; } }


        /// <summary>
        /// Script to be run when the seeking attribute is set to false indicating that seeking has ended
        /// </summary>
        public OnEventAttribute OnMediaSeekedEvent { get { return _onMediaSeekedEventAttribute; }}


        /// <summary>
        /// Script to be run when the seeking attribute is set to true indicating that seeking is active
        /// </summary>
        public OnEventAttribute OnMediaSeekingEvent { get { return _onMediaSeekingEventAttribute; }}

        /// <summary>
        /// Script to be run when the browser is unable to fetch the media data for whatever reason
        /// </summary>
        public OnEventAttribute OnMediaStalledEvent { get { return _onMediaStalledEventAttribute; }}


        /// <summary>
        /// Script to be run when fetching the media data is stopped before it is completely loaded for whatever reason
        /// </summary>
        public OnEventAttribute OnMediaSuspendEvent { get { return _onMediaSuspendEventAttribute; }}


        /// <summary>
        /// Script to be run when the playing position has changed (like when the user fast forwards to a different point in the media)
        /// </summary>
        public OnEventAttribute OnMediaTimeUpdateEvent { get { return _onMediaTimeUpdateEventAttribute; }}


        /// <summary>
        /// Script to be run each time the volume is changed which (includes setting the volume to "mute")
        /// </summary>
        public OnEventAttribute OnMediaVolumeChangeEvent { get { return _onMediaVolumeChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when the media has paused but is expected to resume (like when the media pauses to buffer more data)
        /// </summary>
        public OnEventAttribute OnMediaWaitingEvent { get { return _onMediaWaitingEventAttribute; }}


        /// <summary>
        /// 
        /// </summary>
        public OnEventAttribute OnMediaCueChangeEvent { get { return _onMediaCueChangeEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnEventAttribute OnMediaCancelEvent { get { return _onMediaCancelEventAttribute; }}

      
    }
}
