using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("dr_formuls", "db_norma_measure", OldDBName = "bd_cable", OldTableName = "dr_formuls")]
    public class dRFormula : BaseEntity
    {
        public dRFormula(DataRowBuilder builder) : base(builder)
        {
        }


        public static dRFormula find_drFormula_by_id(uint dr_formula_id)
        {
            DBEntityTable r = find_by_primary_key(dr_formula_id, typeof(dRFormula));
            if (r.Rows.Count > 0) return (dRFormula)r.Rows[0];
            else return null;
        }

        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(dRFormula));
        }

        #region Колонки таблицы
        [DBColumn(FormulaId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "DRInd", Nullable = true, IsPrimaryKey = true)]
        public uint FormulaId
        {
            get
            {
                return tryParseUInt(FormulaId_ColumnName);
            }
            set
            {
                this[FormulaId_ColumnName] = value;
            }
        }


        [DBColumn(FormulaName_ColumnName, ColumnDomain.Tinytext, Order = 11, OldDBColumnName = "DRName", Nullable = true)]
        public string FormulaName
        {
            get
            {
                return this[FormulaName_ColumnName].ToString();
            }
            set
            {
                this[FormulaName_ColumnName] = value;
            }
        }

        [DBColumn(FormulaMeasure_ColumnName, ColumnDomain.Tinytext, Order = 12, OldDBColumnName = "ResEd", Nullable = true)]
        public string ResultMeasure
        {
            get
            {
                return this[FormulaMeasure_ColumnName].ToString();
            }
            set
            {
                this[FormulaMeasure_ColumnName] = value;
            }
        }

        [DBColumn(FormulaDescription_ColumnName, ColumnDomain.Tinytext, Order = 13, OldDBColumnName = "DROpis", Nullable = true)]
        public string Formula
        {
            get
            {
                return this[FormulaDescription_ColumnName].ToString();
            }
            set
            {
                this[FormulaDescription_ColumnName] = value;
            }
        }

        public const string FormulaId_ColumnName = "dr_formula_id";
        public const string FormulaName_ColumnName = "dr_formula_name";
        public const string FormulaMeasure_ColumnName = "dr_formula_result_measure";
        public const string FormulaDescription_ColumnName = "dr_formula_description";
        #endregion

    }
}
