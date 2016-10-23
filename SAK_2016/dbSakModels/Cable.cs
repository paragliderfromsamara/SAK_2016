using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace SAK_2016
{

    public class Cable : dbBase
    {
        private DBControl mySql = new DBControl(Properties.dbSakQueries.Default.dbName);
        //Характеристики кабеля
        public uint documentId;
        public string name, structName, notes, codeOkp, codeKch;
        public decimal buildLength, linearMass, uCover, pMin, pMax;
        
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
            this.getFromDB();
            if (this.isExistsInDB)
            {
                fillMainParameters();
            }                
        }

        /// <summary>
        /// Собирает параметры кабеля
        /// </summary>
        public void fillMainParameters()
        {
            this.name = dbParams["name"].ToString();
            this.structName = dbParams["struct_name"].ToString();
            this.notes = dbParams["notes"].ToString();
            this.codeOkp = dbParams["code_okp"].ToString();
            this.codeKch = dbParams["code_kch"].ToString();
            this.buildLength = ServiceFunctions.convertToDecimal(dbParams["build_length"]);
            this.linearMass = ServiceFunctions.convertToDecimal(dbParams["linear_mass"]);
            this.uCover = ServiceFunctions.convertToDecimal(dbParams["u_cover"]); 
            this.pMin = ServiceFunctions.convertToDecimal(dbParams["p_min"]); 
            this.pMax = ServiceFunctions.convertToDecimal(dbParams["p_max"]);
            this.documentId = ServiceFunctions.convertToUInt("document_id");
        }


    }
}