using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NormaMeasure.DBControl.SAC
{
    public abstract class DBSACBase : DBBase
    {
        static DBSACBase()
        {
            dbName = "db_sak";
        }
        protected void setDefaultParameters()
        {
            throw new NotImplementedException();
        }
    }
}
