#nullable enable
using Misc;
using QuizGameCore;
using System.Collections.Generic;
using TMPro;
using Uitls;
using UnityEngine;


namespace View
{
    public class QuizView : MonoBehaviour, IView<IQuiz>
    {
        [SerializeField] private TMP_Text textQuestion = null!;
        [SerializeField] private Transform buttonsHolder = null!;
        [SerializeField] private AnswerButton prefab = null!;
        private ObjectPool<AnswerButton> buttonsPool = null!;
        private IQuiz currentQuiz;
        private IList<AnswerButton> activeButtons = new List<AnswerButton>();


        private void Awake()
        {
            textQuestion.EnsureNotNull();
            buttonsHolder.EnsureNotNull();
            prefab.EnsureNotNull();

            buttonsPool = new ObjectPool<AnswerButton>(
                5,
                () =>
                {
                    var button = Instantiate(prefab, buttonsHolder).EnsureNotNull();
                    var effects = button.GetComponents<IButtonEffect>()!;
                    button.clicked.AddListener(value =>
                    {
                        var correct = currentQuiz.Answer(value);
                        foreach (var effect in effects)
                        {
                            effect.Notify(correct);
                        }
                    });
                    return button;
                },
                postPush: obj => obj.gameObject.SetActive(false),
                prePop: obj => obj.gameObject.SetActive(true)
            );
        }


        public void Render(IQuiz value)
        {
            currentQuiz = value;
            textQuestion.SetText(value.Question);

            foreach (var activeButton in activeButtons)
            {
                buttonsPool.Push(activeButton);
            }

            activeButtons.Clear();

            foreach (var variant in value.Variants)
            {
                var button = buttonsPool.Pop();
                button.Render(variant);
                activeButtons.Add(button);
            }
        }
    }
}