using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAK_2016
{
    public class CableStructuresList:dbBase
    {

        public CableStructure[] list;
        public CableStructuresList()
        {
            this.tableName = "cable_structures";
        }
        public CableStructuresList(long cable_id)
        {
            this.tableName = "cable_structures";
            string q = String.Format(Properties.dbSakQueries.Default.selectCableStructuresListByCableId, cable_id);
            initFromDb(q);
        }

        protected override void fillMainParameters()
        {
            int c = this.dbParams.Count;
            if (c > 0)
            {
                this.list = new CableStructure[c];
                for(int i=0; i<c; i++)
                {
                    this.list[i] = new CableStructure(dbParams[i]); 
                }
            }
        }

    }
}
