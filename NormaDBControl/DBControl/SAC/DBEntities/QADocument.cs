using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NormaMeasure.DBControl.SAC.DBEntities
{
    public class QADocument : DBSACBase
    {
        private static QADocument qaDoc;
        private string _short_name, _full_name;

        public string ShortName
        {
            set
            {
                _short_name = value;
            }get
            {
                return _short_name;
            }
        }

        public string FullName
        {
            get
            {
                return _full_name;
            }
            set
            {
                _full_name = value;
            }
        }


        #region Конструкторы

        static QADocument()
        {
            qaDoc = new QADocument();
        }

        public QADocument()
        {
            initEntity();
        }

        public QADocument(uint id) : this()
        {
            _id = id;
            GetById();
        }



        public static QADocument[] GetAll()
        {
            QADocument[] els = new QADocument[] { };
            DataTable dt = qaDoc.GetAllFromDB();
            if (dt.Rows.Count > 0)
            {
                els = new QADocument[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++) els[i] = new QADocument(dt.Rows[i]);
            }
            return els;
        }


        public QADocument(DataRow row) : this()
        {
            FillFromDataRow(row);
        }

        #endregion 
        protected override string getPropertyValueByColumnName(string colName)
        {
            string value = null;
            switch (colName)
            {
                case "short_name":
                    return $"'{ShortName}'";
                case "full_name":
                    return $"'{FullName}'";
                default:
                    return value;
            }
        }


        protected override bool setPropertyByColumnName(object value, string colName)
        {
            switch (colName)
            {
                case "short_name":
                    _short_name = value.ToString();
                    return true;
                case "full_name":
                    _full_name = value.ToString();
                    return true;
                default:
                    return false;
            }
        }

        protected override void setDefaultProperties()
        {
            _full_name = "";
            _short_name = "";
        }

        protected override void initEntity()
        {
           // _dbTable = DBSACTablesMigration.DocumentsTable;
        }
    }
}
