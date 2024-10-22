// Decompiled with JetBrains decompiler
// Type: HydroGene.SplashParticle
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace HydroGene
{
    internal class SplashParticle : Sprite
    {
        private readonly float GRAVITY;

        public SplashParticle(bool useGravity = false)
          : base(Primitive.CreatePixel())
        {
            this.Name = "Splash";
            this.Scale = new Vector2(Util.RandomFloat(0.5f, 2.6f));
            this.Velocity = new Vector2((float)Util.RandomInt(-6, 6, new int[1]),
                (float)Util.RandomInt(-6, 6, new int[1]));
            if (!useGravity)
                return;
            this.Scale = Vector2.Multiply(this.Scale, Util.RandomFloat(2f, 4.2f));
            this.GRAVITY = 0.7f;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.ToRemove)
                return;
            this.Alpha -= Math.Abs(this.Velocity.X / 40f);
            this.Velocity.Y += this.GRAVITY;
            if ((double)this.Alpha <= 0.0)
                this.ToRemove = true;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.ToRemove)
                return;
            base.Draw(spriteBatch);
        }
    }
}
