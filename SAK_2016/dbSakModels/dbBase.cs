using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace SAK_2016
{
    public class dbBase
    {
        string[] columns;
        string[] columns_values;
        /// <summary>
        /// Облачает текст в ковычки. Необходимо для преобразование строковых значений в формат sql запросов
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string qStr(string text)
        {
            return String.Format("\"{0}\"", text);
        }
        protected int sendQuery(string query)
        {
            int sts = 0;
            try
            {
                MySqlConnection dbCon = new MySqlConnection(Properties.Settings.Default.rootConnectionString);
                MySqlCommand cmd = new MySqlCommand(query, dbCon);
                dbCon.Open();
                sts = cmd.ExecuteNonQuery();
                dbCon.Close();
                dbCon.Dispose();
                cmd.Dispose();
                return sts;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK);
                return sts;
            }

        }
    }


}