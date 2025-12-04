using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Cores.VC;

namespace MOCS.Cores.VCU
{
    public class OBCControlStruct
    {
        /// <summary>
        /// 是否切换远程模式
        /// </summary>
        public bool IsRemoteModeActivate { get; set; } = false;

        /// <summary>
        /// 是否急停
        /// </summary>
        public bool IsEmergencyStop { get; set; } = false;

        /// <summary>
        /// 电池是否启动
        /// </summary>
        public bool IsBatteryEnable { get; set; } = false;

        /// <summary>
        /// 电源输出开关是否合闸
        /// </summary>
        public bool IsPowerSwitchClose { get; set; } = false;

        /// <summary>
        /// 受流器是否伸出
        /// </summary>
        public bool IsPantographExtend { get; set; } = false;

        /// <summary>
        /// 是否悬浮
        /// </summary>
        public bool IsLeviateActivate { get; set; } = false;

        /// <summary>
        /// 是否开启导向
        /// </summary>
        public bool IsGuideActivate { get; set; } = false;

        /// <summary>
        /// 是否切换至紧急模式
        /// </summary>
        public bool IsEmergencyModeActivate { get; set; } = false;

        /// <summary>
        /// 制动等级
        /// </summary>
        public byte BrakeLevel { get; set; } = 0;

        private static readonly byte UserDataBytesNum = 14;

        public byte[] ToBytesArray()
        {
            BitArray cmd = new BitArray(40);
            cmd[1] = IsRemoteModeActivate; // 本地/远程切换（0：本地 1：远程）
            cmd[2] = IsEmergencyStop; // 急停（0：消失 1：发生）

            cmd[3] = IsBatteryEnable; // 电池启动（0：消失 1：发生）
            cmd[4] = !IsBatteryEnable; // 电池停止（0：消失 1：发生）
            cmd[5] = IsPowerSwitchClose; // 电源合闸（0：消失 1：发生）
            cmd[6] = !IsPowerSwitchClose; // 电源分闸（0：消失 1：发生）
            cmd[9] = IsPantographExtend; // 受流器伸（0：消失 1：发生）
            cmd[10] = !IsPantographExtend; // 受流器缩（0：消失 1：发生）
            cmd[11] = IsLeviateActivate; // 悬浮起浮（0：消失 1：发生）
            cmd[12] = IsGuideActivate; // 导向起浮（0：消失 1：发生）
            cmd[13] = IsLeviateActivate && IsGuideActivate; // 悬浮导向起浮（0：消失 1：发生）
            cmd[14] = !IsGuideActivate; // 导向落浮（0：消失 1：发生）
            cmd[15] = false; // 悬浮导向落浮（0：消失 1：发生）

            cmd[16] = IsEmergencyModeActivate; // 紧急模式切换（0：正常模式 1：紧急模式）

            cmd[19] = IsBatteryEnable; // 电池启动（0：消失 1：发生）
            cmd[20] = !IsBatteryEnable; // 电池停止（0：消失 1：发生）
            cmd[21] = IsPowerSwitchClose; // 电源合闸（0：消失 1：发生）
            cmd[22] = !IsPowerSwitchClose; // 电源分闸（0：消失 1：发生）
            cmd[23] = IsPantographExtend; // 受流器伸（0：消失 1：发生）
            cmd[24] = !IsPantographExtend; // 受流器缩（0：消失 1：发生）
            cmd[25] = IsLeviateActivate; // 悬浮起浮（0：消失 1：发生）
            cmd[26] = IsGuideActivate; // 导向起浮（0：消失 1：发生）
            cmd[27] = IsLeviateActivate && IsGuideActivate; // 悬浮导向起浮（0：消失 1：发生）
            cmd[28] = !IsGuideActivate; // 导向落浮（0：消失 1：发生）
            cmd[29] = false; // 悬浮导向落浮（0：消失 1：发生）

            cmd[30] = (BrakeLevel & (1 << 0)) != 0;
            cmd[31] = (BrakeLevel & (1 << 1)) != 0;
            cmd[32] = (BrakeLevel & (1 << 2)) != 0;

            byte[] result = new byte[UserDataBytesNum];
            cmd.CopyTo(result, 0);
            return result;
        }
    }
}
