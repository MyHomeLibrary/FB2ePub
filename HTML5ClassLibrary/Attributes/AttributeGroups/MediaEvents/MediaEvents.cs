using System.Collections.Generic;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    public class MediaEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnMediaAbortEventAttribute _onAbortEventAttribute = new OnMediaAbortEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaCanPlayEventAttribute _onCanPlayEventAttribute = new OnMediaCanPlayEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaCanPlayThroughEventAttribute _onCanPlayThroughEventAttribute = new OnMediaCanPlayThroughEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaDurationChangeEventAttribute _onDurationChangeEventAttribute = new OnMediaDurationChangeEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaEmptiedEventAttribute _onEmptiedEventAttribute = new OnMediaEmptiedEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaEndedEventAttribute _onEndedEventAttribute = new OnMediaEndedEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaErrorEventAttribute _onMediaErrorEventAttribute = new OnMediaErrorEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaLoadedDataEventAttribute _onMediaLoadedDataEventAttribute = new OnMediaLoadedDataEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaLoadedMetadateEventAttribute _onMediaLoadedMetadateEventAttribute = new OnMediaLoadedMetadateEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaLoadStartEventAttribute _onMediaLoadStartEventAttribute = new OnMediaLoadStartEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaPauseEventAttribute _onMediaPauseEventAttribute = new OnMediaPauseEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaPlayEventAtrribute _onMediaPlayEventAtrribute = new OnMediaPlayEventAtrribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaPlayingEventAttribute _onMediaPlayingEventAttribute = new OnMediaPlayingEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaProgressEventAttribute _onMediaProgressEventAttribute = new OnMediaProgressEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaRateChangeEventAttribute _onMediaRateChangeEventAttribute = new OnMediaRateChangeEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaReadyStateChangeEventAttribute _onMediaReadyStateChangeEventAttribute = new OnMediaReadyStateChangeEventAttribute(); //global

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaSeekedEventAttribute _onMediaSeekedEventAttribute = new OnMediaSeekedEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaSeekingEventAttribute _onMediaSeekingEventAttribute = new OnMediaSeekingEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaStalledEventAttribute _onMediaStalledEventAttribute = new OnMediaStalledEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaSuspendEventAttribute _onMediaSuspendEventAttribute = new OnMediaSuspendEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaTimeUpdateEventAttribute _onMediaTimeUpdateEventAttribute = new OnMediaTimeUpdateEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaVolumeChangeEventAttribute _onMediaVolumeChangeEventAttribute = new OnMediaVolumeChangeEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaWaitingEventAttribute _onMediaWaitingEventAttribute = new OnMediaWaitingEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaCueChangeEventAttribute _onMediaCueChangeEventAttribute = new OnMediaCueChangeEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnMediaCancelEventAttribute _onMediaCancelEventAttribute = new OnMediaCancelEventAttribute(); 



        /// <summary>
        /// Script to be run on abort
        /// </summary>
        public OnMediaAbortEventAttribute OnAbortEvent { get { return _onAbortEventAttribute; }}

        /// <summary>
        /// Script to be run when a file is ready to start playing (when it has buffered enough to begin)
        /// </summary>
        public OnMediaCanPlayEventAttribute OnCanPlayEvent { get { return _onCanPlayEventAttribute; }}


        /// <summary>
        /// Script to be run when a file can be played all the way to the end without pausing for buffering
        /// </summary>
        public OnMediaCanPlayThroughEventAttribute OnCanPlayThroughEvent { get { return _onCanPlayThroughEventAttribute; }}


        /// <summary>
        /// Script to be run when the length of the media changes
        /// </summary>
        public OnMediaDurationChangeEventAttribute OnDurationChangeEvent { get { return _onDurationChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when something bad happens and the file is suddenly unavailable (like unexpectedly disconnects)
        /// </summary>
        public OnMediaEmptiedEventAttribute OnEmptiedEvent { get { return _onEmptiedEventAttribute; }}


        /// <summary>
        /// Script to be run when the media has reach the end (a useful event for messages like "thanks for listening")
        /// </summary>
        public OnMediaEndedEventAttribute OnEndedEvent { get { return _onEndedEventAttribute; }}


        /// <summary>
        /// Script to be run when an error occurs when the file is being loaded 
        /// </summary>
        public OnMediaErrorEventAttribute OnMediaErrorEvent { get { return _onMediaErrorEventAttribute; }}


        /// <summary>
        /// Script to be run when media data is loaded
        /// </summary>
        public OnMediaLoadedDataEventAttribute OnMediaLoadedDataEvent { get { return _onMediaLoadedDataEventAttribute; }}


        /// <summary>
        /// Script to be run when meta data (like dimensions and duration) are loaded
        /// </summary>
        public OnMediaLoadedMetadateEventAttribute OnMediaLoadedMetadateEvent { get { return _onMediaLoadedMetadateEventAttribute; }}


        /// <summary>
        /// Script to be run just as the file begins to load before anything is actually loaded
        /// </summary>
        public OnMediaLoadStartEventAttribute OnMediaLoadStartEvent { get { return _onMediaLoadStartEventAttribute; }}


        /// <summary>
        /// Script to be run when the media is paused either by the user or programmatically
        /// </summary>
        public OnMediaPauseEventAttribute OnMediaPauseEvent { get { return _onMediaPauseEventAttribute; }}


        /// <summary>
        /// Script to be run when the media is ready to start playing
        /// </summary>
        public OnMediaPlayEventAtrribute OnMediaPlayEvent { get { return _onMediaPlayEventAtrribute; }}


        /// <summary>
        /// Script to be run when the media actually has started playing
        /// </summary>
        public OnMediaPlayingEventAttribute OnMediaPlayingEvent { get { return _onMediaPlayingEventAttribute; }}


        /// <summary>
        /// Script to be run when the browser is in the process of getting the media data
        /// </summary>
        public OnMediaProgressEventAttribute OnMediaProgressEvent {get { return _onMediaProgressEventAttribute; }}


        /// <summary>
        /// Script to be run each time the playback rate changes (like when a user switches to a slow motion or fast forward mode)
        /// </summary>
        public OnMediaRateChangeEventAttribute OnMediaRateChangeEvent { get { return _onMediaRateChangeEventAttribute; }}


        /// <summary>
        /// Script to be run each time the ready state changes (the ready state tracks the state of the media data)
        /// </summary>
        public OnMediaReadyStateChangeEventAttribute OnMediaReadyStateChangeEvent { get { return _onMediaReadyStateChangeEventAttribute; } }


        /// <summary>
        /// Script to be run when the seeking attribute is set to false indicating that seeking has ended
        /// </summary>
        public OnMediaSeekedEventAttribute OnMediaSeekedEvent { get { return _onMediaSeekedEventAttribute; }}


        /// <summary>
        /// Script to be run when the seeking attribute is set to true indicating that seeking is active
        /// </summary>
        public OnMediaSeekingEventAttribute OnMediaSeekingEvent { get { return _onMediaSeekingEventAttribute; }}

        /// <summary>
        /// Script to be run when the browser is unable to fetch the media data for whatever reason
        /// </summary>
        public OnMediaStalledEventAttribute OnMediaStalledEvent { get { return _onMediaStalledEventAttribute; }}


        /// <summary>
        /// Script to be run when fetching the media data is stopped before it is completely loaded for whatever reason
        /// </summary>
        public OnMediaSuspendEventAttribute OnMediaSuspendEvent { get { return _onMediaSuspendEventAttribute; }}


        /// <summary>
        /// Script to be run when the playing position has changed (like when the user fast forwards to a different point in the media)
        /// </summary>
        public OnMediaTimeUpdateEventAttribute OnMediaTimeUpdateEvent { get { return _onMediaTimeUpdateEventAttribute; }}


        /// <summary>
        /// Script to be run each time the volume is changed which (includes setting the volume to "mute")
        /// </summary>
        public OnMediaVolumeChangeEventAttribute OnMediaVolumeChangeEvent { get { return _onMediaVolumeChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when the media has paused but is expected to resume (like when the media pauses to buffer more data)
        /// </summary>
        public OnMediaWaitingEventAttribute OnMediaWaitingEvent { get { return _onMediaWaitingEventAttribute; }}


        /// <summary>
        /// 
        /// </summary>
        public OnMediaCueChangeEventAttribute OnMediaCueChangeEvent { get { return _onMediaCueChangeEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnMediaCancelEventAttribute OnMediaCancelEvent { get { return _onMediaCancelEventAttribute; }}


        /// <summary>
        /// Add all attributes set to specified xElement
        /// </summary>
        /// <param name="attributesList"></param>
        public void AddAttributes(List<IBaseAttribute> attributesList)
        {
            attributesList.Add(_onAbortEventAttribute);
            attributesList.Add(_onCanPlayEventAttribute);
            attributesList.Add(_onCanPlayThroughEventAttribute);
            attributesList.Add(_onDurationChangeEventAttribute);
            attributesList.Add(_onEmptiedEventAttribute);
            attributesList.Add(_onEndedEventAttribute);
            attributesList.Add(_onMediaErrorEventAttribute);
            attributesList.Add(_onMediaLoadedDataEventAttribute);
            attributesList.Add(_onMediaLoadedMetadateEventAttribute);
            attributesList.Add(_onMediaLoadStartEventAttribute);
            attributesList.Add(_onMediaPauseEventAttribute);
            attributesList.Add(_onMediaPlayEventAtrribute);
            attributesList.Add(_onMediaPlayingEventAttribute);
            attributesList.Add(_onMediaProgressEventAttribute);
            attributesList.Add(_onMediaRateChangeEventAttribute);
            attributesList.Add(_onMediaReadyStateChangeEventAttribute);
            attributesList.Add(_onMediaSeekedEventAttribute);
            attributesList.Add(_onMediaSeekingEventAttribute);
            attributesList.Add(_onMediaStalledEventAttribute);
            attributesList.Add(_onMediaSuspendEventAttribute);
            attributesList.Add(_onMediaTimeUpdateEventAttribute);
            attributesList.Add(_onMediaVolumeChangeEventAttribute);
            attributesList.Add(_onMediaWaitingEventAttribute);
            attributesList.Add(_onMediaCueChangeEventAttribute);
            attributesList.Add(_onMediaCancelEventAttribute);
        }
        
    }
}
