using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB2EPubConverter
{
    public static class UnicodeHelpers
    {
        /// <summary>
        /// Check if unicode character looks anything like space
        /// </summary>
        /// <param name="ch">character to check</param>
        /// <returns>true if it's a space like character</returns>
        public static bool IsSpaceLike(char ch)
        {
            switch (ch)
            {
                case '\u0020':
                case '\u00A0':
                case '\u1680':
                case '\u180E':
                case '\u2000':
                case '\u2001':
                case '\u2002':
                case '\u2003':
                case '\u2004':
                case '\u2005':
                case '\u2006':
                case '\u2007':
                case '\u2008':
                case '\u2009':
                case '\u200A':
                case '\u200B':
                case '\u200C':
                case '\u200D':
                case '\u202F':
                case '\u205F':
                case '\u2060':
                case '\u2800':
                case '\u3000':
                case '\uFEFF':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if speciffic unicode character looks anything like dash or hyphen
        /// </summary>
        /// <param name="s">character to check</param>
        /// <returns>true if it's dash/hyphen like character</returns>
        public static bool IsDashLike(char s)
        {
            switch (s)
            {
                case '\u002D':
                case '\u005F':
                case '\u007E':
                case '\u00AD':
                case '\u00AF':
                case '\u058A':
                case '\u05BE':
                case '\u1173':
                case '\u1400':
                case '\u1806':
                case '\u2010':
                case '\u2011':
                case '\u2012':
                case '\u2013':
                case '\u2014':
                case '\u2015':
                case '\u203C':
                case '\u2043':
                case '\u2053':
                case '\u207B':
                case '\u208B':
                case '\u2212':
                case '\u301C':
                case '\u3030':
                case '\u30FC':
                case '\u3161':
                case '\u4E00':
                case '\uFF5E':
                    return true;
            }

            return false;
        }

        public static bool IsNeedToBeJoinInDrop(char s)
        {
            if (IsDashLike(s))
            {
                return true;
            }
            switch (s)
            {
                case '\u00AB': //«
                    return true;
            }
            return false;
        }
    }
}
