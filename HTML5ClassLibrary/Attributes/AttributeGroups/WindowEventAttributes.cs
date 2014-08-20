using XHTMLClassLibrary.Attributes.Events;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Events triggered for the window object (applies to the "body" tag):
    /// </summary>
    public class WindowEventAttributes
    {
        [AttributeTypeAttributeMember(Name = "onafterprint", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onAfterPrintEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onbeforeprint", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onBeforePrintEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onbeforeunload", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onBeforeUnloadEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onerror", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onErrorEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onhaschange", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onHasChangeEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onload", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onLoadEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onmessage", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onMessageEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onoffline", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onOfflineEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "ononline", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onOnlineEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onpagehide", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onPageHideEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onpageshow", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onPageShowEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onpopstate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onPopStateEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onredo", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onRedoEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onresize", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onResizeEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onstorage", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onStorageEventAttribute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onundo", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onUndoEventAttrinute = new OnEventAttribute();

        [AttributeTypeAttributeMember(Name = "onunload", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onUnloadEventAttribute = new OnEventAttribute();




        /// <summary>
        /// Script to be run after the document is printed
        /// </summary>
        public OnEventAttribute OnAfterPrintEvent { get { return _onAfterPrintEventAttribute; } }
        

        /// <summary>
        /// Script to be run before the document is printed
        /// </summary>
        public OnEventAttribute OnBeforePrintEvent { get { return _onBeforePrintEventAttribute; } }


        /// <summary>
        /// Script to be run before the document is unloaded
        /// </summary>
        public OnEventAttribute OnBeforeUnloadEvent { get { return _onBeforeUnloadEventAttribute; } }


        /// <summary>
        /// Script to be run when an error occur
        /// </summary>
        public OnEventAttribute OnErrorEvent { get { return _onErrorEventAttribute; } }


        /// <summary>
        /// Script to be run when the document has changed
        /// </summary>
        public OnEventAttribute OnHasChangeEvent { get { return _onHasChangeEventAttribute; } }

        /// <summary>
        /// Fires after the page is finished loading
        /// </summary>
        public OnEventAttribute OnLoadEvent { get { return _onLoadEventAttribute; } }

        /// <summary>
        /// Script to be run when the message is triggered
        /// </summary>
        public OnEventAttribute OnMessageEvent { get { return _onMessageEventAttribute; } }

        /// <summary>
        /// Script to be run when the document goes offline
        /// </summary>
        public OnEventAttribute OnOfflineEvent { get { return _onOfflineEventAttribute; } }


        /// <summary>
        /// Script to be run when the document comes online
        /// </summary>
        public OnEventAttribute OnOnlineEvent { get { return _onOnlineEventAttribute; } }


        /// <summary>
        /// Script to be run when the window is hidden
        /// </summary>
        public OnEventAttribute OnPageHideEvent { get { return _onPageHideEventAttribute; }}


        /// <summary>
        /// Script to be run when the window becomes visible 
        /// </summary>
        public OnEventAttribute OnPageShowEvent { get { return _onPageShowEventAttribute; }}


        /// <summary>
        /// Script to be run when the window's history changes
        /// </summary>
        public OnEventAttribute OnPopStateEvent{ get { return _onPopStateEventAttribute; }}


        /// <summary>
        /// Script to be run when the document performs a redo
        /// </summary>
        public OnEventAttribute OnRedoEvent { get { return _onRedoEventAttribute; }}


        /// <summary>
        /// Fires when the browser window is resized
        /// </summary>
        public OnEventAttribute OnResizeEvent { get { return _onResizeEventAttribute; }}


        /// <summary>
        /// Script to be run when a Web Storage area is updated
        /// </summary>
        public OnEventAttribute OnStorageEvent { get { return _onStorageEventAttribute; }}


        /// <summary>
        /// Script to be run when the document performs an undo
        /// </summary>
        public OnEventAttribute OnUndoEvent { get { return _onUndoEventAttrinute; }}


        /// <summary>
        /// Fires once a page has unloaded (or the browser window has been closed)
        /// </summary>
        public OnEventAttribute OnUnloadEvent { get { return _onUnloadEventAttribute; }}

    }
}
