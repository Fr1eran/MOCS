using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Accessibility;
using MOCS.Protocals;
using NLog;

namespace MOCS.Coms
{
    public class UdpMessageService<TBaseMsg> : IDisposable, IAsyncDisposable
        where TBaseMsg : class
    {
        private readonly UdpClient _sender;
        private readonly UdpClient _receiver;

        // 根据接收报文的类型注册的回调集合
        private readonly MessageDispatcher _dispatcher;

        // 报文实例工厂类
        private readonly IMessageFactory<TBaseMsg> _messageFactory;

        private CancellationTokenSource? _cts;
        private Task? _receivingTask;
        private readonly object _sync = new();

        // 日志记录器
        private readonly ILogger _recvLogger;
        private readonly ILogger _sendLogger;

        public IPEndPoint RemoteEndPoint { get; }

        public UdpMessageService(
            IPAddress localIp,
            int localPort,
            IPAddress remoteIp,
            int remotePort,
            ILogger recvLogger,
            ILogger sendLogger,
            IMessageFactory<TBaseMsg>? messageFactory = null
        )
        {
            _sender = new UdpClient();
            _receiver = new UdpClient(new IPEndPoint(localIp, localPort));
            RemoteEndPoint = new IPEndPoint(remoteIp, remotePort);
            _dispatcher = new MessageDispatcher();
            _recvLogger = recvLogger;
            _sendLogger = sendLogger;
            _messageFactory = messageFactory ?? new MessageFactory<TBaseMsg>();
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
            try
            {
                var payLoad = msg.ToByteArray();
                var bytes = _messageFactory.ToTransmitByteArray(payLoad);
                await _sender.SendAsync(bytes, bytes.Length, RemoteEndPoint);
                _sendLogger.Debug(
                    $"发送成功 - 目标: {RemoteEndPoint.ToString()}, 类型: {typeof(T).Name}, 长度: {bytes.Length}字节, 数据: {BitConverter.ToString(bytes)}"
                );
            }
            catch (Exception ex)
            {
                _sendLogger.Error(ex, $"发送失败 - 类型: {typeof(T).Name}");
            }
        }

        public void StartListening()
        {
            lock (_sync)
            {
                if (_receivingTask?.IsCompleted == false)
                {
                    // 已监听
                    return;
                }
                _cts = new CancellationTokenSource();
                _receivingTask = Task.Run(() => ReceiveLoop(_cts.Token), _cts.Token);
            }
        }

        public async Task StopListeningAsync()
        {
            Task? runningTask = null;
            CancellationTokenSource? ctsToDispose = null;
            lock (_sync)
            {
                if (_cts == null)
                {
                    return;
                }
                ctsToDispose = _cts;
                runningTask = _receivingTask;
                _cts = null;
                _receivingTask = null;
            }

            ctsToDispose.Cancel();
            try
            {
                if (runningTask != null)
                {
                    await runningTask.ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消
            }
            finally
            {
                ctsToDispose.Dispose();
            }
        }

        private async Task ReceiveLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await _receiver.ReceiveAsync(token).ConfigureAwait(false);
                    if (_messageFactory.TryParseMessage(result.Buffer, out var msg, out var err))
                    {
                        //_recvLogger.Debug(
                        //    $"报文解析成功 - 来源: {result.RemoteEndPoint}, 原始报文: {BitConverter.ToString(result.Buffer)}"
                        //);
                        await _dispatcher.Dispatch(msg).ConfigureAwait(false);
                    }
                    else
                    {
                        _recvLogger.Warn(
                            $"报文解析失败 - 来源: {result.RemoteEndPoint}, 错误信息: {err}, 原始报文: {BitConverter.ToString(result.Buffer)} "
                        );
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _recvLogger.Error(ex, "UDP接收报文时出现异常");
                }
            }
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _sender.Dispose();
            _receiver.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await StopListeningAsync().ConfigureAwait(false);
            _sender.Dispose();
            _receiver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
