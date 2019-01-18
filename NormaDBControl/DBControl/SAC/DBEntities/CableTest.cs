using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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



        protected override string getPropertyValueByColumnName(string name)
        {
            throw new NotImplementedException();
        }

        protected override void initEntity()
        {
            throw new NotImplementedException();
        }

        protected void setDefaultParameters()
        {
            throw new NotImplementedException();
        }

        protected override void setDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override bool setPropertyByColumnName(object value, string colName)
        {
            throw new NotImplementedException();
        }
    }
}
