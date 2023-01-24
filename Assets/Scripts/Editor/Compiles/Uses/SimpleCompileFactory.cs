using Uitls;
using UnityEditor;


namespace Compiles
{
    public class SimpleCompileFactory : ICompileFactory
    {
        private readonly BuildTarget buildTarget;


        public SimpleCompileFactory(BuildTarget buildTarget)
        {
            this.buildTarget = buildTarget;
        }


        public void Compile(string path, BuildOptions buildOptions)
        {
            BuildPipeline.BuildPlayer(
                Scenes(),
                path,
                buildTarget,
                buildOptions
            );
        }


        private static EditorBuildSettingsScene[] Scenes()
        {
            return EditorBuildSettings.scenes.EnsureNotNull();
        }
    }
}