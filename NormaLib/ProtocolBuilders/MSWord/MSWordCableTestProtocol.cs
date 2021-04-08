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
namespace NormaLib.ProtocolBuilders.MSWord
{
    internal class MSWordCableTestProtocol : MSWordProtocol
    {
        private CableTest cableTest;

        public MSWordCableTestProtocol(CableTest test, string path)
        {
            filePath = path + @"\" + $"Протокол {test.TestId}.docx";
            cableTest = test;
        }

        protected override Body BuildBody()
        {
            Body body = base.BuildBody();
            if (!string.IsNullOrWhiteSpace(ProtocolTitle))
            {
                body.Append(BuildHeaderParagraph());
                body.Append(BuildTableCellParagraph(JustificationValues.Left));
            }
            foreach (TestedCableStructure s in cableTest.TestedCable.CableStructures.Rows)
            {
                //body.Append(new Paragraph());
                body.Append(BuildTableCellParagraph(JustificationValues.Left));
                body.Append(BuildStructureData(s));
            }
            return body;
        }

        private IEnumerable<OpenXmlElement> BuildStructureData(TestedCableStructure structure)
        {
            List<OpenXmlElement> els = new List<OpenXmlElement>();
            //wordProtocol.CurrentFontSize = wordProtocol.FontSize;
            els.AddRange(BuildPrimaryParametersTable(structure));
            //addRizolByGroupTable(structure);
            //add_al_Table(structure);
            //add_AoAz_Table(structure, Tables.MeasuredParameterType.Ao);
            //add_AoAz_Table(structure, Tables.MeasuredParameterType.Az);
            //add_Statistic_Table(structure);
            //wordProtocol.CurrentFontSize = 11f;
            //add_VSVI_TestResult(structure);
            //add_StructElements_Conclusion(structure);
            return els;
        }


