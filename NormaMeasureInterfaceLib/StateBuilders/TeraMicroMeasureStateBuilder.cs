using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NormaMeasure.Interface.StateBuilders
{
    public class TeraMicroMeasureStatesBuilder : StateBuilder
    {
        public TeraMicroMeasureStatesBuilder(Control controlElement, WorkspaceState _state) : base(controlElement, _state)
        {

        }

        protected override InterfaceState initState()
        {
            InterfaceState s;
            switch(state)
            {
                case WorkspaceState.SETTINGS:
                    s = buildSettingsState();
                    break;
                default:
                    s = new InterfaceState(WorkspaceState.NONE);
                    break;
            }
            return s;
        }

        protected override WorkspaceSubState[] assignSubStates()
        {
            WorkspaceSubState[] arr;
            switch (state)
            {
                case WorkspaceState.SETTINGS:
                    arr = new  WorkspaceSubState[] { };
                    break;
                default:
                    arr = new WorkspaceSubState[] { WorkspaceSubState.NONE};
                    break;
            }
            return arr;
        }

        private InterfaceState buildSettingsState()
        {
            InterfaceState s = new InterfaceState(state);
            s.StatePanel = new Panel();
            s.StatePanel.Dock = DockStyle.Fill;
           // s.StatePanel.BackColor = 
            return s;
        }

        //private Panel buildLeftPanel()
        //{
        //}
    }
}
