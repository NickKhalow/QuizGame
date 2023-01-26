using System.Collections.Generic;
using System.Threading.Tasks;


namespace LeaderBoards
{
    public interface ILeaderBoard
    {
        Task Note(string name, float time);


        Task<IReadOnlyList<Record>> Leaders();
    }


    public class Record
    {
        public string Name { get; set; }


        public float Time { get; set; }
    }
}