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
    public class UdpMessageService<TBaseMsg> : IDisposable
        where TBaseMsg : class
    {
        private readonly UdpClient _sender;
        private readonly UdpClient _receiver;
        private readonly CancellationTokenSource _cts = new();

        // 根据接收报文的类型注册的回调集合
        private readonly MessageDispatcher _dispatcher;

        // 报文实例工厂类
        private readonly IMessageParser<TBaseMsg> _messageFactory;

        public IPEndPoint RemoteEndPoint { get; }

        public UdpMessageService(
            IPAddress localIp,
            int localPort,
            IPAddress remoteIp,
            int remotePort,
            IMessageParser<TBaseMsg>? messageFactory = null
        )
        {
            _sender = new UdpClient();
            _receiver = new UdpClient(new IPEndPoint(localIp, localPort));
            RemoteEndPoint = new IPEndPoint(remoteIp, remotePort);
            _dispatcher = new MessageDispatcher();
            _messageFactory = messageFactory ?? new MessageFactory<TBaseMsg>();

            Task.Run(ReceiveLoop, _cts.Token);
        }

        public void RegisterParser(
            byte msgId,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        ) => _messageFactory.RegisterParser(msgId, parser);

        public void RegisterRangeParser(
            byte low,
            byte high,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        ) => _messageFactory.RegisterRangeParser(low, high, parser);

        public void Subscribe<T>(Action<T> handler)
            where T : TBaseMsg
        {
            _dispatcher.Subscribe(handler);
        }

        public void UnSubscribe<T>()
            where T : TBaseMsg
        {
            _dispatcher.UnSubscribe<T>();
        }

        public async Task SendAsync<T>(T msg)
            where T : TBaseMsg, IOutgoingMsg
        {
            ArgumentNullException.ThrowIfNull(msg);
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
                    if (_messageFactory.TryParseMessage(result.Buffer, out var msg, out var err))
                    {
                        await _dispatcher.Dispatch(msg);
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
                catch (Exception ex)
                {
                    // 记录异常
                }
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
