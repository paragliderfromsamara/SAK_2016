using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;

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
            WordDocument.Saved = true;
            if (WordApp != null) WordApp.Visible = true;
        }

        private Shape AddTable(ProtocolTable table)
        {
            Word.Shape oShape = CreateShape();
            Table oTab = BuildTable(row, col, oShape, 0);
            oTab.Cell(1, 1).Range.Text = "Таблица зависимости кол-ва отобранных пар от скорости ШПД";
            oTab.Cell(2, 1).Range.Text = "Скорость ШПД, (Мбит/с)";
            oTab.Cell(2, 2).Range.Text = "Кол-во пар";
            oTab.Cell(2, 3).Range.Text = "%";
            for (int i = 0; i < tres.Length; i++)
            {
                oTab.Cell(i + 3, 1).Range.Text = tres[i].Split('-')[0];
                int kolvo = int.Parse(tres[i].Split('-')[1]);
                int prec = (kolvo * 100) / (res.Plan.StructQuantity * res.Plan.Structure / 2);
                oTab.Cell(i + 3, 2).Range.Text = kolvo.ToString();
                oTab.Cell(i + 3, 3).Range.Text = prec.ToString();
            }
            return oShape;
        }

        private Table BuildTable(int row, int col, Word.Shape oShape, int ns_field)
        {
            object b1 = WdDefaultTableBehavior.wdWord9TableBehavior;
            object b2 = WdAutoFitBehavior.wdAutoFitContent;
            Table oTab = oShape.TextFrame.TextRange.Tables.Add(oShape.TextFrame.TextRange, row, col, ref b1, ref b2);
            oTab.Cell(1, 1).Merge(oTab.Cell(1, col));
            for (int i = row - (ns_field - 1); i <= row; i++)
            {
                oTab.Cell(i, 1).Merge(oTab.Cell(i, col));
                if (i < (row - 2)) oTab.Cell(i, 1).Range.Font.Italic = 1;
                else oTab.Cell(i, 1).Range.Font.Bold = 1;
            }
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

        public void PlaceShapes(Word.Document WordDoc)
        {
            object oEndOfDoc = "\\endofdoc";
            float cx = 0f;
            float wd = WordDoc.PageSetup.PageWidth - WordDoc.PageSetup.LeftMargin;// -WordDoc.PageSetup.RightMargin;
            float ht = WordDoc.PageSetup.PageHeight - WordDoc.PageSetup.TopMargin;// -WordDoc.PageSetup.BottomMargin;
            int cpage = 0;
            List<ShapeCoord> lst = new List<ShapeCoord>();
            float[] line = new float[(int)wd];
            foreach (Word.Shape oShape in WordDoc.Shapes)
            {
                CutShape(oShape);
                float w = oShape.Width;
                float h = oShape.Height;
                if ((cx + w) > wd) cx = 0f;
                if ((GetLine(line, (int)cx, (int)w) + h) > ht)
                {
                    object ob = WdBreakType.wdPageBreak;
                    WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range.InsertBreak(ref ob);
                    for (int ps = 0; ps < line.Length; ps++) line[ps] = 0f;
                    cx = 0f;
                    cpage++;
                }
                lst.Add(new ShapeCoord(cx, GetLine(line, (int)cx, (int)w), cpage));
                SetLine(line, (int)cx, (int)w, (int)h);
                cx += w;
            }
            object pos = 1;
            for (int i = 0; i < WordDoc.Shapes.Count; i++)
            {
                if (lst[i].page == 0)
                {
                    pos = i + 1;
                    WordDoc.Shapes.get_Item(ref pos).Left = lst[i].x;
                    WordDoc.Shapes.get_Item(ref pos).Top = lst[i].y;
                    pos = i + 2;
                }
                else
                {
                    Word.ShapeRange srng = WordDoc.Shapes.Range(ref pos);
                    object jdx = true;
                    srng.Select(ref jdx);
                    WordDoc.ActiveWindow.Selection.Cut();
                    int tm = lst[i].page;
                    while (tm-- > 0) WordDoc.ActiveWindow.Selection.GoToNext(WdGoToItem.wdGoToPage);
                    WordDoc.ActiveWindow.Selection.Paste();
                    object tp = WordDoc.Shapes.Count;
                    WordDoc.Shapes.get_Item(ref tp).Left = lst[i].x;
                    WordDoc.Shapes.get_Item(ref tp).Top = lst[i].y;
                }
            }
        }

        private void CutShape(Word.Shape oShape)
        {
            oShape.Line.Transparency = 1f;
            if (oShape.TextFrame.TextRange.Tables.Count > 0)
            {
                oShape.Width = oShape.TextFrame.TextRange.Tables[1].Rows[1].Cells[1].Width + 5f;
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
