using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;
using NormaLib.Measure;
using NormaLib.ProtocolBuilders;
using NormaLib.SessionControl;

namespace NormaLib.ProtocolBuilders.MSWord
{
    internal class MSWordCableTestProtocol : MSWordProtocol
    {
        private CableTest cableTest;
        public MSWordCableTestProtocol(ProtocolPathBuilder path_builder)
        {
            filePath = path_builder.Path_WithFileName;
            cableTest = path_builder.Entity as CableTest;
            CreatorName = $"{cableTest.TestOperator.FullNameShort}"; 
            EditorName = $"{SessionControl.SessionControl.CurrentUser.FullNameShort}";
            ProtocolTitle = ProtocolSettings.ProtocolHeader;
            if (ProtocolSettings.DoesAddTestIdOnProtocolHeader) ProtocolTitle = $"{ProtocolTitle} № {cableTest.TestId}";
            FirstPageHeaderText = ProtocolSettings.CompanyName;
            CreatedAt = cableTest.FinishedAt;
            AnotherPageHeaderText = cableTest.TestedCable.FullName;
        }

        protected override Body BuildBody()
        {
            Body body = base.BuildBody();
            if (!string.IsNullOrWhiteSpace(ProtocolTitle))
            {
                body.Append(BuildHeaderParagraph());
                body.Append(BuildParagraph(JustificationValues.Left));
            }
            body.Append(BuildCableTestHeaderInfo());
            foreach (TestedCableStructure s in cableTest.TestedCable.CableStructures.Rows)
            {
                //body.Append(new Paragraph());
                body.Append(BuildParagraph(JustificationValues.Left));
                body.Append(BuildStructureData(s));
            }
            return body;
        }
        #region HeaderInfo Panel
        private IEnumerable<OpenXmlElement> BuildCableTestHeaderInfo()
        {
            List<OpenXmlElement> els = new List<OpenXmlElement>();
            Table table = BuildCableTestInfoTable();
            TableRow row =  new TableRow() { RsidTableRowAddition = "00924D08", RsidTableRowProperties = "001D44D8" };
            TableRow cableMarkRow = BuildCableMarkRow(); 
            row.Append(BuildTestInfoTableLeftCell());
            row.Append(BuildTestInfoTableCenterCell());
            row.Append(BuildTestInfoTableRightCell());
            table.Append(cableMarkRow, row);
            els.Add(table);
            return els;
        }

        private TableRow BuildCableMarkRow()
        {
            TableRow cableMarkRow = new TableRow() { RsidTableRowAddition = "00924D08", RsidTableRowProperties = "001D44D8" };
            Paragraph p = BuildParagraph(JustificationValues.Left);
            TableCellProperties props = new TableCellProperties();
            TableCellWidth w = new TableCellWidth() { Width = $"{MaxColsPerPage*ResultCellWidth}"};
            props.Append(w);
            GridSpan gridSpan7 = new GridSpan() { Val = 3 };
            props.Append(gridSpan7);
            p.Append(AddRun($"Марка кабеля: "), AddRun($"{ cableTest.TestedCable.FullName}", MSWordStringTypes.Typical, true, true));
            TableCell cableMarkCell = BuildCell(p);
            cableMarkCell.Append(props);
            cableMarkRow.Append(cableMarkCell);
            return cableMarkRow;
        }

        private Table BuildCableTestInfoTable()
        {
            Table table1 = new Table();

            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle() { Val = "a3" };
            TableWidth tableWidth1 = new TableWidth() { Width = (MaxColsPerPage*ResultCellWidth).ToString(), Type = TableWidthUnitValues.Dxa };
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };

            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLayout tableLayout1 = new TableLayout() { Type = TableLayoutValues.Fixed };

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            TableCellLeftMargin tableCellLeftMargin1 = new TableCellLeftMargin() { Width = 28, Type = TableWidthValues.Dxa };
            TableCellRightMargin tableCellRightMargin1 = new TableCellRightMargin() { Width = 28, Type = TableWidthValues.Dxa };

