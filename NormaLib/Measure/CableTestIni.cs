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

        public bool SaveTest()
        {
            if (cableTest.Save())
            {
                TestedCable cable = TestedCable.create_for_test(cableTest);
                foreach(TestedCableStructure tcs in cable.CableStructures.Rows)
                {
                    CableStructure sourceStrucure = GetSourceStructureById(tcs.SourceStructureId);
                    List<uint> lst = new List<uint>(sourceStrucure.MeasuredParameterTypes_ids);
                    lst.Add(MeasuredParameterType.Calling);
                    foreach(var pTypeId in lst)
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
                                CableTestResult r = cableTest.BuildTestResult(mpt, tcs, (uint)point.ElementNumber, (uint)point.MeasureNumber);
                                r.Temperature = temperature;
                                r.Result = value;
                                cableTest.TestResults.Add(r);
                            }
                        } while (mpm.TryGetNextPoint());
                        Debug.WriteLine(cableTest.TestResults.Count);
                    }
                    cableTest.SetFinished();
                }
                return true;
            }
            else return false;
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
                file.Write(CableTest.CableLength_ColumnName, value.ToString(), TestAttrs_SectionName);
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
                    cable_test[dc] = file.Read(dc.ColumnName, TestAttrs_SectionName);
                }
                t.Rows.Add(cable_test);
                cable_test.SourceCable = BuildCableFromFile();
            }
            catch
            {
                cable_test = CableTest.New(t);
                FillCableTestToFile();
            }
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
            //cableTest.BuildTestResult();

            if (temperature >= 5) file.Write(temperatureAttrName, temperature.ToString(), section);
        }

        public void SetLeadStatusOfPoint(MeasurePoint point, int lead_status_id)
        {
            string section = GetTestResultSectionName((int)point.StructureId);
            string valueAttrName = GetTestValueAttrName((int)MeasuredParameterType.Calling, point.PointIndex);
            file.Write(valueAttrName, lead_status_id.ToString(), section);
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

        #endregion

    }

    public class CableTestIniException : Exception
    {
        public CableTestIniException(string message) : base(message)
        {

        }

    }

}
