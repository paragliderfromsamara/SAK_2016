using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("measure_devices", "db_norma_measure", OldDBName = "", OldTableName = "")]
    public class MeasureDevice : BaseEntity
    {
        public MeasureDevice(DataRowBuilder builder) : base(builder)
        {

        }


        #region Колонки таблицы
        [DBColumn(MeasureDeviceId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint DeviceId
        {
            get
            {
                return tryParseUInt(MeasureDeviceId_ColumnName);
            }
            set
            {
                this[MeasureDeviceId_ColumnName] = value;
            }
        }

        [DBColumn(DeviceTypeId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "", Nullable = true)]
        public uint DeviceTypeId
        {
            get
            {
                return tryParseUInt(DeviceTypeId_ColumnName);
            }
            set
            {
                this[DeviceTypeId_ColumnName] = value;
            }
        }


        [DBColumn(DeviceTypeName_ColumnName, ColumnDomain.Tinytext, OldDBColumnName = "", Order = 12, Nullable = true)]
        public string DeviceTypeName
        {
            get
            {
                return this[DeviceTypeName_ColumnName].ToString();
            }
            set
            {
                this[DeviceTypeName_ColumnName] = value;
            }
        }

        [DBColumn(DeviceModelName_ColumnName, ColumnDomain.Tinytext, OldDBColumnName = "", Order = 13, DefaultValue = "", Nullable = true)]
        public string ModelName
        {
            get
            {
                return this[DeviceModelName_ColumnName].ToString();
            }
            set
            {
                this[DeviceModelName_ColumnName] = value;
            }
        }

        [DBColumn(DeviceSerial_ColumnName, ColumnDomain.Tinytext, OldDBColumnName = "", Order = 14, DefaultValue = "", Nullable = true)]
        public string Serial
        {
            get
            {
                return this[DeviceSerial_ColumnName].ToString();
            }
            set
            {
                this[DeviceSerial_ColumnName] = value;
            }
        }

        [DBColumn(Version_ColumnName, ColumnDomain.UInt, Order = 15, OldDBColumnName = "", Nullable = true)]
        public uint Version
        {
            get
            {
                return tryParseUInt(Version_ColumnName);
            }
            set
            {
                this[Version_ColumnName] = value;
            }
        }




        public const string MeasureDeviceId_ColumnName = "measure_device_id";
        public const string DeviceTypeId_ColumnName = "device_type_id";
        public const string DeviceTypeName_ColumnName = "type_name";
        public const string DeviceModelName_ColumnName = "model_name";
        public const string DeviceSerial_ColumnName = "serial";
        public const string Version_ColumnName = "version";


        #endregion

    }
}
