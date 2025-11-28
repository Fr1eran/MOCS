using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Protocals;

namespace MOCS.Coms
{
    public interface IHandlerInvoker
    {
        Task Invoke(object message);
    }

    public class HandlerInvoker<T> : IHandlerInvoker
    {
        private readonly Func<T, Task> _typedHandler;

        public HandlerInvoker(Func<T, Task> typedHandler)
        {
            _typedHandler = typedHandler;
        }

        public Task Invoke(object message)
        {
            return _typedHandler((T)message);
        }
    }

    public class MessageDispatcher
    {
        // 根据接收报文的类型注册的回调集合
        private readonly ConcurrentDictionary<Type, IHandlerInvoker> _handlers = [];

        /// <summary>
        /// 注册同步回调（内部包装为异步）
        /// 若已有回调，则会被覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        public void Subscribe<T>(Action<T> handler)
        {
            ArgumentNullException.ThrowIfNull(handler);
            Subscribe<T>(msg =>
            {
                handler(msg);
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 注册异步回调，同类型回调会被替换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        public void Subscribe<T>(Func<T, Task> handler)
        {
            ArgumentNullException.ThrowIfNull(handler);
            var messageType = typeof(T);

            var invoker = new HandlerInvoker<T>(handler);
            _handlers[messageType] = invoker;
        }

        /// <summary>
        /// 取消注册指定类型的回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool UnSubscribe<T>()
        {
            return _handlers.TryRemove(typeof(T), out _);
        }

        public Task Dispatch(object msg)
        {
            if (msg == null)
            {
                return Task.CompletedTask;
            }
            var messageType = msg.GetType();
            if (_handlers.TryGetValue(messageType, out var invoker) && invoker != null)
            {
                return invoker.Invoke(msg);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取已注册指定类型的异步回调数量
        /// </summary>
        public int HandlerCount => _handlers.Count;

        /// <summary>
        /// 检查是否已注册指定类型的异步回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsSubscribed<T>() => _handlers.ContainsKey(typeof(T));

        /// <summary>
        /// 清空所有已注册的异步回调
        /// </summary>
        public void Clear() => _handlers.Clear();
    }
}
