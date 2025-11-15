using System;
using System.Diagnostics;

namespace MOCS.Utils
{
    public class HighPrecisionTimer : IDisposable
    {
        private readonly System.Threading.Timer _timer;
        private readonly TimeSpan _period;
        private readonly Action _callback;

        private readonly object _lock = new();
        private bool _running = false;
        private bool _disposed = false;

        private Stopwatch _stopwatch;
        private long _nextTick; // 下一次应该触发的绝对时间（Stopwatch ticks）

        public HighPrecisionTimer(TimeSpan period, Action callback)
        {
            _period = period;
            _callback = callback;

            _timer = new System.Threading.Timer(TimerTick, null, Timeout.Infinite, Timeout.Infinite);
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            lock (_lock)
            {
                if (_running) return;
                _running = true;
                _stopwatch.Restart();

                _nextTick = _period.Ticks * Stopwatch.Frequency / TimeSpan.TicksPerSecond;
                ScheduleNext();
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                _running = false;
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        private void TimerTick(object? state)
        {
            lock (_lock)
            {
                if (!_running || _disposed) return;
            }

            try
            {
                _callback?.Invoke();
            }
            catch
            {
                // 若需要可加日志
            }

            lock (_lock)
            {
                if (!_running || _disposed) return;

                // 计算下一次绝对触发点
                _nextTick += _period.Ticks * Stopwatch.Frequency / TimeSpan.TicksPerSecond;
                ScheduleNext();
            }
        }

        private void ScheduleNext()
        {
            long now = _stopwatch.ElapsedTicks;
            long remaining = _nextTick - now;

            if (remaining < 0)
                remaining = 0; // 回调耗时太长，立即触发下一次，不积累误差

            long ms = (long)(remaining * 1000.0 / Stopwatch.Frequency);

            _timer.Change(ms, Timeout.Infinite);
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _disposed = true;
                _running = false;
                _timer.Dispose();
            }
        }
    }
}