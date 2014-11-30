using System.Text;
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

    }
}
