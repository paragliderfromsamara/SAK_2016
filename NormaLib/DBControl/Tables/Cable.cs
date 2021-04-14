using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl;
using System.Diagnostics;

namespace NormaLib.DBControl.Tables
{
    [DBTable("cables", "db_norma_measure", OldDBName = "bd_cable", OldTableName = "cables")]
    public class Cable : BaseEntity
    {

        public static DBEntityTable get_all_including_docs()
        {
            DBEntityTable docsTable = new DBEntityTable(typeof(Document));
            string select_cmd = $"SELECT *, CONCAT({CableName_ColumnName}, ' ', {StructName_ColumnName}) AS {FullCableName_ColumnName} FROM cables LEFT OUTER JOIN {docsTable.TableName} USING({docsTable.PrimaryKey[0]}) WHERE {IsDraftFlag_ColumnName} = 0";
            return find_by_query(select_cmd, typeof(Cable));
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

        public static Cable GetCableCopy(uint cable_id) 
        {
            Cable c = find_by_cable_id(cable_id);
            return c == null ? null : GetCableCopy(c);
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

        public bool RemoveStructure(CableStructure structure)
        {
            bool f = true;
          
            DataRow[] rows = cableStructures.Select($"{CableStructure.StructureId_ColumnName} = {structure.CableStructureId}");
            if (rows.Length > 0)
            {
                if (!structure.IsNewRecord()) f = structure.Destroy();
                if (f && rows.Length > 0) cableStructures.Rows.Remove(rows[0]);
            }else
            {
                f = false;
            }
            return f;
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

        public virtual CableStructure AddCableStructure(CableStructure copied_structure)
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
            string q = $"{t.SelectQuery} WHERE {IsDraftFlag_ColumnName} = 0 ORDER BY {CableName_ColumnName} ASC";
            string selectString = $" DISTINCT {CableName_ColumnName} AS cable_mark ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;    
        }

        public static DBEntityTable get_PminValues()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable), DBEntityTableMode.NoColumns);
            t.TableName = "p_min_values";
            t.Columns.Add("value");
            string q = $"{t.SelectQuery} WHERE {IsDraftFlag_ColumnName} = 0 ORDER BY {PMin_ColumnName} ASC";
            string selectString = $" DISTINCT {PMin_ColumnName} AS value ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;
        }

