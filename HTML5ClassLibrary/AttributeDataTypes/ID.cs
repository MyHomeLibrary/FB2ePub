using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A document-unique identifier. For maximum compatability, 
    /// this value should start with a letter from the English aphabet (A-Z or a-z) and 
    /// then followed by either letters, numbers, dashes, underscores or periods.
    /// </summary>
    public class Id
    {
        private string _internalIDValue = string.Empty;

        /// <summary>
        /// list of valid characters allowed in IDs
        /// </summary>
        const string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";

        public string Value 
        { 
            get
            {
                return _internalIDValue;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Regex pattern = new Regex(@"[^a-zA-Z]");
                    if ((value.Length == 0) || pattern.IsMatch(value[0].ToString()))
                    {
                        throw new HTML5ViolationException("invalid characters in ID");
                    }
                    foreach (var character in value)
                    {
                        if (!ValidChars.Contains(character))
                        {
                            throw new HTML5ViolationException("invalid characters in ID");
                        }
                    }
                }
                _internalIDValue = value;
            }
        }
    }
}
