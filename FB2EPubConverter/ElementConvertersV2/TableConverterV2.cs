using System;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.TableElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class TableConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class TableConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 Table object into XHTML representation
        /// </summary>
        /// <param name="tableItem">item to convert</param>
        /// <param name="tableConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TableItem tableItem,TableConverterParamsV2 tableConverterParams)
        {
            if (tableItem == null)
            {
                throw new ArgumentNullException("tableItem");
            }
            var table = new Table(HTMLElementType.XHTML11);

            foreach (var row in tableItem.Rows)
            {
                var rowConverter = new RowConverterV2();
                table.Add(rowConverter.Convert(row,  new RowConverterParamsV2 { Settings = tableConverterParams.Settings}));
            }

            SetClassType(table,string.Empty);

            table.GlobalAttributes.ID.Value = tableConverterParams.Settings.ReferencesManager.AddIdUsed(tableItem.ID, table);

            return table;
        }
    }
}
