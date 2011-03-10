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
        public TableItem Item { get; set; }

        public IXHTMLItem Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Table table = new Table();

            foreach (var row in Item.Rows)
            {
                RowConverter rowConverter = new RowConverter
                                                {
                                                    Item = row,
                                                    Settings = Settings
                                                };
                table.Add(rowConverter.Convert());
            }

            table.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, table);

            return table;
        }

    }
}
