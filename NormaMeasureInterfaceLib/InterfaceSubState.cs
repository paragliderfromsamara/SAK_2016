using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Interface
{
    public class InterfaceSubState
    {
        private WorkspaceSubState subStateId;
        private InterfaceState interfaceState;
        public InterfaceSubState(WorkspaceSubState subState, InterfaceState interface_state)
        {
            subStateId = subState;
            interfaceState = interface_state;
        }
    }
    public enum WorkspaceSubState
    {
        NONE,
        CONNECTION_SETTINGS,
        DATABASE_SETTINGS,
        CLIENTS_SETTINGS,
        DATABASE_USERS,
        DATABASE_CABLES,
        DATABASE_TEST_RESULTS
    }
}
