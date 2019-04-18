using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.DBControl.SAC
{
    public class DBSACTablesMigration : DBTablesMigration
    {
       // public static DBTable DocumentsTable
       // {
       //     get
       //     {
       //         return buildDocumentsTableMigration();
       //     }
       // }

        //public static DBTable CablesTable
        //{
        //    get
        //    {
        //        return buildCablesTableMigration();
        //    }
        //}

        static DBSACTablesMigration()
        {
            //dbName = "db_norma_sac";
        }

        public DBSACTablesMigration() : base()
        {
            dbName = "db_norma_sac";
            tableTypes = new Type[]
            {
               typeof(Cable),
               typeof(Document),
               typeof(CableStructure),
               typeof(LeadMaterial),
               typeof(IsolationMaterial),
               typeof(MeasuredParameterType),
               typeof(dRBringingFormula),
               typeof(dRFormula),
               typeof(CableStructureType),
               typeof(UserRole),
               typeof(User),
               typeof(BarabanType),
               typeof(ReleasedBaraban),
               typeof(LengthBringingType),
               typeof(LeadTestStatus),
               typeof(FrequencyRange),
               typeof(MeasuredParameterData),
               typeof(CableStructureMeasuredParameterData),
               typeof(CableTestStatus),
               typeof(CableTest),
               typeof(CableTestResult),
               typeof(TestedCable),
               typeof(TestedCableStructure),
               typeof(TestedStructureMeasuredParameterData)
            };
            /*
            _tablesList = new DBTable[] 
            {
                DocumentsTable,
                CablesTable
            };
            */
        }


        protected override DBEntityTable getSeeds(Type type)
        {
            DBEntityTable t = base.getSeeds(type);
            if (type == typeof(Document)) makeDocumentTableSeeds(ref t);
            else if (type == typeof(LeadMaterial)) makeLeadMaterialsTableSeeds(ref t);
            else if (type == typeof(IsolationMaterial)) makeIsolationMaterialsTableSeeds(ref t);
            else if (type == typeof(MeasuredParameterType)) makeMeasuredParameterTypesTableSeeds(ref t);
            else if (type == typeof(dRBringingFormula)) make_dRBringingFormulsTableSeeds(ref t);
            else if (type == typeof(dRFormula)) make_dRFormulsTableSeeds(ref t);
            else if (type == typeof(CableStructureType)) make_CableStructureTypesTableSeeds(ref t);
            else if (type == typeof(UserRole)) make_UserGroupsTableSeeds(ref t);
            else if (type == typeof(User)) make_UsersTablesSeeds(ref t);
            else if (type == typeof(LengthBringingType)) make_LengthBringingTypesTablesSeeds(ref t);
            else if (type == typeof(LeadTestStatus)) make_LeadTestStatussTablesSeeds(ref t);
            else if (type == typeof(FrequencyRange)) make_FrequencyRangesTablesSeeds(ref t);
            else if (type == typeof(CableTestStatus)) make_CableTestStatusesTablesSeeds(ref t);
            return t;
        }

        private void make_CableTestStatusesTablesSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                new string[] {$"{CableTestStatus.NotStarted}", "Испытание не начато"},
                new string[] {$"{CableTestStatus.Started}", "Испытание начато"},
                new string[] {$"{CableTestStatus.Finished}", "Испытание окончено"},
                new string[] {$"{CableTestStatus.StopedOutOfNorma}", "Остановлено по причине выхода за норму"},
                new string[] {$"{CableTestStatus.StopedByOperator}", "Остановлено пользователем"}
            };
            foreach (string[] rData in data)
            {
                CableTestStatus d = (CableTestStatus)t.NewRow();
                d.StatusId = Convert.ToUInt16(rData[0]);
                d.Description = rData[1];
                t.Rows.Add(d);
            }
        }

        private void make_FrequencyRangesTablesSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                new string[] {"1", null, null, null}
            };
            foreach (string[] rData in data)
            {
                FrequencyRange d = (FrequencyRange)t.NewRow();
                d.FrequencyRangeId = Convert.ToUInt16(rData[0]);
                d.FrequencyMin = Convert.ToUInt16(rData[1]);
                d.FrequencyMax = Convert.ToUInt16(rData[2]);
                d.FrequencyStep= Convert.ToUInt16(rData[3]);
                t.Rows.Add(d);
            }
        }

        private void make_LeadTestStatussTablesSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                new string[] {"0", "Годна"},
                new string[] {"1", "Оборвана"},
                new string[] {"2", "Замкнута"},
                new string[] {"3", "Пробита"}
            };
            foreach (string[] rData in data)
            {
                LeadTestStatus d = (LeadTestStatus)t.NewRow();
                d.StatusId = Convert.ToUInt16(rData[0]);
                d.StatusTitle = rData[1];
                t.Rows.Add(d);
            }
        }

        private void make_LengthBringingTypesTablesSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                new string[] {"0", "", "Не задана"},
                new string[] {"1", "/Lстр", "к строительной длине"},
                new string[] {"2", "/км", "к километру"},
                new string[] {"3", "/({0}м)", "другая" }
            };
            foreach (string[] rData in data)
            {
                LengthBringingType d = (LengthBringingType)t.NewRow();
                d.TypeId = Convert.ToUInt16(rData[0]);
                d.MeasureTitle = rData[1];
                d.BringingName = rData[2];
                t.Rows.Add(d);
            }
        }

        private void make_UsersTablesSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                 new string[] { "1", "Sysadmin", "Sysadmin", "Sysadmin", "0", "1", "123456", "true"}
            };
            foreach (string[] rData in data)
            {
                User d = (User)t.NewRow();
                bool isActive = false;
                d.UserId = Convert.ToUInt16(rData[0]);
                d.LastName = rData[1];
                d.FirstName = rData[2];
                d.ThirdName = rData[3];
                d.EmployeeNumber = rData[4];
                d.RoleId = Convert.ToUInt16(rData[5]);
                d.Password = rData[6];
                Boolean.TryParse(rData[7], out isActive);
                d.IsActive = isActive;
                t.Rows.Add(d);
            }
        }

        private void make_UserGroupsTableSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                new string[] { UserRole.DBAdmin.ToString(), "Администратор БД" },
                new string[] { UserRole.Metrolog.ToString(), "Метролог" },
                new string[] { UserRole.Master.ToString(), "Мастер" },
                new string[] { UserRole.Operator.ToString(), "Оператор" },
                new string[] { UserRole.PleasureTester.ToString(), "Опрессовщик"  },
                new string[] { UserRole.Rewinder.ToString(), "Перемотчик"}
            };
            foreach (string[] rData in data)
            {
                UserRole d = (UserRole)t.NewRow();
                d.UserRoleId = Convert.ToUInt16(rData[0]);
                d.UserRoleName = rData[1];
                t.Rows.Add(d);
            }
        }

        private void makeDocumentTableSeeds(ref DBEntityTable t)
        {
            string[][] data = {
                                new string[] { "1", "ГОСТ Р 51311-99", "КАБЕЛИ ТЕЛЕФОННЫЕ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия" },
                                new string[] { "2", "ГОСТ Р 51312-99", "КАБЕЛИ ДЛЯ СИГНАЛИЗАЦИИ И БЛОКИРОВКИ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия" },
                                new string[] { "3", "ГОСТ 15125-92", "КАБЕЛИ СВЯЗИ СИММЕТРИЧНЫЕ ВЫСОКОЧАСТОТНЫЕ С КОРДЕЛЬНО-ПОЛИСТИРОЛЬНОЙ ИЗОЛЯЦИЕЙ Технические условия"} };
            foreach(string[] rData in data)
            {
                Document d = (Document)t.NewRow();
                d.DocumentId = Convert.ToUInt16(rData[0]);
                d.ShortName = rData[1];
                d.FullName = rData[2];
                t.Rows.Add(d);
            }

        }

        private void makeLeadMaterialsTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
            {
                new string[] {"1", "Медь марки ММ", "0.00393"},
                new string[] {"2", "Медь марки МТ", "0.00381"},
                new string[] {"3", "Алюминий", "0.00403" }
            };

            foreach (string[] rData in data)
            {
                LeadMaterial d = (LeadMaterial)t.NewRow();
                d.MaterialId = Convert.ToUInt16(rData[0]);
                d.MaterialName = rData[1];
                d.MaterialTKC = Convert.ToSingle(rData[2]);
                t.Rows.Add(d);
            }
        }

        private void makeIsolationMaterialsTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
{
                new string[] {"1", "Полиэтилен"},
                new string[] {"2", "Резина"},
                new string[] {"3", "Пропитанная бумага"}
            };

            foreach (string[] rData in data)
            {
                IsolationMaterial d = (IsolationMaterial)t.NewRow();
                d.MaterialId = Convert.ToUInt16(rData[0]);
                d.MaterialName = rData[1];
                t.Rows.Add(d);
            }
        }

        private void makeMeasuredParameterTypesTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
{
                new string[] {$"{MeasuredParameterType.Calling}", "Prozvon", "NULL", "Прозвонка"},
                new string[] { $"{MeasuredParameterType.Rleads}", "Rж", "Ом","Сопротивление жил"},
                new string[] { $"{MeasuredParameterType.dR}", "dR", "NULL","Оммическая ассиметрия"},
                new string[] { $"{MeasuredParameterType.Risol1}", "Rиз1", "МОм", "Сопротивление изоляции жилы" },
                new string[] { $"{MeasuredParameterType.Risol2}", "Rиз2", "сек", "Время установления измеряемого значения до нормы" },
                new string[] { $"{MeasuredParameterType.Risol3}", "Rиз3", "МОм", "Сопротивление изоляции комбинации жил" },
                new string[] { $"{MeasuredParameterType.Risol4}", "Rиз4", "сек", "Время установления измеряемого значения до нормы для комбинаци жил" },
                new string[] { $"{MeasuredParameterType.Cp}", "Cр", "нФ","Рабочая ёмкость"},
                new string[] { $"{MeasuredParameterType.dCp}", "dCр", "нФ","Разность рабочей емкости"},
                new string[] { $"{MeasuredParameterType.Co}", "Co", "нФ","Емкость жилы"},
                new string[] { $"{MeasuredParameterType.Ea}", "Ea", "нФ","Ёмкостная ассиметрия в паре"},
                new string[] { $"{MeasuredParameterType.K1}", "K1", "пФ","Ёмкостная связь К_1"},
                new string[] { $"{MeasuredParameterType.K23}", "K2,K3", "пФ","Емкостная связь К_2-3"},
                new string[] { $"{MeasuredParameterType.K9_12}", "K9-12", "пФ","Емкостная связь К_9-12"},
                new string[] { $"{MeasuredParameterType.al}", "al", "дБ","Рабочее затухание"},
                new string[] { $"{MeasuredParameterType.Ao}", "Ao", "дБ","Переходное затухание на ближнем конце"},
                new string[] { $"{MeasuredParameterType.Az}", "Az", "дБ","Защищённость на дальнем конце"},
                new string[] { $"{MeasuredParameterType.K2}", "K2", "пФ","Ёмкостная связь К_2"},
                new string[] { $"{MeasuredParameterType.K3}", "K3", "пФ","Ёмкостная связь К_3"},
                new string[] { $"{MeasuredParameterType.K9}", "K9", "пФ","Ёмкостная связь К_9"},
                new string[] { $"{MeasuredParameterType.K10}", "K10", "пФ","Ёмкостная связь К_10"},
                new string[] { $"{MeasuredParameterType.K11}", "K11", "пФ","Ёмкостная связь К_11"},
                new string[] { $"{MeasuredParameterType.K12}", "K12", "пФ","Ёмкостная связь К_12"}
            };

            foreach (string[] rData in data)
            {
                MeasuredParameterType d = (MeasuredParameterType)t.NewRow();
                d.ParameterTypeId = Convert.ToUInt16(rData[0]);
                d.ParameterName = rData[1];
                d.Measure = rData[2];
                d.Description = rData[3];
                t.Rows.Add(d);
            }
        }

        private void make_dRBringingFormulsTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
            {
                new string[] {"1", "Priv1", "Без приведения"},
                new string[] {"2", "Priv2", "DR*Lпр/Lф"},
                new string[] {"3", "Priv3", "DR*sqrt(Lпр/Lф)"}
            };

            foreach (string[] rData in data)
            {
                dRBringingFormula d = (dRBringingFormula)t.NewRow();
                d.FormulaId = Convert.ToUInt16(rData[0]);
                d.FormulaName = rData[1];
                d.Formula = rData[2];
                t.Rows.Add(d);
            }
        }

        private void make_dRFormulsTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
            {
                new string[] {"1", "DR1", "Ом", "Ra-Rb"},
                new string[] {"2", "DR2", "Ом", "(Ra-Rb)/2"},
                new string[] {"3", "DR3", "%", "(Ra-Rb)/(Ra-Rb)*100%"},
                new string[] {"4", "DR4", "%", "(Ra-Rb)/(Ra-Rb)*200%"}
            };

            foreach (string[] rData in data)
            {
                dRFormula d = (dRFormula)t.NewRow();
                d.FormulaId = Convert.ToUInt16(rData[0]);
                d.FormulaName = rData[1];
                d.ResultMeasure = rData[2];
                d.Formula = rData[3];
                t.Rows.Add(d);
            }
        }

        private void make_CableStructureTypesTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
{
                new string[] {$"{CableStructureType.Lead}", "Жила", "1", "1,2,4,5,6,7,10"},
                new string[] {$"{CableStructureType.Pair}", "Пара", "2","1,2,3,4,5,6,7,8,9,10,11,15,16,17"},
                new string[] {$"{CableStructureType.Triplet}", "Тройка", "3","1,2,4,5,6,7,10"},
                new string[] {$"{CableStructureType.Quattro}", "Четвёрка", "4", "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17" },
                new string[] {$"{CableStructureType.HightFreqQuattro}", "Высокочастотная четвёрка", "4", "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17" }
            };

            foreach (string[] rData in data)
            {
                CableStructureType d = (CableStructureType)t.NewRow();
                d.StructureTypeId = Convert.ToUInt16(rData[0]);
                d.StructureTypeName = rData[1];
                d.StructureLeadsAmount = Convert.ToInt16(rData[2]);
                d.StructureMeasuredParameters = rData[3];

                t.Rows.Add(d);
            }
        }

        /*

        #region объявление структур таблиц базы данных
        /// <summary>
        /// Заполняем миграцию таблицы кабелей
        /// </summary>
        /// <returns></returns>
        private static DBTable buildCablesTableMigration()
        {
            DBTable table = new DBTable();
            table.tableName = "cables";
            table.entityName = "cable";
            table.oldTableName = "cables";
            table.oldDbName = "bd_cable";
            table.primaryKey = "id";
            table.dbName = dbName;
            table.columns = new DBTableColumn[]
            {
                new DBTableColumn { Name = "id", Type = "INT UNSIGNED AUTO_INCREMENT NOT NULL", OldName = "CabNum" },
                new DBTableColumn { Name = "name", Type = "TINYTEXT", OldName = "CabName", DefaultValue = "''" },
                new DBTableColumn { Name = "struct_name", Type = "TINYTEXT", OldName = "CabNameStruct", DefaultValue = "''" },
                new DBTableColumn { Name = "build_length", Type = "float", OldName = "StrLengt", DefaultValue = "0" },
                new DBTableColumn { Name = "document_id", Type = "INT UNSIGNED NOT NULL", OldName = "DocInd", JoinedTable = DocumentsTable, DefaultValue = "0" },
                new DBTableColumn { Name = "notes", Type = "TINYTEXT", OldName = "TextPrim", DefaultValue = "''" },
                new DBTableColumn { Name = "linear_mass", Type = "float", OldName = "PogMass" },
                new DBTableColumn { Name = "code_okp", Type = "CHAR(12)", OldName = "KodOKP" },
                new DBTableColumn { Name = "code_kch", Type = "CHAR(2)", OldName = "KodOKP_KCH" },
                new DBTableColumn { Name = "u_cover", Type = "float", OldName = "U_Obol", DefaultValue = "NULL" },
                new DBTableColumn { Name = "p_min", Type = "float", OldName = "P_min", DefaultValue = "NULL" },
                new DBTableColumn { Name = "p_max", Type ="float", OldName = "P_max", DefaultValue = "NULL" },
                new DBTableColumn { Name = "is_draft", Type = "TINYINT(1)", DefaultValue = "0"},
                new DBTableColumn { Name = "is_deleted", Type = "TINYINT(1)", DefaultValue = "0"}
            };
            table.SelectAllQuery = $"{table.SelectQuery} WHERE {table.tableName}.is_draft = 0 AND {table.tableName}.is_deleted = 0";
            return table;
        }

        /// <summary>
        /// Заполняем миграцию таблицы нормативных документов
        /// </summary>
        /// <returns></returns>
        private static DBTable buildDocumentsTableMigration()
        {
            DBTable table = new DBTable();

            table.tableName = "documents";
            table.entityName = "document";
            table.oldTableName = "norm_docum";
            table.oldDbName = "bd_cable";
            table.primaryKey = "id";
            table.dbName = dbName;
            table.columns = new DBTableColumn[]
            {
                new DBTableColumn {Name = "id", Type = "INT UNSIGNED AUTO_INCREMENT NOT NULL", OldName = "DocInd"},
                new DBTableColumn {Name = "short_name", Type = "TINYTEXT", OldName = "DocNum"},
                new DBTableColumn {Name = "full_name", Type = "VARCHAR(1000)", OldName = "DocName"}
            };
            table.seeds = new string[][] {
                new string[] { "1", "'ГОСТ Р 51311-99'", "'КАБЕЛИ ТЕЛЕФОННЫЕ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия'" },
                new string[] { "2", "'ГОСТ Р 51312-99'", "'КАБЕЛИ ДЛЯ СИГНАЛИЗАЦИИ И БЛОКИРОВКИ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия'" },
                new string[] { "3", "'ГОСТ 15125-92'", "'КАБЕЛИ СВЯЗИ СИММЕТРИЧНЫЕ ВЫСОКОЧАСТОТНЫЕ С КОРДЕЛЬНО-ПОЛИСТИРОЛЬНОЙ ИЗОЛЯЦИЕЙ Технические условия'" }
            };

            return table;
        }
        #endregion
        */
    }
}