            tableCellMarginDefault1.Append(tableCellLeftMargin1);
            tableCellMarginDefault1.Append(tableCellRightMargin1);
            TableLook tableLook1 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };

            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableIndentation1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLayout1);
            tableProperties1.Append(tableCellMarginDefault1);
            tableProperties1.Append(tableLook1);

            TableGrid tableGrid1 = new TableGrid();
            GridColumn gridColumn1 = new GridColumn() { Width = $"{(MaxColsPerPage * ResultCellWidth)/3}" };
            GridColumn gridColumn2 = new GridColumn() { Width = $"{(MaxColsPerPage * ResultCellWidth) / 3}" };
            GridColumn gridColumn3 = new GridColumn() { Width = $"{(MaxColsPerPage * ResultCellWidth) / 3}" };

            tableGrid1.Append(gridColumn1);
            tableGrid1.Append(gridColumn2);
            tableGrid1.Append(gridColumn3);

            TableRow tableRow1 = new TableRow() { RsidTableRowAddition = "00924D08", RsidTableRowProperties = "001D44D8" };

            TableCell tableCell1 = new TableCell();

            TableCellProperties tableCellProperties1 = new TableCellProperties();
            TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "7797", Type = TableWidthUnitValues.Dxa };

            tableCellProperties1.Append(tableCellWidth1);

            table1.Append(tableProperties1);
            table1.Append(tableGrid1);

            return table1;
        }

        private TableCell BuildTestInfoTableLeftCell()
        {
            TableCell cell = new TableCell();
            Paragraph p1 = BuildParagraph(JustificationValues.Left);
            Paragraph p2 = BuildParagraph(JustificationValues.Left);
            Paragraph p3 = BuildParagraph(JustificationValues.Left);
            p1.Append(AddRun($"Заказ № "));
            p2.Append(AddRun($"Длина кабеля: {cableTest.CableLength} м"));
            p3.Append(AddRun($"БРУТТО: "));
            cell.Append(p1, p2, p3);
            return cell;
        }

        private TableCell BuildTestInfoTableRightCell()
        {
            TableCell cell = new TableCell();
            Paragraph p1 = BuildParagraph(JustificationValues.Left);
            Paragraph p2 = BuildParagraph(JustificationValues.Left);
            Paragraph p3 = BuildParagraph(JustificationValues.Left);
            p1.Append(AddRun($"Тип барабана: {cableTest.TestedCable.FullName}"));
            p2.Append(AddRun($"Температура: {cableTest.Temperature}°C")); 
            p3.Append(AddRun($"Дата испытания: {cableTest.FinishedAt.ToString("dd.MM.yyyy")}"));
            cell.Append(p1, p2, p3);
            return cell;
        }

        private TableCell BuildTestInfoTableCenterCell()
        {
            TableCell cell = new TableCell();
            Paragraph p1 = BuildParagraph(JustificationValues.Left);
            p1.Append(AddRun($"№ барабана: "));
            cell.Append(p1);
            return cell;
        }
        #endregion


        private IEnumerable<OpenXmlElement> BuildStructureData(TestedCableStructure structure)
        {
            List<OpenXmlElement> els = new List<OpenXmlElement>();
            //wordProtocol.CurrentFontSize = wordProtocol.FontSize;
            els.AddRange(BuildPrimaryParametersTable(structure));
            //addRizolByGroupTable(structure);
            //add_al_Table(structure);
            //add_AoAz_Table(structure, Tables.MeasuredParameterType.Ao);
            //add_AoAz_Table(structure, Tables.MeasuredParameterType.Az);
            els.AddRange(BuildStatisticStructureTable(structure));
            //wordProtocol.CurrentFontSize = 11f;
            //add_VSVI_TestResult(structure);
            els.AddRange(add_StructElements_Conclusion(structure));
            return els;
        }

        /// <summary>
        /// Отрисовка вывода о годности элементов
        /// </summary>
        /// <param name="structure"></param>
        private IEnumerable<OpenXmlElement> add_StructElements_Conclusion(TestedCableStructure structure)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
            paragraphs.Add(BuildParagraph(AddRun($"Номинальное количество {structure.StructureType.StructureTypeName_RodPadej_Multiple}: {structure.DisplayedAmount}"), JustificationValues.Left));
            paragraphs.Add(BuildParagraph(AddRun($"Фактическое количество {structure.StructureType.StructureTypeName_RodPadej_Multiple}: {structure.RealAmount}"), JustificationValues.Left));
            paragraphs.Add(BuildParagraph(AddRun($"Годных {structure.StructureType.StructureTypeName_RodPadej_Multiple}: {structure.NormalElementsAmount}"), JustificationValues.Left));
            Paragraph descriptionParagraph = BuildParagraph(JustificationValues.Left);
            descriptionParagraph.Append(AddRun("Значения измеренных параметров вышедшие за установленные нормы выделены", MSWordStringTypes.Typical, false, true));
            descriptionParagraph.Append(AddRun(" жирным ", MSWordStringTypes.Typical, true, true), AddRun("шрифтом.", MSWordStringTypes.Typical, false, true));
            paragraphs.Add(descriptionParagraph);
            return paragraphs;
        }

        private IEnumerable<OpenXmlElement> BuildStatisticStructureTable(TestedCableStructure structure)
        {
            Paragraph p = BuildParagraph();
            Table table = InitStatisticStructureTable(structure);
           
            foreach(uint pTypeId in structure.MeasuredParameterTypes_ids)
            {
                TestedStructureMeasuredParameterData[] pData = structure.GetMeasureParameterDatasByParameterType(pTypeId);
                for (int pDataIdx = 0; pDataIdx < pData.Length; pDataIdx++)
                {
                    TableRow r = new TableRow();
                    TestedStructureMeasuredParameterData d = pData[pDataIdx];
                    TableCell pNameCell = BuildParameterNameCellForStatTable(pDataIdx, pData[pDataIdx]);
                    TableCell pMinValCell = BuildCell(d.HasMinLimit ? d.MinValue.ToString() : "");
                    TableCell pMaxValCell = BuildCell(d.HasMaxLimit ? d.MaxValue.ToString() : "");
                    TableCell pMeasureCell = BuildCell(d.ResultMeasure);
                    TableCell pNormalPercent = BuildCell(d.Percent.ToString());
                    TableCell pMeasuredPercent = BuildCell(AddRun(d.MeasuredPercent.ToString(), MSWordStringTypes.Typical, d.MeasuredPercent < d.Percent));
                    r.Append(new List<OpenXmlElement>() { pNameCell, pMinValCell, pMaxValCell, pMeasureCell, pNormalPercent, pMeasuredPercent });
                    table.Append(r);
                }
            }
            return new List<OpenXmlElement>() { p, table };
        }


        private TableCell BuildParameterNameCellForStatTable(int pDataIdx, TestedStructureMeasuredParameterData pData)
        {
            TableCell tableCell1 = new TableCell();

            TableCellProperties tableCellProperties1 = new TableCellProperties();
            TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge1 = (pDataIdx > 0) ? new VerticalMerge() : new VerticalMerge() { Val = MergedCellValues.Restart };

            TableCellMargin tableCellMargin1 = new TableCellMargin();
            LeftMargin leftMargin1 = new LeftMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin1 = new RightMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };

            tableCellMargin1.Append(leftMargin1);
            tableCellMargin1.Append(rightMargin1);
            TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties1.Append(tableCellWidth1);
            tableCellProperties1.Append(verticalMerge1);
            tableCellProperties1.Append(tableCellMargin1);
            tableCellProperties1.Append(tableCellVerticalAlignment1);

            tableCell1.Append(tableCellProperties1);
            if (pDataIdx == 0)
                tableCell1.Append(ParameterNameText(pData.ParameterTypeId));
            else
                tableCell1.Append(BuildParagraph());
            return tableCell1;

        }

        private Table InitStatisticStructureTable(TestedCableStructure structure)
        {
            Table table1 = new Table();

            TableProperties tableProperties1 = new TableProperties();
            TableWidth tableWidth1 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };

            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)2U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)2U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)2U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)2U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)2U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)2U, Space = (UInt32Value)0U };

            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLook tableLook1 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };

            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLook1);

            TableGrid tableGrid1 = new TableGrid();
            GridColumn gridColumn1 = new GridColumn() { Width = "816" };
            GridColumn gridColumn2 = new GridColumn() { Width = "750" };
            GridColumn gridColumn3 = new GridColumn() { Width = "750" };
            GridColumn gridColumn4 = new GridColumn() { Width = "876" };
            GridColumn gridColumn5 = new GridColumn() { Width = "750" };
            GridColumn gridColumn6 = new GridColumn() { Width = "824" };

            tableGrid1.Append(gridColumn1);
            tableGrid1.Append(gridColumn2);
            tableGrid1.Append(gridColumn3);
            tableGrid1.Append(gridColumn4);
            tableGrid1.Append(gridColumn5);
            tableGrid1.Append(gridColumn6);

            TableRow tableRow1 = new TableRow() { RsidTableRowMarkRevision = "00D91C70", RsidTableRowAddition = "001409DE", RsidTableRowProperties = "00C96235" };

            TableCell tableCell1 = new TableCell();

            TableCellProperties tableCellProperties1 = new TableCellProperties();
            TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge1 = new VerticalMerge() { Val = MergedCellValues.Restart };

            TableCellMargin tableCellMargin1 = new TableCellMargin();
            LeftMargin leftMargin1 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin1 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin1.Append(leftMargin1);
            tableCellMargin1.Append(rightMargin1);
            TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties1.Append(tableCellWidth1);
            tableCellProperties1.Append(verticalMerge1);
            tableCellProperties1.Append(tableCellMargin1);
            tableCellProperties1.Append(tableCellVerticalAlignment1);

            Paragraph paragraph1 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification1 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
            RunFonts runFonts1 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color1 = new Color() { Val = "000000" };
            FontSize fontSize1 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties1.Append(runFonts1);
            paragraphMarkRunProperties1.Append(color1);
            paragraphMarkRunProperties1.Append(fontSize1);

            paragraphProperties1.Append(spacingBetweenLines1);
            paragraphProperties1.Append(justification1);
            paragraphProperties1.Append(paragraphMarkRunProperties1);

            Run run1 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties1 = new RunProperties();
            RunFonts runFonts2 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color2 = new Color() { Val = "000000" };
            FontSize fontSize2 = new FontSize() { Val = "17" };

            runProperties1.Append(runFonts2);
            runProperties1.Append(color2);
            runProperties1.Append(fontSize2);
            Text text1 = new Text();
            text1.Text = "Параметр";

            run1.Append(runProperties1);
            run1.Append(text1);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);

            tableCell1.Append(tableCellProperties1);
            tableCell1.Append(paragraph1);

            TableCell tableCell2 = new TableCell();

            TableCellProperties tableCellProperties2 = new TableCellProperties();
            TableCellWidth tableCellWidth2 = new TableCellWidth() { Width = "1500", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan1 = new GridSpan() { Val = 2 };

            TableCellMargin tableCellMargin2 = new TableCellMargin();
            LeftMargin leftMargin2 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin2 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin2.Append(leftMargin2);
            tableCellMargin2.Append(rightMargin2);
            TableCellVerticalAlignment tableCellVerticalAlignment2 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties2.Append(tableCellWidth2);
            tableCellProperties2.Append(gridSpan1);
            tableCellProperties2.Append(tableCellMargin2);
            tableCellProperties2.Append(tableCellVerticalAlignment2);

            Paragraph paragraph2 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties2 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification2 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties2 = new ParagraphMarkRunProperties();
            RunFonts runFonts3 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color3 = new Color() { Val = "000000" };
            FontSize fontSize3 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties2.Append(runFonts3);
            paragraphMarkRunProperties2.Append(color3);
            paragraphMarkRunProperties2.Append(fontSize3);

            paragraphProperties2.Append(spacingBetweenLines2);
            paragraphProperties2.Append(justification2);
            paragraphProperties2.Append(paragraphMarkRunProperties2);

            Run run2 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties2 = new RunProperties();
            RunFonts runFonts4 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color4 = new Color() { Val = "000000" };
            FontSize fontSize4 = new FontSize() { Val = "17" };

            runProperties2.Append(runFonts4);
            runProperties2.Append(color4);
            runProperties2.Append(fontSize4);
            Text text2 = new Text();
            text2.Text = "Норма";

            run2.Append(runProperties2);
            run2.Append(text2);

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(run2);

            tableCell2.Append(tableCellProperties2);
            tableCell2.Append(paragraph2);

            TableCell tableCell3 = new TableCell();

            TableCellProperties tableCellProperties3 = new TableCellProperties();
            TableCellWidth tableCellWidth3 = new TableCellWidth() { Width = "767", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge2 = new VerticalMerge() { Val = MergedCellValues.Restart };

            TableCellMargin tableCellMargin3 = new TableCellMargin();
            LeftMargin leftMargin3 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin3 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin3.Append(leftMargin3);
            tableCellMargin3.Append(rightMargin3);
            TableCellVerticalAlignment tableCellVerticalAlignment3 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties3.Append(tableCellWidth3);
            tableCellProperties3.Append(verticalMerge2);
            tableCellProperties3.Append(tableCellMargin3);
            tableCellProperties3.Append(tableCellVerticalAlignment3);

            Paragraph paragraph3 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties3 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines3 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification3 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();
            RunFonts runFonts5 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color5 = new Color() { Val = "000000" };
            FontSize fontSize5 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties3.Append(runFonts5);
            paragraphMarkRunProperties3.Append(color5);
            paragraphMarkRunProperties3.Append(fontSize5);

            paragraphProperties3.Append(spacingBetweenLines3);
            paragraphProperties3.Append(justification3);
            paragraphProperties3.Append(paragraphMarkRunProperties3);

            Run run3 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties3 = new RunProperties();
            RunFonts runFonts6 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color6 = new Color() { Val = "000000" };
            FontSize fontSize6 = new FontSize() { Val = "17" };

            runProperties3.Append(runFonts6);
            runProperties3.Append(color6);
            runProperties3.Append(fontSize6);
            Text text3 = new Text();
            text3.Text = "Единица измерения";

            run3.Append(runProperties3);
            run3.Append(text3);

            paragraph3.Append(paragraphProperties3);
            paragraph3.Append(run3);

            tableCell3.Append(tableCellProperties3);
            tableCell3.Append(paragraph3);

            TableCell tableCell4 = new TableCell();

            TableCellProperties tableCellProperties4 = new TableCellProperties();
            TableCellWidth tableCellWidth4 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge3 = new VerticalMerge() { Val = MergedCellValues.Restart };

            TableCellMargin tableCellMargin4 = new TableCellMargin();
            LeftMargin leftMargin4 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin4 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin4.Append(leftMargin4);
            tableCellMargin4.Append(rightMargin4);
            TableCellVerticalAlignment tableCellVerticalAlignment4 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties4.Append(tableCellWidth4);
            tableCellProperties4.Append(verticalMerge3);
            tableCellProperties4.Append(tableCellMargin4);
            tableCellProperties4.Append(tableCellVerticalAlignment4);

            Paragraph paragraph4 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "00C96235", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties4 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines4 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification4 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();
            RunFonts runFonts7 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color7 = new Color() { Val = "000000" };
            FontSize fontSize7 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties4.Append(runFonts7);
            paragraphMarkRunProperties4.Append(color7);
            paragraphMarkRunProperties4.Append(fontSize7);

            paragraphProperties4.Append(spacingBetweenLines4);
            paragraphProperties4.Append(justification4);
            paragraphProperties4.Append(paragraphMarkRunProperties4);

            Run run4 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties4 = new RunProperties();
            RunFonts runFonts8 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color8 = new Color() { Val = "000000" };
            FontSize fontSize8 = new FontSize() { Val = "17" };

            runProperties4.Append(runFonts8);
            runProperties4.Append(color8);
            runProperties4.Append(fontSize8);
            Text text4 = new Text();
            text4.Text = "Задано %";

            run4.Append(runProperties4);
            run4.Append(text4);

            paragraph4.Append(paragraphProperties4);
            paragraph4.Append(run4);

            tableCell4.Append(tableCellProperties4);
            tableCell4.Append(paragraph4);

            TableCell tableCell5 = new TableCell();

            TableCellProperties tableCellProperties5 = new TableCellProperties();
            TableCellWidth tableCellWidth5 = new TableCellWidth() { Width = "757", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge4 = new VerticalMerge() { Val = MergedCellValues.Restart };

            TableCellMargin tableCellMargin5 = new TableCellMargin();
            LeftMargin leftMargin5 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin5 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin5.Append(leftMargin5);
            tableCellMargin5.Append(rightMargin5);
            TableCellVerticalAlignment tableCellVerticalAlignment5 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties5.Append(tableCellWidth5);
            tableCellProperties5.Append(verticalMerge4);
            tableCellProperties5.Append(tableCellMargin5);
            tableCellProperties5.Append(tableCellVerticalAlignment5);

            Paragraph paragraph5 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "00C96235", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties5 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines5 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification5 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties5 = new ParagraphMarkRunProperties();
            RunFonts runFonts9 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color9 = new Color() { Val = "000000" };
            FontSize fontSize9 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties5.Append(runFonts9);
            paragraphMarkRunProperties5.Append(color9);
            paragraphMarkRunProperties5.Append(fontSize9);

            paragraphProperties5.Append(spacingBetweenLines5);
            paragraphProperties5.Append(justification5);
            paragraphProperties5.Append(paragraphMarkRunProperties5);

            Run run5 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties5 = new RunProperties();
            RunFonts runFonts10 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color10 = new Color() { Val = "000000" };
            FontSize fontSize10 = new FontSize() { Val = "17" };

            runProperties5.Append(runFonts10);
            runProperties5.Append(color10);
            runProperties5.Append(fontSize10);
            Text text5 = new Text();
            text5.Text = "Измерено %";

            run5.Append(runProperties5);
            run5.Append(text5);

            paragraph5.Append(paragraphProperties5);
            paragraph5.Append(run5);

            tableCell5.Append(tableCellProperties5);
            tableCell5.Append(paragraph5);

            tableRow1.Append(tableCell1);
            tableRow1.Append(tableCell2);
            tableRow1.Append(tableCell3);
            tableRow1.Append(tableCell4);
            tableRow1.Append(tableCell5);

            TableRow tableRow2 = new TableRow() { RsidTableRowMarkRevision = "00D91C70", RsidTableRowAddition = "001409DE", RsidTableRowProperties = "00C96235" };

            TableCell tableCell6 = new TableCell();

            TableCellProperties tableCellProperties6 = new TableCellProperties();
            TableCellWidth tableCellWidth6 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge5 = new VerticalMerge();

            TableCellMargin tableCellMargin6 = new TableCellMargin();
            LeftMargin leftMargin6 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin6 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin6.Append(leftMargin6);
            tableCellMargin6.Append(rightMargin6);
            TableCellVerticalAlignment tableCellVerticalAlignment6 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties6.Append(tableCellWidth6);
            tableCellProperties6.Append(verticalMerge5);
            tableCellProperties6.Append(tableCellMargin6);
            tableCellProperties6.Append(tableCellVerticalAlignment6);

            Paragraph paragraph6 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties6 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines6 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification6 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties6 = new ParagraphMarkRunProperties();
            RunFonts runFonts11 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color11 = new Color() { Val = "000000" };
            FontSize fontSize11 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties6.Append(runFonts11);
            paragraphMarkRunProperties6.Append(color11);
            paragraphMarkRunProperties6.Append(fontSize11);

            paragraphProperties6.Append(spacingBetweenLines6);
            paragraphProperties6.Append(justification6);
            paragraphProperties6.Append(paragraphMarkRunProperties6);

            paragraph6.Append(paragraphProperties6);

            tableCell6.Append(tableCellProperties6);
            tableCell6.Append(paragraph6);

            TableCell tableCell7 = new TableCell();

            TableCellProperties tableCellProperties7 = new TableCellProperties();
            TableCellWidth tableCellWidth7 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };

            TableCellMargin tableCellMargin7 = new TableCellMargin();
            LeftMargin leftMargin7 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin7 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin7.Append(leftMargin7);
            tableCellMargin7.Append(rightMargin7);
            TableCellVerticalAlignment tableCellVerticalAlignment7 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties7.Append(tableCellWidth7);
            tableCellProperties7.Append(tableCellMargin7);
            tableCellProperties7.Append(tableCellVerticalAlignment7);

            Paragraph paragraph7 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties7 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines7 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification7 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();
            RunFonts runFonts12 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color12 = new Color() { Val = "000000" };
            FontSize fontSize12 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties7.Append(runFonts12);
            paragraphMarkRunProperties7.Append(color12);
            paragraphMarkRunProperties7.Append(fontSize12);

            paragraphProperties7.Append(spacingBetweenLines7);
            paragraphProperties7.Append(justification7);
            paragraphProperties7.Append(paragraphMarkRunProperties7);
            ProofError proofError1 = new ProofError() { Type = ProofingErrorValues.SpellStart };

            Run run6 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties6 = new RunProperties();
            RunFonts runFonts13 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color13 = new Color() { Val = "000000" };
            FontSize fontSize13 = new FontSize() { Val = "17" };

            runProperties6.Append(runFonts13);
            runProperties6.Append(color13);
            runProperties6.Append(fontSize13);
            Text text6 = new Text();
            text6.Text = "min";

            run6.Append(runProperties6);
            run6.Append(text6);
            ProofError proofError2 = new ProofError() { Type = ProofingErrorValues.SpellEnd };

            paragraph7.Append(paragraphProperties7);
            paragraph7.Append(proofError1);
            paragraph7.Append(run6);
            paragraph7.Append(proofError2);

            tableCell7.Append(tableCellProperties7);
            tableCell7.Append(paragraph7);

            TableCell tableCell8 = new TableCell();

            TableCellProperties tableCellProperties8 = new TableCellProperties();
            TableCellWidth tableCellWidth8 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };

            TableCellMargin tableCellMargin8 = new TableCellMargin();
            LeftMargin leftMargin8 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin8 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin8.Append(leftMargin8);
            tableCellMargin8.Append(rightMargin8);
            TableCellVerticalAlignment tableCellVerticalAlignment8 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties8.Append(tableCellWidth8);
            tableCellProperties8.Append(tableCellMargin8);
            tableCellProperties8.Append(tableCellVerticalAlignment8);

            Paragraph paragraph8 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties8 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines8 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification8 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();
            RunFonts runFonts14 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color14 = new Color() { Val = "000000" };
            FontSize fontSize14 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties8.Append(runFonts14);
            paragraphMarkRunProperties8.Append(color14);
            paragraphMarkRunProperties8.Append(fontSize14);

            paragraphProperties8.Append(spacingBetweenLines8);
            paragraphProperties8.Append(justification8);
            paragraphProperties8.Append(paragraphMarkRunProperties8);
            ProofError proofError3 = new ProofError() { Type = ProofingErrorValues.SpellStart };

            Run run7 = new Run() { RsidRunProperties = "00D91C70" };

            RunProperties runProperties7 = new RunProperties();
            RunFonts runFonts15 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color15 = new Color() { Val = "000000" };
            FontSize fontSize15 = new FontSize() { Val = "17" };

            runProperties7.Append(runFonts15);
            runProperties7.Append(color15);
            runProperties7.Append(fontSize15);
            Text text7 = new Text();
            text7.Text = "max";

            run7.Append(runProperties7);
            run7.Append(text7);
            ProofError proofError4 = new ProofError() { Type = ProofingErrorValues.SpellEnd };

            paragraph8.Append(paragraphProperties8);
            paragraph8.Append(proofError3);
            paragraph8.Append(run7);
            paragraph8.Append(proofError4);

            tableCell8.Append(tableCellProperties8);
            tableCell8.Append(paragraph8);

            TableCell tableCell9 = new TableCell();

            TableCellProperties tableCellProperties9 = new TableCellProperties();
            TableCellWidth tableCellWidth9 = new TableCellWidth() { Width = "767", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge6 = new VerticalMerge();

            TableCellMargin tableCellMargin9 = new TableCellMargin();
            LeftMargin leftMargin9 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin9 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin9.Append(leftMargin9);
            tableCellMargin9.Append(rightMargin9);
            TableCellVerticalAlignment tableCellVerticalAlignment9 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties9.Append(tableCellWidth9);
            tableCellProperties9.Append(verticalMerge6);
            tableCellProperties9.Append(tableCellMargin9);
            tableCellProperties9.Append(tableCellVerticalAlignment9);

            Paragraph paragraph9 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties9 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines9 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification9 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
            RunFonts runFonts16 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color16 = new Color() { Val = "000000" };
            FontSize fontSize16 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties9.Append(runFonts16);
            paragraphMarkRunProperties9.Append(color16);
            paragraphMarkRunProperties9.Append(fontSize16);

            paragraphProperties9.Append(spacingBetweenLines9);
            paragraphProperties9.Append(justification9);
            paragraphProperties9.Append(paragraphMarkRunProperties9);

            paragraph9.Append(paragraphProperties9);

            tableCell9.Append(tableCellProperties9);
            tableCell9.Append(paragraph9);

            TableCell tableCell10 = new TableCell();

            TableCellProperties tableCellProperties10 = new TableCellProperties();
            TableCellWidth tableCellWidth10 = new TableCellWidth() { Width = "750", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge7 = new VerticalMerge();

            TableCellMargin tableCellMargin10 = new TableCellMargin();
            LeftMargin leftMargin10 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin10 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin10.Append(leftMargin10);
            tableCellMargin10.Append(rightMargin10);
            TableCellVerticalAlignment tableCellVerticalAlignment10 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties10.Append(tableCellWidth10);
            tableCellProperties10.Append(verticalMerge7);
            tableCellProperties10.Append(tableCellMargin10);
            tableCellProperties10.Append(tableCellVerticalAlignment10);

            Paragraph paragraph10 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties10 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines10 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification10 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();
            RunFonts runFonts17 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color17 = new Color() { Val = "000000" };
            FontSize fontSize17 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties10.Append(runFonts17);
            paragraphMarkRunProperties10.Append(color17);
            paragraphMarkRunProperties10.Append(fontSize17);

            paragraphProperties10.Append(spacingBetweenLines10);
            paragraphProperties10.Append(justification10);
            paragraphProperties10.Append(paragraphMarkRunProperties10);

            paragraph10.Append(paragraphProperties10);

            tableCell10.Append(tableCellProperties10);
            tableCell10.Append(paragraph10);

            TableCell tableCell11 = new TableCell();

            TableCellProperties tableCellProperties11 = new TableCellProperties();
            TableCellWidth tableCellWidth11 = new TableCellWidth() { Width = "757", Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge8 = new VerticalMerge();

            TableCellMargin tableCellMargin11 = new TableCellMargin();
            LeftMargin leftMargin11 = new LeftMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin11 = new RightMargin() { Width = "57", Type = TableWidthUnitValues.Dxa };

            tableCellMargin11.Append(leftMargin11);
            tableCellMargin11.Append(rightMargin11);
            TableCellVerticalAlignment tableCellVerticalAlignment11 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties11.Append(tableCellWidth11);
            tableCellProperties11.Append(verticalMerge8);
            tableCellProperties11.Append(tableCellMargin11);
            tableCellProperties11.Append(tableCellVerticalAlignment11);

            Paragraph paragraph11 = new Paragraph() { RsidParagraphMarkRevision = "00D91C70", RsidParagraphAddition = "001409DE", RsidParagraphProperties = "007B7D44", RsidRunAdditionDefault = "001409DE" };

            ParagraphProperties paragraphProperties11 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines11 = new SpacingBetweenLines() { After = "16", AfterLines = 7 };
            Justification justification11 = new Justification() { Val = JustificationValues.Center };

            ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
            RunFonts runFonts18 = new RunFonts() { ComplexScript = "Times New Roman" };
            Color color18 = new Color() { Val = "000000" };
            FontSize fontSize18 = new FontSize() { Val = "17" };

            paragraphMarkRunProperties11.Append(runFonts18);
            paragraphMarkRunProperties11.Append(color18);
            paragraphMarkRunProperties11.Append(fontSize18);

            paragraphProperties11.Append(spacingBetweenLines11);
            paragraphProperties11.Append(justification11);
            paragraphProperties11.Append(paragraphMarkRunProperties11);

            paragraph11.Append(paragraphProperties11);

            tableCell11.Append(tableCellProperties11);
            tableCell11.Append(paragraph11);

            tableRow2.Append(tableCell6);
            tableRow2.Append(tableCell7);
            tableRow2.Append(tableCell8);
            tableRow2.Append(tableCell9);
            tableRow2.Append(tableCell10);
            tableRow2.Append(tableCell11);

            table1.Append(tableProperties1);
            table1.Append(tableGrid1);
            table1.Append(tableRow1);
            table1.Append(tableRow2);

            return table1;
        }

        private IEnumerable<OpenXmlElement> BuildPrimaryParametersTable(TestedCableStructure structure)
        {
            List<OpenXmlElement> tablesForAdd = new List<OpenXmlElement>();
            uint[] tst = structure.TestedElements;
            Debug.WriteLine($"{(structure.OwnCable as TestedCable).CableTest.TestResults.Count}");
            int maxCols = MaxColsPerPage;
            int colsCount = 1; //Первая колонка номер элемента

            List<MeasuredParameterType> typesForTable = new List<MeasuredParameterType>();

            for (int i = 0; i < structure.TestedParameterTypes.Rows.Count; i++)
            {
                Debug.WriteLine($"{i}");
                MeasuredParameterType mpt = (MeasuredParameterType)structure.TestedParameterTypes.Rows[i];
                if (mpt.ParameterTypeId == MeasuredParameterType.Calling) continue;
                bool needToBuildTable = false;
                if (mpt.IsPrimaryParameter)
                {
                    MeasurePointMap map = new MeasurePointMap(structure, mpt.ParameterTypeId);
                    if ((colsCount + map.MeasurePointsPerElement) > maxCols)
                    {
                        needToBuildTable = true;
                    }
                    else
                    {
                        typesForTable.Add(mpt);
                        colsCount += map.MeasurePointsPerElement;
                    }
                }
                if (needToBuildTable || ((i + 1) == structure.TestedParameterTypes.Rows.Count && typesForTable.Count > 0))
                {
                    tablesForAdd.AddRange(BuildPrimaryParametersTable_WithOpenXML(typesForTable.ToArray(), structure, colsCount));
                    typesForTable.Clear();
                    colsCount = 1;
                }
            }
            return tablesForAdd;

        }

        private IEnumerable<OpenXmlElement> BuildPrimaryParametersTable_WithOpenXML(MeasuredParameterType[] measuredParameterTypes, TestedCableStructure structure, int colsCountPerSubTable)
        {
            List<PrimaryParamsTablePage> primaryParamsTablePages = GetPrimaryParamsTablePlan(structure, colsCountPerSubTable);
            List<OpenXmlElement> elements = new List<OpenXmlElement>();
            foreach (var tp in primaryParamsTablePages)
            {
                Table table = InitPrimaryParametersTable(tp);
                table.Append(BuildPrimaryParametersTableHeader(tp, measuredParameterTypes, structure));
                table.Append(BuildMeasureResultRowsOfPrimaryParams(tp, measuredParameterTypes, structure));

                ///Fill Table Before
                elements.Add(table);
                if (!tp.IsLastTablePage) elements.Add(BreakePage());
                /*
                Debug.WriteLine($"-----------------------------------------------");
                Debug.WriteLine($"PageIndex = {tp.TablePageIndex}");
                Debug.WriteLine($"StartElementIndex = {tp.StartElementIndex}");
                Debug.WriteLine($"EndElementIndex = {tp.EndElementIndex}");
                Debug.WriteLine($"RowsPerSubTableNum = {tp.RowsPerSubTableNum}");
                Debug.WriteLine($"LastSubTableRowsNum = {tp.LastSubTableRowsNum}");
                Debug.WriteLine($"SubTablesCount = {tp.SubTablesCount}");
                Debug.WriteLine($"IsLastTablePage = {tp.IsLastTablePage}");
                Debug.WriteLine($"ColsPerSubTableNum = {tp.ColsPerSubTableNum}");
                */
            }
            return elements;
        }

        private IEnumerable<OpenXmlElement> BuildMeasureResultRowsOfPrimaryParams(PrimaryParamsTablePage tp, MeasuredParameterType[] measuredParameterTypes, TestedCableStructure structure)
        {
            List<OpenXmlElement> rowElements = new List<OpenXmlElement>();
            int elementsNumber = tp.EndElementIndex - tp.StartElementIndex;
            int elementsPerTableColumn = tp.RowsPerSubTableNum - 2;
            int lastTableElementsNum = tp.LastSubTableRowsNum - 2;
            for (int rowIdx = 0; rowIdx < lastTableElementsNum; rowIdx++)
            {
                TableRow row = new TableRow() { RsidTableRowMarkRevision = "00586296", RsidTableRowAddition = "00C644DE", RsidTableRowProperties = "001409DE" };
                row.Append(BuildTableRowPropertiesForHeaderRows());
                for (int subTableIdx = 0; subTableIdx < tp.SubTablesCount; subTableIdx++)
                {
                    bool isLastTableFill = rowIdx >= elementsPerTableColumn;
                    if (isLastTableFill && tp.SubTablesCount - 1 != subTableIdx)
                    {
                        TableCell cell = BuildCell();
                        TableCellBorders style = BuildBordersStyle(0, 0, 0, 0);
                        SetCellBordersStyle(cell, style);
                        row.Append(cell);
                        foreach (MeasuredParameterType pType in measuredParameterTypes)
                        {
                            MeasurePointMap map = new MeasurePointMap(structure, pType.ParameterTypeId);
                            for (int measIdx = 0; measIdx < map.MeasurePointsPerElement; measIdx++)
                            {
                                TableCell cellMeasParam = BuildCell();
                                TableCellBorders bordStyle = BuildBordersStyle(0, 0, 0, 0);
                                SetCellBordersStyle(cellMeasParam, bordStyle);
                                row.Append(cellMeasParam);
                            }
                        }
                    }
                    else
                    {
                        int elIndex = rowIdx + subTableIdx * elementsPerTableColumn;
                        bool isMaxValueRow = elIndex == elementsNumber + 1;
                        bool isAverageValueRow = elIndex == elementsNumber + 2;
                        bool isMinValueRow = elIndex == elementsNumber + 3;
                        bool drawBottomBorder = (rowIdx == elementsPerTableColumn - 1 && subTableIdx < tp.SubTablesCount - 1) || (rowIdx == lastTableElementsNum - 1 && subTableIdx == tp.SubTablesCount - 1);
                        if (isMaxValueRow || isAverageValueRow || isMinValueRow)
                        {
                            string cellNumName = string.Empty;
                            if (isMaxValueRow) cellNumName = "max";
                            else if (isAverageValueRow) cellNumName = "сред.";
                            else if (isMinValueRow) cellNumName = "min";
                            TableCell cellNumber = BuildCell(AddRun(cellNumName, MSWordStringTypes.Typical, false, true));
                            TableCellBorders style = BuildBordersStyle(isMaxValueRow ? (uint)2 : 0, drawBottomBorder ? (uint)2 : 0, 2, 2);
                            SetCellBordersStyle(cellNumber, style);
                            row.Append(cellNumber);
                            foreach (MeasuredParameterType pType in measuredParameterTypes)
                            {
                                TestedStructureMeasuredParameterData pData = structure.GetMeasureParameterDatasByParameterType(pType.ParameterTypeId)[0];
                                MeasurePointMap map = new MeasurePointMap(structure, pType.ParameterTypeId);
                                TableCell statCell;
                                float value = -1;
                                if (isMaxValueRow) value = pData.MaxResultValue;
                                else if (isMinValueRow) value = pData.MinResultValue;
                                else if (isAverageValueRow) value = pData.AverageResultValue;

                                statCell = BuildCell(value.ToString());
                                List<TableCell> cells = new List<TableCell>() { statCell };
                                TableCellBorders styleOfValueCell = BuildBordersStyle(isMaxValueRow ? (uint)2 : 0, (drawBottomBorder ? (uint)2 : 0), 2, 2);

                                SetCellBordersStyle(statCell, styleOfValueCell);
                                for (int measIdx = 1; measIdx < map.MeasurePointsPerElement; measIdx++) cells.Add(BuildCell());
                                if (map.MeasurePointsPerElement > 1) HorizontalMergeCells(cells.ToArray());
                                row.Append(cells);
                            }
                        }
                        else
                        {
                            elIndex = elIndex + tp.StartElementIndex;
                            int elNumber = (int)structure.TestedElements[elIndex];
                            TableCell cellNumber = BuildCell($"{structure.TestedElements[elIndex]}");
                            TableCellBorders style = BuildBordersStyle(0, (drawBottomBorder ? (uint)2 : 0), 2, 2);
                            SetCellBordersStyle(cellNumber, style);
                            row.Append(cellNumber);
                            foreach (MeasuredParameterType pType in measuredParameterTypes)
                            {
                                MeasurePointMap map = new MeasurePointMap(structure, pType.ParameterTypeId);
                                for (int measIdx = 0; measIdx < map.MeasurePointsPerElement; measIdx++)
                                {
                                    TableCell cellValue = BuildCell(new Run[] { GetTestResult(elNumber, measIdx+1, (int)pType.ParameterTypeId, structure) });
                                    TableCellBorders styleOfValueCell = BuildBordersStyle(0, (drawBottomBorder ? (uint)2 : 0), 2, 2);
                                    SetCellBordersStyle(cellValue, styleOfValueCell);
                                    row.Append(cellValue);
                                }
                            }
                        }

                    }

                }
                rowElements.Add(row);
            }
            return rowElements;
        }

        private Run GetTestResult(int element_number, int measure_number, int parameter_type_id, TestedCableStructure structure)
        {
            string v = string.Empty;
            Run resultRun = null; 
            if (structure.AffectedElements.ContainsKey(element_number))
            {
                resultRun = AddRun(LeadTestStatus.StatusTitle_Short(structure.AffectedElements[element_number]).ToString(), MSWordStringTypes.Typical, true, true);
            }else
            {
                TestedStructureMeasuredParameterData data = ((TestedStructureMeasuredParameterData[])structure.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {parameter_type_id}"))[0];
                CableTestResult result = data.GetResult((uint)element_number, (uint)measure_number);
                if (result == null) resultRun = AddRun("-", MSWordStringTypes.Typical, true);
                else
                {
                    resultRun = AddRun(result.Result.ToString(), MSWordStringTypes.Typical, !result.IsOnNorma);
                }
            }
            return resultRun;
        }

        private IEnumerable<OpenXmlElement> BuildPrimaryParametersTableHeader(PrimaryParamsTablePage tp, MeasuredParameterType[] measuredParameterTypes, TestedCableStructure structure)
        {
            TableRow row1 = new TableRow() { RsidTableRowMarkRevision = "00586296", RsidTableRowAddition = "00C644DE", RsidTableRowProperties = "001409DE" };
            TableRow row2 = new TableRow() { RsidTableRowMarkRevision = "00586296", RsidTableRowAddition = "00C644DE", RsidTableRowProperties = "001409DE" };
            row1.Append(BuildTableRowPropertiesForHeaderRows());
            row2.Append(BuildTableRowPropertiesForHeaderRows());
            for (int subTableIdx = 0; subTableIdx < tp.SubTablesCount; subTableIdx++)
            {
                List<TableCell> elNumCells = BuildElementNumberHeaderCells(structure);
                row1.Append(elNumCells.First());
                row2.Append(elNumCells.Last());

                foreach(MeasuredParameterType pType in measuredParameterTypes)
                {
                    List<List<TableCell>> measureParamCells = BuildMeasureParameterHeaderCells(pType, structure);
                    row1.Append(measureParamCells.First());
                    row2.Append(measureParamCells.Last());
                }
            }
            return new List<OpenXmlElement>() { row1, row2 };
        }

        private List<List<TableCell>> BuildMeasureParameterHeaderCells(MeasuredParameterType pType, TestedCableStructure structure)
        {
            MeasurePointMap map = new MeasurePointMap(structure, pType.ParameterTypeId);
            List<TableCell> parameterTitleCells = BuildParameterTitleCells(pType, structure, map.MeasurePointsPerElement);
            List<TableCell> elementMeasureNumberCells = BuildElementMeasureNumberCells(pType, map.MeasurePointsPerElement);
            return new List<List<TableCell>>() { parameterTitleCells, elementMeasureNumberCells };
        }

        private List<TableCell> BuildElementMeasureNumberCells(MeasuredParameterType pType, int measurePointsPerElement)
        {
            List<TableCell> cells = new List<TableCell>();
            if (measurePointsPerElement > 1)
            {
                for (int i = 0; i < measurePointsPerElement; i++)
                {
                    Paragraph p = BuildParagraph();
                    p.Append(AddRun($"{i + 1}"));

                    cells.Add(BuildCell(p));
                }
            }else
            {
                TableCell tableCell4 = new TableCell();
                TableCellProperties tableCellProperties4 = new TableCellProperties();
                VerticalMerge verticalMerge5 = new VerticalMerge();
                tableCellProperties4.Append(verticalMerge5);
                tableCell4.Append(tableCellProperties4);
                tableCell4.Append(BuildParagraph());
                cells.Add(tableCell4);
            }

            return cells;
        }

        private List<TableCell> BuildParameterTitleCells(MeasuredParameterType pType, TestedCableStructure structure, int measurePointsPerElement)
        {
            TableCell tableCell13 = new TableCell();

            TableCellProperties tableCellProperties13 = new TableCellProperties();
            TableCellWidth tableCellWidth13 = new TableCellWidth() { Width = $"{ResultCellWidth*measurePointsPerElement}", Type = TableWidthUnitValues.Dxa };
            

            TableCellMargin tableCellMargin11 = new TableCellMargin();
            LeftMargin leftMargin11 = new LeftMargin() { Width = "28", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin11 = new RightMargin() { Width = "28", Type = TableWidthUnitValues.Dxa };

            tableCellMargin11.Append(leftMargin11);
            tableCellMargin11.Append(rightMargin11);
            TableCellVerticalAlignment tableCellVerticalAlignment11 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties13.Append(tableCellWidth13);
            if (measurePointsPerElement > 1)
            {
                GridSpan gridSpan7 = new GridSpan() { Val = measurePointsPerElement };
                tableCellProperties13.Append(gridSpan7);
            }else
            {
                VerticalMerge verticalMerge1 = new VerticalMerge() { Val = MergedCellValues.Restart };
                tableCellProperties13.Append(verticalMerge1);
            }

            tableCellProperties13.Append(tableCellMargin11);
            tableCellProperties13.Append(tableCellVerticalAlignment11);

            tableCell13.Append(tableCellProperties13);
            tableCell13.Append(ParameterNameText(pType, ((TestedStructureMeasuredParameterData[])structure.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {pType.ParameterTypeId}"))[0].ResultMeasure));
            //tableCell13.Append(paragraph30);
            return new List<TableCell>() { tableCell13 };
        }

        private List<TableCell> BuildElementNumberHeaderCells(TestedCableStructure structure)
        {
            TableCell tableCell3 = new TableCell();

            TableCellProperties tableCellProperties3 = new TableCellProperties();
            TableCellWidth tableCellWidth3 = new TableCellWidth() { Width = ResultCellWidth.ToString(), Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge1 = new VerticalMerge() { Val = MergedCellValues.Restart };

            TableCellMargin tableCellMargin1 = new TableCellMargin();
            LeftMargin leftMargin1 = new LeftMargin() { Width = "28", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin1 = new RightMargin() { Width = "28", Type = TableWidthUnitValues.Dxa };

            tableCellMargin1.Append(leftMargin1);
            tableCellMargin1.Append(rightMargin1);
            TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties3.Append(tableCellWidth3);
            tableCellProperties3.Append(verticalMerge1);
            tableCellProperties3.Append(tableCellMargin1);
            tableCellProperties3.Append(tableCellVerticalAlignment1);

            Paragraph paragraph12 = BuildParagraph();

            Run run11 = new Run() { RsidRunProperties = "00586296" };

            RunProperties runProperties2 = new RunProperties();
            FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "18" };

            runProperties2.Append(fontSizeComplexScript2);
            Text text11 = new Text();
            text11.Text = $"№ {BindingTypeText(structure.StructureType.StructureLeadsAmount)}";

            run11.Append(runProperties2);
            run11.Append(text11);

            paragraph12.Append(run11);

            tableCell3.Append(tableCellProperties3);
            tableCell3.Append(paragraph12);


            TableCell tableCell15 = new TableCell();

            TableCellProperties tableCellProperties15 = new TableCellProperties();
            TableCellWidth tableCellWidth15 = new TableCellWidth() { Width = ResultCellWidth.ToString(), Type = TableWidthUnitValues.Dxa };
            VerticalMerge verticalMerge5 = new VerticalMerge();

            TableCellBorders tableCellBorders1 = new TableCellBorders();
            BottomBorder bottomBorder2 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableCellBorders1.Append(bottomBorder2);

            TableCellMargin tableCellMargin13 = new TableCellMargin();
            LeftMargin leftMargin13 = new LeftMargin() { Width = "28", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin13 = new RightMargin() { Width = "28", Type = TableWidthUnitValues.Dxa };

            tableCellMargin13.Append(leftMargin13);
            tableCellMargin13.Append(rightMargin13);
            TableCellVerticalAlignment tableCellVerticalAlignment13 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            tableCellProperties15.Append(tableCellWidth15);
            tableCellProperties15.Append(verticalMerge5);
            tableCellProperties15.Append(tableCellBorders1);
            tableCellProperties15.Append(tableCellMargin13);
            tableCellProperties15.Append(tableCellVerticalAlignment13);

            Paragraph paragraph33 = BuildParagraph();

            tableCell15.Append(tableCellProperties15);
            tableCell15.Append(paragraph33);


            return new List<TableCell>() { tableCell3, tableCell15 };
        }

        private Table InitPrimaryParametersTable(PrimaryParamsTablePage tp)
        {
            Table table2 = new Table();

            TableProperties tableProperties = new TableProperties();
            TableStyle tableStyle = new TableStyle() { Val = "a3" };
            TableWidth tableWidth = new TableWidth() { Width = (tp.SubTablesCount * tp.ColsPerSubTableNum * ResultCellWidth).ToString(), Type = TableWidthUnitValues.Dxa };
            TableJustification tableJustification = new TableJustification() { Val = TableRowAlignmentValues.Left };
           
            TableCellMarginDefault tableCellMarginDefault2 = new TableCellMarginDefault();
            TableCellLeftMargin tableCellLeftMargin2 = new TableCellLeftMargin() { Width = 28, Type = TableWidthValues.Dxa };
            TableCellRightMargin tableCellRightMargin2 = new TableCellRightMargin() { Width = 28, Type = TableWidthValues.Dxa };

            tableCellMarginDefault2.Append(tableCellLeftMargin2);
            tableCellMarginDefault2.Append(tableCellRightMargin2);
            TableLook tableLook2 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };

            tableProperties.Append(tableJustification);
            tableProperties.Append(tableStyle);
            tableProperties.Append(tableWidth);
            tableProperties.Append(tableCellMarginDefault2);
            tableProperties.Append(tableLook2);

            TableGrid tableGrid = new TableGrid();
            for (int i = 0; i < tp.SubTablesCount * tp.ColsPerSubTableNum; i++)
            {
                GridColumn gc = new GridColumn() { Width = ResultCellWidth.ToString()};
                tableGrid.Append(gc);
            }

            table2.Append(tableProperties);
            table2.Append(tableGrid);
            return table2;
        }

        private List<PrimaryParamsTablePage> GetPrimaryParamsTablePlan(TestedCableStructure structure, int colsCountPerSubTable)
        {
            List<PrimaryParamsTablePage> list = new List<PrimaryParamsTablePage>();
            int subTablesNum = GetSubPrimaryParamTablesNum(structure, colsCountPerSubTable);
            int startIndex = 0;
            int headerRowsAmount = 2;
            int statRowsAmount = 3;
            int maxElementsPerPage = (MaxRowsPerPage - headerRowsAmount-statRowsAmount)*subTablesNum;
            int tablePagesNum = structure.TestedElements.Length / maxElementsPerPage;
            if (structure.TestedElements.Length % maxElementsPerPage > 0) tablePagesNum++;
            if (structure.TestedElements.Length % maxElementsPerPage < subTablesNum) maxElementsPerPage--;

            for (int i = 0; i < tablePagesNum; i++)
            {
                bool isLastTable = i == (tablePagesNum - 1);
                PrimaryParamsTablePage tPlan = new PrimaryParamsTablePage() { SubTablesCount = subTablesNum, IsLastTablePage = isLastTable, ColsPerSubTableNum = colsCountPerSubTable, TablePageIndex = i };
                int elsNumOnPage = isLastTable ? structure.TestedElements.Length - startIndex : maxElementsPerPage;
                tPlan.StartElementIndex = startIndex;
                tPlan.EndElementIndex = startIndex + elsNumOnPage - 1;
                tPlan.RowsPerSubTableNum = (elsNumOnPage / subTablesNum) + headerRowsAmount;
                tPlan.LastSubTableRowsNum = isLastTable ? tPlan.RowsPerSubTableNum + elsNumOnPage % subTablesNum + statRowsAmount : tPlan.RowsPerSubTableNum;
                list.Add(tPlan);
                startIndex = tPlan.EndElementIndex + 1;
            }

            return list;
        }

        private int GetSubPrimaryParamTablesNum(TestedCableStructure structure, int colsPerPage)
        {
            int tableNum = MaxColsPerPage / colsPerPage;
            while(tableNum > 1)
            {
                if (tableNum > structure.TestedElements.Length || structure.TestedElements.Length / tableNum < 3)
                {
                    tableNum--;
                }
                else break;
            }
            return tableNum;
   
        }

        private Paragraph BuildHeaderParagraph()
        {
            Paragraph paragraph1 = new Paragraph() { RsidParagraphAddition = "00FC14E5", RsidParagraphProperties = "001477A5", RsidRunAdditionDefault = "004B27DB" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId() { Val = "1" };

            paragraphProperties1.Append(paragraphStyleId1);

            Run run1 = new Run();
            Text text1 = new Text();
            text1.Text = ProtocolTitle;

            run1.Append(text1);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);



            return paragraph1;
        }

        private string BindingTypeText(int els_amount)
        {
            if (els_amount == 1) return "жилы";
            else if (els_amount == 2) return "пары";
            else if (els_amount == 3) return "тр.";
            else if (els_amount == 4) return "чет.";
            else return "пары";
        }

        private List<OpenXmlElement> ParameterNameText(uint parameter_type_id, string measure = null, string default_name = null)
        {
            List<OpenXmlElement> runList = new List<OpenXmlElement>();
            Paragraph measParagraph = BuildParagraph();
            if (string.IsNullOrWhiteSpace(default_name)) default_name = "N/A";
            switch (parameter_type_id)
            {
                case MeasuredParameterType.K12:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("12", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K11:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("11", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K10:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("10", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K9:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("9", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K3:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("3", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K2:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("2", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K1:
                    measParagraph.Append(AddRun("K"));
                    measParagraph.Append(AddRun("1", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Ea:
                    measParagraph.Append(AddRun("E"));
                    measParagraph.Append(AddRun("a", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Cp:
                    measParagraph.Append(AddRun("С"));
                    measParagraph.Append(AddRun("р", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Co:
                    measParagraph.Append(AddRun("С"));
                    measParagraph.Append(AddRun("0", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Rleads:
                    measParagraph.Append(AddRun("R"));
                    measParagraph.Append(AddRun("ж", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Ao:
                    measParagraph.Append(AddRun("A"));
                    measParagraph.Append(AddRun("0", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Az:
                    measParagraph.Append(AddRun("A"));
                    measParagraph.Append(AddRun("з", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.al:
                    measParagraph.Append(AddRun("α"));
                    measParagraph.Append(AddRun("l", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol3:
                    measParagraph.Append(AddRun("R"));
                    measParagraph.Append(AddRun("из", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol4:
                    measParagraph.Append(AddRun("T"));
                    measParagraph.Append(AddRun("из", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.dR:
                    measParagraph.Append(AddRun("ΔR"));
                    break;
                default:

                    measParagraph.Append(AddRun(default_name));
                    break;
            }
            runList.Add(measParagraph);
            if (measure != null)
            {
                
                Paragraph p = BuildParagraph();
                p.Append(AddRun($"{measure}"));
                runList.Add(p);
            }
            return runList;
        }

        private List<OpenXmlElement> ParameterNameText(MeasuredParameterType pType, string measure = null)
        {
            return ParameterNameText(pType.ParameterTypeId, measure, pType.ParameterName);
        }

        private List<OpenXmlElement> ParameterNameText(TestedStructureMeasuredParameterData pData, string measure = null)
        {
            return ParameterNameText(pData.ParameterTypeId, measure, pData.ParameterName);
        }



    }

    struct PrimaryParamsTablePage
    {
        public int SubTablesCount;
        public int ColsPerSubTableNum;
        public int RowsPerSubTableNum;
        public int LastSubTableRowsNum;
        public int StartElementIndex;
        public int EndElementIndex;
        public bool IsLastTablePage;
        public int TablePageIndex;
    }
}
