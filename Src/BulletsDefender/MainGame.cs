// Decompiled with JetBrains decompiler
// Type: HydroGene.MainGame
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace HydroGene
{
    public class MainGame : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GameState gameState;
        private RenderTarget2D render;
        public static int TargetWidth;
        public static int TargetHeight;
        public static string LANGUAGE = "EN";
        public static float VOLUME_MUSIC = 1f;
        public static float VOLUME_SFX = 0.7f;
        public static bool CAN_PAUSE = true;
        public static bool IS_PAUSED = false;
        public const int DEFAULT_WIDTH = 400;//500;
        public const int DEFAULT_HEIGHT = 640;//840;
        public float Scale = 1f;
        public static int WIDTH;
        public static int HEIGHT;
        public static string GAME_VERSION = "1.0.0";
        public static bool IS_DEBUG = false;
        public static int BEST_SCORE = 0;
        public Screen Screen;

        public static MainGame Instance { get; private set; }

        public MainGame()
        {
            this.Window.Title = "Bullets Defender " + MainGame.GAME_VERSION;
            this.graphics = new GraphicsDeviceManager((Game)this);
            this.graphics.GraphicsProfile = (GraphicsProfile)1;
            this.Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 400;//500;
            this.graphics.PreferredBackBufferHeight = 640;//840;
            MainGame.WIDTH = this.graphics.PreferredBackBufferWidth;
            MainGame.HEIGHT = this.graphics.PreferredBackBufferHeight;
            this.graphics.IsFullScreen = false;
            this.gameState = new GameState(this);
            MainGame.Instance = this;
        }

        protected override void Initialize()
        {
            PresentationParameters presentationParameters
                      = this.graphics.GraphicsDevice.PresentationParameters;
            MainGame.TargetWidth = 400;// 500;
            MainGame.TargetHeight = 640;//840;

            this.render = new RenderTarget2D(
                this.graphics.GraphicsDevice, MainGame.TargetWidth, MainGame.TargetHeight);
            this.Screen = new Screen(this, MainGame.TargetWidth, MainGame.TargetHeight, this.Scale);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            AssetManager.Load();
            this.Screen.Initialize();
            this.IsMouseVisible = true;
            GamePadInput.TimerVibration.OnComplete = (OnComplete)(() => GamePadInput.StopVibration());
            this.gameState.ChangeScene(GameState.SceneType.Menu);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            MainGame.WIDTH = this.graphics.PreferredBackBufferWidth;
            MainGame.HEIGHT = this.graphics.PreferredBackBufferHeight;
            GamePadInput.capabilities = GamePad.GetCapabilities((PlayerIndex)0);
            KBInput.newKBState = Keyboard.GetState();
            GamePadInput.newGPState = GamePad.GetState((PlayerIndex)0, (GamePadDeadZone)1);
            MouseInput.newMouseState = Mouse.GetState();
            if (KBInput.JustPressed((Keys)97))
                this.Screen.Scale -= 0.05f;
            else if (KBInput.JustPressed((Keys)99))
                this.Screen.Scale += 0.05f;
            if ((double)this.Screen.Scale > 1.0)
                this.Screen.Scale = 1f;
            else if ((double)this.Screen.Scale <= 0.40000000596046448)
                this.Screen.Scale = 0.4f;
            MainGame.WIDTH = this.Screen.Width;
            MainGame.HEIGHT = this.Screen.Height;
            if (!MainGame.CAN_PAUSE)
                MainGame.IS_PAUSED = false;
            if (MainGame.IS_PAUSED)
            {
                Camera.Angle = 0.0f;
                Camera.Zoom = 1f;
            }
            this.gameState.currentScene?.Update(gameTime);
            Camera.Update(gameTime);
            GamePadInput.oldGPState = GamePadInput.newGPState;
            KBInput.oldKBState = KBInput.newKBState;
            MouseInput.oldMouseState = MouseInput.newMouseState;
            if ((double)GamePadInput.TimerVibration.TotalTimer != 0.0)
                GamePadInput.TimerVibration.Update(gameTime);
            if (!this.IsActive)
                MainGame.IS_PAUSED = true;
            if (this.IsActive)
                MainGame.IS_PAUSED = false;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (((GraphicsResource)this.Screen.RenderTarget).IsDisposed)
                this.Screen.Initialize();
            this.GraphicsDevice.SetRenderTarget(this.Screen.RenderTarget);
            this.GraphicsDevice.Clear(this.Screen.ClearColor);
            this.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, (DepthStencilState)null, (RasterizerState)null, (Effect)null, new Matrix?(Camera.Transformation));
            this.gameState.currentScene?.Draw(gameTime);
            Camera.Draw();

            if (!this.IsActive)
                Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.spriteBatch,
                    Camera.Position.X, Camera.Position.Y, Camera.VisibleArea.Width + 2,
                    Camera.VisibleArea.Height + 2, Color.Multiply(Color.Black, 0.4f));

            this.spriteBatch.End();
            this.GraphicsDevice.SetRenderTarget((RenderTarget2D)null);
            this.GraphicsDevice.Clear(Color.Black);
            this.Screen.Render();
            this.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, new Matrix?());
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
