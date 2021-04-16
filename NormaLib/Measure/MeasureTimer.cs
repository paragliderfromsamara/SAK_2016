using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NormaLib.Measure
{
    public class MeasureTimer : IDisposable
    {
        public EventHandler OnTimerTick;
        public EventHandler OnTimerFinished;
        private Timer timer;
        int limit;
        int counter;
        public int TimeInSeconds => counter;
        public int Minutes => counter / 60;
        public int Seconds => (counter + 60) % 60;
        public string WatchDisplay => $"{convertToStringWatchElements(Minutes)}:{convertToStringWatchElements(Seconds)}";
        private bool IsCountDown = false;
        public MeasureTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            counter = 0;
        }

        public MeasureTimer(int limit_on_seconds, EventHandler on_timer_tick) : this()
        {
            limit = limit_on_seconds;
            OnTimerTick = on_timer_tick;
        }

        public MeasureTimer(int limit_on_seconds, EventHandler on_timer_tick, EventHandler on_timer_finished) : this()
        {
            limit = limit_on_seconds;
            OnTimerTick = on_timer_tick;
            OnTimerFinished = on_timer_finished;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsCountDown)
                --counter;
            else
                counter++;
            OnTimerTick?.Invoke(this, new EventArgs());
            if (counter == limit)
            {
                OnTimerFinished?.Invoke(this, new EventArgs());
                timer.Dispose();
            }
        }

        public void Start(bool is_count_down = false)
        {
            IsCountDown = is_count_down;
            if (is_count_down)
            {
                counter = limit;
                limit = 0;
            }
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private string convertToStringWatchElements(int v)
        {
            return (v > 9) ? $"{v}" : $"0{v}";
        }

        public void Dispose()
        {
            OnTimerTick = null;
            OnTimerFinished = null;
            if (timer != null) timer.Dispose();
        }
    }
}
