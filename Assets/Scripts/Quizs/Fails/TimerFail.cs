#nullable enable
using View;


namespace Quizs.Fails
{
    public class TimerFail : IFail
    {
        private readonly Timer timer;


        public TimerFail(Timer timer)
        {
            this.timer = timer;
        }


        public bool Failed => timer.TimeLasts <= 0;
    }
}