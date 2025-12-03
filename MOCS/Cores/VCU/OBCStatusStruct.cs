using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Cores.VC
{
    public struct OBCStatusStruct
    {
        public bool Is440VBatterySwitchClosed { get; set; }
        public bool Is480VPowerSwitchClosed { get; set; }
        public bool IsDC330VCircuitBreakerEnabled { get; set; }
        public bool Is25kWPowerFailed { get; set; }
        public bool Is5kWPowerFailed { get; set; }
        public bool IsPantographEnergized { get; set; }
        public bool IsPantographExtended1 { get; set; }
        public bool IsPantographExtended2 { get; set; }
        public bool IsPantographRetracted1 { get; set; }
        public bool IsPantographRetracted2 { get; set; }
        public bool IsLeviated { get; set; }
        public bool IsGuideEnabled { get; set; }
        public short Battery440VCapacity { get; set; }
        public short Battery110VCapacity { get; set; }
    }
}
