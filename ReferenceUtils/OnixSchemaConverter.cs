using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EPubLibrary.ReferenceUtils
{
    /// <summary>
    /// http://www.stison.com/onix/codelists/onix-codelist-5.htm
    /// </summary>
    public enum OnixType
    {
        Proprietary =   1,
        ISBN_10     =   2,
        GTIN_13     =   3,
        UPC         =   4,
        ISMN_10     =   5,
        DOI         =   6,
        LCCN        =   13,
        GTIN_14     =   14,
        ISBN_13     =   15,
        Legal_deposit_number    =   17,
        URN                     =   22,
        OCLC_number             =   23,
        Co_publishers_ISBN_13   =   24,
        ISMN_13                 =   25,
        ISBN_A                  =   26,
        JP_e_code               =   27,
        OLCC_number             =   28
    }

    
    class Onix5SchemaConverter
    {
        private OnixType _type = OnixType.Proprietary;
        private string _parsedId = string.Empty;

        public static string GetScheme()
        {
            return "onix:codelist5";
        }

        public string GetIdentifier()
        {
            return _parsedId;
        }

        public string GetIdentifierType()
        {
            return ((int) _type).ToString();
        }
        public Onix5SchemaConverter(Identifier identifier)
        {
            // if isbn
            bool bISBN = false;
            if (string.Compare(identifier.Scheme, "isbn", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
            {
                string stripped = identifier.ID.Replace("-", string.Empty);
                if (stripped.Length == 13)
                {
                    _type = OnixType.ISBN_13;
                    bISBN = true;
                }
                else if (stripped.Length == 10)
                {
                    _type = OnixType.ISBN_10;
                    bISBN = true;
                }
            }
            else if (string.Compare(identifier.Scheme, "uri", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
            {
                _type = OnixType.URN;
                _parsedId = string.Format("{0}", identifier.ID);
            }
            if (bISBN)
            {
                _parsedId = string.Format("urn:isbn:{0}", identifier.ID.Replace("-", string.Empty));
            }

            if (_type == OnixType.Proprietary)
            {
                _parsedId = identifier.ID;
            }
        }

        public bool IsISBN()
        {
            return ((_type == OnixType.ISBN_10) || (_type == OnixType.ISBN_13));
        }
    }
}
