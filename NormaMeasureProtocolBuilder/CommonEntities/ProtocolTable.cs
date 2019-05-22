using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.ProtocolBuilder
{
    public class ProtocolTable
    {
        public Dictionary<int, ProtocolTableRow> Rows = new Dictionary<int, ProtocolTableRow>();
        public int ColumnsCount => CalcColumns();
        public int RowsCount => Rows.Count;

        private int CalcColumns()
        {
            int count = 0;
            foreach(ProtocolTableRow row in Rows.Values)
            {
                if (count < row.Cells.Count) count = row.Cells.Count;
            }
            return count;
        }

        public int AddRow(ProtocolTableRow row)
        {
            row.ParentTable = this;
            Rows.Add(Rows.Count, row);
            return Rows.Count - 1;
        }

        public int AddRow(string[] data)
        {
            ProtocolTableRow row = new ProtocolTableRow(data);
            return AddRow(row);
        }

        public ProtocolTable()
        {

        }


    }

    public class ProtocolTableRow
    {
        public ProtocolTable ParentTable;
        public Dictionary<int, ProtocolTableCell> Cells = new Dictionary<int, ProtocolTableCell>();
        private int lastCellIdx => Cells.Count - 1;

        public ProtocolTableRow(string[] data)
        {
            foreach(string d in data)
            {
                ProtocolTableCell cell = new ProtocolTableCell(d);
                AddCell(cell);
            }
        }

        public ProtocolTableRow()
        {

        }

        public ProtocolTableRow(int cells_count)
        {
            for(int i=0; i<cells_count; i++)
            {
                ProtocolTableCell cell = new ProtocolTableCell();
                AddCell(cell);
            }
        }



        public int AddCell(ProtocolTableCell cell)
        {
            cell.ParentRow = this;
            Cells.Add(lastCellIdx+1, cell);
            return lastCellIdx;
        }

        public int AddCell(string val, int ColSpan = 0, int RowSpan = 0)
        {
            ProtocolTableCell cell = new ProtocolTableCell(val, ColSpan, RowSpan);
            return AddCell(cell);
        }

    }

    public class ProtocolTableCell
    {
        public ProtocolTableRow ParentRow;
        public int ColSpan=0;
        public int RowSpan=0;
        public string Value=String.Empty;

        public ProtocolTableCell()
        {

        }

        public ProtocolTableCell(string val, int col_span=0, int row_span = 0) : this()
        {
            Value = val;
            ColSpan = col_span;
            RowSpan = row_span;
        }
    }

 
}
