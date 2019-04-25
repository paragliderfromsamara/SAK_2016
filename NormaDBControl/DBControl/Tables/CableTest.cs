using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using System.IO;

namespace NormaMeasure.DBControl.Tables
{

    [DBTable("cable_tests", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "ispytan")]
    public class CableTest : BaseEntity
    {
        public CableTest(DataRowBuilder builder) : base(builder)
        {
        }

        public static CableTest GetLastOrCreateNew()
        {
            DBEntityTable t = find_stoped_tests();
            CableTest test;
            if (t.Rows.Count > 0)
            {
                test= (CableTest)t.Rows[0];
            }else
            {
                test = get_new_test();
            }
            return test;

        }

        public void AddResult(CableTestResult result)
        {
            TestResults.Add(result);
        }

        public CableTestResult BuildTestResult(MeasuredParameterType pType, TestedCableStructure structure, uint elNumber, uint measNumber, FrequencyRange frRange = null, uint genEl = 0, uint genPair = 0)
        {
            return TestResults.Build(pType, structure, elNumber, measNumber, frRange, genEl, genPair);
        }

        public static CableTest get_new_test()
        {
            CableTest test = find_not_started_test();
            return test;
        }

        public static CableTest find_not_started_test()
        {
            string select_cmd = $"{CableTestStatus.StatusId_ColumnName} = {CableTestStatus.NotStarted}";
            DBEntityTable t = find_by_criteria(select_cmd, typeof(CableTest));
            if (t.Rows.Count > 0) return (CableTest)t.Rows[0];
            else return create_not_started_test();
        }

        private static CableTest create_not_started_test()
        {
            DBEntityTable t = new DBEntityTable(typeof(CableTest));
            CableTest test = (CableTest)t.NewRow();
            test.StatusId = CableTestStatus.NotStarted;
            test.TestId = 0;
            test.CableLength = 1000;
            test.Temperature = 20;
            test.Save();
            t.Rows.Add(test);
            return test;
        }


        public void SetNotStarted()
        {
            SetStatus(CableTestStatus.NotStarted);
            if (HasTestedCable)
            {
                TestedCable.Destroy();
                testedCable = null;
            }
        }

        public void SetStarted()
        {
            GetTestedCable();
            SetStatus(CableTestStatus.Started);
        }

        public void SetFinished()
        {
            SetStatus(CableTestStatus.Finished);
        }

        public void SetStoppedByOperator()
        {
            SetStatus(CableTestStatus.StopedByOperator);
        }

        private void SetStatus(uint status_id)
        {
            this.StatusId = status_id;
            this.Save();
        }

        private void GetTestedCable()
        {
            if (TestedCable == null)
            {
                TestedCable = TestedCable.create_for_test(this);
            }
        }

        private static DBEntityTable find_stoped_tests()
        {
            string select_cmd = $"{CableTestStatus.StatusId_ColumnName} IN ({CableTestStatus.StopedByOperator}, {CableTestStatus.StopedOutOfNorma}, {CableTestStatus.Started})";
            return find_by_criteria(select_cmd, typeof(CableTest));
        }

        public bool IsFinished => StatusId == CableTestStatus.Finished;
        public bool IsNotStarted => StatusId == CableTestStatus.NotStarted;
        public bool IsInterrupted => StatusId == CableTestStatus.StopedByOperator || StatusId == CableTestStatus.StopedOutOfNorma;

        public MeasuredParameterType[] MeasuredParameterTypes
        {
            get
            {
                if (measuredParameterTypes == null)
                {
                    measuredParameterTypes = getFromTestedCable();
                }
                return measuredParameterTypes;
            }
            set
            {
                measuredParameterTypes = value;
            }
        }

        public uint[] MeasuredParameterTypes_IDs
        {
            get
            {
                if(measuredParameterTypes_IDs == null)
                {
                    List<uint> ids = new List<uint>();
                    foreach (MeasuredParameterType type in MeasuredParameterTypes) ids.Add(type.ParameterTypeId);
                    if (ids.Count > 0) measuredParameterTypes_IDs = ids.ToArray();
                }
                return measuredParameterTypes_IDs;
            }
        }

