using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.DBControl.Tables
{
    [DBTable("baraban_types", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "tipy_baraban")]
    public class BarabanType : BaseEntity
    {
        public BarabanType(DataRowBuilder builder) : base(builder)
        {
        }

        public static BarabanType find_by_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(BarabanType));
            if (t.Rows.Count>0)
            {
                return (BarabanType)t.Rows[0];
            }else
            {
                return null;
            }
        }

        public static BarabanType build()
        {
            DBEntityTable t = new DBEntityTable(typeof(BarabanType));
            BarabanType bt = (BarabanType)t.NewRow();
            bt.BarabanWeight = 0;
            bt.TypeName = "";
            return bt;
        }

        public override bool Save()
        {
            try
            {
                bool f = base.Save();
                return f;

            }
            catch (DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Не удалось добавить тип барабана...", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
        }

        protected override void ValidateActions()
        {
            base.ValidateActions();
            CheckNameUniquiness();
            CheckNameNotBlank();
            CheckWeight();
        }

        protected void CheckWeight()
        {
            if (BarabanWeight < 0)
            {
                this.ErrorsList.Add("Вес барабана не может быть отрицательным");
            }else if (BarabanWeight > 5000)
            {
                this.ErrorsList.Add("Вес барабана не может быть больше 5000 кг");
            }
        }
        protected void CheckNameNotBlank()
        {
            if (String.IsNullOrWhiteSpace(TypeName))
            {
                this.ErrorsList.Add("Наименование типа барабана не должно быть пустым");
            }

        }

        protected void CheckNameUniquiness()
        {

            string select_cmd = $"(NOT {TypeId_ColumnName} = {this.TypeId}) AND {TypeName_ColumnName} = '{this.TypeName}'";
            DBEntityTable t = find_by_criteria(select_cmd, typeof(BarabanType));//new DBEntityTable(typeof(BarabanType));
            if (t.Rows.Count > 0)
            {
                this.ErrorsList.Add("Наименование типа барабана должно быть уникальным");
            }
        }


        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(BarabanType));//new DBEntityTable(typeof(BarabanType));
        }

        #region Колонки таблицы
        [DBColumn(TypeId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "TipInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint TypeId
        {
            get
            {
                return tryParseUInt(TypeId_ColumnName);
            }
            set
            {
                this[TypeId_ColumnName] = value;
            }
        }

        [DBColumn(TypeName_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "TipName", Nullable = true, IsPrimaryKey = false)]
        public string TypeName
        {
            get
            {
                return this[TypeName_ColumnName].ToString();
            }
            set
            {
                this[TypeName_ColumnName] = value;
            }
        }

        [DBColumn(BarabanWeight_ColumnName, ColumnDomain.Float, Order = 12, OldDBColumnName = "Massa", Nullable = true, IsPrimaryKey = false)]
        public float BarabanWeight
        {
            get
            {
                return tryParseFloat(BarabanWeight_ColumnName);
            }
            set
            {
                this[BarabanWeight_ColumnName] = value;
            }
        }

        public const string TypeId_ColumnName = "baraban_type_id";
        public const string TypeName_ColumnName = "baraban_type_name";
        public const string BarabanWeight_ColumnName = "baraban_weight";
        #endregion
    }
}
