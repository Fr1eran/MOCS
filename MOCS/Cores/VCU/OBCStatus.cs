namespace MOCS.Cores.VCU
{
    public class OBCStatus
    {
        public bool Is440VBatterySwitchClosed { get; set; } = false;
        public bool Is480VPowerSwitchClosed { get; set; } = false;
        public bool IsDC330VCircuitBreakerEnabled { get; set; } = false;
        public bool Is25kWPowerFailed { get; set; } = false;
        public bool Is5kWPowerFailed { get; set; } = false;
        public bool IsPantographEnergized { get; set; } = false;
        public bool IsPantographExtended1 { get; set; } = false;
        public bool IsPantographExtended2 { get; set; } = false;
        public bool IsPantographRetracted1 { get; set; } = true;
        public bool IsPantographRetracted2 { get; set; } = true;
        public bool IsLeviated { get; set; } = false;
        public bool IsGuideEnabled { get; set; } = false;
        public short Battery440VCapacity { get; set; } = 0;
        public short Battery110VCapacity { get; set; } = 0;

        public void Reset()
        {
            Is440VBatterySwitchClosed = false;
            Is480VPowerSwitchClosed = false;
            IsDC330VCircuitBreakerEnabled = false;
            Is25kWPowerFailed = false;
            Is5kWPowerFailed = false;
            IsPantographEnergized = false;
            IsPantographExtended1 = false;
            IsPantographExtended2 = false;
            IsPantographRetracted1 = true;
            IsPantographRetracted2 = true;
            IsLeviated = false;
            IsGuideEnabled = false;
            Battery440VCapacity = 0;
            Battery110VCapacity = 0;
        }
    }
}
