using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EPubLibrary.CSS_Items
{
    public class BaseCSSItem
    {
        protected readonly Dictionary<string,object> parameters = new Dictionary<string, object>();

        public string Name { get; set; }

        public Dictionary<string, object> Parameters { get { return parameters; } }

        public override string ToString()
        {
            StringBuilder result    =   new StringBuilder();

            result.AppendLine(Name);
            result.Append("{");
            foreach (var parameter in Parameters)
            {
                result.AppendFormat("\n {0}: {1};",parameter.Key,parameter.Value);
            }
            result.AppendLine("}");

            return result.ToString();
        }

        public void Write(Stream stream)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(ToString());
            stream.Write(byte1,0,byte1.Length);
        }

        public void Parse(string elementString)
        {
            if (string.IsNullOrEmpty(elementString))
            {
                throw new ArgumentException("elementString can't be empty");
            }
            int parametersStart = elementString.IndexOf('{');
            int parametersEnd = elementString.IndexOf('}');
            if ((parametersStart <= 0) || (parametersEnd == -1) || (parametersEnd <= parametersStart ))
            {
                throw new ArgumentException("elementString does not contain valid parameters structure");
            }
            Name = elementString.Substring(0, parametersStart).Trim();
            string parameters = elementString.Substring(parametersStart+1, parametersEnd - parametersStart-1);
            string[] allParameters = parameters.Split(';');
            foreach (var parameter in allParameters)
            {
                if (!string.IsNullOrEmpty(parameter))
                {
                    int valueStart = parameter.IndexOf(':');
                    if (valueStart != -1)
                    {
                        string key = parameter.Substring(0, valueStart).Trim();
                        string value = parameter.Substring(valueStart+1,parameter.Length - valueStart-1).Trim();
                        if (!Parameters.ContainsKey(key))
                        {
                            Parameters.Add(key, value);                            
                        }
                        else
                        {
                            Logger.log.ErrorFormat("CSS Stylesheet for parameter {0} contains two or more values for parameter {1}",Name,key);
                        }
                    }
                }
            }
        }
    }
}