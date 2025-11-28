using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
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

    public class LCUStatusMsgA : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public static bool TryParse(
            ReadOnlyMemory<byte> buffer,
            [NotNullWhen(true)] out BaseMessage? msg,
            out string? error
        )
        {
            msg = null;
            error = null;

            var span = buffer.Span;
            var CANID = span[8];
            if (CANID < 0X21 | CANID > 0X34)
            {
                error = $"悬浮控制器的1类CAN数据帧ID:{CANID:X2}与规定目标范围0x21~0x34不符";
                return false;
            }

            var statusMsgLen = buffer.Length - 10;
            if (statusMsgLen != 8)
            {
                error = $"悬浮控制器的CAN数据帧长度:{statusMsgLen}不为8字节";
                return false;
            }

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];

            msg = new LCUStatusMsgA
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = buffer.Slice(9, 8),
            };
            return true;
        }
    }
}
