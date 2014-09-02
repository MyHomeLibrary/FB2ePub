using System;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.TableElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class TableConverterParams
    {
        public ConverterOptions Settings { get; set; }  
    }

    internal class TableConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 Table object into XHTML reperesentation
        /// </summary>
        /// <param name="tableItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="tableConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TableItem tableItem,HTMLElementType compatibility,TableConverterParams tableConverterParams)
        {
            if (tableItem == null)
            {
                throw new ArgumentNullException("tableItem");
            }
            var table = new Table(compatibility);

            foreach (var row in tableItem.Rows)
            {
                var rowConverter = new RowConverter();
                table.Add(rowConverter.Convert(row, compatibility, new RowConverterParams { Settings = tableConverterParams.Settings}));
            }

            SetClassType(table,string.Empty);

            table.GlobalAttributes.ID.Value = tableConverterParams.Settings.ReferencesManager.AddIdUsed(tableItem.ID, table);

            return table;
        }
    }
}
