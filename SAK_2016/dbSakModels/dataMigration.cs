using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
/*
 * 15-10-2016 Здесь добавляются команды для создания базы данных и ее редактирования, а также миграция данных из старой БД
 */

namespace NormaMeasure.SAC_APP.dbSakModels
{
    
    class dataMigration
    {
        private static string dbName = Properties.dbSakQueries.Default.dbName;
        private static string query;
        private static string message;
        private static string connString = Properties.Settings.Default.rootConnectionString;
        private static MySqlConnection dbCon;


        public dataMigration()
        {
            try
            {
                dbCon = new MySqlConnection(connString); //Устанавливаем соединение
                //dropDBSak();                             //Удаляем бд (необходимо на стадии настройки)
                checkAndCreateDBSak();                   //Создаем бд (если нет её)

            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL подключения", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Создаёт таблицы в базе данных и добавляет в них значения по умолчанию
        /// </summary>
        public void createTables()
        {
            createUsersTable(); //Проверяет и создает таблицу пользователей
            createRolesTable(); //Проверяет и создает таблицу ролей
            createCablesTable(); //Проверяет и создаёт таблицу кабелей
            createDocumentsTable(); //Проверяет и создаёт таблицу документов
            createLeadMaterialsTable(); //Проверяет и создаёт таблицу материалов жил
            createLeadMaterialTCoeffsTable(); //Добавляет таблицу с температурными коэффициентами для материалов жил
            createIsolationMaterialsTable(); //Добавляет таблицу с изоляционными материалами
            createIsolationMaterialTCoeffsTable(); //Добавляет таблицу с температурными коэффициентами для материалов изоляции
            createBarabanTypesTable(); //Добавляет таблицу с типами барабанов
            createBarabansTable(); //Добавляет таблицу с барабанами
            createCableStructureTypesTable(); //Добавляет таблицу типов структур кабеля
            createMeasuredParameterTypesTable(); //Добавляет таблицу с измеряемыми параметрами
            createMeasuredParameterValuesTable(); //Добавляет таблицу для нормативных значений измеряемых параметров
            createBringingLengthTypesTable(); //
            createFrequencyRangesTable(); //
            createDRFormulsTable();
            createDRBringingFormulsTable();
            createDRCableStructuresTable();
        }

        /// <summary>
        /// Добавляет таблицу для структур кабеля
        /// </summary>
        private void createDRCableStructuresTable()
        {
            string tableName = "cable_structures";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "cable_id INT UNSIGNED DEFAULT NULL",
                                    "structure_type_id INT UNSIGNED NOT NULL",
                                    "nominal_amount INT UNSIGNED",
                                    "fact_amount INT UNSIGNED",
                                    "lead_material_id INT UNSIGNED NOT NULL",
                                    "lead_diameter FLOAT NOT NULL",
                                    "isolation_material_id INT UNSIGNED NOT NULL",
                                    "wave_resistance FLOAT NOT NULL",
                                    "u_lead_lead FLOAT",
                                    "u_lead_shield FLOAT",
                                    "test_group_work_capacity BOOL DEFAULT 0",
                                    "dr_formula_id INT UNSIGNED DEFAULT 1",
                                    "dr_bringing_formula_id INT UNSIGNED DEFAULT 1",
                                    "PRIMARY KEY (id)"
                                 };
            checkAndAddTable(tableName, colsArray);
        }

        /// <summary>
        /// Добавляет таблицу для вычисления Омической ассиметрии
        /// </summary>
        private static void createDRFormulsTable()
        {
            string tableName = "dr_formuls";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name VARCHAR(15)",
                                    "measure_of VARCHAR(15)",
                                    "formula TINYTEXT",
                                    "description TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultData = {
                                        new string[] {"1", "'DR1'", "'Ом'", "'Ra-Rb'", "NULL"},
                                        new string[] {"2", "'DR2'", "'Ом'", "'(Ra-Rb)/2'", "NULL"},
                                        new string[] {"3", "'DR3'", "'%'", "'(Ra-Rb)/(Ra-Rb)*100%'", "NULL"},
                                        new string[] {"4", "'DR4'", "'%'", "'(Ra-Rb)/(Ra-Rb)*200%'", "NULL"}
                                     };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultData);
        }

