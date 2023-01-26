#nullable enable
using Newtonsoft.Json;
using QuizGameCore.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;


namespace LeaderBoards
{
    public class NetworkLeaderBoard : MonoBehaviour, ILeaderBoard
    {
        [SerializeField] private string baseUrl = "https://localhost:7087/";


        public async Task Note(string playerName, float time)
        {
            StartCoroutine(SendNote(playerName, time, new Box<bool>(false).Cache(out var doneBox)));
            var yield = Task.Yield();
            while (doneBox.Value == false)
            {
                await yield;
            }
        }


        public async Task<IReadOnlyList<Record>> Leaders()
        {
            StartCoroutine(Fetch(new Box<List<Record>>(null).Cache(out var records)));
            var yield = Task.Yield();
            while (records.Value == null)
            {
                await yield;
            }

            return records.Value!;
        }


        private IEnumerator Fetch(Box<List<Record>> records)
        {
            yield return UnityWebRequest.Get(baseUrl).Cache(out var request)!.SendWebRequest();
            print(request!.result);
            records.Value = JsonConvert.DeserializeObject<List<Record>>(request.downloadHandler!.text!)!;
        }


        private IEnumerator SendNote(string playerName, float time, Box<bool> done)
        {
            yield return UnityWebRequest.Post(baseUrl, new Dictionary<string, string>
            {
                ["name"] = playerName,
                ["time"] = time.ToString()
            }).Cache(out var request).SendWebRequest();
            print(request!.responseCode);
            done.Value = true;
        }


        private class Box<T>
        {
            public T? Value { get; set; }


            public Box(T? value)
            {
                Value = value;
            }
        }
    }
}