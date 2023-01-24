#nullable enable
using UnityEditor;


namespace Compiles
{
    public class ManyCompileFactory : ICompileFactory
    {
        private readonly ICompileFactory[] factories;


        public ManyCompileFactory(params ICompileFactory[] factories)
        {
            this.factories = factories;
        }


        public void Compile(string path, BuildOptions buildOptions)
        {
            foreach (var compileFactory in factories)
            {
                compileFactory.Compile(path, buildOptions);
            }
        }
    }
}