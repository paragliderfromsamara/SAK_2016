using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl;

namespace NormaMeasure.DBControl.Tables
{
    [DBTable("cables", "db_norma_sac", OldDBName = "bd_cable", OldTableName = "cables")]
    public class Cable : BaseEntity
    {

        public static DBEntityTable get_all_including_docs()
        {
            DBEntityTable docsTable = new DBEntityTable(typeof(Document));
            string select_cmd = $"LEFT OUTER JOIN {docsTable.TableName} USING({docsTable.PrimaryKey[0]}) WHERE is_deleted = 0 AND is_draft = 0";
            return find_by_criteria(select_cmd, typeof(Cable));
        }

        public static Cable find_by_cable_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(Cable));//new DBEntityTable(typeof(Cable));
            if (t.Rows.Count > 0) return (Cable)t.Rows[0];
            else
            {
                return null;
            }
        }

        public static Cable GetCableCopy(Cable copiedCable)
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            Cable draft = GetDraft();
            draft.FillColsFromEntity(copiedCable);
            draft.AddStructuresFromCable(copiedCable);
            draft.IsDraft = true;
            //System.Windows.Forms.MessageBox.Show(draft.CableId.ToString());
            return draft;
        }

        private void AddStructuresFromCable(Cable copiedCable)
        {
            ClearStructures();
            foreach (CableStructure cabStruct in copiedCable.CableStructures.Rows)
            {
                AddCableStructure(cabStruct);
            }
        }

        private void ClearStructures()
        {
            if (this.CableStructures.Rows.Count == 0) return;
            foreach(CableStructure s in this.CableStructures.Rows) s.Destroy();
            this.CableStructures.Rows.Clear();
        }

        public CableStructure AddCableStructure(uint cable_structure_type_id)
        {
            Random r = new Random();
            CableStructure draft = (CableStructure)CableStructures.NewRow();
            //draft.CableStructureId = (uint)r.Next(9000000, 10000000); //(cable.CableStructures.Rows.Count > 0) ? ((CableStructure)cable.CableStructures.Rows[cable.CableStructures.Rows.Count-1]).CableStructureId + 1 : CableStructure.get_last_structure_id() + 1;
            draft.StructureTypeId = cable_structure_type_id;
            draft.OwnCable = this;
            draft.LeadMaterialTypeId = 1;
            draft.IsolationMaterialId = 1;
            draft.LeadDiameter = 0.1f;
            draft.WaveResistance = 0;
            draft.LeadToLeadTestVoltage = 0;
            draft.LeadToShieldTestVoltage = 0;
            draft.DRBringingFormulaId = 1;
            draft.DRFormulaId = 1;
            draft.Create();
            CableStructures.Rows.Add(draft);
            draft.AcceptChanges();
            return draft;
        }

        public CableStructure AddCableStructure(CableStructure copied_structure)
        {
            CableStructure structure = AddCableStructure(copied_structure.StructureTypeId);
            structure.CopyFromStructure(copied_structure);
            structure.OwnCable = this;
            structure.Save();
            return structure;
        }


        public static Cable build()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            Cable cable = (Cable)t.NewRow();
            return cable;
        }

        /// <summary>
        /// Выдаёт таблицу марок кабеля
        /// </summary>
        /// <returns></returns>
        public static DBEntityTable get_cable_marks()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable), DBEntityTableMode.NoColumns);
            t.TableName = "cable_marks";
            t.Columns.Add("cable_mark");
            string q = $"{t.SelectQuery} WHERE is_draft = 0 AND is_deleted = 0 ORDER BY name ASC";
            string selectString = " DISTINCT name AS cable_mark ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;    
        }

        public override bool Destroy()
        {
            this.IsDeleted = true;
            return this.Save();
        }

        protected override void ValidateActions()
        {
            base.ValidateActions();
            if (IsDraft || IsDeleted) return;
            validateCableMark();
            validateNormDoc();
            validateStructuresCount();
            
        }

        private void validateStructuresCount()
        {
            if (CableStructures.Rows.Count == 0) ErrorsList.Add("Не было добавлено ни одной структуры кабеля");
        }

        private void validateNormDoc()
        {
            if (DocumentId == 0 && this.QADocument == null) ErrorsList.Add("Не выбран нормативный документ");
        }

        private void validateCableMark()
        {
            if (String.IsNullOrWhiteSpace(Name)) ErrorsList.Add("Не выбрана марка кабеля");
        }

        public static Cable[] get_all_as_array()
        {
            DBEntityTable t = get_all_as_table();
            List<Cable> cables = new List<Cable>();
            if (t.Rows.Count > 0)
            {
                foreach (DataRow r in t.Rows) cables.Add((Cable)r);
            }
            return cables.ToArray();
        }

        public static DBEntityTable get_all_as_table()
        {
            return find_by_criteria("is_draft = 0 AND is_deleted = 0", typeof(Cable));
        }

        /// <summary>
        /// Выдает черновик кабеля
        /// </summary>
        /// <returns></returns>
        public static Cable GetDraft()
        {
            Cable draft = findDraft();
            if (draft == null) draft = createDraft();
            else draft.ClearStructures();
            return draft;
        }


        /// <summary>
        /// Ищет черновик кабеля
        /// </summary>
        /// <returns></returns>
        private static Cable findDraft()
        {
            DBEntityTable t = find_by_criteria("is_draft = 1", typeof(Cable));
            if (t.Rows.Count != 0)
            {
                Cable draft = (Cable)t.Rows[0];
                return draft;
            }

            else return null;
        }

        /// <summary>
        /// Создаёт черновик кабеля
        /// </summary>
        /// <returns></returns>
        private static Cable createDraft()
        {
            Cable draft = build();
            draft.IsDraft = true;
            if (draft.Create()) return findDraft();
            else return null;
        }

        public override bool Save()
        {
            try
            {
                return base.Save();
            }
            catch (DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Не удалось сохранить кабель...", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
        }


        public Cable(DataRowBuilder builder) : base(builder)
        {
        }


        [DBColumn(CableId_ColumnName, ColumnDomain.UInt, Order = 10, OldDBColumnName ="CabNum", Nullable =true, IsPrimaryKey = true, AutoIncrement = true)]
        public uint CableId
        {
            get
            {
                return tryParseUInt(CableId_ColumnName);
            }
            set
            {
                this[CableId_ColumnName] = value;
            }
        }


        [DBColumn("name", ColumnDomain.Tinytext, OldDBColumnName ="name", Order = 11, Nullable = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }

        [DBColumn("struct_name", ColumnDomain.Tinytext, OldDBColumnName = "CabNameStruct", Order = 12, DefaultValue = "", Nullable = true)]
        public string StructName
        {
            get
            {
                return  this["struct_name"].ToString();
            }
            set
            {
                this["struct_name"] = value;
            }
        }

        [DBColumn("notes", ColumnDomain.Tinytext, OldDBColumnName = "TextPrim", Order = 13, DefaultValue = "", Nullable = true)]
        public string Notes
        {
            get
            {
                return this["notes"].ToString();
            }
            set
            {
                this["notes"] = value;
            }
        }

        [DBColumn("code_okp", ColumnDomain.Char, OldDBColumnName = "KodOKP", Nullable =true,Size = 12, Order = 14, DefaultValue = "")]
        public string CodeOKP
        {
            get
            {
                return this["code_okp"].ToString();
            }set
            {
                this["code_okp"] = value;
            }
        }

        [DBColumn("code_kch", ColumnDomain.Char, OldDBColumnName = "KodOKP_KCH", Size = 2, Order = 15, DefaultValue = "", Nullable = true)]
        public string CodeKCH
        {
            get
            {
                return this["code_kch"].ToString();
            }
            set
            {
                this["code_kch"] = value;
            }
        }

        [DBColumn("linear_mass", ColumnDomain.Float, OldDBColumnName = "PogMass", Order = 16, DefaultValue = 0, Nullable = true)]
        public float LinearMass
        {
            get
            {
                return tryParseFloat("linear_mass");
            }
            set
            {
                this["linear_mass"] = value;
            }
        }

        [DBColumn("build_length", ColumnDomain.Float, OldDBColumnName = "StrLengt", Order = 17, DefaultValue = 1000, Nullable = true)]
        public float BuildLength
        {
            get
            {
                return tryParseFloat("build_length");
            }
            set
            {
                this["build_length"] = value;
            }
        }

        [DBColumn("document_id", ColumnDomain.UInt, OldDBColumnName = "DocInd", Order = 18, DefaultValue = 0, Nullable = true)]
        public uint DocumentId
        {
            get
            {
                return tryParseUInt("document_id");
            }
            set
            {
                this["document_id"] = value;
            }
        }

        [DBColumn("u_cover", ColumnDomain.UInt, OldDBColumnName = "U_Obol", Order = 19, DefaultValue = 0, Nullable = true)]
        public float UCover
        {
            get
            {
                return tryParseFloat("u_cover");
            }
            set
            {
                this["u_cover"] = value;
            }
        }

        [DBColumn("p_min", ColumnDomain.UInt, OldDBColumnName = "P_min", Order =20, DefaultValue = 2400, Nullable = true)]
        public float PMin
        {
            get
            {
                return tryParseFloat("p_min");
            }
            set
            {
                this["p_min"] = value;
            }
        }

        [DBColumn("p_max", ColumnDomain.UInt, OldDBColumnName = "P_max", Order = 21, DefaultValue =0, Nullable =true)]
        public float PMax
        {
            get
            {
                return tryParseFloat("p_max");
            }
            set
            {
                this["p_max"] = value;
            }
        }

        [DBColumn("is_draft", ColumnDomain.Boolean, Order = 22, DefaultValue = false)]
        public bool IsDraft
        {
            get
            {
                return tryParseBoolean("is_draft", false);
            }
            set
            {
                this["is_draft"] = value;
            }
        }

        [DBColumn("is_deleted", ColumnDomain.Boolean,  Order = 23, DefaultValue =false)]
        public bool IsDeleted
        {
            get
            {
                return tryParseBoolean("is_deleted", false);
            }
            set
            {
                this["is_deleted"] = value;
            }
        }


        /// <summary>
        /// Выдает нормативный документ по данному кабелю
        /// </summary>
        public Document QADocument
        {
            get
            {
                if (cableNormDocument == null)
                {
                    cableNormDocument = loadDocument();
                }
                return cableNormDocument;
            }
            set
            {
                cableNormDocument = value;
                this.DocumentId = value.DocumentId;
            }
        }

        /// <summary>
        /// Подгружаем документ по качеству
        /// </summary>
        /// <returns></returns>
        private Document loadDocument()
        {
            if (DocumentId == 0) return null;
            else
            {
                return Document.find_by_document_id(DocumentId);
            }
        }



        protected Document cableNormDocument;

        public DBEntityTable CableStructures
        {
            get
            {
                if (cableStructures == null)
                {
                    cableStructures = LoadCableStructures();
                }
                return cableStructures;
            }
        }

        protected virtual DBEntityTable LoadCableStructures()
        {
            return CableStructure.get_by_cable(this);
        }

        protected DBEntityTable cableStructures;


        public const string CableId_ColumnName = "cable_id";

    }

    
    [DBTable("tested_cables", "db_norma_sac", OldDBName = "bd_isp", OldTableName = "cables")]
    public class TestedCable : Cable
    {
        public TestedCable(DataRowBuilder builder) : base(builder)
        {
        }

        protected override DBEntityTable LoadCableStructures()
        {
            return TestedCableStructure.get_by_cable(this);
        }
    }


    
}
