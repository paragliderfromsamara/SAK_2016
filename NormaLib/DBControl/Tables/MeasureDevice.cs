using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Devices;

namespace NormaLib.DBControl.Tables
{
    [DBTable("measure_devices", "db_norma_measure", OldDBName = "", OldTableName = "")]
    public class MeasureDevice : BaseEntity
    {
        public MeasureDevice(DataRowBuilder builder) : base(builder)
        {

        }

        public static int TryGetDeviceIdByDevice(DeviceBase device)
        {
            try
            {
                MeasureDevice device_on_db = null;
                DBEntityTable t = find_by_criteria($"WHERE {DeviceTypeId_ColumnName} = {(uint)device.TypeId} AND {DeviceSerial_ColumnName} = '{device.Serial}'", typeof(MeasureDevice));
                if (t.Rows.Count == 0) device_on_db = Create(device, t);
                else device_on_db = (MeasureDevice)t.Rows[0];
                return (int)device_on_db.DeviceId;
            }catch(Exception)
            {
                return 0;
            }
        }

        public static MeasureDevice Create(DeviceBase device, DBEntityTable table = null)
        {
            if (table == null) table = new DBEntityTable(typeof(MeasureDevice));
            MeasureDevice device_on_db = (MeasureDevice)table.NewRow();
            device_on_db = (MeasureDevice)table.NewRow();
            device_on_db.Serial = device.Serial;
            device_on_db.DeviceTypeId = (uint)device.TypeId;
            device_on_db.Version = device.ModelVersion;
            device_on_db.DeviceTypeName = device.TypeNameFull;

            if (device_on_db.Save()) return device_on_db;
            else return null;
            
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

        [DBColumn(DeviceSerial_ColumnName, ColumnDomain.Tinytext, Size = 20, OldDBColumnName = "", Order = 14, DefaultValue = "", Nullable = true)]
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
