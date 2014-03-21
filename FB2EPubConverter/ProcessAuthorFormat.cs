using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FB2Library.HeaderItems;

namespace FB2EPubConverter
{
    internal class ProcessAuthorFormat
    {
        public string Format { get; set; }

        public string GenerateAuthorString(AuthorType author)
        {
            if (string.IsNullOrEmpty(Format)) // in case format set to empty just return title
            {
                return ""; // need to change to default ?
            }
            string newFirstName = ParseTemplate("f", (author.FirstName==null)? string.Empty :author.FirstName.Text);
            string newMiddleName = ParseTemplate("m", (author.MiddleName == null) ? string.Empty : author.MiddleName.Text);
            string newLastName = ParseTemplate("l", (author.LastName == null) ? string.Empty : author.LastName.Text);
            string newNick = ParseTemplate("n", (author.NickName == null) ? string.Empty : author.NickName.Text);

            String rc = Format.Replace("$f$", newFirstName.Trim());
            rc = rc.Replace("$m$", newMiddleName.Trim());
            rc = rc.Replace("$l$", newLastName.Trim());
            rc = rc.Replace("$n$", newNick.Trim());
            return rc.Trim();

        }


        private string ParseTemplate(String expr, String source)
        {
            Regex rx = new Regex(@"%" + expr + @"(\.(?<mode>[clu]))?(\:(?<sub>[b]))?%", RegexOptions.IgnoreCase);

            string rc = "";
            bool brackets = false;
            if (rx.IsMatch(Format))
            {
                Match mc = rx.Match(Format);

                string mode = mc.Groups["mode"].ToString();
                switch (mode)
                {
                    case "c":
                        rc = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source);
                        break;
                    case "l":
                        rc = source.ToLower();
                        break;
                    case "u":
                        rc = source.ToUpper();
                        break;
                    default:
                        rc = source;
                        break;
                }
                string sub = mc.Groups["sub"].ToString();
                if (String.Compare(sub, "p", StringComparison.InvariantCulture) == 0)
                {
                    brackets = true;
                }
            }
            if (brackets && !string.IsNullOrEmpty(source))
            {
                Format = Regex.Replace(Format, @"%" + expr + @"(\.(?<mode>[clu]))?(\:(?<sub>[b]))?%", "($" + expr + "$) ");
            }
            else
            {
                Format = Regex.Replace(Format, @"%" + expr + @"(\.(?<mode>[clu]))?(\:(?<sub>[b]))?%", "$" + expr + "$");    
            }
            
            return rc;
        }

    }
}
