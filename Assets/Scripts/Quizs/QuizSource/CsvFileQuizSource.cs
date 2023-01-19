#nullable enable
using QuizGameCore;
using System.Collections.Generic;
using System.Linq;
using Uitls;
using UnityEngine;


namespace Quizs.QuizSource
{
    public class CsvFileQuizSource : MonoBehaviour, IQuizSource
    {
        [SerializeField] private TextAsset? textAsset;


        public IReadOnlyList<IQuiz> QuizList()
        {
            var raw = textAsset.EnsureNotNull().text.EnsureNotNull()!;
            var lines = raw.Split('\n')!.Skip(1);
            return lines.Select(l =>
            {
                var elements = l.Split(',').EnsureNotNull();
                return new Quiz(
                    elements[0].EnsureNotNull(),
                    elements[1].EnsureNotNull(),
                    elements.Skip(2).Where(e => e.Trim() is {Length: > 0}).ToArray()
                );
            }).ToList();
        }
    }
}