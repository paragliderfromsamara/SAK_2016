using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace NormaMeasure.DBControl.SAC.DBEntities
{
    public class DBSACTablesMigration : DBTablesMigration
    {
        private static DBTable _cablesTable = buildCablesTableMigration();
        private static DBTable _documentsTable = buildDocumentsTableMigration();
        
        public static DBTable CablesTable
        {
            get
            {
                return _cablesTable;
            }
        }

        public static DBTable DocumentsTable
        {
            get
            {
                return _documentsTable;
            }
        }


        public DBSACTablesMigration() : base()
        {
            _dbName = "db_norma_sac";
            _tablesList = new DBTable[] 
            {
                _documentsTable,
                _cablesTable
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
            table.columns = new DBTableColumn[]
            {
                new DBTableColumn { Name = "id", Type = "INT UNSIGNED AUTO_INCREMENT NOT NULL", OldName = "CabNum" },
                new DBTableColumn { Name = "name", Type = "TINYTEXT", OldName = "CabName" },
                new DBTableColumn { Name = "struct_name", Type = "TINYTEXT", OldName = "CabNameStruct" },
                new DBTableColumn { Name = "build_length", Type = "float", OldName = "StrLengt" },
                new DBTableColumn { Name = "document_id", Type = "INT UNSIGNED NOT NULL", OldName = "DocInd" },
                new DBTableColumn { Name = "notes", Type = "TINYTEXT", OldName = "TextPrim" },
                new DBTableColumn { Name = "linear_mass", Type = "float", OldName = "PogMass" },
                new DBTableColumn { Name = "code_okp", Type = "CHAR(12)", OldName = "KodOKP" },
                new DBTableColumn { Name = "code_kch", Type = "CHAR(2)", OldName = "KodOKP_KCH" },
                new DBTableColumn { Name = "u_cover", Type = "float", OldName = "U_Obol", DefaultValue = "NULL" },
                new DBTableColumn { Name = "p_min", Type = "float", OldName = "P_min", DefaultValue = "NULL" },
                new DBTableColumn { Name = "p_max", Type ="float", OldName = "P_max", DefaultValue = "NULL" },
                new DBTableColumn { Name = "is_draft", Type = "TINYINT(1)", DefaultValue = "0"},
                new DBTableColumn { Name = "is_deleted", Type = "TINYINT(1)", DefaultValue = "0"}
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
    }
}
