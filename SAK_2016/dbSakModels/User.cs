using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace SAK_2016
{
    
    public class User : dbBase
    {

        public dbColumn id = new dbColumn("id", "int");
        public dbColumn last_name = new dbColumn("last_name", "string");
        public dbColumn name = new dbColumn("name", "string");
        public dbColumn third_name = new dbColumn("third_name", "string");
        public dbColumn password = new dbColumn("password", "string");
        public dbColumn role_id = new dbColumn("role_id", "int");
        public dbColumn employee_number = new dbColumn("employee_number", "int");
        public dbColumn is_active = new dbColumn("is_active", "int");
        public dbColumn role_name = new dbColumn("role_name", "string");


        /// <summary>
        /// Инициализирует объект User
        /// </summary>
        private void updRows()
        {
            this.dbCols = new dbColumn[] { id, last_name, name, third_name, employee_number, role_id, password, is_active };
        }
        private void init()
        {
            this.tableName = "users";
            updRows();
        }
        public User()
        {
            id.value = last_name.value = name.value = third_name.value = password.value = role_id.value = employee_number.value = is_active.value = "Default";
            init();
        }

        

        public User(DataRow r)
        {
            id.value = r[id.key].ToString();
            last_name.value = r[last_name.key].ToString();
            name.value = r[name.key].ToString();
            third_name.value = r[third_name.key].ToString();
            password.value = convertByteArrayToString(r[password.key].ToString());
            employee_number.value = r[employee_number.key].ToString();
            role_id.value = r[role_id.key].ToString();
            is_active.value = r[is_active.key].ToString();
            init();
        }
      //  private override init()
       // {

        //}
        /// <summary>
        /// Добавляет пользователя в таблицу
        /// </summary>
        /// <returns></returns>

        public bool CreateFromTable(DataTable t)
        {
            return false;
        }



        private bool isExists()
        {
            string query = String.Format("SELECT * From {0} WHERE last_name={1}");
            return true;
        }

    }
}