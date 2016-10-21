using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAK_2016
{
    public class CableStructure:dbBase
    {
        private DBControl mySql = new DBControl(Properties.dbSakQueries.Default.dbName);
        public CableStructure(long id)
        {
            
        }

        public static CableStructure[] getStructuresByCableID(long cable_id)
        {
            CableStructure[] cableStructures = new CableStructure[] {};

            return cableStructures;
        }
    }
}