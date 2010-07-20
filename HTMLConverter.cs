using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TranslitRu
{
    internal class HtmlConverter :  BaseTrasLitConverter
    {
        public override string ConvertCharacter(char inputCharacter)
        {
            StringBuilder result = new StringBuilder();

            // first take care of special HTML characters like '&' or '<'
            string str = HttpUtility.HtmlEncode(inputCharacter.ToString());
            if (str.Length > 1)
            {
                return str;
            }

            int value = Convert.ToInt32(inputCharacter);
            if (value > 127)
            {
                result.AppendFormat("&#{0};",value);
            }
            else
            {
                result.Append(inputCharacter);
            }
            return result.ToString();
        }
    }
}