        /// <summary>
        /// Добавляет таблицу с формулами приведения для омической ассиметрии
        /// </summary>
        private static void createDRBringingFormulsTable()
        {
            string tableName = "dr_bringing_formuls";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name VARCHAR(15)",
                                    "formula TINYTEXT",
                                    "description TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultData = {
                                        new string[] {"1", "'Priv1'", "'без приведения'", "NULL"},
                                        new string[] {"2", "'Priv2'", "'DR*Lпр/Lф'", "NULL"},
                                        new string[] {"3", "'Priv3'", "'DR*sqrt(Lпр/Lф)'", "NULL"}
                                     };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultData);
        }
        /// <summary>
        /// Добавляет таблицу пользователей администратора если его нет
        /// </summary>
        private static void createUsersTable()
        {
            string tableName = "Users";
            string[][] sysadmParams =
            { new string[] { "DEFAULT",
                "'Sysadmin'",
                "'Sysadmin'",
                "'Sysadmin'",
                "0",
                "1",
                "'123456'",
                "1"}

            };
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "last_name TINYTEXT",
                                    "name TINYTEXT",
                                    "third_name TINYTEXT",
                                    "employee_number INT UNSIGNED",
                                    "role_id INT UNSIGNED",
                                    "password TINYBLOB",
                                    "is_active BOOL",
                                    "PRIMARY KEY (id)" 
                                 };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, sysadmParams);

        }

        /// <summary>
        /// Добавляет таблицу ролей пользователей и заполняет её базовыми ролями, если их нет
        /// </summary>
        private static void createRolesTable()
        {
            string tableName = "Roles";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues = {
                new string[] { "1", "'Администратор БД'" },
                new string[] { "2", "'Метролог'" },
                new string[] { "3", "'Мастер'" },
                new string[] { "4", "'Оператор'" },
                new string[] { "5", "'Опрессовщик'"  },
                new string[] { "6", "'Перемотчик'"} };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }

       /// <summary>
       /// Проверяет и создает таблицу кабелей
       /// </summary>
       private static void createCablesTable()
        {
            string tableName = "cables";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name TINYTEXT",
                                    "struct_name TINYTEXT",
                                    "build_length float default NULL",
                                    "document_id INT UNSIGNED NOT NULL",
                                    "notes TINYTEXT",
                                    "linear_mass float Default NULL",
                                    "code_okp CHAR(12)",
                                    "code_kch CHAR(2)",
                                    "u_cover float Default NULL",
                                    "p_min float Default NULL",
                                    "p_max float Default NULL",
                                    "PRIMARY KEY (id)"
                                 };
            checkAndAddTable(tableName, colsArray);
         }
        /// <summary>
        /// Добавляет таблицу типов структур кабеля
        /// </summary>
        private static void createCableStructureTypesTable()
        {
            string tableName = "cable_structure_types";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name VARCHAR(120)",
                                    "leads_number TINYINT UNSIGNED",
                                    "measured_params SET ('1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17') DEFAULT '1'",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues =
{
                new string[] {"1", "'Жила'", "1", "'1,2,4,5,6,7,10'"},
                new string[] {"2", "'Пара'", "2","'1,2,3,4,5,6,7,8,9,10,11,15,16,17'"},
                new string[] {"3", "'Тройка'", "3","'1,2,4,5,6,7,10'"},
                new string[] {"4", "'Четвёрка'", "4", "'1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17'" },
                new string[] {"5", "'Высокочастотная четвёрка'", "4", "'1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17'" }
            };

            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }
        /// <summary>
        /// Добавляет таблицу с типами измеряемымых параметров
        /// </summary>
        private static void createMeasuredParameterTypesTable()
        {
            string tableName = "measured_parameter_types";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name VARCHAR(15)",
                                    "measure_of VARCHAR(15)",
                                    "description TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues =
{
                new string[] {"1", "'Prozvon'", "NULL", "'Прозвонка'"},
                new string[] {"2", "'Rж'", "'Ом'","'Сопротивление жил'"},
                new string[] {"3", "'dR'", "NULL","'Оммическая ассиметрия'"},
                new string[] {"4", "'Rиз1'", "'МОм'", "'Сопротивление изоляции жилы'" },
                new string[] {"5", "'Rиз2'", "'сек'", "'Время установления измеряемого значения до нормы'" },
                new string[] {"6", "'Rиз3'", "'МОм'", "'Сопротивление изоляции комбинации жил'" },
                new string[] {"7", "'Rиз4'", "'сек'", "'Время установления измеряемого значения до нормы для комбинаци жил'" },
                new string[] {"8", "'Cр'", "'нФ'","'Рабочая ёмкость'"},
                new string[] {"9", "'dCр'", "'нФ'","'Разность рабочей емкости'"},
                new string[] {"10", "'Co'", "'нФ'","'Емкость жилы'"},
                new string[] {"11", "'Ea'", "'нФ'","'Ёмкостная ассиметрия в паре'"},
                new string[] {"12", "'K1'", "'пФ'","'Ёмкостная связь К_1'"},
                new string[] {"13", "'K2,K3'", "'пФ'","'Емкостная связь К_2-3'"},
                new string[] {"14", "'K9-12'", "'пФ'","'Емкостная связь К_9-12'"},
                new string[] {"15", "'al'", "'дБ'","'Рабочее затухание'"},
                new string[] {"16", "'Ao'", "'дБ'","'Переходное затухание на ближнем конце'"},
                new string[] {"17", "'Az'", "'дБ'","'Защищённость на дальнем конце'"},
                new string[] {"18", "'K2'", "'пФ'","'Ёмкостная связь К_2'"},
                new string[] {"19", "'K3'", "'пФ'","'Ёмкостная связь К_3'"},
                new string[] {"20", "'K9'", "'пФ'","'Ёмкостная связь К_9'"},
                new string[] {"21", "'K10'", "'пФ'","'Ёмкостная связь К_10'"},
                new string[] {"22", "'K11'", "'пФ'","'Ёмкостная связь К_11'"},
                new string[] {"23", "'K12'", "'пФ'","'Ёмкостная связь К_12'"}

            };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }

        /// <summary>
        /// Добавляет таблицу для нормативных значений измеряемых параметров
        /// </summary>
        private static void createMeasuredParameterValuesTable()
        {
            string tableName = "measured_parameter_values";
            string[] colsArray = {
                                    "cable_id INT UNSIGNED NOT NULL",
                                    "cable_structure_id	INT UNSIGNED NOT NULL",
                                    "measured_parameter_type_id INT UNSIGNED NOT NULL",
                                    "min_val FLOAT",
                                    "max_val FLOAT",
                                    "bringing_length FLOAT",
                                    "bringing_length_type_id INT UNSIGNED",
                                    "percent FLOAT DEFAULT 100.0",
                                    "frequency_range_id INT UNSIGNED DEFAULT NULL",
                                    "KEY id (cable_id, cable_structure_id, percent)"
                                 };
            checkAndAddTable(tableName, colsArray);
        }
        /// <summary>
        /// Добавляет таблицу типов приведения к длине и заполняет её значения
        /// </summary>
        private static void createBringingLengthTypesTable()
        {
            string tableName = "bringing_length_types";
            string[] colsArray = {
                                    "id INT UNSIGNED",
                                    "name VARCHAR(15)",
                                    "description TINYINT UNSIGNED",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues =
{
                new string[] {"0", "''", "'Не задана'"},
                new string[] {"1", "'/Lстр'", "'к строительной длине'"},
                new string[] {"2", "'/км'", "'к киллометру'"},
                new string[] {"3", "'/(км)'", "'другая'" }
            };

            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }
        /// <summary>
        /// Добавляет таблицу диапазонов частот для измерения
        /// </summary>
        private static void createFrequencyRangesTable()
        {
            string tableName = "frequency_ranges";
            string[] colsArray = {
                                    "id INT AUTO_INCREMENT NOT NULL",
                                    "freq_min INT UNSIGNED",
                                    "freq_max INT UNSIGNED",
                                    "freq_step INT UNSIGNED",
                                    "PRIMARY KEY (id)"
                                 };
            checkAndAddTable(tableName, colsArray);

        }
        /// <summary>
        /// Создаёт таблицу Document и добавляет начальные значения
        /// </summary>
        private static void createDocumentsTable()
        {
            string tableName = "documents";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "short_name TINYTEXT",
                                    "full_name TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues =
            {
                new string[] {"1", "'ГОСТ Р 51311-99'", "'КАБЕЛИ ТЕЛЕФОННЫЕ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия'"},
                new string[] {"2", "'ГОСТ Р 51312-99'", "'КАБЕЛИ ДЛЯ СИГНАЛИЗАЦИИ И БЛОКИРОВКИ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия'"},
                new string[] {"3", "'ГОСТ 15125-92'", "'КАБЕЛИ СВЯЗИ СИММЕТРИЧНЫЕ ВЫСОКОЧАСТОТНЫЕ С КОРДЕЛЬНО-ПОЛИСТИРОЛЬНОЙ ИЗОЛЯЦИЕЙ Технические условия'" }
            };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }
        /// <summary>
        /// Добавляет таблицу с типами барабанов
        /// </summary>
        private static void createBarabanTypesTable()
        {
            string tableName = "baraban_types";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name TINYTEXT",
                                    "weight float default 0",
                                    "PRIMARY KEY (id)"
                                 };
            checkAndAddTable(tableName, colsArray);
        }
        /// <summary>
        /// Добавляет таблицу с барабанами
        /// </summary>
        private static void createBarabansTable()
        {
            string tableName = "barabans";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "baraban_type_id INT UNSIGNED NOT NULL",
                                    "number TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            checkAndAddTable(tableName, colsArray);
        }
        /// <summary>
        /// Добавляет таблицу материалов жил и начальные значения
        /// </summary>
        private static void createLeadMaterialsTable()
        {
            string tableName = "lead_materials";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name TINYTEXT",
                                    "Ro_T FLOAT",
                                    "T_r0 FLOAT",
                                    "TKC_1 FLOAT DEFAULT 0",
                                    "TKC_2 FLOAT DEFAULT 0",
                                    "TKC_3 FLOAT DEFAULT 0",
                                    "Tab_TKC BOOL DEFAULT 0",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues =
            {
                new string[] {"1", "'Медь марки ММ'", "DEFAULT", "DEFAULT", "0.00393", "DEFAULT", "DEFAULT", "DEFAULT"},
                new string[] {"2", "'Медь марки МТ'", "DEFAULT", "DEFAULT", "0.00381", "DEFAULT", "DEFAULT", "DEFAULT"},
                new string[] {"3", "'Алюминий'", "DEFAULT", "DEFAULT", "0.00403", "DEFAULT", "DEFAULT", "DEFAULT"}
            };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }
        /// <summary>
        /// Добавляет таблицу с температурными коэффициентами для материалов жил
        /// </summary>
        private static void createLeadMaterialTCoeffsTable()
        {
            string tableName = "lead_material_tcoeffs";
            string[] colsArray = {
                                    "lead_material_id INT UNSIGNED NOT NULL",
                                    "temperature FLOAT",
                                    "coeff_val FLOAT"
                                 };
            checkAndAddTable(tableName, colsArray);
        }
        /// <summary>
        /// Добавляет таблицу для изоляционных материалов
        /// </summary>
        private static void createIsolationMaterialsTable()
        {
            string tableName = "isolation_materials";
            string[] colsArray = {
                                    "id INT UNSIGNED AUTO_INCREMENT NOT NULL",
                                    "name TINYTEXT",
                                    "PRIMARY KEY (id)"
                                 };
            string[][] defaultValues =
{
                new string[] {"1", "'Полиэтилен'"},
                new string[] {"2", "'Резина'"},
                new string[] {"3", "'Пропитанная бумага'"}
            };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }
        /// <summary>
        /// Добавляет таблицу с температурными коэффициентами для изоляции
        /// </summary>
        private static void createIsolationMaterialTCoeffsTable()
        {
            string tableName = "isolation_material_tcoeffs";
            string[] colsArray = {
                                    "isolation_material_id INT UNSIGNED NOT NULL",
                                    "temperature FLOAT",
                                    "coeff_val FLOAT"
                                 };
            string[][] defaultValues =
{
                //Для полиэтилена
                new string[] {"1", "5", "0.1"},
                new string[] {"1", "6", "0.12"},
                new string[] {"1", "7", "0.15"},
                new string[] {"1", "8", "0.17"},
                new string[] {"1", "9", "0.19"},
                new string[] {"1", "10", "0.22"},
                new string[] {"1", "11", "0.26"},
                new string[] {"1", "12", "0.3"},
                new string[] {"1", "13", "0.35"},
                new string[] {"1", "14", "0.42"},
                new string[] {"1", "15", "0.48"},
                new string[] {"1", "16", "0.56"},
                new string[] {"1", "17", "0.64"},
                new string[] {"1", "18", "0.75"},
                new string[] {"1", "19", "0.87"},
                new string[] {"1", "20", "1"},
                new string[] {"1", "21", "1.17"},
                new string[] {"1", "22", "1.35"},
                new string[] {"1", "23", "1.57"},
                new string[] {"1", "24", "1.82"},
                new string[] {"1", "25", "2.1"},
                new string[] {"1", "26", "2.42"},
                new string[] {"1", "27", "2.83"},
                new string[] {"1", "28", "3.3"},
                new string[] {"1", "29", "3.82"},
                new string[] {"1", "30", "4.45"},
                new string[] {"1", "31", "5.2"},
                new string[] {"1", "32", "6"},
                new string[] {"1", "33", "6.82"},
                new string[] {"1", "34", "7.75"},
                new string[] {"1", "35", "8.8"},
                //Для резины
                new string[] {"2", "5", "0.5"},
                new string[] {"2", "6", "0.53"},
                new string[] {"2", "7", "0.55"},
                new string[] {"2", "8", "0.58"},
                new string[] {"2", "9", "0.61"},
                new string[] {"2", "10", "0.64"},
                new string[] {"2", "11", "0.68"},
                new string[] {"2", "12", "0.7"},
                new string[] {"2", "13", "0.73"},
                new string[] {"2", "14", "0.76"},
                new string[] {"2", "15", "0.8"},
                new string[] {"2", "16", "0.84"},
                new string[] {"2", "17", "0.88"},
                new string[] {"2", "18", "0.91"},
                new string[] {"2", "19", "0.96"},
                new string[] {"2", "20", "1"},
                new string[] {"2", "21", "1.05"},
                new string[] {"2", "22", "1.13"},
                new string[] {"2", "23", "1.2"},
                new string[] {"2", "24", "1.27"},
                new string[] {"2", "25", "1.35"},
                new string[] {"2", "26", "1.43"},
                new string[] {"2", "27", "1.52"},
                new string[] {"2", "28", "1.61"},
                new string[] {"2", "29", "1.71"},
                new string[] {"2", "30", "1.82"},
                new string[] {"2", "31", "1.93"},
                new string[] {"2", "32", "2.05"},
                new string[] {"2", "33", "2.18"},
                new string[] {"2", "34", "2.31"},
                new string[] {"2", "35", "2.46"},
                //Для пропитанной бумаги
                new string[] {"3", "5", "0.58"},
                new string[] {"3", "6", "0.6"},
                new string[] {"3", "7", "0.64"},
                new  string[] {"3", "8", "0.67"},
                new string[] {"3", "9", "0.69"},
                new string[] {"3", "10", "0.72"},
                new string[] {"3", "11", "0.74"},
                new string[] {"3", "12", "0.76"},
                new string[] {"3", "13", "0.79"},
                new string[] {"3", "14", "0.82"},
                new string[] {"3", "15", "0.85"},
                new string[] {"3", "16", "0.87"},
                new string[] {"3", "17", "0.9"},
                new string[] {"3", "18", "0.93"},
                new string[] {"3", "19", "0.97"},
                new string[] {"3", "20", "1"},
                new string[] {"3", "21", "1.03"},
                new string[] {"3", "22", "1.07"},
                new string[] {"3", "23", "1.1"},
                new string[] {"3", "24", "1.14"},
                new string[] {"3", "25", "1.18"},
                new string[] {"3", "26", "1.22"},
                new string[] {"3", "27", "1.27"},
                new string[] {"3", "28", "1.32"},
                new string[] {"3", "29", "1.38"},
                new string[] {"3", "30", "1.44"},
                new string[] {"3", "31", "1.52"},
                new string[] {"3", "32", "1.59"},
                new string[] {"3", "33", "1.67"},
                new string[] {"3", "34", "1.77"},
                new string[] {"3", "35", "1.87"},
            };
            checkAndAddTable(tableName, colsArray);
            addBasicDataToTable(tableName, defaultValues);
        }
        /// <summary>
        /// Проверяет наличие БД db_sak на данном компьютере, если нет то создает ее и выбирает её
        /// </summary>
        /// <returns></returns>
        private static string checkAndCreateDBSak()
        {
            try
            {
                message = "Создаём базу данных испытаний с кодовой страницей cp1251, если она не создана";
                query = "CREATE DATABASE IF NOT EXISTS "+ dbName + " DEFAULT CHARACTER SET cp1251";
                sendQuery();
                query = "USE " + dbName;
                sendQuery();
                return message;
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK);
                return ex.Message;
            }

        }

        /// <summary>
        /// Удаление БД. Функция необходимая на момент отладки программы.
        /// </summary>
        private static void dropDBSak()
        {
            query = "DROP DATABASE IF EXISTS " + dbName;
            sendQuery();  
        }

        private static int sendQuery()
        {
            int sts = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, dbCon);
                dbCon.Open();
                sts = cmd.ExecuteNonQuery();
                dbCon.Close();
                return sts;
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка SQL запроса", MessageBoxButtons.OK);
                return sts;
            }
            
        }

        /// <summary>
        /// Универсальный метод для проверки и добавления таблиц в БД
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        private static void checkAndAddTable(string tableName, string[] columns)
        {
            query = makeColumnsStringFromArray(columns);
            query = String.Format("CREATE TABLE IF NOT EXISTS {0} ({1})", tableName, query);
            sendQuery();
        }
        
        /// <summary>
        /// Добавляет начальные данные в заданную таблицу
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        private static void addBasicDataToTable(string tableName, string[][] data)
        {
            query = "select * from " + tableName;
            int sts = sendQuery();
            if (sts <= 0)
            {
                query = makeColumnsStringFromBiLevelArray(data);
                query = String.Format("INSERT IGNORE INTO {0} VALUES {1}", tableName, query);
                sendQuery();
            }
        }
        /// <summary>
        /// Создаёт из массива строк содержащего столбцы таблицы строку 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string makeColumnsStringFromArray(string[] arr)
        {
            string cols = "";
            for (int i = 0; i < arr.Length; i++)
            {
                
                cols += " " + arr[i];
                if (i != arr.Length - 1)
                    cols += ",";
            }

            return cols;
        }

        /// <summary>
        /// Создаёт из двумерного массива параметров строку для передачи в запрос
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string makeColumnsStringFromBiLevelArray(string[][] arr)
        {
            string str = "";
            for(int i=0; i<arr.Length; i++)
            {
                if (arr[i] == null) break;
                str += "("+makeColumnsStringFromArray(arr[i])+")";
                if (i != arr.Length - 1)
                    str += ", ";
            }
            return str;
        }




    }
}
