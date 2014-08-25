using System.Collections.Generic;
using System.Text;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// One or more white space separated NameToken values.
    /// </summary>
    public class NameTokens : IAttributeDataType
    {
        private readonly List<NameToken> _tokens = new List<NameToken>();

        public string Value
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var token in _tokens)
                {
                    builder.AppendFormat("{0} ", token.Value);
                }
                return builder.ToString().TrimEnd();
            }

            set
            {
                _tokens.Clear();
                string[] ar = value.Split(' ');
                foreach (var s in ar)
                {
                    var token = new NameToken {Value = s};
                    _tokens.Add(token);
                }
            }
            
        }
    }
}
