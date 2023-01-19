#nullable enable
using QuizGameCore;
using System.Collections.Generic;
using UnityEngine;


namespace Quizs
{
    public class ShuffledQuiz : IQuiz
    {
        private readonly IQuiz origin;


        public ShuffledQuiz(IQuiz origin)
        {
            this.origin = origin;
        }


        public string Question => origin.Question;


        public bool Answer(string answer)
        {
            return origin.Answer(answer);
        }


        public IReadOnlyList<string> Variants => new RandomSort(
            origin.Variants
        ).Shuffle();


        private class RandomSort : IComparer<string>
        {
            private readonly int[] possibles = {-1, 1};
            private readonly List<string> list;


            public RandomSort(List<string> list)
            {
                this.list = list;
            }


            public RandomSort(IReadOnlyList<string> readOnlyList) : this(new List<string>(readOnlyList)) { }


            public int Compare(string x, string y)
            {
                return possibles[Random.Range(0, possibles.Length)];
            }


            public List<string> Shuffle()
            {
                list.Sort(this);
                list.Sort(this);
                return list;
            }
        }
    }
}