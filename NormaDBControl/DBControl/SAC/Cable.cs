using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace NormaDB.SAC
{
    public class Cable : DBSACBase
    {
        private List<CableStructure> _structures = null;
        private CableTest _test = null;
        private string _name, _name_was;
        private string _structName, _structName_was;
        private string _notes, _notes_was;
        private string _codeOKP, _codeOKP_was;
        private string _codeKCH, _codeKCH_was;
        private string _qaDocumentName, _qaDocumentNumber;
        private decimal _linearMass = -1,_linearMass_was = -1;
        private decimal _buildLength = -1, _buildLength_was= -1;
        private int _uCover = -1, _uCover_was = -1;
        private int _pMin = -1, _pMin_was = -1;
        private int _pMax = -1, _pMax_was = -1;

        public string Notes
        {
            get
            {
                if (this._notes == null)
                {
                    this._notes_was = this._notes = getStringValueFromDataRow("notes");
                }
                return this._notes;
            }
            set
            {
                this._notes_was = this._notes;
                this._notes = value;
            }
        }
        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", this.Name, this.StructName);
            }
        }

        /// <summary>
        /// Название кабеля
        /// </summary>
        public string Name
        {
            get
            {
                if (this._name == null)
                {
                    this._name_was = this._name = getStringValueFromDataRow("name");
                }
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
                if (this._structName == null)
                {
                    this._structName_was = this._structName = getStringValueFromDataRow("struct_name");
                }
                return this._structName;
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
                if (this._codeOKP == null)
                {
                    this._codeOKP_was = this._codeOKP = getStringValueFromDataRow("code_okp");
                }
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
                if (this._codeKCH == null)
                {
                    this._codeKCH_was = this._codeKCH = getStringValueFromDataRow("code_kch");
                }
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
                if (this._linearMass == -1)
                {
                    this._linearMass_was = this._linearMass = getDecimalValueFromDataRow("linear_weight");
                }
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
                if (this._buildLength == -1)
                {
                    this._buildLength_was = this._buildLength = getDecimalValueFromDataRow("build_length");
                }
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
                if (this._uCover == -1)
                {
                    this._uCover_was = this._uCover = getIntValueFromDataRow("u_obol");
                }
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
                if (this._pMin == -1)
                {
                    this._pMin_was = this._pMin = getIntValueFromDataRow("p_min");
                }
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
                if (this._pMax == -1)
                {
                    this._pMax_was = this._pMax = getIntValueFromDataRow("p_max");
                }
                return this._pMax;
            }
            set
            {
                this._pMax_was = this._pMax;
                this._pMax = value;
            }
        }

        public List<CableStructure> Structures
        {
            get
            {
                if (this._structures == null) this._structures = new List<CableStructure>(); ///loadStructures();
                return this._structures;
            }
        }

        static Cable()
        {
            tableName = "cables";
        }

        public Cable()
        {
            setDefaultParameters();
        }

        public Cable(CableTest test)
        {
            this._test = test;
            this._id = test.CableId;
            setDefaultParameters();
            GetById();

        }

        public Cable(uint id)
        {
            this._id = id;
            setDefaultParameters();
            GetById();
        }

        public Cable(DataRow row)
        {
            this._dataRow = row;
            setDefaultParameters();
        }


        protected override void setDefaultParameters()
        {
            string selectQuery = "cables.id AS id, CONCAT(cables.name,' ', cables.struct_name) AS full_name, cables.name AS name, cables.notes AS notes, cables.struct_name AS struct_name, cables.build_length AS build_length, cables.document_id AS document_id, cables.linear_mass AS linear_mass, cables.code_okp AS code_okp, cables.code_kch AS code_kch, cables.u_cover AS u_cover, cables.p_min AS p_min, cables.p_max AS p_max, documents.full_name AS document_full_name, documents.short_name AS document_name";
            this.getAllQuery = String.Format("SELECT {0} FROM cables LEFT JOIN documents ON cables.document_id = documents.id", selectQuery);
            this.getByIdQuery = String.Format("SELECT {0} FROM cables LEFT JOIN documents ON cables.document_id = documents.id WHERE cables.id = {1}", selectQuery, this.id);
            colsList = new string[] {
                                        "id",
                                        "full_name",
                                        "name",
                                        "struct_name",
                                        "document_id",
                                        "code_okp",
                                        "code_kch",
                                        "build_length",
                                        "linear_weight",
                                        "u_obol",
                                        "p_min",
                                        "p_max",
                                        "document_name",
                                        "document_number"
                                    };
        }

        public Cable[] GetAll()
        {
            Cable[] els = new Cable[] { };
            DataTable dt = GetAllFromDB();
            if (dt.Rows.Count > 0)
            {
                els = new Cable[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++) els[i] = new Cable(dt.Rows[i]);
            }
            return els;
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
