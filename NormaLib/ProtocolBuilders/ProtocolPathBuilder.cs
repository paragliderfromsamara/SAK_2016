using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;

namespace NormaLib.ProtocolBuilders
{
    public class ProtocolPathBuilder
    {
        object protocol_entity;
        public ProtocolPathBuilder(object protocol_entity)
        {
            this.protocol_entity = protocol_entity;
        }

        public string FileName
        {
            get
            {
                Type t = protocol_entity.GetType();
                if (t == typeof(CableTest))
                {
                    CableTest test = protocol_entity as CableTest;
                    return $"Испытание №{test.TestId}";
                }
                else return "";
            }
        }

        //public string FileName_MSWord => $"{FileName}.docx";

        string path = null;
        public string Path
        {
            get
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    Type t = protocol_entity.GetType();
                    if (t == typeof(CableTest))
                    {
                        CableTest test = protocol_entity as CableTest;
                        path = $@"{test.FinishedAt.Year}\{test.FinishedAt.Month}\{test.FinishedAt.Day}";
                    }
                }
                return path;

            }
            set
            {
                path = value;
            }
        }

        public string FullPath_ForMSWordProtocols => $@"{ProtocolSettings.MSWordProtocolsPath}{Path}";

        public string FullPathWithFileName_ForMSWordProtocols => $@"{ProtocolSettings.MSWordProtocolsPath}{PathWithFileName}.docx";
        public string PathWithFileName => $@"{Path}\{FileName}";
    }
}
