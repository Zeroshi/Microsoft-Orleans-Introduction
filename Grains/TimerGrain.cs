using System;
using System.Threading.Tasks;

namespace Introduction
{
    public class TimerGrain : Orleans.Grain, ITimerGrain
    {
        private IDisposable _timer;
        private int _currentValue = 1;

        public void StartTimer()
        {
            Console.WriteLine($"Timer Started {DateTime.Now}");
        }

        public void StopTimer()
        {
            _timer.Dispose();
            Console.WriteLine($"Timer Stopped {DateTime.Now}");
        }

        public override Task OnActivateAsync()
        {
            //createes a state and returns task
            //initial delay
            //frequency there after
            _timer = RegisterTimer(state =>
            {
                //time to run time method is an additional time delay we do not declare
                Console.WriteLine($"Timer number: {_currentValue}");
                _currentValue++;
                return base.OnActivateAsync();

            }, null,
            TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(2));

            return base.OnActivateAsync();
        }

        //Making available for mocking
        public virtual new IDisposable RegisterTimer(Func<object, Task> asyncCallback, object state, TimeSpan dueTime, TimeSpan period) =>
        base.RegisterTimer(asyncCallback, state, dueTime, period);

        Task<int> ITimerGrain.GetTimerNumberAsync()
        {
            return Task.FromResult(_currentValue);
        }
    }
}