        public TestedCable TestedCable
        {
            get
            {
                if (testedCable == null)
                {
                    testedCable = TestedCable.find_by_test_id(TestId);
                }
                return testedCable;
            }
            set
            {
                testedCable = value;
            }
        }


        public Cable SourceCable
        {
            get
            {
                if (sourceCable == null)
                {
                    sourceCable = Cable.find_by_cable_id(SourceCableId);
                }
                return sourceCable;
            }
            set
            {
                sourceCable = value;
                SourceCableId = sourceCable.CableId;
                CleanCableVariables();
            }
        }

        public ReleasedBaraban ReleasedBaraban
        {
            get
            {
                if(releasedBaraban == null)
                {
                    releasedBaraban = ReleasedBaraban.find_by_id(BarabanId);
                }
                return releasedBaraban;
            }
        }

        /// <summary>
        /// Очищаем переменные связанные с исходным кабелем
        /// </summary>
        private void CleanCableVariables()
        {
            measuredParameterTypes = null;
            measuredParameterTypes_IDs = null;
            if (TestedCable != null)
            {
                TestedCable.Destroy();
                TestedCable = null;
            }
        }

        private MeasuredParameterType[] getFromTestedCable()
        {   
            List<uint> ids = new List<uint>();
            if (SourceCable == null && TestedCable == null) return null;
            ids.Add(MeasuredParameterType.Calling);
            uint[] idsFromCable = TestedCable == null ? SourceCable.MeasuredParameterTypes_IDs : TestedCable.MeasuredParameterTypes_IDs;
            foreach (uint id in idsFromCable) ids.Add(id);
            return MeasuredParameterType.get_all_by_ids_as_array(ids.ToArray());
        }


        #region Колонки таблицы
        [DBColumn(CableTestId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "IspInd", Nullable = true, DefaultValue =0, IsPrimaryKey = true, AutoIncrement = true)]
        public uint TestId
        {
            get
            {
                return tryParseUInt(CableTestId_ColumnName);
            }
            set
            {
                this[CableTestId_ColumnName] = value;
            }
        }

        [DBColumn(TestedCable.CableId_ColumnName, ColumnDomain.UInt, Order = 11, Nullable =true, DefaultValue = 0)]
        public uint SourceCableId
        {
            get
            {
                return tryParseUInt(TestedCable.CableId_ColumnName);
            }
            set
            {
                this[TestedCable.CableId_ColumnName] = value;
            }
        }

        [DBColumn(ReleasedBaraban.BarabanId_ColumnName, ColumnDomain.UInt, Order = 12, DefaultValue = 0, OldDBColumnName = "BarabanInd", Nullable = true)]
        public uint BarabanId
        {
            get
            {
                return tryParseUInt(ReleasedBaraban.BarabanId_ColumnName);
            }
            set
            {
                this[ReleasedBaraban.BarabanId_ColumnName] = value;
            }
        }

        [DBColumn(OperatorId_ColumnName, ColumnDomain.UInt, Order = 13, OldDBColumnName = "Operator", DefaultValue = 0, Nullable = true)]
        public uint OperatorId
        {
            get
            {
                return tryParseUInt(OperatorId_ColumnName);
            }
            set
            {
                this[OperatorId_ColumnName] = value;
            }
        }

        [DBColumn(CableTestStatus.StatusId_ColumnName, ColumnDomain.UInt, Order = 14, OldDBColumnName = "Status", DefaultValue = CableTestStatus.NotStarted, Nullable = false)]
        public uint StatusId
        {
            get
            {
                return tryParseUInt(CableTestStatus.StatusId_ColumnName);
            }
            set
            {
                this[CableTestStatus.StatusId_ColumnName] = value;
            }
        }

        [DBColumn(Temperature_ColumnName, ColumnDomain.Float, Order = 15, OldDBColumnName = "Temperetur", DefaultValue = 20, Nullable = true)]
        public float Temperature
        {
            get
            {
                return tryParseFloat(Temperature_ColumnName);
            }
            set
            {
                this[Temperature_ColumnName] = value;
            }
        }

