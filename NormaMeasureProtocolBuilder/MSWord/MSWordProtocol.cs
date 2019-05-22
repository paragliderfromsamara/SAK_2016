using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System.Diagnostics;

namespace NormaMeasure.ProtocolBuilder.MSWord
{
    public class MSWordProtocol
    {
        private Word.Application WordApp;
        private Word.Document WordDocument;
        private object oMissing = System.Reflection.Missing.Value;

        public MSWordProtocol()
        {
        }

        public void Init()
        {
            WordApp = new Word.Application();
            WordDocument = WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            WordDocument.PageSetup.LeftMargin = MarginLeft;
            WordDocument.PageSetup.RightMargin = MarginRight;
            WordDocument.PageSetup.TopMargin = MarginTop;
            WordDocument.PageSetup.BottomMargin = MarginBottom;
        }

        public void Finalise()
        {
            PlaceShapes();
            WordDocument.Saved = true;
            if (WordApp != null) WordApp.Visible = true;
        }

        public void AddTable(ProtocolTable table) //ProtocolTable table
        {
            Word.Shape oShape = CreateShape();
            Table oTab = BuildTable(table, oShape);
            FillTable(oTab, table);
            ResizeShapeByTable(oShape);
        }

        private void FillTable(Table oTable, ProtocolTable tableData)
        {
            foreach(int rIdx in tableData.Rows.Keys)
            {
                int y = rIdx + 1;
                ProtocolTableRow row = tableData.Rows[rIdx];
                foreach (int cIdx in row.Cells.Keys)
                {
                    int x = cIdx + 1;
                    ProtocolTableCell cell = row.Cells[cIdx];
                    oTable.Cell(y, x).Range.Text = cell.Value;
                }
            }
        }
        private Table BuildTable(ProtocolTable table, Word.Shape oShape)
        {
            object b1 = WdDefaultTableBehavior.wdWord9TableBehavior;
            object b2 = WdAutoFitBehavior.wdAutoFitContent;
            Table oTab = oShape.TextFrame.TextRange.Tables.Add(oShape.TextFrame.TextRange, table.RowsCount, table.ColumnsCount, ref b1, ref b2);
            //oTab.Cell(1, 1).Merge(oTab.Cell(1, col));
            foreach(int rowIdx in table.Rows.Keys)
            {
                ProtocolTableRow row = table.Rows[rowIdx];
                int y = rowIdx+1;
                if (row.Cells.Count < table.ColumnsCount)
                {
                    foreach(int cellIdx in row.Cells.Keys)
                    {
                        int x = cellIdx+1;
                        ProtocolTableCell cell = row.Cells[cellIdx];
                        if (cell.ColSpan > 0)
                        {
                            int lastCellIdx = x + cell.ColSpan - 1;
                            if (lastCellIdx > table.ColumnsCount) lastCellIdx = table.ColumnsCount;
                            oTab.Cell(y, x).Merge(oTab.Cell(y, lastCellIdx));
                        }
                    }
                }
            }
            /*
            for (int i = row; i <= row; i++)
            {
                oTab.Cell(i, 1).Range.Font.Bold = 1;
            }
            */
            oTab.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            return oTab;
        }


        private Word.Shape CreateShape(float height = 50f, float width = 600f)
        {
            Word.Shape oShape = WordDocument.Shapes.AddShape(1, 50, 50, width, height, ref oMissing);
            oShape.TextFrame.TextRange.Font.Size = FontSize;
            oShape.TextFrame.TextRange.Font.Name = FontName;
            oShape.TextFrame.TextRange.Font.Color = FontColor;
            oShape.Fill.Transparency = 1f;
            return oShape;
        }

