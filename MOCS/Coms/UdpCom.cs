using MOCS.Protocals.Propulsion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Coms
{
    public class UdpCom : IDisposable
    {
        private readonly UdpClient _sender;
        private readonly UdpClient _receiver;
        private readonly CancellationTokenSource _cts = new();

        public event Action<BaseMessage>? OnMessageReceived;

        public IPEndPoint RemoteEndPoint { get; }

        public UdpCom(IPAddress localIp, int localPort, IPAddress remoteIp, int remotePort)
        {
            _sender = new UdpClient();
            _receiver = new UdpClient(new IPEndPoint(localIp, localPort));
            RemoteEndPoint = new IPEndPoint(remoteIp, remotePort);

            Task.Run(ReceiveLoop, _cts.Token);
        }

        public async Task SendAsync(BaseMessage msg)
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
                    if (BaseMessage.TryParse(result.Buffer, out var msg, out _))
                    {
                        OnMessageReceived?.Invoke(msg);
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