        [DBColumn(CableLength_ColumnName, ColumnDomain.Float, Order = 16, OldDBColumnName = "CabelLengt", DefaultValue = 1000, Nullable = true)]
        public float CableLength
        {
            get
            {
                return tryParseFloat(CableLength_ColumnName);
            }
            set
            {
                this[CableLength_ColumnName] = value;
            }
        }

        [DBColumn(VSVILeadLead_ColumnName, ColumnDomain.Boolean, Order = 17, OldDBColumnName = "Vsvi", DefaultValue = 0, Nullable = true)]
        public bool VSVILeadLeadResult
        {
            get
            {
                return tryParseBoolean(VSVILeadLead_ColumnName, false);
            }
            set
            {
                this[VSVILeadLead_ColumnName] = value;
            }
        }

        [DBColumn(VSVILeadShield_ColumnName, ColumnDomain.Boolean, Order = 18, OldDBColumnName = "Vsvi_Obol", DefaultValue = 0, Nullable = true)]
        public bool VSVILeadShieldResult
        {
            get
            {
                return tryParseBoolean(VSVILeadShield_ColumnName, false);
            }
            set
            {
                this[VSVILeadShield_ColumnName] = value;
            }
        }

        [DBColumn(NettoWeight_ColumnName, ColumnDomain.Float, Order = 19, OldDBColumnName = "Netto", DefaultValue = 0, Nullable = true)]
        public float NettoWeight
        {
            get
            {
                return tryParseFloat(NettoWeight_ColumnName);
            }
            set
            {
                this[NettoWeight_ColumnName] = value;
            }
        }

        [DBColumn(BruttoWeight_ColumnName, ColumnDomain.Float, Order = 20, OldDBColumnName = "Brutto", DefaultValue = 0, Nullable = true)]
        public float BruttoWeight
        {
            get
            {
                return tryParseFloat(BruttoWeight_ColumnName);
            }
            set
            {
                this[BruttoWeight_ColumnName] = value;
            }
        }

        public uint TestedCableId
        {
            get
            {
                return HasTestedCable ? TestedCable.CableId : 0;
            }
        }









        public const string CableTestId_ColumnName = "cable_test_id";
        public const string TestedCableId_ColumnName = "cable_id";
        public const string SourceCableId_ColumnName = "source_cable_id";
        public const string OperatorId_ColumnName = "operator_id";
        public const string Temperature_ColumnName = "temperature";
        public const string CableLength_ColumnName = "cable_length";
        public const string VSVILeadLead_ColumnName = "vsvi_lead_lead";
        public const string VSVILeadShield_ColumnName = "vsvi_lead_shield";
        public const string NettoWeight_ColumnName = "netto_weight";
        public const string BruttoWeight_ColumnName = "brutto_weight";


        #endregion

        public bool HasTestedCable => TestedCableId > 0;
        public bool HasSourceCable => SourceCableId > 0;
        public bool HasOperator => OperatorId > 0;
        public bool HasBaraban => ReleasedBaraban != null;


        public bool IsOnTestProgram(MeasuredParameterType t)
        {
            string pKey = $"{t.RefText}";
            bool defVal = MeasuredParameterTypes_IDs.Contains(t.ParameterTypeId);
            bool keyExists = TestFile.IsExists_OnTestProgram(pKey);
            if (defVal)
            {
                if (keyExists)
                {
                    bool.TryParse(TestFile.Read_TestProgram(pKey), out defVal);
                }
                else
                {
                    TestFile.Write_TestProgram(pKey, defVal);
                }
            }
            return defVal;
        }

        public void SetParameterTypeFlagInTestProgram(string parameterRefText, bool state)
        {
            TestFile.Write_TestProgram(parameterRefText, state);
        }
        

        internal CableTest_File TestFile
        {
            get
            {
                if (testFile == null)
                {
                    testFile = new CableTest_File(this);
                }
                return testFile;
            }
            set
            {
                testFile = value;
            }
        }

