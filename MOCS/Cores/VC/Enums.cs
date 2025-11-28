using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Cores.VC
{
    public enum VCInterfaceState
    {
        Stop,
        Running,
    }

    public enum VCInterfaceTrigger
    {
        Activate,
        Deactivate,
        LCUMsgSend,
    }
}
