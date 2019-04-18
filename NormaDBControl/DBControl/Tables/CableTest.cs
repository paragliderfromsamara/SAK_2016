using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cable_tests", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "ispytan")]
    public class CableTest : BaseEntity
    {
        public CableTest(DataRowBuilder builder) : base(builder)
        {
        }

        

        [DBColumn(CableTestId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName = "IspInd", Nullable = true, IsPrimaryKey = true, AutoIncrement = true)]
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

        [DBColumn(Cable.CableId_ColumnName, ColumnDomain.UInt, Order = 11, OldDBColumnName = "CabNum", Nullable = false, ReferenceTo = "cables(cable_id) ON DELETE CASCADE")]
        public uint CableId
        {
            get
            {
                return tryParseUInt(Cable.CableId_ColumnName);
            }
            set
            {
                this[Cable.CableId_ColumnName] = value;
            }
        }


        [DBColumn(ReleasedBaraban.BarabanId_ColumnName, ColumnDomain.UInt, Order = 12, OldDBColumnName = "BarabanInd", Nullable = false)]
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

        [DBColumn(OperatorId_ColumnName, ColumnDomain.UInt, Order = 13, OldDBColumnName = "Operator", Nullable = false)]
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

        [DBColumn(CableTestStatus.StatusId_ColumnName, ColumnDomain.UInt, Order = 14, OldDBColumnName = "Status", Nullable = false)]
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

        [DBColumn(Temperature_ColumnName, ColumnDomain.Float, Order = 15, OldDBColumnName = "Temperetur", Nullable = true)]
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

        [DBColumn(CableLength_ColumnName, ColumnDomain.Float, Order = 16, OldDBColumnName = "CabelLengt", Nullable = true)]
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

        [DBColumn(VSVILeadLead_ColumnName, ColumnDomain.Boolean, Order = 17, OldDBColumnName = "Vsvi", Nullable = true)]
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

        [DBColumn(VSVILeadShield_ColumnName, ColumnDomain.Boolean, Order = 18, OldDBColumnName = "Vsvi_Obol", Nullable = true)]
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

        [DBColumn(NettoWeight_ColumnName, ColumnDomain.Float, Order = 19, OldDBColumnName = "Netto", Nullable = true)]
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

        [DBColumn(BruttoWeight_ColumnName, ColumnDomain.Float, Order = 20, OldDBColumnName = "Brutto", Nullable = true)]
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





        public const string CableTestId_ColumnName = "cable_test_id";
        public const string OperatorId_ColumnName = "operator_id";
        public const string Temperature_ColumnName = "temperature";
        public const string CableLength_ColumnName = "cable_length";
        public const string VSVILeadLead_ColumnName = "vsvi_lead_lead";
        public const string VSVILeadShield_ColumnName = "vsvi_lead_shield";
        public const string NettoWeight_ColumnName = "netto_weight";
        public const string BruttoWeight_ColumnName = "brutto_weight";

    }
}
