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

namespace TeraMicroMeasure
{
    public class CableTestIni
    {
        const string draft_name = @"cable_test_draft.ini";
        public EventHandler OnDraftLocked;

        IniFile file;



        public CableTestIni()
        {
            file = new IniFile(draft_name);
            ClientId = SettingsControl.GetClientId();
        }

        public CableTestIni(Cable cable) : this()
        {
            FillCableToFile(cable);
            //CableID = (int)cable.CableId;
            //buildStructuresInfo(cable.CableStructures);
            //else throw new CableTestIniException("Есть незавершенное испытание!");
        }

        internal void ResetFile()
        {
            if (File.Exists(draft_name)) File.Delete(draft_name);
            file = new IniFile(draft_name);
        }



        #region TestAttributes
        const string TestAttrs_SectionName = "TestInfo";

        const string ClientID_AttrName = "ClientID";
        public int ClientId
        {
            get
            {
                return file.ReadInt(ClientID_AttrName, TestAttrs_SectionName);
            }
            set
            {
                file.Write(ClientID_AttrName, value.ToString(), TestAttrs_SectionName);
            }
        }

        const string UserID_AttrName = "UserID";
        public int UserID
        {
            get
            {
                return file.ReadInt(UserID_AttrName, TestAttrs_SectionName);
            }
            set
            {
                file.Write(UserID_AttrName, value.ToString(), TestAttrs_SectionName);
            }
        }

        const string TestedCableLength_AttrName = "CableLength";
        public int TestedCableLength
        {
            get
            {
                return file.ReadInt(TestedCableLength_AttrName, TestAttrs_SectionName);
            }
            set
            {
                file.Write(TestedCableLength_AttrName, value.ToString(), TestAttrs_SectionName);
            }
        }

        const string IsLockFileFlag_AttrName = "IsLockDraft";
        private bool isLocked = false;
        public bool IsLockDraft
        {
            get
            {
                return file.Read(IsLockFileFlag_AttrName, TestAttrs_SectionName) == "1";
            }
            set
            {
                if (isLocked && value) return;
                isLocked = value;
                file.Write(IsLockFileFlag_AttrName, (value) ? "1" : "0", TestAttrs_SectionName);
                OnDraftLocked?.Invoke(this, new EventArgs());
            }
        }

        #endregion


        public void FillCableToFile(Cable cable)
        {
            foreach(DataColumn dc in cable.Table.Columns)
            {
                file.Write(dc.ColumnName, cable[dc].ToString(), "TestedCable");
            }
            file.Write(CableTest.CableTestId_ColumnName, "0","TestedCable");
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
                file.WriteUIntArray("measured_params_data_ids", cs.MeasuredParameters_ids, curStructSectName);
                idx++;
            }
            while(file.KeyExists(CableStructure.StructureId_ColumnName, string.Format(structSectName, idx)))
            {
                file.DeleteSection(string.Format(structSectName, idx));
                idx++;
            }
        }

        public TestedCable BuildCableFromFile(DBEntityTable measured_parameters_data)
        {
            DBEntityTable t = new DBEntityTable(typeof(TestedCable));
            TestedCable cable = t.NewRow() as TestedCable;
            foreach(DataColumn dc in t.Columns)
            {
                cable[dc] = (object)file.Read(dc.ColumnName, "TestedCable");
            }
            BuildCableStructuresFromIni(cable, measured_parameters_data);
            t.Rows.Add(cable);
            return cable;
        }

        private void BuildCableStructuresFromIni(TestedCable cable, DBEntityTable measured_parameters_data)
        {
            //DBEntityTable t = new DBEntityTable(typeof(TestedCableStructure));
            string structSectName = "TestedStructure_{0}";
            int idx = 0;
            while(file.KeyExists(CableStructure.StructureId_ColumnName, string.Format(structSectName, idx)))
            {
                TestedCableStructure s = cable.CableStructures.NewRow() as TestedCableStructure;
                s.OwnCable = cable;
                foreach(DataColumn dc in cable.CableStructures.Columns)
                {
                    s[dc] = (object)file.Read(dc.ColumnName, string.Format(structSectName, idx));
                }
                s.CableStructureId = (uint)idx + 1;
                uint[] prmsIds = file.ReadUIntArray("measured_params_data_ids", string.Format(structSectName, idx));
                if (prmsIds.Length > 0)
                {
                    MeasuredParameterData[] data = (MeasuredParameterData[])measured_parameters_data.Select($"{MeasuredParameterData.DataId_ColumnName} IN ({string.Join(",", prmsIds)})");
                    if (data.Length > 0)
                    {
                        foreach(MeasuredParameterData dt in data)
                        {
                            TestedStructureMeasuredParameterData tpd = (TestedStructureMeasuredParameterData)s.MeasuredParameters.NewRow();
                            tpd.ParameterTypeId = dt.ParameterTypeId;
                            tpd.AssignedStructure = s;
                            
                            
                            foreach (DataColumn dc in dt.Table.Columns)
                            {
                                tpd[dc.ColumnName] = dt[dc]; 
                            }
                            tpd.CableStructureId = 0;
                            s.MeasuredParameters.Rows.Add(tpd);
                        }
                    }
                }
                cable.CableStructures.Rows.Add(s);
                idx++;
            }
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

        public void SetMeasurePointValue(int structure_id, int parameter_type_id, int point, float value = 0, float temperature = -1)
        {
            string section = GetTestResultSectionName(structure_id);
            string temperatureAttrName = GetTestTemperatureAttrName(parameter_type_id, point);
            string valueAttrName = GetTestValueAttrName(parameter_type_id, point);
            if (!isLocked) IsLockDraft = true;
            file.Write(valueAttrName, value.ToString(), section);
            if (temperature >= 5) file.Write(temperatureAttrName, temperature.ToString(), section);
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
