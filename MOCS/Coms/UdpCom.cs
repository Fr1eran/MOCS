using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MOCS.Protocals;

namespace MOCS.Coms
{
    public class UdpCom : IDisposable
    {
        private readonly UdpClient _sender;
        private readonly UdpClient _receiver;
        private readonly CancellationTokenSource _cts = new();

        //public event Action<BaseMessage>? OnMessageReceived;

        // 根据接收报文的类型注册的回调集合
        private readonly Dictionary<Type, List<Delegate>> _recvmsghandlers = new();
        private readonly object _handlerslock = new();

        public IPEndPoint RemoteEndPoint { get; }

        public UdpCom(IPAddress localIp, int localPort, IPAddress remoteIp, int remotePort)
        {
            _sender = new UdpClient();
            _receiver = new UdpClient(new IPEndPoint(localIp, localPort));
            RemoteEndPoint = new IPEndPoint(remoteIp, remotePort);

            Task.Run(ReceiveLoop, _cts.Token);
        }

        public void Subscribe<T>(Action<T> handler)
            where T : IIncomingMsg
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            lock (_handlerslock)
            {
                var key = typeof(T);
                if (!_recvmsghandlers.TryGetValue(key, out var list))
                {
                    list = new List<Delegate>();
                    _recvmsghandlers[key] = list;
                }
                list.Add(handler);
            }
        }

        public void UnSubscribe<T>(Action<T> handler)
            where T : IIncomingMsg
        {
            if (handler == null)
            {
                return;
            }
            lock (_handlerslock)
            {
                var key = typeof(T);
                if (_recvmsghandlers.TryGetValue(key, out var list))
                {
                    list.Remove(handler);
                    if (list.Count == 0)
                    {
                        _recvmsghandlers.Remove(key);
                    }
                }
            }
        }

        public async Task SendAsync(BaseSendMsg msg)
        {
            var bytes = msg.ToByteArray();
            await _sender.SendAsync(bytes, bytes.Length, RemoteEndPoint);
        }

        private async Task ReceiveLoop()
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    var result = await _receiver.ReceiveAsync(_cts.Token);
                    if (MessageFactory.TryParseMessage(result.Buffer, out var msg, out var err))
                    {
                        if (msg is IIncomingMsg recvmsg)
                        {
                            // 按类型分发回调
                            List<Delegate>? toInvokeList = null;
                            lock (_handlerslock)
                            {
                                foreach (var kvp in _recvmsghandlers)
                                {
                                    if (kvp.Key.IsInstanceOfType(recvmsg))
                                    {
                                        toInvokeList = kvp.Value;
                                    }
                                }
                            }

                            if (toInvokeList != null)
                            {
                                foreach (var d in toInvokeList)
                                {
                                    d.DynamicInvoke(recvmsg);
                                }
                            }
                        }
                        else
                        {
                            // 无法转换，记录错误
                        }
                    }
                    else
                    {
                        // 记录报文解析错误信息
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch { }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _sender.Dispose();
            _receiver.Dispose();
        }
    }
}
