using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class TableConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 Table object into XHTML reperesentation
        /// </summary>
        /// <param name="tableItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IXHTMLItem Convert(TableItem tableItem)
        {
            if (tableItem == null)
            {
                throw new ArgumentNullException("tableItem");
            }
            Table table = new Table();

            foreach (var row in tableItem.Rows)
            {
                RowConverter rowConverter = new RowConverter{Settings = Settings};
                table.Add(rowConverter.Convert(row));
            }

            table.ID.Value = Settings.ReferencesManager.AddIdUsed(tableItem.ID, table);

            return table;
        }

    }
}
