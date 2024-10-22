// Decompiled with JetBrains decompiler
// Type: HydroGene.Bullet
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace HydroGene
{
    internal class Bullet : Sprite
    {
        private const float GRAVITY = 0.6f;
        private const byte MAX_VELOCITY_Y = 250;
        private const byte MAX_FORCE_Y = 250;
        private const byte FULL_SCALE_SIZE = 140;
        private const byte DIMINUTION_SCALE_SPEED = 20;
        private readonly byte BASE_SCALE_X = 12;
        private readonly byte BASE_SCALE_Y = 12;
        public bool IsJustReleased;
        private bool IsFreezing;
        private Timer TimerFreeze = new Timer(0.12f);
        private Vector2 CaptureInstantVelocity;

        public bool CanUnleash { get; private set; }

        public int Force { get; private set; }

        public Bullet()
          : base(Primitive.CreatePixel())
        {
            this.Scale = new Vector2((float)this.BASE_SCALE_X, (float)this.BASE_SCALE_Y);
            this.Origin = new Vector2(0.5f, 0.5f);
            this.Color = Color.Yellow;
            this.TimerFreeze.OnComplete = (OnComplete)(() =>
            {
                this.Velocity = this.CaptureInstantVelocity;
                this.IsFreezing = false;
            });
        }

        public void Unleash(double angle, int force)
        {
            if (this.CanUnleash)
                return;
            this.CanUnleash = true;
            int num = force > 0 ? -1 : 1;
            this.Velocity.Y = (float)(num * force) * (float)Math.Sin(angle);
            this.Velocity.X = (float)(num * force) * (float)Math.Cos(angle);
            this.EffectTrail(1f / 1000f, 16);
            this.Force = force;
        }

        public override void Update(GameTime gameTime)
        {
            if ((double)this.Scale.X > (double)this.BASE_SCALE_X)
                this.Scale.X -= 20f;
            if ((double)this.Scale.Y > (double)this.BASE_SCALE_Y)
                this.Scale.Y -= 20f;
            if ((double)this.Scale.X <= (double)this.BASE_SCALE_X)
                this.Scale.X = (float)this.BASE_SCALE_X;
            if ((double)this.Scale.Y <= (double)this.BASE_SCALE_Y)
                this.Scale.Y = (float)this.BASE_SCALE_Y;
            if (this.IsFreezing)
            {
                this.Velocity = Vector2.Zero;
                this.TimerFreeze.Update(gameTime);
            }
            if (this.CanUnleash && !this.IsFreezing)
            {
                this.Velocity.Y += 0.6f;
                if ((double)this.Velocity.Y >= 250.0)
                    this.Velocity.Y = 250f;
                if ((double)this.Position.Y <= (double)Camera.VisibleArea.Height)
                {
                    if ((double)this.Position.Y < (double)Camera.Position.Y)
                    {
                        this.Scale.X = 140f;
                        Camera.Shake(8f, 0.12f, Axe.VERTICAL);
                        this.Velocity.Y = Math.Abs(this.Velocity.Y);
                        this.IsFreezing = true;
                        this.CaptureInstantVelocity = this.Velocity;
                        AssetManager.Sound_Touchwall.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
                    }
                    if ((double)this.Position.X < (double)Camera.Position.X)
                    {
                        this.Scale.Y = 140f;
                        Camera.Shake(8f, 0.12f, Axe.HORIZONTAL);
                        this.Velocity.X = Math.Abs(this.Velocity.X);
                        this.IsFreezing = true;
                        this.CaptureInstantVelocity = this.Velocity;
                        AssetManager.Sound_Touchwall.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
                    }
                    if ((double)this.Position.X > (double)Camera.VisibleArea.Width)
                    {
                        this.Scale.Y = 140f;
                        Camera.Shake(8f, 0.12f, Axe.HORIZONTAL);
                        this.Velocity.X = -Math.Abs(this.Velocity.X);
                        this.IsFreezing = true;
                        this.CaptureInstantVelocity = this.Velocity;
                        AssetManager.Sound_Touchwall.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
                    }
                }
                if ((double)this.Position.Y > (double)(2 * Camera.VisibleArea.Height) + (double)this.Scale.Y)
                {
                    this.StopEffectTrail();
                    this.ToRemove = true;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
    }
}
