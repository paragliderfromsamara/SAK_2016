using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NormaLib.DBControl.Tables
{

    [DBTable("isol_material_coeffs", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "tkc_izol")]
    public class IsolMaterialCoeffs : BaseEntity
    {
        public IsolMaterialCoeffs(DataRowBuilder builder) : base(builder)
        {
        }

        public static DBEntityTable get_all_as_table()
        {
            return get_all(typeof(IsolMaterialCoeffs));
        }

        public static DBEntityTable get_all_for_material(uint material_id)
        {
            return find_by_criteria($"{MaterialId_ColumnName} = {material_id}", typeof(IsolMaterialCoeffs));
        }

        #region Колонки таблицы
        [DBColumn(MaterialId_ColumnName, ColumnDomain.UInt, Order = 10, Nullable = true)]
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

        [DBColumn(Temperature_ColumnName, ColumnDomain.Float, Order = 11, Nullable = true)]
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

        [DBColumn(Coeff_ColumnName, ColumnDomain.Float, Order = 12, Nullable = true)]
        public float Coefficient
        {
            get
            {
                return tryParseFloat(Coeff_ColumnName);
            }
            set
            {
                this[Coeff_ColumnName] = value;
            }
        }

        public const string MaterialId_ColumnName = "MaterialId";
        public const string Temperature_ColumnName = "Temperature";
        public const string Coeff_ColumnName = "TKC";
        #endregion



    }
}
