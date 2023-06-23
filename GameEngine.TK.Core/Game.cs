using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GameEngine.TK.Core
{
    public abstract class Game
    {
        protected string WindowTitle { get; set; }
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set;}

        private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
        private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;
        public Game(string windowTitle, int initialWindowWidth, int initialWindowHeight)
        {
            WindowTitle = windowTitle;
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            _nativeWindowSettings.Size = new Vector2i(InitialWindowWidth, InitialWindowHeight);
            _nativeWindowSettings.Title = windowTitle;
        }

        public void Run()
        {
            Initialize();
            using GameWindow gameWindow = new GameWindow(_gameWindowSettings, _nativeWindowSettings);
            GameTime gameTime = new();
            gameWindow.Load += LoadContent;
            gameWindow.UpdateFrame += (FrameEventArgs eventArgs) =>
                {
                    double time = eventArgs.Time;
                    gameTime.ElapsedGameTime = TimeSpan.FromMilliseconds(time);
                    gameTime.TotalGameTime += TimeSpan.FromMilliseconds(time);
                    Update(gameTime);
                };
            gameWindow.RenderFrame += (FrameEventArgs eventArgs) =>
            {
                Render(gameTime);
                gameWindow.SwapBuffers();
            };
            gameWindow.Run();
        }

        protected abstract void Initialize();

        protected abstract void LoadContent();

        protected abstract void Update(GameTime gameTime);
        protected abstract void Render(GameTime gameTime);
    }
}