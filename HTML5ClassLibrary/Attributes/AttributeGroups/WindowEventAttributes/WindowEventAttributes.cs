using System.Collections.Generic;
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
        private readonly OnErrorEventAttribute _onErrorEventAttribute = new OnErrorEventAttribute(); // global
        private readonly OnHasChangeEventAttribute _onHasChangeEventAttribute = new OnHasChangeEventAttribute();
        private readonly OnLoadEventAttribute _onLoadEventAttribute = new OnLoadEventAttribute(); // global
        private readonly OnMessageEventAttribute _onMessageEventAttribute = new OnMessageEventAttribute();
        private readonly OnOfflineEventAttribute _onOfflineEventAttribute = new OnOfflineEventAttribute();
        private readonly OnOnlineEventAttribute _onOnlineEventAttribute = new OnOnlineEventAttribute();
        private readonly OnPageHideEventAttribute _onPageHideEventAttribute = new OnPageHideEventAttribute();
        private readonly OnPageShowEventAttribute _onPageShowEventAttribute = new OnPageShowEventAttribute();
        private readonly OnPopStateEventAttribute _onPopStateEventAttribute = new OnPopStateEventAttribute();
        private readonly OnRedoEventAttribute _onRedoEventAttribute = new OnRedoEventAttribute();
        private readonly OnResizeEventAttribute _onResizeEventAttribute = new OnResizeEventAttribute(); // global
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
        public OnResizeEventAttribute OnResizeEvent { get { return _onResizeEventAttribute; }}


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
        /// <param name="attributesList"></param>
        public void AddAttributes(List<IBaseAttribute> attributesList)
        {
             attributesList.Add(_onAfterPrintEventAttribute);
             attributesList.Add(_onBeforePrintEventAttribute);
             attributesList.Add(_onBeforeUnloadEventAttribute);
             attributesList.Add(_onErrorEventAttribute);
             attributesList.Add(_onHasChangeEventAttribute);
             attributesList.Add(_onLoadEventAttribute);
             attributesList.Add(_onMessageEventAttribute);
             attributesList.Add(_onOfflineEventAttribute);
             attributesList.Add(_onOnlineEventAttribute);
             attributesList.Add(_onPageHideEventAttribute);
             attributesList.Add(_onPageShowEventAttribute);
             attributesList.Add(_onPopStateEventAttribute);
             attributesList.Add(_onRedoEventAttribute);
             attributesList.Add(_onResizeEventAttribute);
             attributesList.Add(_onStorageEventAttribute);
             attributesList.Add(_onUndoEventAttrinute);
             attributesList.Add(_onUnloadEventAttribute);
        }
    }
}
