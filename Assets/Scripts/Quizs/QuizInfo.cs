#nullable enable
using QuizGameCore;
using Uitls;
using UnityEngine;


namespace Quizs
{
    [CreateAssetMenu(fileName = "Quiz", menuName = "Game/Quiz", order = 0)]
    public class QuizInfo : ScriptableObject
    {
        [SerializeField] private string question = null!;
        [SerializeField] private string correctAnswer = null!;
        [SerializeField] private string[] wrongAnswers = null!;


        public IQuiz Quiz()
        {
            return new Quiz(question.EnsureNotNull(), correctAnswer.EnsureNotNull(), wrongAnswers.EnsureNotNull());
        }
    }
}