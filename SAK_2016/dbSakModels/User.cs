using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace SAK_2016
{
    
    public class User : dbBase
    {

        public string id, last_name, name, third_name, password, role_id, employee_number, is_active;
        string dbName = "users"; 
        public User()
        {
            id = last_name = name = third_name = password = role_id = employee_number = is_active = "Default";
        }

        public User(DataRow r)
        {
            id = r["id"].ToString();
            last_name = r["last_name"].ToString();
            name = r["name"].ToString();
            third_name = r["third_name"].ToString();
            password = r["password"].ToString();
            employee_number = r["employee_number"].ToString();
            role_id = r["employee_number"].ToString();
            is_active = r["is_active"].ToString();

        }

        /// <summary>
        /// Добавляет пользователя в таблицу
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            prepareForLoadToDB();
            string query = String.Format("INSERT IGNORE INTO "+dbName+" VALUE "+ makeValsQuery("create") + "", id, last_name, name, third_name, employee_number, role_id, password, is_active);
            int sts = sendQuery(query);
            return (sts>0) ? true : false;
        }

        public bool CreateFromTable(DataTable t)
        {
            return false;
        }

        /// <summary>
        /// Подготавливает к заливке в БД
        /// </summary>
        private void prepareForLoadToDB()
        {
            id = (String.IsNullOrWhiteSpace(id)) ? "Default" : id;
            name = (String.IsNullOrWhiteSpace(name)) ? "NULL" : qStr(name);
            last_name = (String.IsNullOrWhiteSpace(last_name)) ? "NULL" : qStr(last_name);
            third_name = (String.IsNullOrWhiteSpace(third_name)) ? "NULL" : qStr(third_name);
            employee_number = (String.IsNullOrWhiteSpace(employee_number)) ? "DEFAULT" : employee_number;
            role_id = (String.IsNullOrWhiteSpace(role_id)) ? "3" : role_id;
            password = (String.IsNullOrWhiteSpace(password)) ? "1" : qStr(password);
            is_active = (String.IsNullOrWhiteSpace(is_active)) ? "1" : is_active;
        }
        /// <summary>
        /// Создает строку в зависимости от типа действия
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private string makeValsQuery(string action)
        {
            if (action == "update")
                return "id={0}, last_name={1}, name={2}, third_name={3}, employee_number={4}, role_id = {5}, password={6}, is_active={7}";
            else
                return "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})";
        } 
    }
}