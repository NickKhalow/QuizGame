using Newtonsoft.Json;
using Uitls;
using UnityEngine;


namespace LeaderBoards
{
    public class LeaderBoardView : MonoBehaviour
    {
        [SerializeField] private NetworkLeaderBoard leaderBoard;


        private void Awake()
        {
            leaderBoard.EnsureNotNull();
        }


        public async void Show()
        {
            print(JsonConvert.SerializeObject(await leaderBoard!.Leaders()));
        }
    }
}