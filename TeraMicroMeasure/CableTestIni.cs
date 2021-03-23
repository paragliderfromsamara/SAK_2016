using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;
using NormaLib.DBControl.Tables;
using NormaLib.DBControl;
using System.IO;
using System.Diagnostics;

namespace TeraMicroMeasure
{
    public class CableTestIni
    {
        const string draft_name = @"cable_test_draft.ini";
        IniFile file;


        public CableTestIni()
        {
            file = new IniFile(draft_name);
            ClientId = SettingsControl.GetClientId();
        }

        public CableTestIni(Cable cable) : this()
        {
            CableID = (int)cable.CableId;
            buildStructuresInfo(cable.CableStructures);
            //else throw new CableTestIniException("Есть незавершенное испытание!");
        }

        private void buildStructuresInfo(DBEntityTable structures)
        {
            int i = 0;
            foreach(CableStructure s in structures.Rows)
            {
                SetStructureId((int)s.CableStructureId, i);
                SetDisplayedElementAmount((int)s.DisplayedAmount,i);
                SetRealElementAmount((int)s.RealAmount, i);
                SetLeadAmount((int)s.StructureType.StructureLeadsAmount, i);
                Debug.WriteLine($"Количество элементов {s.MeasuredParameterTypes_ids.Length}");
                foreach(uint pt in s.MeasuredParameterTypes_ids)
                {
                    int pointNum = MeasuredParameterType.MeasurePointNumberPerStructureElement(pt, s.StructureType.StructureLeadsAmount) * (int)s.RealAmount;
                    for(int j = 0; j < pointNum; j++)
                    {
                        SetMeasurePointValue((int)s.CableStructureId, (int)pt, j);
                    }
                }
            }
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

        #endregion


        #region CableAttributes
        const string CableAttrs_SectionName = "CableInfo";
        const string CableID_AttrName = "CableID";
        public int CableID
        {
            get
            {
                return file.ReadInt(CableID_AttrName, CableAttrs_SectionName);
            }
            set
            {
                file.Write(CableID_AttrName, value.ToString(), CableAttrs_SectionName);
            }
        }

        #endregion

        #region CableStructures
        const string StructureAttrs_SectionNameMask = "Structure_{0}";

        private string StructureSectionNameByIndex(int idx)
        {
            return String.Format(StructureAttrs_SectionNameMask, idx);
        }

        const string StructureId_AttrName = "StructureId";
        public int GetStrucutureId(int index)
        {
            return file.ReadInt(StructureId_AttrName, StructureSectionNameByIndex(index));
        }
        public void SetStructureId(int value, int index)
        {
            file.Write(StructureId_AttrName, value.ToString(), StructureSectionNameByIndex(index));
        }

        private int StructuresCount
        {
            get
            {
                int i = 0;
                while (StructureExists(i++));
                return i;
            }
        }

        internal void ResetFile()
        {
            if (File.Exists(draft_name)) File.Delete(draft_name);
            file = new IniFile(draft_name);
        }

        private bool StructureExists(int index)
        {
            return file.KeyExists(StructureId_AttrName, StructureSectionNameByIndex(index));
        }

        const string RealElementAmount_AttrName = "RealElementAmount";
        public int GetRealElementAmount(int index)
        {
            return file.ReadInt(RealElementAmount_AttrName, StructureSectionNameByIndex(index));
        }
        public void SetRealElementAmount(int value, int index)
        {
            file.Write(RealElementAmount_AttrName, value.ToString(), StructureSectionNameByIndex(index));
        }

        const string DisplayedElementAmount_AttrName = "DisplayedElementAmount";
        public int GetDisplayedElementAmount(int index)
        {
            return file.ReadInt(DisplayedElementAmount_AttrName, StructureSectionNameByIndex(index));
        }
        public void SetDisplayedElementAmount(int value, int index)
        {
            file.Write(DisplayedElementAmount_AttrName, value.ToString(), StructureSectionNameByIndex(index));
        }

        const string LeadAmount_AttrName = "LeadAmount";
        public int GetLeadAmount(int index)
        {
            return file.ReadInt(LeadAmount_AttrName, StructureSectionNameByIndex(index));
        }
        public void SetLeadAmount(int value, int index)
        {
            file.Write(LeadAmount_AttrName, value.ToString(), StructureSectionNameByIndex(index));
        }

        #endregion

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
            string temperatureAttrName = GetTestTemperatureAttrName(parameter_type_id, point);
            string valueAttrName = GetTestValueAttrName(parameter_type_id, point);
            if (file.KeyExists(temperatureAttrName, section) && file.KeyExists(valueAttrName, section))
            {
                return file.ReadFloat(valueAttrName, section);
            }
            else
                return float.NaN;
              
        }

        public void SetMeasurePointValue(int structure_id, int parameter_type_id, int point, float value = 0, float temperature = -1)
        {
            string section = GetTestResultSectionName(structure_id);
            string temperatureAttrName = GetTestTemperatureAttrName(parameter_type_id, point);
            string valueAttrName = GetTestValueAttrName(parameter_type_id, point);
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
