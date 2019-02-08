﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace NormaMeasure.DBControl.SAC.DBEntities
{
    public class CableOld : DBSACBase
    {
        private static CableOld _cable;
        private List<CableStructure> _structures = null;
        private CableTest _test = null;
        private string _name, _name_was;
        private string _structName, _structName_was;
        private string _notes, _notes_was;
        private string _codeOKP, _codeOKP_was;
        private string _codeKCH, _codeKCH_was;
        private string _documentName, _documentName_was, _documentNumber, _documentNumber_was;
        private decimal _linearMass = -1,_linearMass_was = -1;
        private decimal _buildLength = -1, _buildLength_was= -1;
        private int _uCover = -1, _uCover_was = -1;
        private int _pMin = -1, _pMin_was = -1;
        private int _pMax = -1, _pMax_was = -1;
        private bool _isDraft, _isDeleted, _isDraft_was, _isDeleted_was;
        private uint _documentId, _documentId_was;


        #region Свойства кабеля
        public string DocumentName
        {
            get
            {
                return _documentName;
            }
            set
            {
                _documentName_was = _documentName;
                _documentName = value;
            }
        }

        public string DocumentNumber
        {
            get
            {
                return _documentNumber;
            }
            set
            {
                _documentNumber_was = _documentNumber;
                _documentNumber = value;
            }
        }
        public string Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes_was = this._notes;
                this._notes = value;
            }
        }
        public string FullName => $"{this.Name} {this.StructName}";


        /// <summary>
        /// Название кабеля
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name_was = this._name;
                this._name = value;
            }
        }

        public string StructName
        {
            get
            {
                return _structName;//this._structName;
            }
            set
            {
                this._structName_was = this._structName;
                this._structName = value;
            }
        }

        /// <summary>
        /// Код ОКП
        /// </summary>
        public string CodeOKP
        {
            get
            {
                return this._codeOKP;
            }
            set
            {
                this._codeOKP_was = this._codeOKP;
                this._codeOKP = value;
            }
        }

        /// <summary>
        /// Код КЧ
        /// </summary>
        public string CodeKCH
        {
            get
            {
                return this._codeKCH;
            }
            set
            {
                this._codeKCH_was = this._codeKCH;
                this._codeKCH = value;
            }
        }

        /// <summary>
        /// Линейный вес кабеля на километр длины
        /// </summary>
        public decimal LinearMass
        {
            get
            {
                return this._linearMass;
            }
            set
            {
                this._linearMass_was = this._linearMass;
                this._linearMass = value;
            }
        }

        /// <summary>
        /// Строительная длина кабеля
        /// </summary>
        public decimal BuildLength
        {
            get
            {
                return this._buildLength;
            }
            set
            {
                this._buildLength_was = this._buildLength;
                this._buildLength = value;
            }
        }

        /// <summary>
        /// Испытательное напряжение прочности изоляции
        /// </summary>
        public int UCover
        {
            get
            {
                return this._uCover;
            }
            set
            {
                this._uCover_was = this._uCover;
                this._uCover = value;
            }
        }

        /// <summary>
        /// Минимальное значение испытательного давления
        /// </summary>
        public int PMin
        {
            get
            {
                return this._pMin;
            }
            set
            {
                this._pMin_was = this._pMin;
                this._pMin = value;
            }
        }

        /// <summary>
        /// Максимальное значение испытательного давления
        /// </summary>
        public int PMax
        {
            get
            {
                return this._pMax;
            }
            set
            {
                this._pMax_was = this._pMax;
                this._pMax = value;
            }
        }

        public bool IsDraft
        {
            get
            {
               return _isDraft;
            }
            set
            {
                _isDraft_was = _isDraft;
                _isDraft = value;
            }
        }

        public bool IsDeleted => _isDeleted;

        public uint DocumentId
        {
            get
            {
                return _documentId;
            }
            set
            {
                _documentId_was = _documentId;
                _documentId = value;
            }
        }

        #endregion


        #region Конструкторы

        static CableOld()
        {
            _cable = new CableOld();
        }


        public CableOld()
        {
            initEntity();
            setDefaultProperties();
        }

        public CableOld(CableTest test) : this()
        {
            this._test = test;
            this._id = test.CableId;
            GetById();

        }

        public CableOld(uint id) : this()
        {
            this._id = id;
            GetById();
        }

        public CableOld(DataRow row) : this()
        {
            FillFromDataRow(row);
            //this._dataRow = row;
            //setDefaultParameters();
        }



        #endregion


        /// <summary>
        /// Достаёт черновик 
        /// Если его нет, то добавляет
        /// </summary>
        /// <returns></returns>
        public static CableOld GetDraft()
        {
            try
            {
                CableOld draft = findDraft();
                if (draft == null) draft = createDraft();
                return draft;
            }catch(DBEntityException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

        }

        private static CableOld findDraft()
        {
            string query = $"{_cable.DBTable.SelectQuery} WHERE is_draft = 1";
            DataTable dt = _cable.getFromDB(query);
            if (dt.Rows.Count != 0) return new CableOld(dt.Rows[0]);
            else return null;
        }

        private static CableOld createDraft()
        {
            CableOld c = new CableOld() { IsDraft = true };
            if (c.Save()) return c;
            else throw new DBEntityException("Не удалось создать черновик кабеля!");
        }

        public static new CableOld find(uint cable_id)
        {
            CableOld cable = new CableOld(cable_id);
            return cable;
        }

        public List<CableStructure> Structures
        {
            get
            {
                if (this._structures == null) this._structures = new List<CableStructure>(); ///loadStructures();
                return this._structures;
            }
        }


        public static DataTable GetCableMarks()
        {
            string query = $"SELECT DISTINCT {_cable.TableName}.name FROM {_cable.TableName} WHERE {_cable.TableName}.is_draft = 0 AND {_cable.TableName}.is_deleted = 0";
            return _cable.getFromDB(query);
        }

        public static CableOld[] GetAll()
        {
            CableOld[] els = new CableOld[] { };

            DataTable dt = _cable.GetAllFromDB();
            if (dt.Rows.Count > 0)
            {
                els = new CableOld[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++) els[i] = new CableOld(dt.Rows[i]);
            }
            return els;
        }



        protected override bool setPropertyByColumnName(object value, string colName)
        {
            switch (colName)
            {
                case "name":
                    _name = _name_was = value.ToString();
                    return true;
                case "struct_name":
                    _structName = _structName_was = value.ToString();
                    return true;
                case "build_length":
                    _buildLength = _buildLength_was = ServiceFunctions.convertToDecimal(value);
                    return true;
                case "document_id":
                    _documentId = _documentId_was = ServiceFunctions.convertToUInt(value);
                    return true;
                case "notes":
                    _notes = _notes_was = value.ToString();
                    return true;
                case "u_cover":
                    _uCover = _uCover_was = ServiceFunctions.convertToInt16(value);
                    return true;
                case "linear_mass":
                    _linearMass = _linearMass_was = ServiceFunctions.convertToDecimal(value);
                    return true;
                case "code_okp":
                    _codeOKP = _codeOKP_was = value.ToString();
                    return true;
                case "code_kch":
                    _codeKCH = _codeKCH_was = value.ToString();
                    return true;
                case "p_min":
                    _pMin = _pMin_was = ServiceFunctions.convertToInt16(value);
                    return true;
                case "p_max":
                    _pMax = _pMax_was = ServiceFunctions.convertToInt16(value);
                    return true;
                case "is_draft":
                    bool pdr = false;
                    bool.TryParse(value.ToString(), out pdr);
                    _isDraft = _isDraft_was = pdr;
                    return true;
                case "is_deleted":
                    bool pdel = false;
                    bool.TryParse(value.ToString(), out pdel);
                    _isDeleted = _isDeleted_was = pdel;
                    return true;
                case "document_short_name":
                    _documentNumber = value.ToString();
                    return true;
                case "document_full_name":
                    _documentName = value.ToString();
                    return true;
                default:
                    return false;
            }
        }

        protected override void setDefaultProperties()
        {
            _name = _name_was = string.Empty;
            _structName = _structName_was = string.Empty;
            _notes = _notes_was = "";
            _uCover = _uCover_was = 0;
            _linearMass = _linearMass_was = 0;
            _isDeleted = _isDeleted_was = false;
            _isDraft = _isDraft_was = false;
            _pMax = _pMax_was = 0;
            _pMin = _pMin_was = 0;
            _buildLength = _buildLength_was = 1000;
            _codeKCH = _codeKCH_was = "";
            _codeOKP = _codeOKP_was = "";
        }

        protected override string getPropertyValueByColumnName(string colName)
        {
            string value = null;
            switch(colName)
            {
                case "name":
                    return $"'{Name}'";
                case "struct_name":
                    return $"'{StructName}'";
                case "build_length":
                    return $"{BuildLength}";
                case "document_id":
                    return $"{DocumentId}";
                case "notes":
                    return $"'{Notes}'";
                case "u_cover":
                    return $"{UCover}";
                case "linear_mass":
                    return $"{LinearMass}";
                case "code_okp":
                    return $"'{CodeOKP}'";
                case "code_kch":
                    return $"'{CodeKCH}'";
                case "p_min":
                    return $"'{PMin}'";
                case "p_max":
                    return $"'{PMax}'";
                case "is_draft":
                    return $"{IsDraft}";
                case "is_deleted":
                    return $"{IsDeleted}";
                default:
                    return value;
            }
        }

        protected override void initEntity()
        {
            //_dbTable = DBSACTablesMigration.CablesTable;
        }


        /*
private void loadStructures()
{
CableStructure st = new CableStructure(this);
this.Structures = st.GetCableStructures();
}

/// <summary>
/// Выводим структуры у которых есть выход за норму
/// </summary>
/// <returns></returns>
public CableStructure[] GetFailedStructures()
{
List<CableStructure> failedStructs = new List<CableStructure>();
foreach (CableStructure cs in this.Structures)
{
if (cs.AffectedElements.Count() > 0)
{
failedStructs.Add(cs);
break;
}
foreach (MeasureParameterType pt in cs.MeasuredParameters)
{
if (pt.OutOfNormaCount() > 0)
{
failedStructs.Add(cs);
break;
}
}
}
return failedStructs.ToArray();
}
*/
    }


}