        /// <summary>
        /// Id типа барабана
        /// </summary>
        public uint BarabanTypeId
        {
            get
            {
                uint val = 0;
                if (HasBaraban)
                {
                    val = ReleasedBaraban.BarabanTypeId;
                }else
                {
                    if (!TestFile.IsExists_BarabanInfo(BarabanType.TypeId_ColumnName))
                    {
                        TestFile.Write_BarabanInfo(BarabanType.TypeId_ColumnName, val);
                    }
                    else
                    {
                        uint.TryParse(TestFile.Read_BarabanInfo(BarabanType.TypeId_ColumnName), out val);
                    }
                }

                return val;
            }
            set
            {
                TestFile.Write_BarabanInfo(BarabanType.TypeId_ColumnName, value);
            }
        }

        /// <summary>
        /// Серийный номер барабана
        /// </summary>
        public string BarabanSerial
        {
            get
            {
                if(HasBaraban)
                {
                    return ReleasedBaraban.SerialNumber;
                }else
                {
                    return TestFile.Read_BarabanInfo(ReleasedBaraban.BarabanSerialNumber_ColumnName);
                }
            }
            set
            {
                TestFile.Write_BarabanInfo(ReleasedBaraban.BarabanSerialNumber_ColumnName, value);
            }
        }


        /// <summary>
        /// Испытание с дальним столом? (true - c ДК, false - без ДК)
        /// </summary>
        public bool IsSplittedTable
        {
            get
            {
                bool val = true;
                if (!TestFile.IsExists_TestSettings(IsSplittedTable_iniKeyName))
                {
                    TestFile.Write_TestSettings(IsSplittedTable_iniKeyName, val);
                }else
                {
                    bool.TryParse(TestFile.Read_TestSettings(IsSplittedTable_iniKeyName), out val);
                }
                return val;
            }
            set
            {
                TestFile.Write_TestSettings(IsSplittedTable_iniKeyName, value);
            }
        }

        /// <summary>
        /// Флаг использования термодатчика
        /// </summary>
        public bool IsUseTermoSensor
        {
            get
            {
                bool val = false;
                if (!TestFile.IsExists_TestSettings(UseTermoSensor_iniKeyName))
                {
                    TestFile.Write_TestSettings(UseTermoSensor_iniKeyName, val);
                }
                else
                {
                    bool.TryParse(TestFile.Read_TestSettings(UseTermoSensor_iniKeyName), out val);
                }
                return val;
            }
            set
            {
                TestFile.Write_TestSettings(UseTermoSensor_iniKeyName, value);
            }
        }

        /// <summary>
        /// Подключающее устройство с которого подключен кабель
        /// </summary>
        public uint CableConnectedFrom
        {
            get
            {
                uint val = 1;
                if (!TestFile.IsExists_TestSettings(CableConnectedFrom_iniKeyName))
                {
                    TestFile.Write_TestSettings(CableConnectedFrom_iniKeyName, val);
                }
                else
                {
                    uint.TryParse(TestFile.Read_TestSettings(CableConnectedFrom_iniKeyName), out val);
                }
                return val;
            }
            set
            {
                TestFile.Write_TestSettings(CableConnectedFrom_iniKeyName, value);
            }
        }

        /// <summary>
        /// Тип измеряемого сопротивления изоляции
        /// </summary>
        public uint RisolTypeFavourTypeId
        {
            get
            {
                uint val = 0;
                if (!TestFile.IsExists_TestSettings(RisolFavourTypeId_iniKeyName))
                {
                    TestFile.Write_TestSettings(RisolFavourTypeId_iniKeyName, val);
                }
                else
                {
                    uint.TryParse(TestFile.Read_TestSettings(RisolFavourTypeId_iniKeyName), out val);
                }
                return val;
            }
            set
            {
                TestFile.Write_TestSettings(RisolFavourTypeId_iniKeyName, value);
            }
        }

        public CableTestResultCollection TestResults
        {
            get
            {
                if (testResults == null) testResults = new CableTestResultCollection(this);
                return testResults;
            }
        }

