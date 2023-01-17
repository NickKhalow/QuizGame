#nullable enable
using QuizGameCore.Utils;
using Quizs;
using System.Collections;
using Uitls;
using UnityEngine;
using View;


public class EntryPoint : MonoBehaviour
{
    [SerializeField] private QuizInfo[] quizs = null!;
    [SerializeField] private QuizView quizView = null!;
    [SerializeField] private Timer timer = null!;
    [SerializeField] private Attempts attempts = null!;
    [SerializeField] private float rewardTime = 2;
    [SerializeField] private float suspendNextQuestionSeconds = 1;


    private IEnumerator Start()
    {
        timer.EnsureNotNull();
        attempts.EnsureNotNull();

        foreach (var info in quizs)
        {
            quizView.EnsureNotNull().Render(
                new AwaitCorrectAnswerQuiz(
                    new AttemptsQuiz(
                        new TimedQuiz(
                            info.Quiz(),
                            timer,
                            rewardTime
                        ),
                        attempts
                    )
                ).Cache(out var quiz)
            );
            yield return quiz.WaitCorrect();
            yield return new WaitForSeconds(suspendNextQuestionSeconds);
        }

        quizView.gameObject.SetActive(false);
        timer.enabled = false;
    }
}