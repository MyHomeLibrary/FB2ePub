using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary.Content.Guide
{
    public enum GuideTypeEnum
    {
        Unknown,
        Cover,
        TitlePage,
        TOC,
        Index,
        Glossary,
        Acknowledgements,
        Bibliography,
        Colophon,
        CopyrightPage,
        Dedication,
        Epigraph,
        Foreword,
        ListOfIllustrations,
        ListOfTables,
        Notes,
        Preface,
        Text,
        Ignore,
    }

    internal class GuideElement
    {
        /// <summary>
        /// Guide's element title name
        /// </summary>
        public string Title {get;set;}

        /// <summary>
        /// "link" (address) to XHTML item described
        /// </summary>
        public string Link { get; set; }

        public GuideTypeEnum Type { get; set; }

        public string GetTypeAsString()
        {
            switch (Type)
            {
                case GuideTypeEnum.Acknowledgements:
                    return "acknowledgements";
                case GuideTypeEnum.Bibliography:
                    return "bibliography";
                case GuideTypeEnum.Colophon:
                    return "colophon";
                case GuideTypeEnum.CopyrightPage:
                    return "copyright-page";
                case GuideTypeEnum.Cover:
                    return "cover";
                case GuideTypeEnum.Dedication:
                    return "dedication";
                case GuideTypeEnum.Epigraph:
                    return "epigraph";
                case GuideTypeEnum.Foreword:
                    return "foreword";
                case GuideTypeEnum.Glossary:
                    return "glossary";
                case GuideTypeEnum.Index:
                    return "index";
                case GuideTypeEnum.ListOfIllustrations:
                    return "loi";
                case GuideTypeEnum.ListOfTables:
                    return "lot";
                case GuideTypeEnum.Notes:
                    return "notes";
                case GuideTypeEnum.Preface:
                    return "preface";
                case GuideTypeEnum.Text:
                    return "text";
                case GuideTypeEnum.TitlePage:
                    return "title-page";
                case GuideTypeEnum.TOC:
                    return "toc";                    
            }
            return string.Empty;
        }

        /// <summary>
        /// Checks if Guid element valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (Title == null)
            {
                return false;
            }
            if (Link == null)
            {
                return false;
            }
            if ( Type == GuideTypeEnum.Unknown)
            {
                return false;
            }
            return true;
        }
    }
}
