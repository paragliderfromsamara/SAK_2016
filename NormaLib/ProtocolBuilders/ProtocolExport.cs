using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;
using NormaLib.ProtocolBuilders.MSWord;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace NormaLib.ProtocolBuilders
{
    public static class ProtocolExport
    {
        public static bool ExportTo(CableTest cable_test, NormaExportType export)
        {
            ProtocolPathBuilder path_builder = new ProtocolPathBuilder(cable_test);
            switch (export)
            {
                case NormaExportType.MSWORD:
                    return ExportTo(cable_test, path_builder.FullPathWithFileName_ForMSWordProtocols, export);
                default:
                    return false;
            }
        }
        public static bool ExportTo(CableTest cable_test, string path, NormaExportType export)
        {
            //try
            //{
                CheckPath(path);
                ProtocolPathBuilder builder = new ProtocolPathBuilder(cable_test);
                builder.Path = path;
                switch(export)
                {
                    case NormaExportType.MSWORD:
                        MSWordCableTestProtocol protocol = new MSWordCableTestProtocol(cable_test, builder.Path);
                        protocol.EditorName = "Roman Kozvonin";
                        protocol.FirstPageHeaderText = "ООО \"НПП \"Норма\"";
                        protocol.AnotherPageHeaderText = cable_test.TestedCable.FullName;
                        protocol.CreateDocument();
                        Process.Start(builder.PathWithFileName);
                        break;
                }
                return true;
            //}catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка при формировании протокола", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
        }

        private static void CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }

    public enum NormaExportType
    {
        MSWORD,
        PDF
    }
}
