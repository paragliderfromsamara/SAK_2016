using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaDB;

namespace NormaDB.SAC
{
    public abstract class DBSACBase : DBBase
    {
        static DBSACBase()
        {
            dbName = "db_sak";
        }
        protected override void setDefaultParameters()
        {
            throw new NotImplementedException();
        }
    }
}
