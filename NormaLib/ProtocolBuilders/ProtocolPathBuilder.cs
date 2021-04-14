using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;
using System.IO;

namespace NormaLib.ProtocolBuilders
{
    public class ProtocolPathBuilder
    {
        object protocol_entity;
        private NormaExportType export_type;
        private Type entity_type;
        public Type EntityType => entity_type;
        public object Entity => protocol_entity;


        public ProtocolPathBuilder(object protocol_entity, NormaExportType export_type, string path = null)
        {
            this.protocol_entity = protocol_entity;
            this.entity_type = protocol_entity.GetType();
            this.export_type = export_type;
            if (!string.IsNullOrWhiteSpace(path)) this.path = path;
            
        }

        internal bool ProtocolExists()
        {
            return File.Exists(Path_WithFileName);
        }



        public void InitPath()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        public string FileName
        {
            get
            {
                Type t = protocol_entity.GetType();
                if (t == typeof(CableTest))
                {
                    CableTest test = protocol_entity as CableTest;
                    return $"Испытание №{test.TestId}{FileExtention}";
                }
                else return "";
            }
        }

        private string FileExtention
        {
            get
            {
                switch(export_type)
                {
                    case NormaExportType.MSWORD:
                        return ".docx";
                    case NormaExportType.PDF:
                        return ".pdf";
                    default:
                        return "";
                }
            }
        }

        //public string FileName_MSWord => $"{FileName}.docx";

        public string DefaultSubFolderPath
        {
            get
            {
                Type t = protocol_entity.GetType();
                if (t == typeof(CableTest))
                {
                    CableTest test = protocol_entity as CableTest;
                    return $@"{test.FinishedAt.Year}\{test.FinishedAt.Month}\{test.FinishedAt.Day}";
                }
                return @"temp";

            }
        }

        private string BuildDefaultDirectory()
        {
            return $@"{protocol_default_path}\{DefaultSubFolderPath}";
        }

        private string path = null;

        private string protocol_default_path
        {
            get
            {
                switch(export_type)
                {
                    case NormaExportType.MSWORD:
                        return ProtocolSettings.MSWordProtocolsPath;
                    default:
                        return AppContext.BaseDirectory;
                }
            }
        }

        public string Path => (string.IsNullOrWhiteSpace(path) ? path = BuildDefaultDirectory() : path);
        public string Path_WithFileName => $@"{Path}\{FileName}";

    }

    public enum NormaExportType
    {
        MSWORD,
        PDF
    }
}
