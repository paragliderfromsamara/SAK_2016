using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.ProtocolBuilders
{
    public class ProtocolViewer
    {
        private ProtocolExport export;
        public ProtocolViewer(ProtocolExport export)
        {
            this.export = export;
            Debug.WriteLine(this.export.FilePath);
            OpenFile(this.export.FilePath);
        }

        public ProtocolViewer(ProtocolPathBuilder path_builder)
        {
            if (path_builder.FileExists())
            {
                OpenFile(path_builder.Path_WithFileName);
            }else
            {
                export = new ProtocolExport(path_builder);
                OpenFile(this.export.FilePath);
            }
        }

        private void OpenFile(string file_path)
        {
            Process.Start(file_path);
        } 
    }
}
