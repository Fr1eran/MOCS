using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;

namespace MOCS.Protocals.VehicleControl.VehicleToMOCS
{
    public class OBCStatusMsg : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public bool ControlModeSwitch { get; }
        public bool EmergencyStop { get; }
        public bool BatteryStatus { get; }
        public bool BatteryContactStatus { get; }
        public bool CollectorStatus { get; }
        public bool ModeSwitch { get; }
        public bool ModeSwitch { get; }
        public bool ModeSwitch { get; }
        public bool ModeSwitch { get; }

        public static bool TryParse(
            ReadOnlyMemory<byte> buffer,
            [NotNullWhen(true)] out BaseMessage? msg,
            out string? error
        ) { }
    }
}
