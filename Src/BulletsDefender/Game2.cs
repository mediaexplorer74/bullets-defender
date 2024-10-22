// Decompiled with JetBrains decompiler
// Type: Bullets_Defender.Game1
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace Bullets_Defender
{
    public class Game2 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game2()
        {
            this.graphics = new GraphicsDeviceManager((Game)this);
            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            GamePadState state1 = GamePad.GetState((PlayerIndex)0);

            GamePadButtons buttons = state1.Buttons;
            if (buttons.Back != ButtonState.Pressed)
            {
                KeyboardState state2 = Keyboard.GetState();
                if (!state2.IsKeyDown(Keys.Escape))
                    goto label_3;
            }
            this.Exit();
        label_3:
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
