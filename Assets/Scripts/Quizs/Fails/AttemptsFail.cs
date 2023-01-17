#nullable enable
using View;


namespace Quizs.Fails
{
    public class AttemptsFail : IFail
    {
        private readonly Attempts attempts;


        public AttemptsFail(Attempts attempts)
        {
            this.attempts = attempts;
        }


        public bool Failed => attempts.Count <= 0;
    }
}