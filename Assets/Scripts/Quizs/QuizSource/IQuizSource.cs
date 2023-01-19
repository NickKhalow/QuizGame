#nullable enable
using QuizGameCore;
using System.Collections.Generic;


namespace Quizs.QuizSource
{
    public interface IQuizSource
    {
        IReadOnlyList<IQuiz> QuizList();
    }
}