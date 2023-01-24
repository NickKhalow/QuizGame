using UnityEditor;


namespace Compiles
{
    public interface ICompileFactory
    {
        void Compile(string path, BuildOptions buildOptions);
    }
}