using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace SAK_2016
{

    public class Cable : dbBase
    {

        //Характеристики кабеля
        //Собственные атрибуты--------------------------------------------
        public Document cableDocument;
        public CableStructuresList structures;
        public uint documentId;
        public decimal buildLength, linearMass, uCover, pMin, pMax;
        public string name, structName, notes, codeOkp, codeKch;
        //----------------------------------------------------------------
        //Атрибуты привязанного документа
        public string document_short_name, document_full_name;   
        //----------------------------------------------------------------
        
        //---------------------------------------------------------------------------------------------

        public Cable()
        {
            this.tableName = "cables";
        }
        /// <summary>
        /// Ищет кабель по id
        /// </summary>
        /// <param name="cableId"></param>
        public Cable(long cableId)
        {
            this.tableName = "cables";
            this.id = cableId;
            this.initFromDb();
            
        }


        protected override void fillMainParameters()
        {
            DataRow dr = dbParams[0];
            this.name = dr["name"].ToString();
            this.structName = dr["struct_name"].ToString();
            this.notes = dr["notes"].ToString();
            this.codeOkp = dr["code_okp"].ToString();
            this.codeKch = dr["code_kch"].ToString();
            this.buildLength = ServiceFunctions.convertToDecimal(dr["build_length"]);
            this.linearMass = ServiceFunctions.convertToDecimal(dr["linear_mass"]);
            this.uCover = ServiceFunctions.convertToDecimal(dr["u_cover"]); 
            this.pMin = ServiceFunctions.convertToDecimal(dr["p_min"]); 
            this.pMax = ServiceFunctions.convertToDecimal(dr["p_max"]);
            this.documentId = ServiceFunctions.convertToUInt(dr["document_id"]);
            this.cableDocument = new Document(this.documentId); //Подгружаем документ
            this.structures = new CableStructuresList(this.id);
        }


    }
}