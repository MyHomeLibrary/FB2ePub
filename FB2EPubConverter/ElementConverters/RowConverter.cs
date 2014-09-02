using System;
using Fb2ePubConverter;
using FB2Library.Elements.Table;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.TableElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class RowConverter : BaseElementConverter
    {

        public IHTMLItem Convert(TableRowItem tableRowItem)
        {
            if (tableRowItem == null)
            {
                throw new ArgumentNullException("tableRowItem");
            }
            TableRow tableRow = new TableRow();

            foreach (var element in tableRowItem.Cells)
            {
                if (element is TableHeadingItem)
                {
                    var th = element as TableHeadingItem;
                    var cell = new TableHeaderCell();
                    var paragraphConverter = new ParagraphConverter {Settings = Settings};
                    var cellData = paragraphConverter.Convert(th,ParagraphConvTargetEnum.Paragraph);
                    if (cellData.SubElements() != null)
                    {
                        foreach (var subElement in cellData.SubElements())
                        {
                            cell.Add(subElement);
                        }
                    }
                    else
                    {
                        cell.Add(cellData);
                    }
                    //cell.Add(new SimpleHTML5Text { Text = th.Text });
                    if (th.ColSpan.HasValue)
                    {
                        cell.ColSpan.Value = th.ColSpan.ToString();
                    }
                    if (th.RowSpan.HasValue)
                    {
                        cell.RowSpan.Value = th.RowSpan.ToString();
                    }
                    switch (th.Align)
                    {
                        case TableAlignmentsEnum.Center:
                            cell.Align.Value = "center";
                            break;
                        case TableAlignmentsEnum.Left:
                            cell.Align.Value = "left";
                            break;
                        case TableAlignmentsEnum.Right:
                            cell.Align.Value = "right";
                            break;
                    }
                    switch (th.VAlign)
                    {
                        case TableVAlignmentsEnum.Top:
                            cell.VAlign.Value = "top";
                            break;
                        case TableVAlignmentsEnum.Middle:
                            cell.VAlign.Value = "middle";
                            break;
                        case TableVAlignmentsEnum.Bottom:
                            cell.VAlign.Value = "bottom";
                            break;
                    }
                    tableRow.Add(cell);
                }
                else if (element is TableCellItem)
                {
                    var td = element as TableCellItem;
                    var cell = new TableData();
                    var paragraphConverter = new ParagraphConverter {Settings = Settings};
                    var cellData = paragraphConverter.Convert(td,ParagraphConvTargetEnum.Paragraph);
                    if (cellData.SubElements() != null)
                    {
                        foreach (var subElement in cellData.SubElements())
                        {
                            cell.Add(subElement);
                        }
                    }
                    else
                    {
                        cell.Add(cellData);
                    }
                    //cell.Add(new SimpleHTML5Text { Text = td.Text });
                    if (td.ColSpan.HasValue)
                    {
                        cell.ColSpan.Value = td.ColSpan.ToString();
                    }
                    if (td.RowSpan.HasValue)
                    {
                        cell.RowSpan.Value = td.RowSpan.ToString();
                    }
                    switch (td.Align)
                    {
                        case TableAlignmentsEnum.Center:
                            cell.Align.Value = "center";
                            break;
                        case TableAlignmentsEnum.Left:
                            cell.Align.Value = "left";
                            break;
                        case TableAlignmentsEnum.Right:
                            cell.Align.Value = "right";
                            break;
                    }
                    switch (td.VAlign)
                    {
                        case TableVAlignmentsEnum.Top:
                            cell.VAlign.Value = "top";
                            break;
                        case TableVAlignmentsEnum.Middle:
                            cell.VAlign.Value = "middle";
                            break;
                        case TableVAlignmentsEnum.Bottom:
                            cell.VAlign.Value = "bottom";
                            break;
                    }
                    tableRow.Add(cell);
                }
                else
                {
                    // invalid structure , we ignore
                    Logger.Log.ErrorFormat("Invalid/unexpected table row sub element type : {0}", element.GetType().ToString());
                    continue;
                }
            }
            tableRow.Align.Value = tableRowItem.Align ?? "left";
            return tableRow;
        }


        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
