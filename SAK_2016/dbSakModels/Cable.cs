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
        public long documentId;
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
            this.buildLength = (long)dbParams["build_length"];
            this.linearMass = (long)dbParams["linear_mass"];
            this.uCover = (long)dbParams["u_cover"];
            this.pMin = (long)dbParams["p_min"];
            this.pMax = (long)dbParams["p_max"];
        }

    }
}