#nullable enable
using LeaderBoards;
using LeaderBoards.View;
using QuizGameCore.Utils;
using Quizs;
using Quizs.Fails;
using Quizs.QuizSource;
using System;
using System.Collections;
using Uitls;
using UnityEngine;
using View;


public class EntryPoint : MonoBehaviour
{
    [SerializeField] private QuizView quizView = null!;
    [SerializeField] private Timer timer = null!;
    [SerializeField] private Attempts attempts = null!;
    [SerializeField] private SubmitLeaderView submitLeaderView = null!;
    [SerializeField] private LeaderBoardView leaderBoardView = null!;
    [SerializeField] private float rewardTime = 2;
    [SerializeField] private float suspendNextQuestionSeconds = 1;


    private void Awake()
    {
        timer.EnsureNotNull();
        attempts.EnsureNotNull();
        submitLeaderView.EnsureNotNull();
        leaderBoardView.EnsureNotNull();
    }


    private IEnumerator Start()
    {
        submitLeaderView.StartAndHide();

        var waitFail = WaitFail(
            new IFail.Any(
                new AttemptsFail(attempts),
                new TimerFail(timer)
            ).Cache(out var fail)
        );

        foreach (var info in GetComponent<IQuizSource>().EnsureNotNull().QuizList())
        {
            quizView.EnsureNotNull().Render(
                new AwaitCorrectAnswerQuiz(
                    new ShuffledQuiz(
                        new AttemptsQuiz(
                            new TimedQuiz(
                                info,
                                timer,
                                rewardTime
                            ),
                            attempts
                        )
                    )
                ).Cache(out var quiz)
            );
            yield return WaitAny(
                quiz.WaitCorrect(),
                waitFail
            );

            if (fail.Failed)
            {
                print("Game failed");
                break;
            }

            yield return new WaitForSeconds(suspendNextQuestionSeconds);
        }

        quizView.gameObject.SetActive(false);
        timer.enabled = false;
        //submitLeaderView.ShowWithRecord();
        //yield return WaitSubmit();
        leaderBoardView.Show();
    }


    private IEnumerator WaitSubmit()
    {
        var done = false;
        submitLeaderView.submitted.AddListener(() => done = true);
        while (done == false)
        {
            yield return null;
        }
    }


    private IEnumerator WaitAny(params IEnumerator[] items)
    {
        bool doneAny = false;

        IEnumerator WaitDone(IEnumerator enumerator)
        {
            yield return enumerator;
            doneAny = true;
        }

        foreach (var item in items)
        {
            StartCoroutine(WaitDone(item));
        }

        while (doneAny == false)
        {
            yield return null;
        }
    }


    private IEnumerator WaitFail(IFail fail)
    {
        while (fail.Failed == false)
        {
            yield return null;
        }
    }


    private class ValueBox<T>
    {
        public T? value;
    }
}