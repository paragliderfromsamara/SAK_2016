using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("test_measure_devices", "db_norma_measure")]
    public class TestMeasureDevices : BaseEntity
    {
        public TestMeasureDevices(DataRowBuilder builder) : base(builder)
        {
        }



        #region Колонки таблицы 
        [DBColumn(TestTypeName_ColumnName, ColumnDomain.Varchar, Size = 20, Order = 10, Nullable = false)]
        public string TestTypeName
        {
            get
            {
                return this[TestTypeName_ColumnName].ToString();
            }
            set
            {
                this[TestTypeName_ColumnName] = value;
            }
        }


        [DBColumn(TestId_ColumnName, ColumnDomain.Int, Order = 11, Nullable = false)]
        public int TestId
        {
            get
            {
                return tryParseInt(TestId_ColumnName);
            }
            set
            {
                this[TestId_ColumnName] = value;
            }
        }

        [DBColumn(MeasureDeviceId_ColumnName, ColumnDomain.Int, Order = 12, Nullable = false)]
        public int MeasureDeviceId
        {
            get
            {
                return tryParseInt(MeasureDeviceId_ColumnName);
            }
            set
            {
                this[MeasureDeviceId_ColumnName] = value;
            }
        }


        public const string TestTypeName_ColumnName = "test_type";
        public const string TestId_ColumnName = "test_id";
        public const string MeasureDeviceId_ColumnName = "measure_device_id";

        #endregion
    }
}
