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
            -0.5f, -0.5f, 0.0f, // bottom left vertex
             0.5f, -0.5f, 0.0f, // bottom right
             0.0f,  0.5f, 0.0f  // top
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;



        protected override void Initialize()
        {
            
        }

        protected override void LoadContent()
        {
            _vertexBufferObject = GL.GenBuffer(); // Prep buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject); // Bind buffer
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw); // Give data

            _vertexArrayObject = GL.GenVertexArray(); // Gen vertex array
            GL.BindVertexArray(_vertexArrayObject); // Bind array object
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0); // Enable a vertex attribute pointer
            GL.EnableVertexAttribArray(0);
        }

        protected override void Update(GameTime gameTime)
        {
            
        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3); 
        }

    }
}
