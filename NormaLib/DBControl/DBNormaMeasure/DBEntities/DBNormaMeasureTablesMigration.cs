using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;

namespace NormaLib.DBControl.DBNormaMeasure
{
    public class DBNormaMeasureTablesMigration : DBTablesMigration
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

        static DBNormaMeasureTablesMigration()
        {
            //dbName = "db_norma_sac";
        }

        public DBNormaMeasureTablesMigration() : base()
        {
            dbName = "db_norma_measure";
            tableTypes = new Type[]
            {
               typeof(Cable),
               typeof(Document),
               typeof(CableStructure),
               typeof(LeadMaterial),
               typeof(IsolationMaterial),
               typeof(IsolMaterialCoeffs),
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
               typeof(TestedStructureMeasuredParameterData),
               typeof(MeasureDevice)
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
            else if (type == typeof(IsolMaterialCoeffs)) make_IsolationMaterialCoeffsTableSeeds(ref t);
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
                new string[] {$"{LeadTestStatus.Ok}", "Годна"},
                new string[] {$"{LeadTestStatus.Ragged}", "Оборвана"},
                new string[] {$"{LeadTestStatus.Closured}", "Замкнута"},
                new string[] {$"{LeadTestStatus.Broken}", "Пробита"}
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

        private void make_IsolationMaterialCoeffsTableSeeds(ref DBEntityTable t)
        {
            string[][] data =
{
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
new string[] {"3", "5", "0.58"},
new string[] {"3", "6", "0.6"},
new string[] {"3", "7", "0.64"},
new string[] {"3", "8", "0.67"},
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
new string[] {"3", "35", "1.87"}

            };

            foreach (string[] rData in data)
            {
                IsolMaterialCoeffs d = (IsolMaterialCoeffs)t.NewRow();
                d.MaterialId = Convert.ToUInt16(rData[0]);
                d.Temperature = Convert.ToInt16(rData[1]); 
                d.Coefficient = Convert.ToSingle(rData[2]);

                t.Rows.Add(d);
            }
        }

    }
}
