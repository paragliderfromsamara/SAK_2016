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
    public class ProtocolExport
    {
        private ProtocolPathBuilder path_builder = null;
        public string FilePath => path_builder.Path_WithFileName;
        public ProtocolExport(ProtocolPathBuilder path_builder)
        {
            this.path_builder = path_builder;
            try
            {
                path_builder.InitPath();
                if (path_builder.EntityType == typeof(CableTest))
                    ExportCableTestProtocol();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при формировании протокола", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ExportCableTestProtocol()
        {
            CableTest test = path_builder.Entity as CableTest;
            MSWordCableTestProtocol protocol = new MSWordCableTestProtocol(path_builder);
            protocol.CreateDocument();
            //Process.Start(path_builder.Path_WithFileName);
        }



       
        

    }


}
