using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    internal static class XhtmlItemExtender
    {
        /// <summary>
        /// Extends IHTMLItem class to evaluate the size of generated output
        /// </summary>
        /// <param name="item">item to evaluate</param>
        /// <returns></returns>
        public static long EstimateSize(this IHTMLItem item)
        {
            var stream = new MemoryStream();
            var node = item.Generate();
            var doc = new XDocument();
            doc.Add(node);
            using (var writer = XmlWriter.Create(stream))
            {
                //node.WriteTo(writer);
                doc.WriteTo(writer);
            }
            return stream.Length;
        }

    }
}
