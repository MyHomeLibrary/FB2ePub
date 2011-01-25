using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml;

namespace XMLFixerLibrary
{
    /// <summary>
    /// Remove all invalid characters from the XML stream replacing them with spaces
    /// </summary>
    public class XmlRepair
    {
        private readonly List<InvalidSequence> invalidChars = new List<InvalidSequence>();

        public XmlRepair()
        {
            invalidChars.Add(new InvalidSequence { StartChar = 0x1, EndChar = 0x8 });
            invalidChars.Add(new InvalidSequence { StartChar = 0xB, EndChar = 0xC });
            invalidChars.Add(new InvalidSequence { StartChar = 0xE, EndChar = 0x1F });
            invalidChars.Add(new InvalidSequence { StartChar = 0x7F, EndChar = 0x84 });
            invalidChars.Add(new InvalidSequence { StartChar = 0x86, EndChar = 0x9F });
            invalidChars.Add(new InvalidSequence { StartChar = 0xFDD0, EndChar = 0xFDDF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x1FFFE, EndChar = 0x1FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x2FFFE, EndChar = 0x2FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x3FFFE, EndChar = 0x3FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x4FFFE, EndChar = 0x4FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x5FFFE, EndChar = 0x5FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x6FFFE, EndChar = 0x6FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x7FFFE, EndChar = 0x7FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x8FFFE, EndChar = 0x8FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x9FFFE, EndChar = 0x9FFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0xAFFFE, EndChar = 0xAFFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0xBFFFE, EndChar = 0xBFFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0xCFFFE, EndChar = 0xCFFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0xDFFFE, EndChar = 0xDFFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0xEFFFE, EndChar = 0xEFFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0xFFFFE, EndChar = 0xFFFFF });
            invalidChars.Add(new InvalidSequence { StartChar = 0x10FFFE, EndChar = 0x10FFFF });
        }

        public void Repair(Stream inputStream, Stream outputStream)
        {
            string line;
            StreamWriter writer = null;
            byte[] buffer = new byte[5];
            inputStream.Read(buffer, 0, 5);
            Encoding encoding = DetectEncoding(buffer);
            StreamReader sr = new StreamReader(inputStream, encoding);
            while((line = sr.ReadLine()) != null)
            {
                if (writer == null)
                {
                    line = encoding.GetString(buffer) + line;
                    writer = new StreamWriter(outputStream, encoding);
                }
                string newLine = CheckAndFixLine(line);
                writer.WriteLine(newLine);    
            }
            writer.Flush();
        }

        private static Encoding DetectEncoding(byte[] buffer)
        {
            // Basically all we need to detect is a UTF encodings
            // all others do not matter as long it's not UTF
            Encoding enc = Encoding.Default;
           if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)

                enc = Encoding.UTF8;

            else if (buffer[0] == 0xfe && buffer[1] == 0xff)

                enc = Encoding.Unicode;

            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)

