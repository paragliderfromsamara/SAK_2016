using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaMeasure.Interface.StateBuilders
{
    public class StateBuilder
    {
        protected Control parent;
        protected WorkspaceState state;
        protected InterfaceState interfaceState;
        protected WorkspaceSubState[] subStates;
        public InterfaceState InterfaceState => interfaceState;

        public StateBuilder(Control controlElement, WorkspaceState _state)
        {
            parent = controlElement;
            state = _state;
            subStates = assignSubStates();
            interfaceState = initState();
        }

       protected virtual WorkspaceSubState[] assignSubStates()
        {
            return new WorkspaceSubState[] { WorkspaceSubState.NONE };
        }

        protected virtual InterfaceState initState()
        {
            return new InterfaceState(WorkspaceState.NONE);
        }


    }
}
