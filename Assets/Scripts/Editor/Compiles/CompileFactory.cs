#nullable enable
using Editor.Compiles.Uses;
using Uitls;
using UnityEditor;
using UnityEngine;


namespace Compiles
{
    public class CompileFactory : MonoBehaviour
    {
        [SerializeField] private string path = string.Empty;


        //[Menu(nameof(Compile))]
        [MenuItem("CompileFactory/Compile")]
        public static void Compile()
        {
            // if (path.Split('.')!.Cache(out var pathElements).Length > 1
            //     && pathElements.Last().Length >= 2) //.txt .apk .jpg .mp3
            // {
            //     throw new ArgumentException($"Path must not contains extension {path}");
            // }

            new ManyCompileFactory(
                new ExtensionWhenFolderCompileFactory(
                    new ApkCompileFactory(),
                    ".apk"
                ),
                new ExtensionWhenFolderCompileFactory(
                    new AabCompileFactory(),
                    ".aab"
                ),
                new ExtensionWhenFolderCompileFactory(
                    new SimpleCompileFactory(BuildTarget.StandaloneOSX),
                    ".app"
                )
            ).Compile(
                EditorUtility.OpenFolderPanel(
                    "Choose folder",
                    "Assets",
                    "build"
                ).EnsureNotNull(),
                BuildOptions()
            );
        }


        private static BuildOptions BuildOptions()
        {
            return GetCurrentBuildOptions().options;
        }


        private static BuildPlayerOptions GetCurrentBuildOptions(BuildPlayerOptions defaultOptions = new())
        {
            return BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(defaultOptions);
        }
    }
}