        private const string RisolFavourTypeId_iniKeyName = "RisolFavourId";
        private const string CableConnectedFrom_iniKeyName = "cable_connected_from";
        private const string UseTermoSensor_iniKeyName = "using_termo_sensor";
        private const string IsSplittedTable_iniKeyName = "is_splitted_table";
        private const string TestSettings_iniSectionName = "test_settings";
        private const string TestProgram_iniSectionName = "test_program";
        private const string CableTestTempFile_IniFileName = "CableTest_{0}.ini";
        private CableTest_File testFile;

        public MeasuredParameterType[] measuredParameterTypes;
        public uint[] measuredParameterTypes_IDs;

        private Cable sourceCable;
        private TestedCable testedCable;
        private ReleasedBaraban releasedBaraban;
        private CableTestResultCollection testResults;

    } 

    internal class CableTest_File
    {

        public CableTest_File(CableTest test)
        {
            cableTest = test;
            InitFile();
        }


        private void InitFile()
        {
            if (!Directory.Exists(fileDir)) Directory.CreateDirectory(fileDir);
            file = new IniFile($@"{fileDir}\{FileName}");
        }

        private string Read(string key, string section)
        {
            return file.Read(key, section);
        }

        private void Write(string key, object val, string section)
        {
            file.Write(key, val.ToString(), section);
        }

        private bool Exists(string key, string section)
        {
            return file.KeyExists(key, section);
        }

        #region Управление программой испытаний
        /// <summary>
        /// Записываем в программу испытаний
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Write_TestProgram(string key, object val)
        {
            Write(key, val, TestProgram_iniSectionName);
        }

        /// <summary>
        /// Читаем из программы испытаний
        /// </summary>
        /// <param name="key"></param>
        public string Read_TestProgram(string key)
        {
            return Read(key, TestProgram_iniSectionName);
        }

        public bool IsExists_OnTestProgram(string key)
        {
            return Exists(key, TestSettings_iniSectionName);
        }
        #endregion

        #region Управление настройками испытаний
        public void Write_TestSettings(string key, object val)
        {
            Write(key, val, TestSettings_iniSectionName);
        }

        /// <summary>
        /// Читаем из настроек испытаний
        /// </summary>
        /// <param name="key"></param>
        public string Read_TestSettings(string key)
        {
            return Read(key, TestSettings_iniSectionName);
        }

        public bool IsExists_TestSettings(string key)
        {
            return Exists(key, TestSettings_iniSectionName);
        }


        #endregion

        #region BarabanInfo
        public void Write_BarabanInfo(string key, object val)
        {
            Write(key, val, BarabanInfo_iniSectionName);
        }

        /// <summary>
        /// Читаем из информации по барабану
        /// </summary>
        /// <param name="key"></param>
        public string Read_BarabanInfo(string key)
        {
            return Read(key, BarabanInfo_iniSectionName);
        }

        public bool IsExists_BarabanInfo(string key)
        {
            return Exists(key, BarabanInfo_iniSectionName);
        }


        #endregion

        #region Результаты испытаний

        public void WriteCurrentPoint(CableTestResult testResult)
        {
            file.Write(MeasuredParameterType.ParameterTypeId_ColumnName, testResult.ParameterTypeId.ToString(), LastMeasurePoint_iniSectionName);
            file.Write(TestedCableStructure.StructureId_ColumnName, testResult.CableStructureId.ToString(), LastMeasurePoint_iniSectionName);
            file.Write(CableTestResult.StructElementNumber_ColumnName, testResult.ElementNumber.ToString(), LastMeasurePoint_iniSectionName);
            file.Write(CableTestResult.MeasureOnElementNumber_ColumnName, testResult.MeasureNumber.ToString(), LastMeasurePoint_iniSectionName);
            file.Write(CableTestResult.ElementNumberOnGenerator_ColumnName, testResult.GeneratorElementNumber.ToString(), LastMeasurePoint_iniSectionName);
            file.Write(CableTestResult.PairNumberOnGenerator_ColumnName, testResult.GeneratorPairNumber.ToString(), LastMeasurePoint_iniSectionName);

            file.Write(FrequencyRange.FreqRangeId_ColumnName, testResult.FrequencyRangeId.ToString(), LastMeasurePoint_iniSectionName);
        }

