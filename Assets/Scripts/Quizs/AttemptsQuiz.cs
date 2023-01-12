#nullable enable
using QuizGameCore;
using System.Collections.Generic;
using View;


namespace Quizs
{
    public class AttemptsQuiz : IQuiz
    {
        private readonly IQuiz origin;
        private readonly Attempts attempts;


        public AttemptsQuiz(IQuiz origin, Attempts attempts)
        {
            this.origin = origin;
            this.attempts = attempts;
        }


        public string Question => origin.Question;


        public IReadOnlyList<string> Variants => origin.Variants;


        public bool Answer(string answer)
        {
            if (origin.Answer(answer))
            {
                return true;
            }

            attempts.Decrease();
            return false;
        }
    }
}