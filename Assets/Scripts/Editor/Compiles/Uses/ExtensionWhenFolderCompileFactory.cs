#nullable enable
using Compiles;
using System.IO;
using UnityEditor;


namespace Editor.Compiles.Uses
{
    public class ExtensionWhenFolderCompileFactory : ICompileFactory
    {
        private readonly ICompileFactory origin;
        private readonly string extension;


        public ExtensionWhenFolderCompileFactory(ICompileFactory origin, string extension)
        {
            this.origin = origin;
            this.extension = extension;
        }


        public void Compile(string path, BuildOptions buildOptions)
        {
            if (Directory.Exists(path))
            {
                path = Path.ChangeExtension(path, extension);
            }

            origin.Compile(path, buildOptions);
        }
    }
}