                enc = Encoding.UTF32;

            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)

                enc = Encoding.UTF7;
            return enc;
        }

        private string CheckAndFixLine(string line)
        {
            char[] chars = line.ToCharArray();
            for (int i = 0; i < chars.Length; i++ )
            {
                if (IsInvalidChar(chars[i]))
                {
                    chars[i] = ' ';
                }
            }
            string resString = new string(chars);
            int ampersendPosition = resString.IndexOf('&');
            while(ampersendPosition >= 0)
            {
                if ((ampersendPosition+1 < resString.Length) && (resString[ampersendPosition+1] == ' '))
                {
                    resString = resString.Remove(ampersendPosition, 1).Insert(ampersendPosition, "&amp;");
                }
                else 
                {
                    int spacePos = line.IndexOf(' ', ampersendPosition + 1);
                    if (spacePos >= 0)
                    {
                        string subText = line.Substring(ampersendPosition + 1, spacePos - ampersendPosition-1);
                        if (!IsValidAmersendSeq(subText))
                        {
                            resString = resString.Remove(ampersendPosition, 1).Insert(ampersendPosition, "&amp;");
                        }
                    }
                }
                ampersendPosition = resString.IndexOf('&',ampersendPosition+1);
            }

            int leftPosition = resString.IndexOf('<');
            while (leftPosition >= 0)
            {
                if ((leftPosition > 1) && (leftPosition + 1 < resString.Length) && (resString[leftPosition+1] == ' ') && (resString[leftPosition-1] == ' '))
                {
                    resString = resString.Remove(leftPosition, 1).Insert(leftPosition, "&lt;");
                }
                leftPosition = resString.IndexOf('<', leftPosition + 1);
            }

            int rightPosition = resString.IndexOf('>');
            while (rightPosition >= 0)
            {
                if ((rightPosition > 1) && (rightPosition + 1 < resString.Length) && (resString[rightPosition + 1] == ' ') && (resString[rightPosition - 1] == ' '))
                {
                    resString = resString.Remove(rightPosition, 1).Insert(rightPosition, "&gt;");
                }
                rightPosition = resString.IndexOf('>', rightPosition + 1);
            }

            return resString;
        }

        private bool IsValidAmersendSeq(string text)
        {
            if (text[text.Length-1] != ';')
            {
                return false;
            }
            switch (text)
            {
                case "quot;":
                case "amp;":
                case "apos;":
                case "lt;":
                case "gt;":
                case "nbsp;":
                case "iexcl;":
                case "cent;":
                case "pound;":
                case "curren;":
                case "yen;":
                case "brvbar;":
                case "sect;":
                case "uml;":
                case "copy;":
                case "ordf;":
                case "laquo;":
                case "not;":
                case "shy;":
                case "reg;":
                case "macr;":
                case "deg;":
                case "plusmn;":
                case "sup2;":
                case "sup3;":
                case "acute;":
                case "micro;":
                case "para;":
                case "middot;":
                case "cedil;":
                case "sup1;":
                case "ordm;":
                case "raquo;":
                case "frac14;":
                case "frac12;":
                case "frac34;":
                case "iquest;":
                case "Agrave;":
                case "Aacute;":
                case "Acirc;":
                case "Atilde;":
                case "Auml;":
                case "Aring;":
                case "AElig;":
                case "Ccedil;":
                case "Egrave;":
                case "Eacute;":
                case "Ecirc;":
                case "Euml;":
                case "Igrave;":
                case "Iacute;":
                case "Icirc;":
                case "Iuml;":
                case "ETH;":
                case "Ntilde;":
                case "Ograve;":
                case "Oacute;":
                case "Ocirc;":
                case "Otilde;":
                case "Ouml;":
                case "times;":
                case "Oslash;":
                case "Ugrave;":
                case "Uacute;":
                case "Ucirc;":
                case "Uuml;":
                case "Yacute;":
                case "THORN;":
                case "szlig;":
                case "agrave;":
                case "aacute;":
                case "acirc;":
                case "atilde;":
                case "auml;":
                case "aring;":
                case "aelig;":
                case "ccedil;":
                case "egrave;":
                case "eacute;":
                case "ecirc;":
                case "euml;":
                case "igrave;":
                case "iacute;":
                case "icirc;":
                case "iuml;":
                case "eth;":
                case "ntilde;":
                case "ograve;":
                case "oacute;":
                case "ocirc;":
                case "otilde;":
                case "ouml;":
                case "divide;":
                case "oslash;":
                case "ugrave;":
                case "uacute;":
                case "ucirc;":
                case "uuml;":
                case "yacute;":
                case "thorn;":
                case "yuml;":
                case "OElig;":
                case "oelig;":
                case "Scaron;":
                case "scaron;":
                case "Yuml;":
                case "fnof;":
                case "circ;":
                case "tilde;":
                case "Alpha;":
                case "Beta;":
                case "Gamma;":
                case "Delta;":
                case "Epsilon;":
                case "Zeta;":
                case "Eta;":
                case "Theta;":
                case "Iota;":
                case "Kappa;":
                case "Lambda;":
                case "Mu;":
                case "Nu;":
                case "Xi;":
                case "Omicron;":
                case "Pi;":
                case "Rho;":
                case "Sigma;":
                case "Tau;":
                case "Upsilon;":
                case "Phi;":
                case "Chi;":
                case "Psi;":
                case "Omega;":
                case "alpha;":
                case "beta;":
                case "gamma;":
                case "delta;":
                case "epsilon;":
                case "zeta;":
                case "eta;":
                case "theta;":
                case "iota;":
                case "kappa;":
                case "lambda;":
                case "mu;":
                case "nu;":
                case "xi;":
                case "omicron;":
                case "pi;":
                case "rho;":
                case "sigmaf;":
                case "sigma;":
                case "tau;":
                case "upsilon;":
                case "phi;":
                case "chi;":
                case "psi;":
                case "omega;":
                case "thetasym;":
                case "upsih;":
                case "piv;":
                case "ensp;":
                case "emsp;":
                case "thinsp;":
                case "zwnj;":
                case "zwj;":
                case "lrm;":
                case "rlm;":
                case "ndash;":
                case "mdash;":
                case "lsquo;":
                case "rsquo;":
                case "sbquo;":
                case "ldquo;":
                case "rdquo;":
                case "bdquo;":
                case "dagger;":
                case "Dagger;":
                case "bull;":
                case "hellip;":
                case "permil;":
                case "prime;":
                case "Prime;":
                case "lsaquo;":
                case "rsaquo;":
                case "oline;":
                case "frasl;":
                case "euro;":
                case "image;":
                case "weierp;":
                case "real;":
                case "trade;":
                case "alefsym;":
                case "larr;":
                case "uarr;":
                case "rarr;":
                case "darr;":
                case "harr;":
                case "crarr;":
                case "lArr;":
                case "uArr;":
                case "rArr;":
                case "dArr;":
                case "hArr;":
                case "forall;":
                case "part;":
                case "exist;":
                case "empty;":
                case "nabla;":
                case "isin;":
                case "notin;":
                case "ni;":
                case "prod;":
                case "sum;":
                case "minus;":
                case "lowast;":
                case "radic;":
                case "prop;":
                case "infin;":
                case "ang;":
                case "and;":
                case "or;":
                case "cap;":
                case "cup;":
                case "int;":
                case "there4;":
                case "sim;":
                case "cong;":
                case "asymp;":
                case "ne;":
                case "equiv;":
                case "le;":
                case "ge;":
                case "sub;":
                case "sup;":
                case "nsub;":
                case "sube;":
                case "supe;":
                case "oplus;":
                case "otimes;":
                case "perp;":
                case "sdot;":
                case "lceil;":
                case "rceil;":
                case "lfloor;":
                case "rfloor;":
                case "lang;":
                case "rang;":
                case "loz;":
                case "spades;":
                case "clubs;":
                case "hearts;":
                case "diams;":
                    return true;
                default:
                    if (text[0] == '#')
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private bool IsInvalidChar(char c)
        {
            foreach (var invalidSequence in invalidChars)
            {
                int code =  c;
                if ( (code >= invalidSequence.StartChar) && (code <= invalidSequence.EndChar))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
