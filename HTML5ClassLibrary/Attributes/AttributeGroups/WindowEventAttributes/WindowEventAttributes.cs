using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Events triggered for the window object (applies to the "body" tag):
    /// </summary>
    public class WindowEventAttributes
    {
        private readonly OnAfterPrintEventAttribute _onAfterPrintEventAttribute = new OnAfterPrintEventAttribute();
        private readonly OnBeforePrintEventAttribute _onBeforePrintEventAttribute = new OnBeforePrintEventAttribute();
        private readonly OnBeforeUnloadEventAttribute _onBeforeUnloadEventAttribute = new OnBeforeUnloadEventAttribute();
        private readonly OnErrorEventAttribute _onErrorEventAttribute = new OnErrorEventAttribute();
        private readonly OnHasChangeEventAttribute _onHasChangeEventAttribute = new OnHasChangeEventAttribute();
        private readonly OnLoadEventAttribute _onLoadEventAttribute = new OnLoadEventAttribute();
        private readonly OnMessageEventAttribute _onMessageEventAttribute = new OnMessageEventAttribute();
        private readonly OnOfflineEventAttribute _onOfflineEventAttribute = new OnOfflineEventAttribute();
        private readonly OnOnlineEventAttribute _onOnlineEventAttribute = new OnOnlineEventAttribute();
        private readonly OnPageHideEventAttribute _onPageHideEventAttribute = new OnPageHideEventAttribute();
        private readonly OnPageShowEventAttribute _onPageShowEventAttribute = new OnPageShowEventAttribute();
        private readonly OnPopStateEventAttribute _onPopStateEventAttribute = new OnPopStateEventAttribute();
        private readonly OnRedoEventAttribute _onRedoEventAttribute = new OnRedoEventAttribute();
        private readonly OnResizeEventAttribute _onResizeEventAttribute = new OnResizeEventAttribute();
        private readonly OnStorageEventAttribute _onStorageEventAttribute = new OnStorageEventAttribute();
        private readonly OnUndoEventAttrinute _onUndoEventAttrinute = new OnUndoEventAttrinute();
        private readonly OnUnloadEventAttribute _onUnloadEventAttribute = new OnUnloadEventAttribute();




        /// <summary>
        /// Script to be run after the document is printed
        /// </summary>
        public OnAfterPrintEventAttribute OnAfterPrintEvent { get { return _onAfterPrintEventAttribute; } }
        

        /// <summary>
        /// Script to be run before the document is printed
        /// </summary>
        public OnBeforePrintEventAttribute OnBeforePrintEvent { get { return _onBeforePrintEventAttribute; }}


        /// <summary>
        /// Script to be run before the document is unloaded
        /// </summary>
        public OnBeforeUnloadEventAttribute OnBeforeUnloadEvent { get { return _onBeforeUnloadEventAttribute; }}


        /// <summary>
        /// Script to be run when an error occur
        /// </summary>
        public OnErrorEventAttribute OnErrorEvent { get { return _onErrorEventAttribute; }}


        /// <summary>
        /// Script to be run when the document has changed
        /// </summary>
        public OnHasChangeEventAttribute OnHasChangeEvent {get { return _onHasChangeEventAttribute; }}

        /// <summary>
        /// Fires after the page is finished loading
        /// </summary>
        public OnLoadEventAttribute OnLoadEvent { get { return _onLoadEventAttribute; }}

        /// <summary>
        /// Script to be run when the message is triggered
        /// </summary>
        public OnMessageEventAttribute OnMessageEvent { get { return _onMessageEventAttribute; }}

        /// <summary>
        /// Script to be run when the document goes offline
        /// </summary>
        public OnOfflineEventAttribute OnOfflineEvent { get { return _onOfflineEventAttribute; }}


        /// <summary>
        /// Script to be run when the document comes online
        /// </summary>
        public OnOnlineEventAttribute OnOnlineEvent { get { return _onOnlineEventAttribute; }}


        /// <summary>
        /// Script to be run when the window is hidden
        /// </summary>
        public OnPageHideEventAttribute OnPageHideEvent { get { return _onPageHideEventAttribute; }}


        /// <summary>
        /// Script to be run when the window becomes visible 
        /// </summary>
        public OnPageShowEventAttribute OnPageShowEvent { get { return _onPageShowEventAttribute; }}


        /// <summary>
        /// Script to be run when the window's history changes
        /// </summary>
        public OnPopStateEventAttribute OnPopStateEvent{ get { return _onPopStateEventAttribute; }}


        /// <summary>
        /// Script to be run when the document performs a redo
        /// </summary>
        public OnRedoEventAttribute OnRedoEvent { get { return _onRedoEventAttribute; }}


        /// <summary>
        /// Fires when the browser window is resized
        /// </summary>
        public OnResizeEventAttribute OnResetEvent { get { return _onResizeEventAttribute; }}


        /// <summary>
        /// Script to be run when a Web Storage area is updated
        /// </summary>
        public OnStorageEventAttribute OnStorageEvent { get { return _onStorageEventAttribute; }}


        /// <summary>
        /// Script to be run when the document performs an undo
        /// </summary>
        public OnUndoEventAttrinute OnUndoEvent { get { return _onUndoEventAttrinute; }}


        /// <summary>
        /// Fires once a page has unloaded (or the browser window has been closed)
        /// </summary>
        public OnUnloadEventAttribute OnUnloadEvent { get { return _onUnloadEventAttribute; }}



        /// <summary>
        /// Add all attributes set to specified xElement
        /// </summary>
        /// <param name="xElement">element to store/write attributes to</param>
        public void AddAttributes(XElement xElement)
        {
            _onAfterPrintEventAttribute.AddAttribute(xElement);
            _onBeforePrintEventAttribute.AddAttribute(xElement);
            _onBeforeUnloadEventAttribute.AddAttribute(xElement);
            _onErrorEventAttribute.AddAttribute(xElement);
            _onHasChangeEventAttribute.AddAttribute(xElement);
            _onLoadEventAttribute.AddAttribute(xElement);
            _onMessageEventAttribute.AddAttribute(xElement);
            _onOfflineEventAttribute.AddAttribute(xElement);
            _onOnlineEventAttribute.AddAttribute(xElement);
            _onPageHideEventAttribute.AddAttribute(xElement);
            _onPageShowEventAttribute.AddAttribute(xElement);
            _onPopStateEventAttribute.AddAttribute(xElement);
            _onRedoEventAttribute.AddAttribute(xElement);
            _onResizeEventAttribute.AddAttribute(xElement);
            _onStorageEventAttribute.AddAttribute(xElement);
            _onUndoEventAttrinute.AddAttribute(xElement);
            _onUnloadEventAttribute.AddAttribute(xElement);
        }

        /// <summary>
        /// Loads all attributes from provided xElement
        /// </summary>
        /// <param name="xElement">element to load attributes from</param>
        public void ReadAttributes(XElement xElement)
        {
            _onAfterPrintEventAttribute.ReadAttribute(xElement);
            _onBeforePrintEventAttribute.ReadAttribute(xElement);
            _onBeforeUnloadEventAttribute.ReadAttribute(xElement);
            _onErrorEventAttribute.ReadAttribute(xElement);
            _onHasChangeEventAttribute.ReadAttribute(xElement);
            _onLoadEventAttribute.ReadAttribute(xElement);
            _onMessageEventAttribute.ReadAttribute(xElement);
            _onOfflineEventAttribute.ReadAttribute(xElement);
            _onOnlineEventAttribute.ReadAttribute(xElement);
            _onPageHideEventAttribute.ReadAttribute(xElement);
            _onPageShowEventAttribute.ReadAttribute(xElement);
            _onPopStateEventAttribute.ReadAttribute(xElement);
            _onRedoEventAttribute.ReadAttribute(xElement);
            _onResizeEventAttribute.ReadAttribute(xElement);
            _onStorageEventAttribute.ReadAttribute(xElement);
            _onUndoEventAttrinute.ReadAttribute(xElement);
            _onUnloadEventAttribute.ReadAttribute(xElement);
        }
    }
}
