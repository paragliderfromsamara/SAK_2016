using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    public partial class DBEntityControlForm : Form
    {
        protected void AddOrMergeTableToFormDataSet(DataTable t)
        {
            if (formDataSet.Tables.Contains(t.TableName))
            {
                formDataSet.Tables[t.TableName].Merge(t);
            }
            else
            {
                formDataSet.Tables.Add(t);
            }

        }

        protected DataSet formDataSet;
    }
}
