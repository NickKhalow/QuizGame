#nullable enable
using QuizGameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quizs.QuizSource
{
    public class ScriptableObjectQuizSource : MonoBehaviour, IQuizSource
    {
        [SerializeField] private QuizInfo[] quizList = Array.Empty<QuizInfo>();
        
        public IReadOnlyList<IQuiz> QuizList()
        {
            return quizList.Select(q => q.Quiz()).ToList();
        }
    }
}