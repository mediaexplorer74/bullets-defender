﻿// Decompiled with JetBrains decompiler
// Type: HydroGene.InvertColors
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
{
    internal class InvertColors : PostProcessingEffect
    {
        private RenderTarget2D buffer;
        private BlendState blendState;

        public InvertColors(GraphicsDevice device, SpriteBatch spriteBatch)
          : base(device, spriteBatch)
        {
            this.blendState = new BlendState();
            this.blendState.ColorBlendFunction = (BlendFunction)2;
            this.blendState.AlphaBlendFunction = (BlendFunction)2;
            this.blendState.AlphaSourceBlend = this.blendState.ColorSourceBlend = (Blend)0;
            this.blendState.AlphaDestinationBlend = this.blendState.ColorDestinationBlend = (Blend)0;
            GraphicsDevice graphicsDevice = this.graphicsDevice;
            Viewport viewport = this.graphicsDevice.Viewport;
            int width = viewport.Width;
            viewport = this.graphicsDevice.Viewport;
            int height = viewport.Height;
            this.buffer = new RenderTarget2D(graphicsDevice, width, height);
        }

        public override Texture2D Apply(Texture2D input, GameTime gameTime)
        {
            this.graphicsDevice.SetRenderTarget(Game1.Instance.Screen.RenderTarget);
            this.graphicsDevice.Clear(Color.White);
            this.spriteBatch.Begin((SpriteSortMode)0, this.blendState, (SamplerState)null,
                (DepthStencilState)null, (RasterizerState)null, (Effect)null, new Matrix?());
            this.spriteBatch.Draw(input, Camera.Position, Color.White);
            this.spriteBatch.End();
            this.graphicsDevice.SetRenderTarget((RenderTarget2D)null);
            return (Texture2D)this.buffer;
        }
    }
}