        public void PlaceShapes()
        {
            object oEndOfDoc = "\\endofdoc";
            float cx = 0f;
            float wd = WordDocument.PageSetup.PageWidth - WordDocument.PageSetup.LeftMargin;// -WordDoc.PageSetup.RightMargin;
            float ht = WordDocument.PageSetup.PageHeight - WordDocument.PageSetup.TopMargin;// -WordDoc.PageSetup.BottomMargin;
            int cpage = 0;
            List<ShapeCoord> lst = new List<ShapeCoord>();
            float[] line = new float[(int)wd];
            foreach (Word.Shape oShape in WordDocument.Shapes)
            {
                //CutShape(oShape);
                float w = oShape.Width;
                float h = oShape.Height;
                if ((cx + w) > wd) cx = 0f;
                if ((GetLine(line, (int)cx, (int)w) + h) > ht)
                {
                    object ob = WdBreakType.wdPageBreak;
                    WordDocument.Bookmarks.get_Item(ref oEndOfDoc).Range.InsertBreak(ref ob);
                    for (int ps = 0; ps < line.Length; ps++) line[ps] = 0f;
                    cx = 0f;
                    cpage++;
                }
                lst.Add(new ShapeCoord(cx, GetLine(line, (int)cx, (int)w), cpage));
                SetLine(line, (int)cx, (int)w, (int)h);
                cx += w;
            }
            object pos = 1;
            for (int i = 0; i < WordDocument.Shapes.Count; i++)
            {
                if (lst[i].page == 0)
                {
                    pos = i + 1;
                    WordDocument.Shapes.get_Item(ref pos).Left = lst[i].x;
                    WordDocument.Shapes.get_Item(ref pos).Top = lst[i].y;
                    pos = i + 2;
                }
                else
                {
                    Word.ShapeRange srng = WordDocument.Shapes.Range(ref pos);
                    object jdx = true;
                    srng.Select(ref jdx);
                    WordDocument.ActiveWindow.Selection.Cut();
                    int tm = lst[i].page;
                    while (tm-- > 0) WordDocument.ActiveWindow.Selection.GoToNext(WdGoToItem.wdGoToPage);
                    WordDocument.ActiveWindow.Selection.Paste();
                    object tp = WordDocument.Shapes.Count;
                    WordDocument.Shapes.get_Item(ref tp).Left = lst[i].x;
                    WordDocument.Shapes.get_Item(ref tp).Top = lst[i].y;
                }
            }
        }

        private void ResizeShapeByTable(Word.Shape oShape)
        {
            oShape.Line.Transparency = 1f;

            if (oShape.TextFrame.TextRange.Tables.Count > 0)
            {
                float width = 20f;
                float height = oShape.TextFrame.TextRange.Tables[1].Rows.Count * FontSize *1.44f;
                foreach(Cell cell in oShape.TextFrame.TextRange.Tables[1].Rows[1].Cells) width += cell.Width;
                oShape.Width = width;
                oShape.Height = height;
                Debug.WriteLine($"MSWordProtocol.CutShape(): RowsHeight = {oShape.TextFrame.TextRange.Tables[1].Rows.Height}");
                Debug.WriteLine($"MSWordProtocol.CutShape(): CalculatedWidth = {width}; CalculatedHeight = {height}");
            }
        }

        private float GetLine(float[] mass, int begin, int width)
        {
            float ret = 0f;
            for (int pos = begin; pos < (begin + width); pos++) if (mass[pos] > ret) ret = mass[pos];
            return ret;
        }
        private void SetLine(float[] mass, int begin, int width, float height)
        {
            float val = GetLine(mass, begin, width) + height;
            for (int pos = begin; pos < (begin + width); pos++) mass[pos] = val;
        }


        private Word.WdColor FontColor
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.FontColor;
            }set
            {
                NormaMeasureProtocolBuilder.ProtocolSettings.Default.FontColor = value;
                NormaMeasureProtocolBuilder.ProtocolSettings.Default.Save();
            }
        }

        private float FontSize
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.FontSize;
            }
            set
            {
                NormaMeasureProtocolBuilder.ProtocolSettings.Default.FontSize = value;
                NormaMeasureProtocolBuilder.ProtocolSettings.Default.Save();
            }
        }

        private string FontName
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.FontName;
            }
            set
            {
                NormaMeasureProtocolBuilder.ProtocolSettings.Default.FontName = value;
                NormaMeasureProtocolBuilder.ProtocolSettings.Default.Save();
            }
        }


        private float MarginRight
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.Page_MarginRight;

            }
        }

        private float MarginTop
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.Page_MarginTop;

            }
        }
        private float MarginLeft
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.Page_MarginLeft;

            }
        }
        private float MarginBottom
        {
            get
            {
                return NormaMeasureProtocolBuilder.ProtocolSettings.Default.Page_MarginBottom;

            }
        }
    }

    internal class ShapeCoord
    {
        public float x;
        public float y;
        public int page;

        public ShapeCoord(float tx, float ty, int tpage)
        {
            x = tx;
            y = ty;
            page = tpage;
        }
    }
}
