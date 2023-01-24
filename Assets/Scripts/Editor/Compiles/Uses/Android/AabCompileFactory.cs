#nullable enable
using UnityEditor;


namespace Compiles
{
    public class AabCompileFactory : ICompileFactory
    {
        private readonly ICompileFactory factory;


        public AabCompileFactory()
        {
            factory = new SimpleCompileFactory(BuildTarget.Android);
        }


        public void Compile(string path, BuildOptions buildOptions)
        {
            EditorUserBuildSettings.buildAppBundle = true;
            factory.Compile(path, buildOptions);
        }
    }
}