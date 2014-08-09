using System;

using HTML5ClassLibrary.BaseElements;

namespace HTML5ClassLibrary.Exceptions
{
    public class HTML5ViolationException : Exception
    {
        public HTML5ViolationException()
            : base("Adding this element violates XHTML structure rules")
        {
        }
        public HTML5ViolationException(string addMessage)
            : base(string.Format("Adding this element violates HTML5 structure rules: {0}",addMessage))
        {
            
        }

        public HTML5ViolationException(IHTML5Item item, string addMessage)
            : base(string.Format("Adding this element <{0}> violates HTML5 structure rules: {1}", item.GetType(), addMessage))
        {

        }

        public override string Message
        {
            get
            {
                return string.Format("{0} - {1}",base.StackTrace,base.Message);

            }
        }
    }
}
