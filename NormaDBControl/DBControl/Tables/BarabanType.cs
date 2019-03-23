using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("baraban_types", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "tipy_baraban")]
    public class BarabanType : BaseEntity
    {
        public BarabanType(DataRowBuilder builder) : base(builder)
        {
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
                System.Windows.Forms.MessageBox.Show($"{TypeId}");
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
            DBEntityTable t = new DBEntityTable(typeof(BarabanType));
            string select_cmd = $"{t.SelectQuery} WHERE (NOT baraban_type_id = {this.TypeId}) AND baraban_type_name = '{this.TypeName}'";
            t.FillByQuery(select_cmd);
            if (t.Rows.Count > 0)
            {
                this.ErrorsList.Add("Наименование типа барабана должно быть уникальным");
            }
        }


        public static DBEntityTable get_all_as_table()
        {
            DBEntityTable t = new DBEntityTable(typeof(BarabanType));
            string select_cmd = $"{t.SelectQuery}";
            t.FillByQuery(select_cmd);
            return t;
        }


        [DBColumn("baraban_type_id", ColumnDomain.UInt, Order = 10, OldDBColumnName = "TipInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint TypeId
        {
            get
            {
                return tryParseUInt("baraban_type_id");
            }
            set
            {
                this["baraban_type_id"] = value;
            }
        }

        [DBColumn("baraban_type_name", ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "TipName", Nullable = true, IsPrimaryKey = false)]
        public string TypeName
        {
            get
            {
                return this["baraban_type_name"].ToString();
            }
            set
            {
                this["baraban_type_name"] = value;
            }
        }

        [DBColumn("baraban_weight", ColumnDomain.Float, Order = 12, OldDBColumnName = "Massa", Nullable = true, IsPrimaryKey = false)]
        public float BarabanWeight
        {
            get
            {
                return tryParseFloat("baraban_weight");
            }
            set
            {
                this["baraban_weight"] = value;
            }
        }

    }
}