        private IEnumerable<OpenXmlElement> BuildPrimaryParametersTable(TestedCableStructure structure)
        {
            List<OpenXmlElement> tablesForAdd = new List<OpenXmlElement>();
            uint[] tst = structure.TestedElements;
            Debug.WriteLine($"{(structure.OwnCable as TestedCable).CableTest.TestResults.Count}");
            int maxCols = MaxColsPerPage;
            int colsCount = 1; //Первая колонка номер элемента
            bool progrBarIsInited = false;

            List<MeasuredParameterType> typesForTable = new List<MeasuredParameterType>();

            for (int i = 0; i < structure.TestedParameterTypes.Rows.Count; i++)
            {
                Debug.WriteLine($"{cableTest.TestId} - {structure.CableStructureId}");
                TestedStructureMeasuredParameterData mpd = (TestedStructureMeasuredParameterData)structure.MeasuredParameters.Rows[i];
                MeasuredParameterType mpt = (MeasuredParameterType)structure.TestedParameterTypes.Rows[i];
                bool needToBuildTable = false;
                if (mpt.IsPrimaryParameter)
                {
                    //if (!progrBarIsInited)
                    //{
                    //statusPanel.SetBarPosition("Таблица перв. параметров", 10);
                    //    progrBarIsInited = true;
                    //}
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
                            TableCell cellNumber = BuildCell(cellNumName);
                            TableCellBorders style = BuildBordersStyle(isMaxValueRow ? (uint)2 : 0, drawBottomBorder ? (uint)2 : 0, 2, 2);
                            SetCellBordersStyle(cellNumber, style);
                            row.Append(cellNumber);
                            foreach (MeasuredParameterType pType in measuredParameterTypes)
                            {
                                MeasurePointMap map = new MeasurePointMap(structure, pType.ParameterTypeId);
                                TableCell statCell = BuildCell("stat");
                                List<TableCell> cells = new List<TableCell>() { statCell };
                                TableCellBorders styleOfValueCell = BuildBordersStyle(isMaxValueRow ? (uint)2 : 0, (drawBottomBorder ? (uint)2 : 0), 2, 2);
                                SetCellBordersStyle(statCell, styleOfValueCell);

                                for (int measIdx = 1; measIdx < map.MeasurePointsPerElement; measIdx++)
                                {
                                    cells.Add(BuildCell());
                                    //TableCellBorders styleOfValueCell = BuildBordersStyle(isMaxValueRow ? (uint)2 : 0, (drawBottomBorder ? (uint)2 : 0), 2, 2);
                                    //SetCellBordersStyle(cellValue, styleOfValueCell);
                                    //row.Append(cellValue);
                                }
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
            CableTestResult[] results = (CableTestResult[])structure.TestResults.Select($"{CableTestResult.StructElementNumber_ColumnName} = {element_number} AND {CableTestResult.MeasureOnElementNumber_ColumnName} = {measure_number} AND {MeasuredParameterType.ParameterTypeId_ColumnName} = {parameter_type_id}");
            CableTestResult result = results.Length > 0 ? results[0] : null;
            v = result == null ? "-" : result.Result.ToString();
            return AddTableCellRun(v);
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
                    Paragraph p = BuildTableCellParagraph();
                    p.Append(AddTableCellRun($"{i + 1}"));

                    cells.Add(BuildCell(p));
                }
            }else
            {
                TableCell tableCell4 = new TableCell();
                TableCellProperties tableCellProperties4 = new TableCellProperties();
                VerticalMerge verticalMerge5 = new VerticalMerge();
                tableCellProperties4.Append(verticalMerge5);
                tableCell4.Append(tableCellProperties4);
                tableCell4.Append(BuildTableCellParagraph());
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

            Paragraph paragraph12 = BuildTableCellParagraph();

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

            Paragraph paragraph33 = BuildTableCellParagraph();

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
            Paragraph measParagraph = BuildTableCellParagraph();
            if (string.IsNullOrWhiteSpace(default_name)) default_name = "N/A";
            switch (parameter_type_id)
            {
                case MeasuredParameterType.K12:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("12", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K11:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("11", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K10:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("10", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K9:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("9", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K3:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("3", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K2:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("2", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.K1:
                    measParagraph.Append(AddTableCellRun("K"));
                    measParagraph.Append(AddTableCellRun("1", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Ea:
                    measParagraph.Append(AddTableCellRun("E"));
                    measParagraph.Append(AddTableCellRun("a", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Cp:
                    measParagraph.Append(AddTableCellRun("С"));
                    measParagraph.Append(AddTableCellRun("р", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Co:
                    measParagraph.Append(AddTableCellRun("С"));
                    measParagraph.Append(AddTableCellRun("0", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Rleads:
                    measParagraph.Append(AddTableCellRun("R"));
                    measParagraph.Append(AddTableCellRun("ж", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Ao:
                    measParagraph.Append(AddTableCellRun("A"));
                    measParagraph.Append(AddTableCellRun("0", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Az:
                    measParagraph.Append(AddTableCellRun("A"));
                    measParagraph.Append(AddTableCellRun("з", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.al:
                    measParagraph.Append(AddTableCellRun("α"));
                    measParagraph.Append(AddTableCellRun("l", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol3:
                    measParagraph.Append(AddTableCellRun("R"));
                    measParagraph.Append(AddTableCellRun("из", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol4:
                    measParagraph.Append(AddTableCellRun("T"));
                    measParagraph.Append(AddTableCellRun("из", MSWordStringTypes.Subscript));
                    break;
                case MeasuredParameterType.dR:
                    measParagraph.Append(AddTableCellRun("ΔR"));
                    break;
                default:

                    measParagraph.Append(AddTableCellRun(default_name));
                    break;
            }
            runList.Add(measParagraph);
            if (measure != null)
            {
                
                Paragraph p = BuildTableCellParagraph();
                p.Append(AddTableCellRun($"{measure}"));
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
