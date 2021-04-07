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
                body.Append(new Paragraph());
                body.Append(new Paragraph());
            }
            foreach (TestedCableStructure s in cableTest.TestedCable.CableStructures.Rows)
            {
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
            uint[]tst = structure.TestedElements;
            Debug.WriteLine($"{(structure.OwnCable as TestedCable).CableTest.TestResults.Count}");
            int maxCols = MaxColsPerPage;
            int colsCount = 1; //Первая колонка номер элемента
            bool progrBarIsInited = false;
            
            List<MeasuredParameterType> typesForTable = new List<MeasuredParameterType>();

            for (int i = 0; i < structure.TestedParameterTypes.Rows.Count; i++)
            {
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
                    BuildPrimaryParametersTable_WithOpenXML(typesForTable.ToArray(), structure, colsCount);
                    typesForTable.Clear();
                    colsCount = 1;
                }
            }
            return tablesForAdd;

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

    }
}
