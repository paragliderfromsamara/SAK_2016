using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using MySql.Data.MySqlClient;

namespace SAK_2016
{
        class DBControl : IDisposable
        {
            #region Данные класса DBControl
            public MySqlConnection MyConn;
          
            MySqlCommand MC;
            private string Host = "localhost";
            private string User = "root";
            private string Password = "";
            private string cur_base;
            private string encoding = "cp1251";
            public string HOST { get { return Host; } }
            public string USER { get { return User; } }
            public string PASSWORD { get { return Password; } }
            private string command;
            public string Command { set { command = value; } }
            #endregion
            //------------------------------------------------------------------------------------------------------------------------
            public DBControl(string hst, string usr, string pswrd)
            {
                Host = hst;
                User = usr;
                Password = pswrd;
                string connstr = "UserId=" + User + ";Server=" + Host + ";Password=" + Password + ";";
                try
                {
                    MyConn.ConnectionString = connstr;
                    //MyConn.Direct = false;
                    MyConn.Open();
                    MC = new MySqlCommand("SET NAMES '"+encoding+"'", MyConn);
                    MC.ExecuteScalar();
                }
                catch (MySqlException ex)
                {
                    throw new DBException(ex.ErrorCode, ex.Message);
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            public DBControl(string cb)
            {
                cur_base = cb;
                IsolatedStorageFile storFile = IsolatedStorageFile.GetUserStoreForDomain();
                try
                {
                    StreamReader storStream = new StreamReader(new IsolatedStorageFileStream("sql_queries.xml", FileMode.Open, storFile));
                    System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(storStream);
                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            case "Host":
                                Host = reader.ReadString();
                                break;
                            case "User":
                                User = reader.ReadString();
                                break;
                            case "Password":
                                Password = reader.ReadString();
                                break;
                        }
                    }
                    storStream.Close();
                }
                catch (Exception) { }
                storFile.Close();
                string connstr = "UserId=" + User + ";Server=" + Host + ";Password=" + Password + ";";
                try
                {
                    if (Host == null || Host == "") throw new DBException("Неверный хост!");
                    MyConn = new MySqlConnection(connstr);
                   // MyConn.Direct = false;
                    MyConn.Open();
                    MC = new MySqlCommand("USE " + cur_base, MyConn);
                    if (cur_base != "") MC.ExecuteScalar();
                    MC.CommandText = "SET NAMES '" + encoding + "'";
                    MC.ExecuteScalar();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException(ex.ErrorCode, "MySQL сервер не доступен!  ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException(0, "MySQL сервер не доступен!  ");
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            public void CheckAndCreateTables()
            {
                try
                {
                    MC.CommandText = "SET NAMES '"+ encoding + "';";
                    MC.ExecuteScalar();
                    MC.CommandText = GetSQLCommand("CreateTables");
                    MC.ExecuteScalar();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + " №" + ex.ErrorCode.ToString(), "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            public void Dispose()
            {
                MyConn.Close();
                MyConn.Dispose();
                MC.Dispose();
            }
            //------------------------------------------------------------------------------------------------------------------------
            public string GetSQLCommand(string ncom)
            {
                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.IgnoreWhitespace = true;
                    settings.IgnoreComments = true;
                    XmlReader reader = XmlReader.Create("sql_queries.xml", settings);
                    string scom = "";
                    while (reader.Read()) if (reader.Name == ncom) { scom = reader.ReadString(); break; }
                    reader.Close();
                    if (scom == "") throw new DBException("Не найдена команда:'" + ncom + "' в sql_queries.xml!  ");
                    return scom;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Не найден файл 'sql_queries.xml'! Повторная установка приложения поможет решить эту проблему!  ", "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException("");
                }
                catch (XmlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException("");
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            public MySqlDataReader GetReaderXML(string comtp)
            {
                string sc = GetSQLCommand(comtp);
                return GetReader(sc);
            }
            //------------------------------------------------------------------------------------------------------------------------
            public MySqlDataReader GetReader(string comm)
            {
                try
                {
                    MC.CommandText = comm;
                    // MC.FetchAll = true;
                    return MC.ExecuteReader();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + " №" + ex.ErrorCode.ToString() + " SQL команда: " + comm, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException("");
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            public long RunNoQuery(string comm)
            {
                try
                {
                    //MC.CommandText = "SET NAMES cp1251;";
                    //MC.ExecuteScalar();
                    MC.CommandText = comm;
                    object or = MC.ExecuteScalar();
                    long ret;
                    if (or != null) ret = (long)or;
                    else ret = 0;
                    return ret;

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + " №" + ex.ErrorCode.ToString() + " SQL команда: " + comm, "Ошибка...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new DBException("");
                }
            }
            //------------------------------------------------------------------------------------------------------------------------
            public string GetOneValue(string com)
            {
                MySqlDataReader msdr = GetReader(com);
                string ins = "";
                if (!msdr.Read()) return ins;
                ins = msdr[0].ToString();
                msdr.Close();
                return ins;
            }
            //------------------------------------------------------------------------------------------------------------------------
            public string GetOneText(string com)
            {
                MySqlDataReader msdr = GetReader(com);
                string ins = "";
                if (!msdr.Read()) return ins;
                ins = msdr[0].ToString();
                while (msdr.Read())
                    if (msdr[0].ToString() != ins) { msdr.Close(); return "#Разное#"; }
                    else ins = msdr[0].ToString();
                msdr.Close();
                return ins;

            }
            //------------------------------------------------------------------------------------------------------------------------
            public string GetMultiCommand(string fromXML, List<string> ellist)
            {
                string sc = GetSQLCommand(fromXML);
                int pos;
                pos = sc.IndexOf('#');
                if (pos < 0) return "";
                sc = sc.Remove(pos, 1);
                StringBuilder sb = new StringBuilder(ellist.Count * ellist[0].Length);
                foreach (string el in ellist) sb.Append(el + ",");
                sb = sb.Remove(sb.Length - 1, 1);
                sc = sc.Insert(pos, sb.ToString());
                return sc;
            }
            //------------------------------------------------------------------------------------------------------------------------
            public List<string> GetListXML(string com)
            {
                com = GetSQLCommand(com);
                return GetList(com);
            }
            //------------------------------------------------------------------------------------------------------------------------
            public List<string> GetList(string com)
            {
                MySqlDataReader msdr = GetReader(com);
                List<string> ls = new List<string>();
                while (msdr.Read()) ls.Add(msdr[0].ToString());
                msdr.Close();
                return ls;
            }
            //------------------------------------------------------------------------------------------------------------------------
            public string GetOneCommand(string comXML, string val)
            {
                string str = GetSQLCommand(comXML);
                if (val != null && val != "")
                {
                    int pos;
                    while ((pos = str.IndexOf('#')) >= 0)
                    {
                        str = str.Remove(pos, 1);
                        str = str.Insert(pos, val);
                    }
                    return str;
                }
                return str;
            }
            //------------------------------------------------------------------------------------------------------------------------        
            public void GetTwoListsXML(string comXML, List<string> Ids, List<string> CSumms)
            {
                MySqlDataReader msdr = GetReaderXML(comXML);
                Ids.Clear();
                CSumms.Clear();
                while (msdr.Read()) { Ids.Add(msdr[0].ToString()); CSumms.Add(msdr[1].ToString()); }
                msdr.Close();
            }
        }
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public class DBException : Exception
        {
            int errorcode;
            public int ErrorCode { get { return errorcode; } }
            public DBException(string mess)
                : base(mess)
            {
                errorcode = 0;
            }
            public DBException(int errcode)
            {
                errorcode = errcode;
            }
            public DBException(int errcode, string mess)
                : base(mess)
            {
                errorcode = errcode;
            }
        }
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    }

