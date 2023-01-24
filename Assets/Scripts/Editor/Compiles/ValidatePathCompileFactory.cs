using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;


namespace Compiles
{
    public class ValidatePathCompileFactory : ICompileFactory
    {
        private readonly ICompileFactory factory;


        public ValidatePathCompileFactory(ICompileFactory factory)
        {
            this.factory = factory;
        }


        public void Compile(string path, BuildOptions buildOptions)
        {

            throw new Exception($"Path {path}");
            factory.Compile(path, buildOptions);
        }
    }
}