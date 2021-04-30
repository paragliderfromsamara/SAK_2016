using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;
using NormaLib.DBControl.Tables;
using NormaLib.DBControl;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace NormaLib.Measure
{
    public class CableTestIni
    {
        const string draft_name = @"cable_test_draft.ini";
        public EventHandler OnDraftLocked;

        CableTest cable_test;
        private CableTest cableTest
        {
            get
            {
                if (cable_test == null)
                {
                    BuildTestFromFile();
                }
                return cable_test;
            }
        }

        public Cable SourceCable
        {
            get
            {
                return cableTest.SourceCable;
            }set
            {
                cableTest.SourceCable = value;
                FillCableToFile(cableTest.SourceCable);
            }
        }

        IniFile file;

        public CableTestIni(int _test_line_number = -1)
        {
            file = new IniFile(draft_name);
            TestLineNumber = _test_line_number;
        }

        public void ResetFile()
        {
            int lineId = cableTest.TestLineNumber;
            if (File.Exists(draft_name)) File.Delete(draft_name);
            cable_test = null;
            file = new IniFile(draft_name);
            cableTest.TestLineNumber = lineId; 
        }

        public bool SaveTest(out CableTest test)
        {
            if (cableTest.Save())
            {
                TestedCable cable = TestedCable.create_for_test(cableTest);
                cableTest.NettoWeight = cable.LinearMass > 0 ? cableTest.CableLength * cable.LinearMass / 1000.0f : 0;
                cableTest.BruttoWeight = cableTest.NettoWeight + BarabanTypeWeight;
                if (BarabanTypeId != 0)
                {
                    ReleasedBaraban b = ReleasedBaraban.CreateBaraban(BarabanTypeId, BarabanNumber);
                    cableTest.BarabanId = b.BarabanId;
                }
                //cableTest.TestResults.CleanList();
                Debug.WriteLine(cableTest.TestResults.Rows.Count);
                FillTestResultToStructure(cable, cableTest);
                cableTest.SetFinished();
                test = cableTest;
                return true;
            }
            else
            {
                test = null;
                return false;
            }
        }

        private void FillTestResultToStructure(Cable cable, CableTest test)
        {
            foreach (TestedCableStructure tcs in cable.CableStructures.Rows)
            {
                CableStructure sourceStrucure = GetSourceStructureById(tcs.SourceStructureId);
                List<uint> lst = new List<uint>(sourceStrucure.MeasuredParameterTypes_ids);
                lst.Add(MeasuredParameterType.Calling);
                foreach (var pTypeId in lst)
                {
                    MeasurePointMap mpm = new MeasurePointMap(sourceStrucure, pTypeId);
                    do
                    {
                        MeasurePoint point = mpm.CurrentPoint;
                        MeasuredParameterType mpt = MeasuredParameterType.find_by_parameter_type_id(pTypeId);
                        float value = GetMeasurePointValue((int)point.StructureId, (int)pTypeId, point.PointIndex);
                        if (!float.IsNaN(value))
                        {
                            float temperature = GetMeasurePointTemperature((int)point.StructureId, (int)pTypeId, point.PointIndex);
                            CableTestResult r = test.BuildTestResult(mpt, tcs, (uint)point.ElementNumber, (uint)point.MeasureNumber);
                            r.Temperature = temperature;
                            r.Result = value;
                            test.AddResult(r);
                        }
                        else
                        {
                            Debug.WriteLine(mpt.ParameterName);
                        }
                    } while (mpm.TryGetNextPoint());
                    Debug.WriteLine(test.TestResults.Rows.Count);
                }
            }
        }

        private CableStructure GetSourceStructureById(uint structure_id)
        {
            CableStructure[] strucs = (CableStructure[])cableTest.SourceCable.CableStructures.Select($"{CableStructure.StructureId_ColumnName} = {structure_id}");
            if (strucs.Length > 0) return strucs[0];
            else return null;
        }

        #region TestAttributes
        const string TestAttrs_SectionName = "TestInfo";
        public int TestLineNumber
        {
            get
            {
                return cableTest.TestLineNumber;
            }
            set
            {
                cableTest.TestLineNumber = value;
                file.Write(CableTest.TestLineNumber_ColumnName, value.ToString(), TestAttrs_SectionName);
            }
        }
        public uint OperatorID
        {
            get
            {
                return cableTest.OperatorId;
            }
            set
            {
                cableTest.OperatorId = value;
                file.Write(CableTest.OperatorId_ColumnName, value.ToString(), TestAttrs_SectionName);
            }
        }

        public float TestedCableLength
        {
            get
            {
                return cableTest.CableLength;
            }
            set
            {
                cableTest.CableLength = value;
                file.Write(CableTest.CableLength_ColumnName, cableTest.CableLength.ToString(), TestAttrs_SectionName);
            }
        }


        public bool IsLockDraft
        {
            get
            {
                return cableTest.StatusId != CableTestStatus.NotStarted;
            }
        }

        public uint TestStatus
        {
            get
            {
                return cableTest.StatusId;
            }set
            {
                if (cableTest.StatusId != value)
                {
                    uint was = cableTest.StatusId;
                    cableTest.StatusId = value;
                    file.Write(CableTestStatus.StatusId_ColumnName, value.ToString(), TestAttrs_SectionName);
                    if (was == CableTestStatus.NotStarted)
                    {
                        StartedAt = DateTime.Now;
                        OnDraftLocked?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public float Temperature
        {
            get
            {
                return cableTest.Temperature;
            }
            set
            {
                cableTest.Temperature = value;
                file.Write(CableTest.Temperature_ColumnName, cableTest.Temperature.ToString(), TestAttrs_SectionName);
            }
        }

        public DateTime StartedAt
        {
            get
            {
                return cableTest.StartedAt;
            }
            set
            {
                cableTest.StartedAt = value;
                file.Write(CableTest.TestStartedAt_ColumnName, cableTest.StartedAt.ToString(), TestAttrs_SectionName);
            }
        }

        public uint BarabanTypeId
        {
            get
            {
                return (uint)file.ReadInt(BarabanType.TypeId_ColumnName, TestAttrs_SectionName);
            }
            set
            {
                file.Write(BarabanType.TypeId_ColumnName, value.ToString(), TestAttrs_SectionName);
            }
        }

        public string BarabanNumber
        {
            get
            {
                return file.Read(ReleasedBaraban.BarabanSerialNumber_ColumnName, TestAttrs_SectionName);
            }
            set
            {
                file.Write(ReleasedBaraban.BarabanSerialNumber_ColumnName, value, TestAttrs_SectionName);
            }
        }

        public float BarabanTypeWeight
        {
            get
            {
                return file.ReadFloat(BarabanType.BarabanWeight_ColumnName, TestAttrs_SectionName);
            }
            set
            {
                file.Write(BarabanType.BarabanWeight_ColumnName, value.ToString(), TestAttrs_SectionName);
            }
        }


        #endregion

        public void FillCableTestToFile()
        {
            foreach(DataColumn dc in cableTest.Table.Columns)
            {
                file.Write(dc.ColumnName, cableTest[dc].ToString(), TestAttrs_SectionName);
            }
            if (cableTest.SourceCable != null) FillCableToFile(cableTest.SourceCable);
        }

        public void FillCableToFile(Cable cable)
        {
            foreach(DataColumn dc in cable.Table.Columns)
            {
                file.Write(dc.ColumnName, cable[dc].ToString(), "SourceCable");
            }
            //file.Write(CableTest.CableTestId_ColumnName, "0","SourceCable");
            FillCableStructures(cable.CableStructures);
        }

        private void FillCableStructures(DBEntityTable cableStructures)
        {
            string structSectName = "TestedStructure_{0}";
            int idx = 0;
            foreach(CableStructure cs in cableStructures.Rows)
            {
                string curStructSectName = string.Format(structSectName, idx);
                foreach (DataColumn dc in cs.Table.Columns)
                {
                    file.Write(dc.ColumnName, cs[dc].ToString(), curStructSectName);
                }
                FillMeasuredParameterData(cs.MeasuredParameters, idx);
                //file.WriteUIntArray("measured_params_data_ids", cs.MeasuredParameters_ids, curStructSectName);
                idx++;
            }
            while(file.KeyExists(CableStructure.StructureId_ColumnName, string.Format(structSectName, idx)))
            {
                file.DeleteSection(string.Format(structSectName, idx));
                idx++;
            }
        }

        private void FillMeasuredParameterData(DBEntityTable measured_parameters_data, int structure_idx)
        {
            string params_sect_name = "ParameterData_{0}_of_structure_{1}";
            int param_idx = 0;
            foreach(CableStructureMeasuredParameterData csmpd in measured_parameters_data.Rows)
            {
                string sect_name = string.Format(params_sect_name, param_idx++, structure_idx);
                foreach (DataColumn dc in measured_parameters_data.Columns)
                {
                    file.Write(dc.ColumnName, csmpd[dc].ToString(), sect_name);
                }
            }
            while (file.KeyExists(CableStructure.StructureId_ColumnName, string.Format(params_sect_name, param_idx, structure_idx)))
            {
                file.DeleteSection(string.Format(params_sect_name, param_idx, structure_idx));
                param_idx++;
            }
        }

        private void BuildTestFromFile()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableTest));
            cable_test = t.NewRow() as CableTest;
            try
            {
                cable_test = t.NewRow() as CableTest;
                foreach (DataColumn dc in t.Columns)
                {
                    cable_test[dc] = (string.IsNullOrEmpty(file.Read(dc.ColumnName, TestAttrs_SectionName))) ? GetDefaultVal(dc.DataType) : string.IsNullOrEmpty(file.Read(dc.ColumnName, TestAttrs_SectionName));
                }
                t.Rows.Add(cable_test);
                cable_test.SourceCable = BuildCableFromFile();
            }
            catch(Exception)
            {
                t.Clear();
                cable_test = CableTest.New(t);
                FillCableTestToFile();
            }
        }

        private object GetDefaultVal(Type d_type)
        {
            if (d_type == typeof(DateTime))
            {
                return DateTime.Now;
            }return null;
        }

        public Cable BuildCableFromFile()
        {
            try
            {
                DBEntityTable t = new DBEntityTable(typeof(Cable));
                Cable cable = t.NewRow() as Cable;
                foreach (DataColumn dc in t.Columns)
                {
                    cable[dc] = (object)file.Read(dc.ColumnName, "SourceCable");
                }
                cable.CableStructures = new DBEntityTable(typeof(CableStructure));
                BuildCableStructuresFromIni(cable);
                t.Rows.Add(cable);
                return cable;
            }
            catch(Exception)
            {
                return null;
            }

        }

        private void BuildCableStructuresFromIni(Cable cable)
        {
            //DBEntityTable t = new DBEntityTable(typeof(TestedCableStructure));
            string structSectName = "TestedStructure_{0}";
            int idx = 0;
            while(file.KeyExists(CableStructure.StructureId_ColumnName, string.Format(structSectName, idx)))
            {
                CableStructure s = cable.CableStructures.NewRow() as CableStructure;
                s.OwnCable = cable;
                foreach(DataColumn dc in cable.CableStructures.Columns)
                {
                    s[dc] = (object)file.Read(dc.ColumnName, string.Format(structSectName, idx));
                }
                s.MeasuredParameters = new DBEntityTable(typeof(CableStructureMeasuredParameterData));
                BuildMeasureParametersData(s, idx);
                cable.CableStructures.Rows.Add(s);
                idx++;
            }
        }


        private void BuildMeasureParametersData(CableStructure structure, int structure_idx)
        {
            string params_sect_name = "ParameterData_{0}_of_structure_{1}";
            int param_idx = 0;
            string sect_name;
            while (file.KeyExists(MeasuredParameterType.ParameterTypeId_ColumnName, sect_name = string.Format(params_sect_name, param_idx++, structure_idx)))
            {
                CableStructureMeasuredParameterData csmpd = structure.MeasuredParameters.NewRow() as CableStructureMeasuredParameterData;
                foreach (DataColumn dc in structure.MeasuredParameters.Columns)
                {
                    csmpd[dc] = file.Read(dc.ColumnName, sect_name);
                }
                csmpd.AssignedStructure = structure;
                structure.MeasuredParameters.Rows.Add(csmpd);
            }
            FillAffectedElementOnStructure(structure);
        }


        #region TestResults on structure

        const string TestResults_SectionNameMask = "TestResults_Of_Structure_{0}";
        const string TestResultValue_AttrNameMask = "<value_of_{0}:{1}>"; // 0 - id параметра, 1 - id точки
        const string TestResultTemperature_AttrNameMask = "<temperature_of_{0}:{1}>"; // 0 - id параметра, 1 - id точки

        string GetTestResultSectionName(int structure_id) => string.Format(TestResults_SectionNameMask, structure_id) ;
        string GetTestValueAttrName(int parameter_type_id, int point) => string.Format(TestResultValue_AttrNameMask, parameter_type_id, point);
        string GetTestTemperatureAttrName(int parameter_type_id, int point) => string.Format(TestResultTemperature_AttrNameMask, parameter_type_id, point);




        public float GetMeasurePointValue(int structure_id, int parameter_type_id, int point)
        {
            string section = GetTestResultSectionName(structure_id);
            string valueAttrName = GetTestValueAttrName(parameter_type_id, point);
            if (file.KeyExists(valueAttrName, section))
            {
                return file.ReadFloat(valueAttrName, section);
            }
            else
                return float.NaN;
        }

        public float GetMeasurePointTemperature(int structure_id, int parameter_type_id, int point)
        {
            string section = GetTestResultSectionName(structure_id);
            string temperatureAttrName = GetTestTemperatureAttrName(parameter_type_id, point);
            if (file.KeyExists(temperatureAttrName, section))
            {
                return file.ReadFloat(temperatureAttrName, section);
            }
            else
                return 20f;
        }

        public void SetMeasurePointValue(MeasurePoint point, float value = 0, float temperature = -1)
        {
            string section = GetTestResultSectionName((int)point.StructureId);
            string temperatureAttrName = GetTestTemperatureAttrName((int)point.ParameterTypeId, point.PointIndex);
            string valueAttrName = GetTestValueAttrName((int)point.ParameterTypeId, point.PointIndex);
            file.Write(valueAttrName, value.ToString(), section);
            SetMeasureStarted();
            //cableTest.BuildTestResult();

            if (temperature >= 5 && temperature <= 35) file.Write(temperatureAttrName, temperature.ToString(), section);
        }

        public void SetLeadStatusOfPoint(MeasurePoint point, int lead_status_id)
        {
            string section = GetTestResultSectionName((int)point.StructureId);
            string valueAttrName = GetTestValueAttrName((int)MeasuredParameterType.Calling, point.PointIndex);
            SetMeasureStarted();
            if (lead_status_id == LeadTestStatus.Ok) file.DeleteKey(valueAttrName, section);
            else file.Write(valueAttrName, lead_status_id.ToString(), section);
        }

        private void SetMeasureStarted()
        {
            TestStatus = CableTestStatus.Started;
        }

        private void FillAffectedElementOnStructure(CableStructure structure)
        {
            string section = GetTestResultSectionName((int)structure.CableStructureId);
            MeasurePointMap point_map = new MeasurePointMap(structure, MeasuredParameterType.Calling);
            Dictionary<int, uint> affected_elements = new Dictionary<int, uint>();
            do
            {
                string val_attr_name;
                if (affected_elements.ContainsKey(point_map.CurrentElementNumber)) continue;
                val_attr_name = GetTestValueAttrName((int)MeasuredParameterType.Calling, point_map.CurrentPoint.PointIndex);
                if (file.KeyExists(val_attr_name, section))
                {
                    uint sts = (uint)file.ReadInt(val_attr_name, section);
                    if (sts != LeadTestStatus.Ok)
                    {
                        affected_elements.Add(point_map.CurrentElementNumber, sts);
                    }
                }
            } while (point_map.TryGetNextPoint());
            structure.AffectedElements = affected_elements;
        }

        public void ClearParameterDataResults(CableStructureMeasuredParameterData currentParameterData)
        {
            MeasurePointMap map = new MeasurePointMap(currentParameterData.AssignedStructure, currentParameterData.ParameterTypeId);
            do
            {
                ClearMeasurePointValue(map.CurrentPoint);
            } while (map.TryGetNextPoint());
        }

        public void ClearMeasurePointValue(MeasurePoint currentPoint)
        {
            string section = GetTestResultSectionName((int)currentPoint.StructureId);
            string val_attr_name;
            string temperatureAttrName;
            temperatureAttrName = GetTestTemperatureAttrName((int)currentPoint.ParameterTypeId, currentPoint.PointIndex);
            val_attr_name = GetTestValueAttrName((int)currentPoint.ParameterTypeId, currentPoint.PointIndex);
            file.DeleteKey(val_attr_name, section);
            file.DeleteKey(temperatureAttrName, section);
        }

        public bool HasMeasurePointValue(MeasurePoint point)
        {
            string section = GetTestResultSectionName((int)point.StructureId);
            string val_attr_name = GetTestValueAttrName((int)point.ParameterTypeId, point.PointIndex);
            return file.KeyExists(val_attr_name, section);
        }

        #endregion

        #region TestStats 

        private CableTest virtual_test = null;
        private CableTest virtualTest => virtual_test == null ? virtual_test = BuildVirtualTest() : virtual_test;

        private CableTest BuildVirtualTest()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableTest));
            virtual_test = t.NewRow() as CableTest;
            try
            {
                virtual_test = t.NewRow() as CableTest;
                foreach (DataColumn dc in t.Columns)
                {
                    virtual_test[dc] = (string.IsNullOrEmpty(file.Read(dc.ColumnName, TestAttrs_SectionName))) ? GetDefaultVal(dc.DataType) : string.IsNullOrEmpty(file.Read(dc.ColumnName, TestAttrs_SectionName));
                }
                t.Rows.Add(virtual_test);
                virtual_test.TestedCable = BuildTestedCableFromFile();
                return virtual_test;
            }
            catch (Exception)
            {
                t.Clear();
                virtual_test = CableTest.New(t);
                return virtual_test;
                //FillCableTestToFile();
            }
        }

        private TestedCable BuildTestedCableFromFile()
        {
            //try
            //{
                DBEntityTable t = new DBEntityTable(typeof(TestedCable));
                TestedCable cable = t.NewRow() as TestedCable;
                foreach (DataColumn dc in t.Columns)
                {
                    cable[dc] = (object)file.Read(dc.ColumnName, "SourceCable");
                }
                cable.CableStructures = new DBEntityTable(typeof(CableStructure));
                cable.SetCableTest(virtualTest);
                BuildVirtualCableStructuresFromIni(cable);
                t.Rows.Add(cable);
                return cable;
          //  }
         //   catch (Exception)
       //     {
      //          return null;
     //       }
//
        }


        public CableTestStat GetCablleTestStat()
        {
            CableTest test = BuildVirtualTest();
            CableTestStat stat = new CableTestStat();
            stat.StructuresNormalPercents = new Dictionary<uint, StructureTestStat>();
            if (test.TestedCable != null)
            {
                stat.IsOnNorma = test.TestedCable.IsOnNorma;
                stat.CableNormalPercent = test.TestedCable.NormalElementsPercent;

                foreach (TestedCableStructure s in test.TestedCable.CableStructures.Rows)
                {
                    StructureTestStat ss = new StructureTestStat();
                    ss.StructureNormalPercent = s.NormalElementPercent;
                    ss.MeasuredParameterDataNormalPercents = new Dictionary<uint, double>();
                    foreach (TestedStructureMeasuredParameterData td in s.MeasuredParameters.Rows)
                    {
                        if (!s.TestedParametersIds.Contains(td.ParameterTypeId))
                            ss.MeasuredParameterDataNormalPercents.Add(td.MeasuredParameterDataId, 0);
                        else
                        {
                            ss.MeasuredParameterDataNormalPercents.Add(td.MeasuredParameterDataId, td.MeasuredPercent);
                        }
                    }
                    ///
                    stat.StructuresNormalPercents.Add(s.CableStructureId, ss);
                }
            }

            return stat;
        }

        //public bool TestedCableIsOnNorma => virtualTest.TestedCable == null ? false : virtualTest.TestedCable.IsOnNorma;

        private void BuildVirtualCableStructuresFromIni(TestedCable cable)
        {
            //DBEntityTable t = new DBEntityTable(typeof(TestedCableStructure));
            string structSectName = "TestedStructure_{0}";
            int idx = 0;
            while (file.KeyExists(CableStructure.StructureId_ColumnName, string.Format(structSectName, idx)))
            {
                TestedCableStructure s = cable.CableStructures.NewRow() as TestedCableStructure;
                s.OwnCable = cable;
                foreach (DataColumn dc in cable.CableStructures.Columns)
                {
                    s[dc] = (object)file.Read(dc.ColumnName, string.Format(structSectName, idx));
                }
                s.MeasuredParameters = new DBEntityTable(typeof(TestedStructureMeasuredParameterData));
                BuildVirtualMeasureParametersData(s, idx);
                cable.CableStructures.Rows.Add(s);
                idx++;
            }
        }


        private void BuildVirtualMeasureParametersData(CableStructure structure, int structure_idx)
        {
            string params_sect_name = "ParameterData_{0}_of_structure_{1}";
            int param_idx = 0;
            string sect_name;
            while (file.KeyExists(MeasuredParameterType.ParameterTypeId_ColumnName, sect_name = string.Format(params_sect_name, param_idx++, structure_idx)))
            {
                TestedStructureMeasuredParameterData csmpd = structure.MeasuredParameters.NewRow() as TestedStructureMeasuredParameterData;
                foreach (DataColumn dc in structure.MeasuredParameters.Columns)
                {
                    csmpd[dc] = file.Read(dc.ColumnName, sect_name);
                }
                csmpd.AssignedStructure = structure;
                structure.MeasuredParameters.Rows.Add(csmpd);
            }
            FillAffectedElementOnStructure(structure);
        }


        
        #endregion
    }

    public class CableTestIniException : Exception
    {
        public CableTestIniException(string message) : base(message)
        {

        }

    }

    public struct CableTestStat
    {
        public bool IsOnNorma;
        public double CableNormalPercent;
        public Dictionary<uint, StructureTestStat> StructuresNormalPercents;
    }

    public struct StructureTestStat
    {
        public double StructureNormalPercent;
        public Dictionary<uint, double> MeasuredParameterDataNormalPercents;
    }

}
