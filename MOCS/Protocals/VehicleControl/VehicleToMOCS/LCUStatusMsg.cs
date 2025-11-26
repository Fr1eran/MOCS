using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;

namespace MOCS.Protocals.VehicleControl.VehicleToMOCS
{
    enum GapSensorsStatus : byte
    {
        Normal = 0x00,
        OneFailed = 1 << 5,
        TwoFailed = 1 << 6,
        ThreeFailed = 0b_0110_0000,
        FourFailed = 1 << 7,
    }

    enum LevCmdSignal : byte
    {
        Leviate = 1 << 4,
        Land = 0x00,
    }

    enum LeviStatus : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    enum OverloadStatus : byte
    {
        Deactivate = 0x00,
        Activate = 1 << 6,
    }

    enum AccSensorStatus : byte
    {
        Normal = 0x00,
        OneFailed = 1 << 4,
        TwoFailed = 1 << 5,
    }

    enum LeviStatusSignal : byte
    {
        Leviate = 1 << 3,
        Land = 0x00,
    }

    enum WarningStatus : byte
    {
        Normal = 0x00,
        Warn = 1 << 2,
    }

    enum LeviControllerStatus : byte
    {
        Normal = 0x00,
        Short = 1 << 0,
        Voltage = 1 << 1,
        Other = 0b_0000_0011,
    }

    /// <summary>
    /// 接触器故障状态
    /// </summary>
    enum KMStatus : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    /// <summary>
    /// 接触器闭合断开状态
    /// </summary>
    enum KMContactStatus : byte
    {
        Closed = 1 << 7,
        Open = 0x00,
    }

    enum CPUStatus : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    public class LCUStatusMsg : BaseMessage, IIncomingMsg<BaseMessage> { }
}
