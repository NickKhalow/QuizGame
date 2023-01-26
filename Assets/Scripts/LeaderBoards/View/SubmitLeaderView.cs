#nullable enable
using QuizGameCore.Utils;
using TMPro;
using Uitls;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace LeaderBoards.View
{
    public class SubmitLeaderView : MonoBehaviour
    {
        public readonly UnityEvent submitted = new();
        [SerializeField] private TMP_Text timeText = null!;
        [SerializeField] private TMP_InputField nameInputField = null!;
        [SerializeField] private Button submitButton = null!;
        [Header("Debug")] [SerializeField] private float recordTime;
        private ILeaderBoard leaderBoard = null!;
        private readonly StopWatch stopWatch = new();


        private void Awake()
        {
            timeText.EnsureNotNull();
            nameInputField.EnsureNotNull();
            submitButton.EnsureNotNull();
            leaderBoard = GetComponent<ILeaderBoard>().EnsureNotNull();
        }


        private void Start()
        {
            submitButton.onClick!.AddListener(Submit);
        }


        private async void Submit()
        {
            await leaderBoard.Note(nameInputField.text!, recordTime)!;
            submitted.Invoke();
        }


        public void ShowWithRecord()
        {
            timeText.SetText($"Your record: {stopWatch.Stop().Cache(out recordTime):N2}");
            transform.localScale = Vector3.one;
        }


        public void StartAndHide()
        {
            stopWatch.Start();
            transform.localScale = Vector3.zero;
        }
    }
}