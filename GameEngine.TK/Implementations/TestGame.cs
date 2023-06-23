using GameEngine.TK.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameEngine.TK
{
    internal class TestGame : Game
    {
        public TestGame(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private readonly float[] _vertices =
        {
            // Positions x,y,z  // Colours
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, // bottom left - Red
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, // bottom right - Green
             0.0f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f  // top - Blue
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private int _shaderHandle;

        protected override void Initialize()
        {
            
        }

        protected override void LoadContent()
        {
            string vertexShader = @"
            #version 330 core
            layout (location = 0) in vec3 aPosition;
            layout (location = 1) in vec3 aColor;
            out vec4 vertexColor;

            void main() 
            {
                vertexColor = vec4(aColor.rgb, 1.0);
                gl_Position = vec4(aPosition.xyz, 1.0);
            }";

            string fragmentShader = @"
            #version 330 core
            out vec4 color;
            in vec4 vertexColor;

            void main() 
            {
                color = vertexColor;
            }";

            int vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, vertexShader); // Give it the source
            GL.CompileShader(vertexShaderId); // Compile the shader
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if (vertexShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
            }

            int fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, fragmentShader); // Specify source
            GL.CompileShader(fragmentShaderId); // Compile the shader
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmentShaderCompilationCode);
            if (fragmentShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderId));
            }

            // Create actual shader and link them to pass information
            _shaderHandle = GL.CreateProgram();
            GL.AttachShader(_shaderHandle, vertexShaderId);
            GL.AttachShader(_shaderHandle, fragmentShaderId);
            GL.LinkProgram(_shaderHandle);

            // Cleanup
            GL.DetachShader(_shaderHandle, vertexShaderId);
            GL.DetachShader(_shaderHandle, fragmentShaderId);
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            _vertexBufferObject = GL.GenBuffer(); // Prep buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject); // Bind buffer
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw); // Give data

            _vertexArrayObject = GL.GenVertexArray(); // Gen vertex array
            GL.BindVertexArray(_vertexArrayObject); // Bind array object
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0); // Enable a vertex attribute pointer
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float)); // Grab aColor
            GL.EnableVertexAttribArray(1);
        }

        protected override void Update(GameTime gameTime)
        {
            
        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);

            // Do this before binding and drawing arrays
            GL.UseProgram(_shaderHandle);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3); 
        }

    }
}
