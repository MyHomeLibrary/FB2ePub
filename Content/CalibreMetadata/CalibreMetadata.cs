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
        private static readonly XName MetaName = EPubNamespaces.OpfNameSpace + "meta";

        /// <summary>
        /// Get/Set Series name (only one 1st Series used)
        /// </summary>
        public string SeriesName { get; set; }

        /// <summary>
        /// Get/Set Series index if > 0
        /// </summary>
        public int SeriesIndex { get; set; }

        /// <summary>
        /// Get/Set Title name to be used for sorting by Calibre
        /// </summary>
        public string TitleForSort { get; set; }


        /// <summary>
        /// Injects Calibre's namespace into XHTML element
        /// </summary>
        /// <param name="metadata"></param>
        internal void InjectNamespace(XElement metadata)
        {
            metadata.Add(new XAttribute(XNamespace.Xmlns + "calibre", CalibreNamespaces.CalibreNamespace));
        }

        /// <summary>
        /// Injects Calibre's metadata into XHTML element (for metadata part)
        /// </summary>
        /// <param name="metadata"></param>
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
