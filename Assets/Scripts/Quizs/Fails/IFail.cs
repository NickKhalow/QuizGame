#nullable enable
namespace Quizs.Fails
{
    public interface IFail
    {
        public bool Failed { get; }


        public class Any : IFail
        {
            private readonly IFail[] fails;


            public Any(params IFail[] fails)
            {
                this.fails = fails;
            }


            public bool Failed
            {
                get
                {
                    foreach (var fail in fails)
                    {
                        if (fail.Failed)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }
    }
}