// Decompiled with JetBrains decompiler
// Type: HydroGene.PostProcessingEffect
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
{
    internal class PostProcessingEffect
    {
        protected GraphicsDevice graphicsDevice;
        protected SpriteBatch spriteBatch;

        public PostProcessingEffect(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
        }

        public virtual Texture2D Apply(Texture2D input, GameTime gameTime) => input;
    }
}
