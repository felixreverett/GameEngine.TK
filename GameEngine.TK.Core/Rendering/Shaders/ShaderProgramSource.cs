using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.TK.Core.Rendering
{
    public class ShaderProgramSource
    {
        public string VertexShaderSource;
        public string FragmentShaderSource;

        public ShaderProgramSource(string vertexShaderSource, string fragmentShaderSource)
        {
            VertexShaderSource = vertexShaderSource;
            FragmentShaderSource = fragmentShaderSource;
        }
    }
}
