using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConverterContracts.Settings;
using FB2Library.Elements;
using FB2Library.HeaderItems;

namespace FB2EPubConverter
{
    internal static class DescriptionConverters
    {
        public static string GetAuthorAsSting(AuthorType author)
        {
            var sb = new StringBuilder();
            if ((author.FirstName != null) && !string.IsNullOrEmpty(author.FirstName.Text))
            {
                sb.AppendFormat("{0} ", author.FirstName.Text);
            }

            if ((author.MiddleName != null) && !string.IsNullOrEmpty(author.MiddleName.Text))
            {
                sb.AppendFormat("{0} ", author.MiddleName.Text);
            }

            if ((author.LastName != null) && !string.IsNullOrEmpty(author.LastName.Text))
            {
                sb.AppendFormat("{0} ", author.LastName.Text);
            }

            if ((author.NickName != null) && !string.IsNullOrEmpty(author.NickName.Text))
            {
                sb.AppendFormat(sb.Length == 0 ? "{0} " : "({0}) ", author.NickName.Text);
            }

            if ((author.UID != null) && !string.IsNullOrEmpty(author.UID.Text))
            {
                sb.AppendFormat(": {0}", author.UID.Text);
            }
            return sb.ToString();
        }


        public static List<string> GetSequencesAsStrings(SequenceType seq)
        {
            var allSequences = new List<string>();
            if (seq != null)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    string sequence = seq.Name;
                    if (seq.Number.HasValue && seq.Number != 0)
                    {
                        sequence = string.Format("{0} - {1}", seq.Name, seq.Number);
                    }
                    allSequences.Add(sequence);
                }
                if (seq.SubSections != null)
                {
                    allSequences.AddRange(seq.SubSections.SelectMany(GetSequencesAsStrings));
                }
            }
            return allSequences;
        }

        public  static string GenerateFileAsString(AuthorType author,IEPubConversionSettings commonSettings)
        {
            var processor = new ProcessAuthorFormat { Format =commonSettings.FileAsFormat };
            return processor.GenerateAuthorString(author);

        }

        public static string GenerateAuthorString(AuthorType author,IEPubConversionSettings commonSettings)
        {
            var processor = new ProcessAuthorFormat { Format = commonSettings.AuthorFormat };
            return processor.GenerateAuthorString(author);
        }


        public static string Fb2GenreToDescription(string genre)
        {
            // TODO: implement real description conversion
            return genre;
        }

        public static string FormatBookTitle(ItemTitleInfo titleInfo, IEPubConversionSettings commonSettings)
        {
            var formatTitle = new ProcessSeqFormatString
            {
                BookTitleFormatSeqNum = commonSettings.SequenceFormat,
                BookTitleFormatNoSeqNum = commonSettings.NoSequenceFormat,
                BookTitleFormatNoSeries = commonSettings.NoSeriesFormat
            };

            String rc;
            if ((titleInfo.Sequences.Count > 0) && commonSettings.AddSeqToTitle)
            {
                rc = formatTitle.GenerateBookTitle(titleInfo.BookTitle.Text, titleInfo.Sequences[0].Name,
                                                   titleInfo.Sequences[0].Number);
            }
            else
            {
                rc = formatTitle.GenerateBookTitle(titleInfo.BookTitle.Text, "", 0);
            }
            return rc;
        }

    }
}
