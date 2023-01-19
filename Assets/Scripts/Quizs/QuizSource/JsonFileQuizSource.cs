#nullable enable
using Newtonsoft.Json;
using QuizGameCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Uitls;
using UnityEngine;


namespace Quizs.QuizSource
{
    public class JsonFileQuizSource : MonoBehaviour, IQuizSource
    {
        [SerializeField] private TextAsset textAsset = null!;


        public IReadOnlyList<IQuiz> QuizList()
        {
            var raw = textAsset.EnsureNotNull().text;
            var list = JsonConvert.DeserializeObject<List<QuizData>>(raw).EnsureNotNull();
            return list.Select(q => q.Create()).ToList();
        }


        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public class QuizData
        {
            public string? Question;
            public string? CorrectAnswer;
            public string[]? WrongAnswers;


            public IQuiz Create()
            {
                return new Quiz(
                    Question.EnsureNotNull(),
                    CorrectAnswer.EnsureNotNull(),
                    WrongAnswers.EnsureNotNull()
                );
            }
        }
    }
}