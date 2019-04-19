using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{


    [DBTable("isolation_materials", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "materialy_izol")]
    public class IsolationMaterial : BaseEntity
    {
        public IsolationMaterial(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(IsolationMaterial));
        }

        #region Колонки таблицы
        [DBColumn(MaterialId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "MaterInd", Nullable = false, IsPrimaryKey = true, AutoIncrement = true)]
        public uint MaterialId
        {
            get
            {
                return tryParseUInt(MaterialId_ColumnName);
            }
            set
            {
                this[MaterialId_ColumnName] = value;
            }
        }

        [DBColumn(MaterialName_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "MaterName", Nullable = true)]
        public string MaterialName
        {
            get
            {
                return this[MaterialName_ColumnName].ToString();
            }
            set
            {
                this[MaterialName_ColumnName] = value;
            }
        }

        public const string MaterialId_ColumnName = "isolation_material_id";
        public const string MaterialName_ColumnName = "isolation_material_name";

        #endregion
    }
}
