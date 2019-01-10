using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NormaMeasure.SAC_APP
{
    public class Document : dbBase
    {
        public string shortName, fullName;

        public Document()
        {
            this.tableName = "documents";
        }

        public Document(long doc_id)
        {
            this.tableName = "documents";
            this.id = doc_id;
            this.initFromDb();

        }
        protected override void fillMainParameters()
        {
            DataRow dr = dbParams[0];
            this.shortName = dr["short_name"].ToString();
            this.fullName = dr["full_name"].ToString();
        }
    }
}
