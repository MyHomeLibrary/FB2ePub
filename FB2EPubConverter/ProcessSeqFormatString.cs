using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FB2EPubConverter
{
    internal class ProcessSeqFormatString
    {

        /// <summary>
        /// BookTitle format when sequence number provided 
        /// </summary>
        public string BookTitleFormatSeqNum { get; set; }

        /// <summary>
        /// BookTitle format when sequence number not provided 
        /// </summary>
        public string BookTitleFormatNoSeqNum { get; set; }

        /// <summary>
        /// BookTitle format when series name not provided
        /// </summary>
        public string BookTitleFormatNoSeries { get; set; }



        private string Format { get; set; }

        
        /// <summary>
        /// Converts booktitle
        /// remember that after single use format need to be "reset" since format string changes when running
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <param name="seqName"></param>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public string GenerateBookTitle(string bookTitle, string seqName, int? seqNo)
        {
            Format = BookTitleFormatSeqNum;

            // if no sequence number use "no sequence" format
            if (!seqNo.HasValue || seqNo.Value == 0)
            {
                Format = BookTitleFormatNoSeqNum;
            }

            // if no series name use "no series" format
            if (string.IsNullOrEmpty(seqName))
            {
                Format = BookTitleFormatNoSeries;
            }

            if (string.IsNullOrEmpty(Format)) // in case format set to empty just return title
            {
                return bookTitle;
            }
            string newBookTitle = ParseTemplate("bt", bookTitle),
              newSeqName = ParseTemplate("sf", seqName),
              newSeqAbbr = Abbreviate(ParseTemplate("sa", seqName)),
              newSeqNo = ParseTemplate("sn", seqNo);

            string rc = Format.Replace("$bt$", newBookTitle);
            rc = rc.Replace("$sf$", newSeqName);
            rc = rc.Replace("$sa$", newSeqAbbr);
            rc = rc.Replace("$sn$", newSeqNo);
            return rc;
        }

        public static string Abbreviate(string name)
        {
            string[] words = name.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach (var word in words)
                if (!string.IsNullOrEmpty(word))
                    sb.Append(word[0]);

            return sb.ToString().TrimEnd(' ');
        }

        private string ParseTemplate(string expr, String source)
        {
            Regex rx = new Regex(@"%" + expr + @"(\.(?<mode>[clu]))?%", RegexOptions.IgnoreCase);

            string rc = "";
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
            }
            Format = Regex.Replace(Format, @"%" + expr + @"(\.(?<mode>[clu]))?%", "$" + expr + "$");
            return rc;
        }


        private string ParseTemplate(string expr, int? source)
        {
            Regex rx = new Regex(@"%" + expr + @"(\.(?<mode>\d*))?%", RegexOptions.IgnoreCase);

            string rc = source.ToString();
            if (rx.IsMatch(Format))
            {
                Match mc = rx.Match(Format);
                string mode = mc.Groups["mode"].ToString();

                if (!string.IsNullOrEmpty(mode))
                    rc = string.Format("{0:D" + mode + "}", source);
            }
            Format = Regex.Replace(Format, @"%" + expr + @"(\.(?<mode>\d*))?%", "$" + expr + "$");

            return rc;
        }

    }
}
