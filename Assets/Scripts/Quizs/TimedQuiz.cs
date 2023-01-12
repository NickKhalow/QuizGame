#nullable enable
using QuizGameCore;
using System.Collections.Generic;
using View;


namespace Quizs
{
    public class TimedQuiz : IQuiz
    {
        private readonly IQuiz origin;
        private readonly Timer timer;
        private readonly float rewardTime;


        public TimedQuiz(IQuiz origin, Timer timer, float rewardTime)
        {
            this.origin = origin;
            this.timer = timer;
            this.rewardTime = rewardTime;
        }


        public bool Answer(string answer)
        {
            var value = origin.Answer(answer);
            if (value)
            {
                timer.Add(rewardTime);
            }

            return value;
        }


        public string Question => origin.Question;


        public IReadOnlyList<string> Variants => origin.Variants;
    }
}