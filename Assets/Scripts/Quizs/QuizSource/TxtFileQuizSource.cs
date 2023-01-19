#nullable enable
using QuizGameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uitls;
using UnityEngine;


namespace Quizs.QuizSource
{
    public class TxtFileQuizSource : MonoBehaviour, IQuizSource
    {
        [SerializeField] private TextAsset? textAsset;


        public IReadOnlyList<IQuiz> QuizList()
        {
            var raw = textAsset.EnsureNotNull().text.EnsureNotNull()!;
            var lines = raw.Split('\n')!.Where(l => l.Trim() is {Length: > 0}).ToList();
            if (lines.Count % 3 != 0)
            {
                throw new InvalidOperationException("Invalid document");
            }

            var list = new List<IQuiz>(lines.Count / 3);
            for (var i = 0; i < lines.Count; i += 3)
            {
                list.Add(
                    new Quiz(
                        lines[i].EnsureNotNull(),
                        lines[i + 1].EnsureNotNull(),
                        lines[i + 2].EnsureNotNull()
                            .Split(',', ';').EnsureNotNull()
                            .Where(e => e.Trim() is {Length: > 0})
                            .ToList()
                    )
                );
            }

            return list;
        }
    }
}