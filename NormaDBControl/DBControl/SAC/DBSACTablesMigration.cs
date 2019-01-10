using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaDB.SAC
{
    class DBSACTablesMigration : DBTablesMigration
    {
        static DBSACTablesMigration()
        {
            _dbName = "db_norma_sac";
            _tablesList = new DBTable[] 
            {
                buildDocumentsTableMigration(),
                buildCablesTableMigration(),
            };
        }


        /// <summary>
        /// Заполняем миграцию таблицы кабелей
        /// </summary>
        /// <returns></returns>
        private static DBTable buildCablesTableMigration()
        {
            DBTable table = new DBTable();
            table.tableName = "cables";
            table.oldTableName = "cables";
            table.oldDbName = "bd_cable";
            table.primaryKey = "id";
            table.columns = new string[][]
            {
                new string[] { "id", "INT UNSIGNED AUTO_INCREMENT NOT NULL", "CabNum" },
                new string[] { "name", "TINYTEXT", "CabName" },
                new string[] { "struct_name", "TINYTEXT", "CabNameStruct" },
                new string[] { "build_length", "float default NULL", "StrLengt" },
                new string[] { "document_id", "UNSIGNED NOT NULL",  "DocInd" },
                new string[] { "notes", "TINYTEXT", "TextPrim" },
                new string[] { "linear_mass", "float Default NULL", "PogMass" },
                new string[] { "code_okp", "CHAR(12)", "KodOKP" },
                new string[] { "code_kch", "CHAR(2)", "KodOKP_KCH" },
                new string[] { "u_cover", "float Default NULL", "U_Obol" },
                new string[] { "p_min", "float Default NULL", "P_min" },
                new string[] { "p_max", "float Default NULL", "P_max" },
                new string[] { "is_draft", "TINYINT(1)", ""},
                new string[] { "is_deleted", "TINYINT(1)", ""}
            };
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
            table.oldTableName = "norm_docum";
            table.oldDbName = "bd_cable";
            table.primaryKey = "id";
            table.columns = new string[][]
            {
                new string[] {"id", "INT UNSIGNED AUTO_INCREMENT NOT NULL", "DocInd"},
                new string[] {"short_name", "TINYTEXT", "DocNum"},
                new string[] {"full_name", "VARCHAR(1000)", "DocName"}
            };
            table.seeds = new string[][] {
                new string[] { "1", "'ГОСТ Р 51311-99'", "'КАБЕЛИ ТЕЛЕФОННЫЕ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия'" },
                new string[] { "2", "'ГОСТ Р 51312-99'", "'КАБЕЛИ ДЛЯ СИГНАЛИЗАЦИИ И БЛОКИРОВКИ С ПОЛИЭТИЛЕНОВОЙ ИЗОЛЯЦИЕЙ В ПЛАСТМАССОВОЙ ОБОЛОЧКЕ Технические условия'" },
                new string[] { "3", "'ГОСТ 15125-92'", "'КАБЕЛИ СВЯЗИ СИММЕТРИЧНЫЕ ВЫСОКОЧАСТОТНЫЕ С КОРДЕЛЬНО-ПОЛИСТИРОЛЬНОЙ ИЗОЛЯЦИЕЙ Технические условия'" }
            };

            return table;
        }
    }
}