        public float Read_TestResult(CableTestResult testResult)
        {
            float r = 0;
            string str = file.Read(testResult.IniFileKey, testResult.IniFileSection);
            float.TryParse(str, out r);
            return r;
        }

        public void Write_TestResult(CableTestResult testResult)
        {
            WriteCurrentPoint(testResult);
            file.Write(testResult.IniFileKey, testResult.Result.ToString(), testResult.IniFileSection);
        }

        #endregion


        public string FileName => String.Format(fileNameMask, cableTest.TestId);
        private const string TestSettings_iniSectionName = "test_settings";
        private const string TestProgram_iniSectionName = "test_program";


        private const string LastMeasurePoint_iniSectionName = "test_current_point";
        private const string BarabanInfo_iniSectionName = "baraban_info";
        private const string fileNameMask = "CableTest_{0}.ini";
        private const string fileDir = "tmp";
        private CableTest cableTest;
        private IniFile file;

    }

    public class CableTestResultCollection
    {
        public CableTestResultCollection(CableTest test)
        {
            cable_test = test;
            System.Windows.Forms.MessageBox.Show("Constructor");
            Init();
        }

        private void Init()
        {
            System.Windows.Forms.MessageBox.Show("Init");
            switch (cable_test.StatusId)
            {
                case CableTestStatus.NotStarted:
                    CleanList();
                    break;
                case CableTestStatus.Finished:
                    LoadFromDB();
                    break;
                default:
                    LoadFromIniFile();
                    break;
            }
        }

        private void LoadFromDB()
        {
            //System.Windows.Forms.MessageBox.Show("LoadFromDB");
            throw new NotImplementedException();
        }

        private void CleanList()
        {
            //System.Windows.Forms.MessageBox.Show("CleanList");
            throw new NotImplementedException();
        }

        public void Add(CableTestResult result)
        {
            results_Table.Rows.Add(result);
            cable_test.TestFile.Write_TestResult(result);
        }

        public CableTestResult Build(MeasuredParameterType pType, TestedCableStructure structure, uint elNumber, uint measNumber, FrequencyRange frRange = null, uint genEl = 0, uint genPair = 0)
        {
            //System.Windows.Forms.MessageBox.Show("Build");
            CableTestResult r = (CableTestResult)results_Table.NewRow();
            r.TestedCableStructure = structure;
            r.ParameterType = pType;
            r.ElementNumber = elNumber;
            r.MeasureNumber = measNumber;
            r.TestId = cable_test.TestId;
            if (frRange != null)
            {
                r.FrequencyRange = frRange;
                r.GeneratorElementNumber = genEl;
                r.GeneratorPairNumber = genPair;
            }
            return r;
        }

        private void LoadFromIniFile()
        {
            foreach (MeasuredParameterType pType in cable_test.TestedCable.MeasuredParameterTypes.Rows)
            {
                foreach(TestedCableStructure structure in cable_test.TestedCable.CableStructures.Rows)
                {
            
                    if (pType.IsFreqParameter)
                    {

                    }else
                    {
                        for(uint i = 0; i<structure.RealAmount; i++)
                        {
                            for(uint j = 0; j<structure.StructureType.StructureLeadsAmount; j++)
                            {
                                CableTestResult r = (CableTestResult)results_Table.NewRow();
                                r.ParameterType = pType;
                                r.TestedCableStructure = structure;
                                r.ElementNumber = i+1;
                                r.MeasureNumber = j + 1;
                                r.Result = cable_test.TestFile.Read_TestResult(r);
                                r.TestId = cable_test.TestId;
                                results_Table.Rows.Add(r);
                            }

                        }
                    }
                }
            }
            //System.Windows.Forms.MessageBox.Show("LoadFromIniFile");
            //cable_test 
        }

        

        private CableTest cable_test;
        private DBEntityTable results_Table = new DBEntityTable(typeof(CableTestResult));
        private Dictionary<string, List<CableTestResult>> results_Dictionary = new Dictionary<string, List<CableTestResult>>();
    }


}
