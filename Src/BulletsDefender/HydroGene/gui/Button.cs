// Decompiled with JetBrains decompiler
// Type: HydroGene.Button
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace HydroGene
{
    public class Button : Sprite
    {
        private MouseState oldMouseState;

        public bool isHover { get; private set; }

        public OnClick onClick { get; set; }

        public Button(Texture2D Texture)
          : base(Texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            Point position = state.Position;
            Rectangle boundingBox = this.BoundingBox;
            if (boundingBox.Contains(position))
            {
                if (!this.isHover)
                    this.isHover = true;
            }
            else
            {
                int num = this.isHover ? 1 : 0;
                this.isHover = false;
            }
            if (this.isHover && state.LeftButton == ButtonState.Pressed
                      /*&& this.oldMouseState.LeftButton == null*/
                      && this.onClick != null)
                this.onClick(this);
            this.oldMouseState = state;
            base.Update(gameTime);
        }
    }
}
