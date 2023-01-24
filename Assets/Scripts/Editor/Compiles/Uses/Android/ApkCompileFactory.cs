#nullable enable
using UnityEditor;


namespace Compiles
{
    public class ApkCompileFactory : ICompileFactory
    {
        private readonly ICompileFactory factory;


        public ApkCompileFactory()
        {
            factory = new SimpleCompileFactory(BuildTarget.Android);
        }


        public void Compile(string path, BuildOptions buildOptions)
        {
            EditorUserBuildSettings.buildAppBundle = false;
            factory.Compile(path, buildOptions);
        }
    }
}