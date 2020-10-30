using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaMeasure.Interface
{
    public class InterfaceState
    {
        private WorkspaceState stateId;
        public WorkspaceState StateId => stateId;
        public Panel StatePanel;
        public InterfaceState(WorkspaceState state)
        {
            this.stateId = state;
        }    
    }
    
    public enum WorkspaceState
    {
        NONE,
        SETTINGS,
        DATABASE,
        MEASURE
    }

}
