using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace NormaMeasure.DBControl.SAC.DBEntities
{
    public class CableTest : DBEntityBase
    {
        private uint _cableId = 0;
        public uint CableId
        {
            get
            {
                return _cableId;
            }
        }

        protected override void fillEntityFromReader(MySqlDataReader r)
        {
            throw new NotImplementedException();
        }

        protected void setDefaultParameters()
        {
            throw new NotImplementedException();
        }
    }
}
