using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.CalibreMetadata
{
    /// <summary>
    /// Represent metadata set by Calibre, used by some readers and librusec
    /// </summary>
    public class CalibreMetadataObject
    {
        private static readonly XNamespace CalibreNamespace = @"http://calibre.kovidgoyal.net/2009/metadata";
        private static readonly XNamespace _opfNameSpace = @"http://www.idpf.org/2007/opf";
        private static readonly XName MetaName = _opfNameSpace + "meta";


        public string SeriesName { get; set; }

        public int SeriesIndex { get; set; }

        public string TitleForSort { get; set; }


        internal void InjectNamespace(XElement metadata)
        {
            metadata.Add(new XAttribute(XNamespace.Xmlns + "calibre", CalibreNamespace));
        }

        internal void InjectData(XElement metadata)
        {
            if (!string.IsNullOrEmpty(SeriesName))
            {
                XElement serie = new XElement(MetaName);
                serie.Add(new XAttribute("name", "calibre:series"));
                serie.Add(new XAttribute("content", SeriesName));
                metadata.Add(serie);           
            }

            if (SeriesIndex != 0)
            {
                XElement serieIndex = new XElement(MetaName);
                serieIndex.Add(new XAttribute("name", "calibre:series_index"));
                serieIndex.Add(new XAttribute("content", SeriesIndex));
                metadata.Add(serieIndex);
            }

            if (!string.IsNullOrEmpty(TitleForSort))
            {
                XElement title4Sort = new XElement(MetaName);
                title4Sort.Add(new XAttribute("name", "calibre:title_sort"));
                title4Sort.Add(new XAttribute("content", TitleForSort));
                metadata.Add(title4Sort);
            }

            XElement date = new XElement(MetaName);
            date.Add(new XAttribute("name", "calibre:timestamp"));
            date.Add(new XAttribute("content", DateTime.UtcNow.ToString("O")));
            metadata.Add(date);
        }
    }
}
