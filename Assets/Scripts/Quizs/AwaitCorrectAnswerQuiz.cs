#nullable enable
using QuizGameCore;
using QuizGameCore.Utils;
using System.Collections;
using System.Collections.Generic;


namespace Quizs
{
    public class AwaitCorrectAnswerQuiz : IQuiz
    {
        private readonly IQuiz origin;
        private bool correct = false;


        public AwaitCorrectAnswerQuiz(IQuiz origin)
        {
            this.origin = origin;
        }


        public bool Answer(string answer)
        {
            if (origin.Answer(answer).Cache(out var result))
            {
                correct = true;
            }

            return result;
        }


        public IEnumerator WaitCorrect()
        {
            while (correct == false)
            {
                yield return null;
            }
        }


        public string Question => origin.Question;


        public IReadOnlyList<string> Variants => origin.Variants;
    }
}