        public static DBEntityTable get_PmaxValues()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable), DBEntityTableMode.NoColumns);
            t.TableName = "p_max_values";
            t.Columns.Add("value");
            string q = $"{t.SelectQuery} WHERE {IsDraftFlag_ColumnName} = 0 ORDER BY {PMax_ColumnName} ASC";
            string selectString = $" DISTINCT {PMax_ColumnName} AS value ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;
        }

        public static DBEntityTable get_CoverTestVoltageValues()
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable), DBEntityTableMode.NoColumns);
            t.TableName = "cover_voltage_values";
            t.Columns.Add("value");
            string q = $"{t.SelectQuery} WHERE {IsDraftFlag_ColumnName} = 0 ORDER BY {UCover_ColumnName} ASC";
            string selectString = $" DISTINCT {UCover_ColumnName} AS value ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;
        }

        protected override void ValidateActions()
        {
            base.ValidateActions();
            if (IsDraft) return;
            validateCableMark();
            validateNormDoc();
            validateStructuresCount();
        }

        private void validateStructuresCount()
        {
            bool f = false;
            if (CableStructures.Rows.Count == 0) f = true;  
            else
            {
                f = true;
                foreach(CableStructure s in CableStructures.Rows)
                {

                    f &= (s.RowState == DataRowState.Deleted);
                }
            }
            if (f)ErrorsList.Add("Не было добавлено ни одной структуры кабеля");
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
            string select_cmd = $"SELECT *, CONCAT({CableName_ColumnName}, ' ', {StructName_ColumnName}) AS {FullCableName_ColumnName} FROM cables WHERE {IsDraftFlag_ColumnName} = 0";
            return find_by_query(select_cmd, typeof(Cable));
           // return find_by_criteria($"{IsDraftFlag_ColumnName} = 0", typeof(Cable));
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
            DBEntityTable t = find_by_criteria($"{IsDraftFlag_ColumnName} = 1", typeof(Cable));
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

        #region Колонки таблицы
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


        [DBColumn(CableName_ColumnName, ColumnDomain.Tinytext, OldDBColumnName ="name", Order = 11, Nullable = true)]
        public string Name
        {
            get
            {
                return this[CableName_ColumnName].ToString();
            }
            set
            {
                this[CableName_ColumnName] = value;
            }
        }

        [DBColumn(StructName_ColumnName, ColumnDomain.Tinytext, OldDBColumnName = "CabNameStruct", Order = 12, DefaultValue = "", Nullable = true)]
        public string StructName
        {
            get
            {
                return  this[StructName_ColumnName].ToString();
            }
            set
            {
                this[StructName_ColumnName] = value;
            }
        }

        [DBColumn(Notes_ColumnName, ColumnDomain.Tinytext, OldDBColumnName = "TextPrim", Order = 13, DefaultValue = "", Nullable = true)]
        public string Notes
        {
            get
            {
                return this[Notes_ColumnName].ToString();
            }
            set
            {
                this[Notes_ColumnName] = value;
            }
        }

        [DBColumn(CodeOKP_ColumnName, ColumnDomain.Char, OldDBColumnName = "KodOKP", Nullable =true,Size = 12, Order = 14, DefaultValue = "")]
        public string CodeOKP
        {
            get
            {
                return this[CodeOKP_ColumnName].ToString();
            }set
            {
                this[CodeOKP_ColumnName] = value;
            }
        }

        [DBColumn(CodeKCH_ColumnName, ColumnDomain.Char, OldDBColumnName = "KodOKP_KCH", Size = 2, Order = 15, DefaultValue = "", Nullable = true)]
        public string CodeKCH
        {
            get
            {
                return this[CodeKCH_ColumnName].ToString();
            }
            set
            {
                this[CodeKCH_ColumnName] = value;
            }
        }

        [DBColumn(LinearMass_ColumnName, ColumnDomain.Float, OldDBColumnName = "PogMass", Order = 16, DefaultValue = 0, Nullable = true)]
        public float LinearMass
        {
            get
            {
                return tryParseFloat(LinearMass_ColumnName);
            }
            set
            {
                this[LinearMass_ColumnName] = value;
            }
        }

        [DBColumn(BuildLength_ColumnName, ColumnDomain.Float, OldDBColumnName = "StrLengt", Order = 17, DefaultValue = 1000, Nullable = true)]
        public float BuildLength
        {
            get
            {
                return tryParseFloat(BuildLength_ColumnName);
            }
            set
            {
                this[BuildLength_ColumnName] = value;
            }
        }

        [DBColumn(Document.DocumentId_ColumnName, ColumnDomain.UInt, OldDBColumnName = "DocInd", Order = 18, DefaultValue = 0, Nullable = true)]
        public uint DocumentId
        {
            get
            {
                return tryParseUInt(Document.DocumentId_ColumnName);
            }
            set
            {
                this[Document.DocumentId_ColumnName] = value;
            }
        }

        [DBColumn(UCover_ColumnName, ColumnDomain.UInt, OldDBColumnName = "U_Obol", Order = 19, DefaultValue = 0, Nullable = true)]
        public float UCover
        {
            get
            {
                return tryParseFloat(UCover_ColumnName);
            }
            set
            {
                this[UCover_ColumnName] = value;
            }
        }

        [DBColumn(PMin_ColumnName, ColumnDomain.UInt, OldDBColumnName = "P_min", Order =20, DefaultValue = 2400, Nullable = true)]
        public float PMin
        {
            get
            {
                return tryParseFloat(PMin_ColumnName);
            }
            set
            {
                this[PMin_ColumnName] = value;
            }
        }

        [DBColumn(PMax_ColumnName, ColumnDomain.UInt, OldDBColumnName = "P_max", Order = 21, DefaultValue =0, Nullable =true)]
        public float PMax
        {
            get
            {
                return tryParseFloat(PMax_ColumnName);
            }
            set
            {
                this[PMax_ColumnName] = value;
            }
        }

        [DBColumn(IsDraftFlag_ColumnName, ColumnDomain.Boolean, Order = 22, DefaultValue = false)]
        public bool IsDraft
        {
            get
            {
                return tryParseBoolean(IsDraftFlag_ColumnName, false);
            }
            set
            {
                this[IsDraftFlag_ColumnName] = value;
            }
        }

        [DBColumn(FullCableName_ColumnName, ColumnDomain.Varchar, Order = 23, IsVirtual = true, Nullable = true)]
        public string FullName
        {
            get
            {
                return String.IsNullOrWhiteSpace(this[FullCableName_ColumnName].ToString()) ? $"{Name} {StructName}" : this[FullCableName_ColumnName].ToString();
            }
            set
            {
                this[FullCableName_ColumnName] = value;
            }
        }


        public const string CableId_ColumnName = "cable_id";
        public const string CableName_ColumnName = "name";
        public const string StructName_ColumnName = "struct_name";
        public const string Notes_ColumnName = "notes";
        public const string CodeOKP_ColumnName = "code_okp";
        public const string CodeKCH_ColumnName = "code_kch";
        public const string LinearMass_ColumnName = "linear_mass";
        public const string BuildLength_ColumnName = "build_length";
        public const string UCover_ColumnName = "u_cover";
        public const string PMin_ColumnName = "p_min";
        public const string PMax_ColumnName = "p_max";
        public const string IsDraftFlag_ColumnName = "is_draft";
        public const string FullCableName_ColumnName = "full_cable_name";

        #endregion

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
                    if (cableNormDocument != null) this.DocumentId = cableNormDocument.DocumentId;
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

        /// <summary>
        /// Находит все типы измеряемых параметров для данного кабеля
        /// </summary>
        public DBEntityTable MeasuredParameterTypes
        {
            get
            {
                if(measured_parameters == null)
                {
                    measured_parameters = MeasuredParameterType.get_all_by_ids(MeasuredParameterTypes_IDs);
                }
                return measured_parameters;
            }
        }

        private DBEntityTable measured_parameters;

        public uint[] MeasuredParameterTypes_IDs
        {
            get
            {
                if (MeasuredParameterTypes_ids == null)
                {
                    List<uint> ids = new List<uint>();
                    foreach(CableStructure s in CableStructures.Rows)
                    {
                        foreach(CableStructureMeasuredParameterData mpd in s.MeasuredParameters.Rows)
                        {
                            if (!ids.Contains(mpd.ParameterTypeId)) ids.Add(mpd.ParameterTypeId);
                        }
                    }
                    MeasuredParameterTypes_ids = ids.ToArray();
                }
                return MeasuredParameterTypes_ids;
            }
        }

        private uint[] MeasuredParameterTypes_ids;



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
            set
            {
                cableStructures = value;
            }

        }

        protected virtual DBEntityTable LoadCableStructures()
        {
            return CableStructure.get_by_cable(this);
        }

        protected DBEntityTable cableStructures;

    }

    
    [DBTable("tested_cables", "db_norma_measure", OldDBName = "bd_isp", OldTableName = "cables")]
    public class TestedCable : Cable
    {
        private CableTest cableTest;
        public CableTest CableTest => cableTest;
        public void SetCableTest(CableTest cab_test)
        {
            cableTest = cab_test;
            TestId = cableTest.TestId;
        }
        public TestedCable(DataRowBuilder builder) : base(builder)
        {
        }

        protected override void ValidateActions()
        {
            
        }

        /// <summary>
        /// Выдаёт таблицу марок кабеля
        /// </summary>
        /// <returns></returns>
        public static DBEntityTable get_tested_cable_marks()
        {
            DBEntityTable t = new DBEntityTable(typeof(TestedCable), DBEntityTableMode.NoColumns);
            t.TableName = "full_cable_marks";
            t.Columns.Add(FullCableName_ColumnName);
            string q = $"{t.SelectQuery} WHERE {IsDraftFlag_ColumnName} = 0 ORDER BY {FullCableName_ColumnName} ASC";
            string selectString = $" DISTINCT CONCAT({CableName_ColumnName}, ' ', {StructName_ColumnName}) AS {FullCableName_ColumnName} ";
            q = q.Replace("*", selectString);
            t.FillByQuery(q);
            return t;
        }

        public static TestedCable find_by_test_id(uint test_id)
        {
            DBEntityTable t = find_by_criteria($"{CableTest.CableTestId_ColumnName} = {test_id}", typeof(TestedCable));
            if (t.Rows.Count == 0) return null;
            else return (TestedCable)t.Rows[0];
        }

        public static new TestedCable find_by_cable_id(uint id)
        {
            DBEntityTable t = find_by_primary_key(id, typeof(Cable));//new DBEntityTable(typeof(Cable));
            if (t.Rows.Count > 0) return (TestedCable)t.Rows[0];
            else
            {
                return null;
            }
        }

        public static TestedCable create_for_test(CableTest test)
        {
            DBEntityTable t = new DBEntityTable(typeof(TestedCable));
            TestedCable tCable = (TestedCable)t.NewRow();
            tCable.TestId = test.TestId;
            tCable.FillColsFromEntity(test.SourceCable);
            tCable.Save();
            t.Rows.Add(tCable);
            tCable.AddStructuresFromCable(test.SourceCable);
            return tCable; 
            //return (TestedCable)GetCableCopy(source_cable);
        }

        private void AddStructuresFromCable(Cable copiedCable)
        {
            foreach (CableStructure cabStruct in copiedCable.CableStructures.Rows)
            {
                AddCableStructure(cabStruct);
            }
        }

        private new TestedCableStructure AddCableStructure(uint cable_structure_type_id)
        {
            Random r = new Random();
            TestedCableStructure draft = (TestedCableStructure)CableStructures.NewRow();
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

        private new TestedCableStructure AddCableStructure(CableStructure copied_structure)
        {
            TestedCableStructure structure = AddCableStructure(copied_structure.StructureTypeId);
            structure.CopyFromStructure(copied_structure);
            structure.OwnCable = this;
            structure.SourceStructureId = copied_structure.CableStructureId;
            structure.Save();
            return structure;
        }

        protected override DBEntityTable LoadCableStructures()
        {
            DBEntityTable structs = TestedCableStructure.get_by_cable(this);
            foreach (TestedCableStructure s in structs.Rows) s.OwnCable = this;
            return structs;
        }

        #region Колонки таблицы
        [DBColumn(CableTest.CableTestId_ColumnName, ColumnDomain.UInt, Order = 24, ReferenceTo = "cable_tests(" + CableTest.CableTestId_ColumnName + ") ON DELETE CASCADE")]
        public uint TestId
        {
            get
            {
                return tryParseUInt(CableTest.CableTestId_ColumnName);
            }
            set
            {
                this[CableTest.CableTestId_ColumnName] = value;
            }
        }

        #endregion


        protected CableTest cable_test;
    }


    
}
