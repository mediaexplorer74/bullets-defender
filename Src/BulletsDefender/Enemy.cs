// Decompiled with JetBrains decompiler
// Type: HydroGene.Enemy
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace HydroGene
{
    internal class Enemy : Sprite
    {
        private Text TextHP;

        public Enemy.EnemyType Type { get; private set; }

        public byte HP { get; set; }

        public byte HP_MAX { get; private set; }

        public Enemy(Enemy.EnemyType pType)
          : base(Primitive.CreatePixel())
        {
            this.Reset(pType);
            this.Scale = new Vector2(42f);
            switch (pType)
            {
                case Enemy.EnemyType.Big:
                    this.HP_MAX = (byte)10;
                    this.HP = (byte)10;
                    this.Scale = new Vector2(80f);
                    break;
                case Enemy.EnemyType.UltraBig:
                    this.HP_MAX = (byte)20;
                    this.HP = (byte)20;
                    this.Scale = new Vector2(100f);
                    break;
            }
            this.Width = (int)this.Scale.X;
            this.Height = (int)this.Scale.Y;
            this.Origin = new Vector2(0.5f, 0.5f);
            switch (this.Type)
            {
                case Enemy.EnemyType.VeryWeek:
                    this.Color = Color.DarkGreen;
                    this.Drag = new Vector2(Util.RandomFloat(0.2f, 0.7f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
                case Enemy.EnemyType.Week:
                    this.Color = Color.DarkOrange;
                    this.Drag = new Vector2(Util.RandomFloat(0.7f, 1.6f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
                case Enemy.EnemyType.Normal:
                    this.Color = Color.DarkBlue;
                    this.Drag = new Vector2(Util.RandomFloat(1.6f, 2.1f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
                case Enemy.EnemyType.Strong:
                    this.Color = Color.DarkViolet;
                    this.Drag = new Vector2(Util.RandomFloat(2.1f, 3f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
                case Enemy.EnemyType.VeryStrong:
                    this.Color = Color.DarkRed;
                    this.Drag = new Vector2(Util.RandomFloat(3f, 3.8f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
                case Enemy.EnemyType.Big:
                    this.Color = Color.DarkTurquoise;
                    this.Drag = new Vector2(Util.RandomFloat(0.4f, 0.6f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
                case Enemy.EnemyType.UltraBig:
                    this.Color = Color.DarkOrchid;
                    this.Drag = new Vector2(Util.RandomFloat(0.3f, 0.5f));
                    this.Velocity.Y = this.Drag.Y;
                    break;
            }
            if (Util.RandomIntBetween2Numbers(0, 1) == 0)
                this.Velocity.X = Util.RandomFloat(this.Drag.X - this.Drag.X / 4f, this.Drag.X + this.Drag.X / 4f);
            this.TextHP = new Text(AssetManager.FontFont28, this.HP.ToString(), Vector2.Zero, Color.White);
        }

        public void Reset() => this.HP = this.HP_MAX;

        public void Reset(Enemy.EnemyType newType)
        {
            this.Type = newType;
            this.HP_MAX = (byte)(this.Type + (byte)1);
            this.HP = this.HP_MAX;
        }

        public void ReceiveDamage()
        {
            --this.HP;
            this.Scale = new Vector2((float)(this.Width + 38), (float)(this.Height + 38));
        }

        public override void Update(GameTime gameTime)
        {
            switch (this.Type)
            {
                default:
                    if (this.HP > (byte)0)
                    {
                        this.TextHP.Position = new Vector2((float)((double)this.Position.X - (double)this.Origin.X * (double)this.Scale.X + ((double)this.Width - (double)this.TextHP.Width) / 2.0), (float)((double)this.Position.Y - (double)this.Origin.Y * (double)this.Scale.Y + ((double)this.Height - (double)this.TextHP.Height) / 2.0));
                        this.TextHP.CurrentString = this.HP.ToString();
                        this.TextHP.Update(gameTime);
                    }
                    else
                        this.HP = (byte)0;
                    if (this.HP <= (byte)0 || this.HP > (byte)200)
                        this.Alpha = 0.0f;
                    this.Angle += this.Velocity.Y;
                    if ((double)this.Scale.X > (double)this.Width)
                        this.Scale.X -= 0.85f;
                    if ((double)this.Scale.Y > (double)this.Height)
                        this.Scale.Y -= 0.85f;
                    if ((double)this.Scale.X <= (double)this.Width)
                        this.Scale.X = (float)this.Width;
                    if ((double)this.Scale.Y <= (double)this.Height)
                        this.Scale.Y = (float)this.Height;
                    if ((double)this.Velocity.Y < (double)this.Drag.Y)
                        ++this.Velocity.Y;
                    if ((double)this.Velocity.Y >= (double)this.Drag.Y)
                        this.Velocity.Y = this.Drag.Y;
                    if ((double)this.Velocity.Y < 0.0)
                        this.EffectTrail(nbMaxPosition: 14);
                    else
                        this.StopEffectTrail();
                    if ((double)this.Velocity.X != 0.0)
                    {
                        if ((double)this.Position.X - (double)this.Origin.X + (double)this.Width >= (double)Camera.VisibleArea.Width)
                            this.Velocity.X = -Math.Abs(this.Velocity.X);
                        else if ((double)this.Position.X - (double)this.Origin.X <= (double)Camera.Position.X)
                            this.Velocity.X = Math.Abs(this.Velocity.X);
                    }
                    if ((double)this.Position.Y > 1.2 * (double)Camera.VisibleArea.Height)
                        this.ToRemove = true;
                    base.Update(gameTime);
                    this.BoundingBox = new Rectangle((int)((double)this.Position.X - (double)this.Width * 0.5), (int)((double)this.Position.Y - (double)this.Height * 0.5), this.Width, this.Height);
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (this.HP <= (byte)0 || Game1.Instance.Screen.Effect != null)
                return;
            this.TextHP.Draw(spriteBatch);
        }

        public enum EnemyType : byte
        {
            VeryWeek,
            Week,
            Normal,
            Strong,
            VeryStrong,
            Big,
            UltraBig,
        }
    }
}
