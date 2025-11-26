using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Protocals;

namespace MOCS.Coms
{
    public class MessageDispatcher<TBaseMsg>
    {
        // 根据接收报文的类型注册的回调集合
        private readonly ConcurrentDictionary<Type, Func<TBaseMsg, Task>> _handlers = [];

        //private readonly Dictionary<Type, Delegate> _handlers = [];
        //private readonly object _handlerslock = new();

        /// <summary>
        /// 注册同步回调（内部包装为异步）
        /// 若已有回调，则会被覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        public void Subscribe<T>(Action<T> handler)
            where T : TBaseMsg
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
            where T : TBaseMsg
        {
            ArgumentNullException.ThrowIfNull(handler);
            var messageType = typeof(T);
            Task wrapper(TBaseMsg msg) => handler((T)msg);
            _handlers[messageType] = wrapper;
        }

        /// <summary>
        /// 取消注册指定类型的回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool UnSubscribe<T>()
            where T : TBaseMsg
        {
            return _handlers.TryRemove(typeof(T), out _);
        }

        public Task Dispatch(TBaseMsg msg)
        {
            if (msg == null)
            {
                return Task.CompletedTask;
            }
            var messageType = msg.GetType();
            if (_handlers.TryGetValue(messageType, out var handler) && handler != null)
            {
                return handler(msg) ?? Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
