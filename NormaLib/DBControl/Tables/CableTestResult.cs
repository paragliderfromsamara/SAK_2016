using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("cable_test_measure_results", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "resultism")]
    public class CableTestResult : BaseEntity
    {
        public CableTestResult(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable FindByTestId(uint cable_test_id)
        {
            return find_by_criteria($"{CableTest.CableTestId_ColumnName} = {cable_test_id}", typeof(CableTestResult));
        }

        public static void delete_all_from_cable_test(uint cable_test_id)
        {
            delete_by_criteria($"{CableTest.CableTestId_ColumnName} = {cable_test_id}", typeof(CableTestResult));
        }


        #region Колонки таблицы
        [DBColumn(CableTest.CableTestId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "IspInd", ReferenceTo = "cable_tests(cable_test_id) ON DELETE CASCADE", Nullable = true)]
        public uint TestId
        {
            get
            {
                return tryParseUInt(CableTest.CableTestId_ColumnName);
            }
            set
            {
                this[CableTest.CableTestId_ColumnName] = value;
            }
        }

        [DBColumn(MeasuredParameterType.ParameterTypeId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "ParamInd", Nullable = true)]
        public uint ParameterTypeId
        {
            get
            {
                return tryParseUInt(MeasuredParameterType.ParameterTypeId_ColumnName);
            }
            set
            {
                this[MeasuredParameterType.ParameterTypeId_ColumnName] = value;
            }
        }

        [DBColumn(CableStructure.StructureId_ColumnName, ColumnDomain.UInt, Order = 12, OldDBColumnName = "StruktInd", Nullable = true)]
        public uint CableStructureId
        {
            get
            {
                return tryParseUInt(CableStructure.StructureId_ColumnName);
            }
            set
            {
                this[CableStructure.StructureId_ColumnName] = value;
            }
        }

        [DBColumn(StructElementNumber_ColumnName, ColumnDomain.UInt, Order = 13, OldDBColumnName = "StruktElNum", Nullable = true)]
        public uint ElementNumber
        {
            get
            {
                return tryParseUInt(StructElementNumber_ColumnName);
            }
            set
            {
                this[StructElementNumber_ColumnName] = value;
            }
        }

        [DBColumn(MeasureOnElementNumber_ColumnName, ColumnDomain.UInt, Order = 14, OldDBColumnName = "IsmerNum", Nullable = true)]
        public uint MeasureNumber
        {
            get
            {
                return tryParseUInt(MeasureOnElementNumber_ColumnName);
            }
            set
            {
                this[MeasureOnElementNumber_ColumnName] = value;
            }
        }

        [DBColumn(MeasureResult_ColumnName, ColumnDomain.Float, Order = 15, OldDBColumnName = "Resultat", Nullable = true)]
        public float Result
        {
            get
            {
                return tryParseFloat(MeasureResult_ColumnName);
            }
            set
            {
                this[MeasureResult_ColumnName] = value;
            }
        }

        [DBColumn(CableTest.Temperature_ColumnName, ColumnDomain.Float, Order = 16, OldDBColumnName = "Temperatur", Nullable = true)]
        public float Temperature
        {
            get
            {
                return tryParseFloat(CableTest.Temperature_ColumnName);
            }
            set
            {
                this[CableTest.Temperature_ColumnName] = value;
            }
        }

        [DBColumn(FrequencyRange.FreqRangeId_ColumnName, ColumnDomain.UInt, Order = 17, OldDBColumnName = "FreqDiap", Nullable = true)]
        public uint FrequencyRangeId
        {
            get
            {
                return tryParseUInt(FrequencyRange.FreqRangeId_ColumnName);
            }
            set
            {
                this[FrequencyRange.FreqRangeId_ColumnName] = value;
            }
        }

        [DBColumn(ElementNumberOnGenerator_ColumnName, ColumnDomain.UInt, Order = 18, OldDBColumnName = "StruktElNum_gen", Nullable = true)]
        public uint GeneratorElementNumber
        {
            get
            {
                return tryParseUInt(ElementNumberOnGenerator_ColumnName);
            }
            set
            {
                this[ElementNumberOnGenerator_ColumnName] = value;
            }
        }

        [DBColumn(PairNumberOnGenerator_ColumnName, ColumnDomain.UInt, Order = 19, OldDBColumnName = "PairNum_gen", Nullable = true)]
        public uint GeneratorPairNumber
        {
            get
            {
                return tryParseUInt(PairNumberOnGenerator_ColumnName);
            }
            set
            {
                this[PairNumberOnGenerator_ColumnName] = value;
            }
        }

        public bool IsOnNorma = true;

        public uint ReceiverElementNumber
        {
            get
            {
                return ElementNumber;
            }
            set
            {
                ElementNumber = value;
            }
        }

        public uint SubElementNumber
        {
            get
            {
                return MeasureNumber;
            }
            set
            {
                MeasureNumber = value;
            }
        }




        public const string StructElementNumber_ColumnName = "element_number";
        public const string MeasureOnElementNumber_ColumnName = "measure_number";
        public const string MeasureResult_ColumnName = "result";
        public const string ElementNumberOnGenerator_ColumnName = "generator_element_number";
        public const string PairNumberOnGenerator_ColumnName = "generator_pair_number";
        #endregion



        public string IniFileKey
        {
            get
            {
                string keyName = $"rslt_{this.ElementNumber}_{this.MeasureNumber}";
                if (MeasuredParameterType.IsItFreqParameter(ParameterTypeId)) keyName += $"_{GeneratorElementNumber}_{GeneratorPairNumber}";
                return keyName;
            }
        }

        public string IniFileSection
        {
            get
            {
                string sectName = $"results_{this.ParameterTypeId}_{this.CableStructureId}";
                if (MeasuredParameterType.IsItFreqParameter(ParameterTypeId)) sectName += $"_{FrequencyRangeId}";
                return sectName;
            }
        }


        public TestedCableStructure TestedCableStructure
        {
            get
            {
                if (cableStructure == null)
                {
                    cableStructure = TestedCableStructure.find_by_structure_id(CableStructureId);
                }
                return cableStructure;
            }
            set
            {
                cableStructure = value;
                CableStructureId = cableStructure.CableStructureId;
            }
        }

        public MeasuredParameterType ParameterType
        {
            get
            {
                if(parameterType== null)
                {
                    parameterType = MeasuredParameterType.find_by_id(ParameterTypeId);
                }
                return parameterType;
            }set
            {
                parameterType = value;
                ParameterTypeId = parameterType.ParameterTypeId;
            }
        }

        public FrequencyRange FrequencyRange
        {
            get
            {
                if (frequencyRange == null)
                {
                    frequencyRange = FrequencyRange.find_by_id(FrequencyRangeId);
                }
                return frequencyRange;
            }
            set
            {
                frequencyRange = value;
                FrequencyRangeId = frequencyRange.FrequencyRangeId;
            }
        }


        private TestedCableStructure cableStructure;
        private MeasuredParameterType parameterType;
        private FrequencyRange frequencyRange;

    }
}
