using System;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.TableElements;

namespace FB2EPubConverter.ElementConvertersV3.Tables
{
    internal class TableConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }

    internal class TableConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 Table object into XHTML representation
        /// </summary>
        /// <param name="tableItem">item to convert</param>
        /// <param name="tableConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TableItem tableItem, TableConverterParamsV3 tableConverterParams)
        {
            if (tableItem == null)
            {
                throw new ArgumentNullException("tableItem");
            }
            var table = new Table(HTMLElementType.HTML5);

            foreach (var row in tableItem.Rows)
            {
                var rowConverter = new RowConverterV3();
                table.Add(rowConverter.Convert(row, new RowConverterParamsV3 { Settings = tableConverterParams.Settings }));
            }

            SetClassType(table, string.Empty);

            table.GlobalAttributes.ID.Value = tableConverterParams.Settings.ReferencesManager.AddIdUsed(tableItem.ID, table);

            return table;
        }

    }
}
