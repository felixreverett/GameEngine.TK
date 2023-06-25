using GameEngine.TK.Core;
using GameEngine.TK.Core.Management;
using GameEngine.TK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameEngine.TK
{
    internal class TextureTest : Game
    {
        public TextureTest(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
        {
        }

        private readonly float[] _vertices = {
            //Positions         //texCoords
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, //top right
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, //bottom left
            -0.5f,  0.5f, 0.0f, 1.0f, 0.0f  //top left
        };

        private uint[] _indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3  // second triangle
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _shader;
        private Texture2D _texture;

        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Texture.glsl"));
            _shader.CompileShader();

            _vertexBufferObject = GL.GenBuffer(); // Prep buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject); // Bind buffer
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw); // Give data

            _vertexArrayObject = GL.GenVertexArray(); // Gen vertex array
            GL.BindVertexArray(_vertexArrayObject); // Bind array object

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0); // Find vertices info. in _vertices (aPos)
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float)); // Find texCoord info. in _vertices (aTexCoord)
            GL.EnableVertexAttribArray(1);

            _elementBufferObject = GL.GenBuffer(); // prep EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject); // Bind buffer
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw); // Give data

            _texture = ResourceManager.Instance.LoadTexture("Resources/Textures/wall.jpg");
            _texture.Use(); // Only call once for 1 texture slot use
        }

        protected override void Update(GameTime gameTime)
        {

        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // Used for drawing array objects
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0); // Used for drawing Elements
        }

    }
}
