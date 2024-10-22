// Decompiled with JetBrains decompiler
// Type: HydroGene.SceneGame
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace HydroGene
{
    internal class SceneGame : Scene
    {
        private const byte THICKNESS = 4;
        private const byte WIDTH_PARTS = 6;
        private const byte HEIGHT_PARTS = 160;
        private const byte NB_PARTICLES = 12;
        private Rectangle LeftPart;
        private Rectangle RightPart;
        private Vector2 endPosition;
        private Vector2 lastEndPosition;
        private Vector2 captureFirstClickPosition;
        private Vector2 MiddlePoint;
        private Vector2 OppositePosition = Vector2.Zero;
        private Vector2 StartPointLeftPart = Vector2.Zero;
        private Vector2 StartPointRightPart = Vector2.Zero;
        private Vector2 EndPointOppositeLeftPart = Vector2.Zero;
        private Vector2 EndPointOppositeRightPart = Vector2.Zero;
        private double AngleBetween;
        private Text TextAngle;
        private Text TextScore;
        private Text TextIncrement;
        private int Score;
        private List<Bullet> ListBullets = new List<Bullet>();
        private List<Enemy> ListEnemies = new List<Enemy>();
        private List<SplashParticle> ListSplashParticle = new List<SplashParticle>();
        private Sprite[] Protectors;
        private Timer TimerGenerateEnemy1 = new Timer(1f);
        private Text TextGameOver;

        public override void Load()
        {
            Camera.Flash(1f, Color.Black);
            MediaPlayer.Stop();
            MediaPlayer.Play(AssetManager.Song_BulletDefenderInGame);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Game1.VOLUME_MUSIC;

            this.LeftPart.Location = new Point((int)((double)Camera.Position.X
                + (double)Camera.VisibleArea.Width * 0.40000000596046448),
                (int)((double)Camera.Position.Y + (double)Camera.VisibleArea.Height - 160.0));

            this.RightPart.Location = new Point((int)((double)Camera.Position.X
                + (double)Camera.VisibleArea.Width * 0.60000002384185791),
                (int)((double)Camera.Position.Y + (double)Camera.VisibleArea.Height - 160.0));
            this.TextAngle = new Text(AssetManager.FontFont28, "ANGLE BETWEEN : "
                + (object)this.AngleBetween, new Vector2(10f), Color.White);
            this.TextAngle.IsActive = false;
            this.listActors.Add((IActor)this.TextAngle);
            this.TextScore = new Text(AssetManager.FontFont28, "SCORE: "
                + (object)this.Score, new Vector2(80f, (float)(Camera.VisibleArea.Height - 40)), Color.White);

            this.TextScore.Origin = new Vector2(this.TextScore.Width / 2f, this.TextScore.Height / 2f);
            this.listActors.Add((IActor)this.TextScore);
            this.TextIncrement = new Text(AssetManager.FontFont28, "+0",
                new Vector2(100f, (float)(Camera.VisibleArea.Height - 40)), Color.White);
            this.TextIncrement.Alpha = 0.0f;
            this.listActors.Add((IActor)this.TextIncrement);
            this.TextGameOver = new Text(AssetManager.FontFont28, "GAME OVER\nYOUR SCORE: "
                + (object)(this.Score * 50) + "\nClick to Menu.", Vector2.Zero, Color.White);

            this.TextGameOver.Scale = new Vector2(1.1f);
            this.TextGameOver.Align = Util.Alignement.CENTER_X | Util.Alignement.CENTER_Y;
            this.TextGameOver.IsActive = false;
            this.listActors.Add((IActor)this.TextGameOver);
            this.TimerGenerateEnemy1.OnComplete = (OnComplete)(() =>
            {
                if (!Game1.Instance.IsActive)
                    return;
                byte max1 = 2;
                int num = this.Score * 50;
                if (num >= 2000 && num <= 3999)
                    max1 = (byte)3;
                else if (num >= 4000 && num <= 5999)
                    max1 = (byte)4;
                else if (num >= 6000)
                    max1 = (byte)5;
                Enemy enemy = new Enemy((Enemy.EnemyType)Util.RandomInt(0, (int)max1));
                enemy.Position = new Vector2(Camera.Position.X
              + (float)Util.RandomInt(enemy.Width, Camera.VisibleArea.Width - enemy.Width),
              (float)(-enemy.Height - 10));
                this.ListEnemies.Add(enemy);
                if (this.TimerGenerateEnemy1.Turn % 20 != 0)
                    return;
                byte max2 = 6;
                if (num >= 10000)
                    max2 = (byte)7;
                this.ListEnemies.Add(new Enemy((Enemy.EnemyType)Util.RandomInt(6, (int)max2))
                {
                    Position = new Vector2(Camera.Position.X
            + (float)Util.RandomInt(enemy.Width, Camera.VisibleArea.Width - enemy.Width),
            (float)(-enemy.Height - 10))
                });
            });
            Camera.OnCompleteFade = (OnComplete)(() =>
            {
                Game1.Instance.Screen.Effect = (Effect)null;
                if (this.Score * 50 > Game1.BEST_SCORE)
                    Game1.BEST_SCORE = this.Score * 50;
                this.mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
            });
            this.Protectors = new Sprite[8];
            for (int index = 0; index < this.Protectors.Length; ++index)
            {
                this.Protectors[index] = new Sprite(Primitive.CreatePixel());
                this.Protectors[index].Name = index.ToString();
                this.Protectors[index].Origin = new Vector2(0.5f, 0.5f);
                this.Protectors[index].Scale = new Vector2(32f);
                this.Protectors[index].Position = new Vector2((float)(60 * (index + 1) - 20),
                    Camera.Position.Y + (float)Camera.VisibleArea.Height * 0.7f);
                this.listActors.Add((IActor)this.Protectors[index]);
            }
            base.Load();
        }

        private void RefreshScore(byte amount, Vector2 posTextIncrement)
        {
            this.Score += (int)amount;
            this.TextIncrement.Position = posTextIncrement;
            this.TextIncrement.Alpha = 1f;
            this.TextIncrement.CurrentString = "+" + (object)((int)amount * 50);
            this.TextScore.CurrentString = "SCORE: " + (object)(this.Score * 50);
            this.TextScore.Scale = new Vector2(1.2f);
        }

        private void GenerateSplashParticle(byte amount, Vector2 pos, Color col, bool isBig = false)
        {
            for (int index = 0; index < (int)amount; ++index)
            {
                SplashParticle splashParticle = new SplashParticle(isBig);
                splashParticle.Position = pos;
                splashParticle.Color = col;
                this.ListSplashParticle.Add(splashParticle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.Instance.Screen.Effect == null)
            {
                if (Game1.Instance.IsActive)
                    this.TimerGenerateEnemy1.Update(gameTime);
                if ((double)this.TextIncrement.Alpha > 0.0)
                {
                    this.TextIncrement.Alpha -= 0.012f;
                    Text textIncrement = this.TextIncrement;
                    textIncrement.Position = Vector2.Subtract(textIncrement.Position, new Vector2(0.0f, 1.2f));
                }
                if ((double)this.TextScore.Scale.X > 1.0)
                {
                    Text textScore = this.TextScore;
                    textScore.Scale = Vector2.Subtract(textScore.Scale, new Vector2(0.02f));
                }
                if ((double)this.TextScore.Scale.X <= 1.0)
                    this.TextScore.Scale = Vector2.One;
                if (KBInput.JustPressed((Keys)112))
                    Game1.IS_DEBUG = !Game1.IS_DEBUG;
                this.TextAngle.IsActive = Game1.IS_DEBUG;
                this.endPosition = MouseInput.LeftClicked() ? MouseInput.GetPosition() : new Vector2(Camera.Position.X + (float)(Camera.VisibleArea.Width / 2), (float)(this.LeftPart.Y + 20));
                this.MiddlePoint = new Vector2((float)(((double)this.EndPointOppositeLeftPart.X + (double)this.EndPointOppositeRightPart.X) / 2.0), this.EndPointOppositeRightPart.Y);
                if (MouseInput.LeftClicked())
                {
                    if ((double)MouseInput.GetPosition().Y >= (double)(Camera.VisibleArea.Height - 4))
                        this.endPosition.Y = (float)(Camera.VisibleArea.Height - 4);
                    else if ((double)MouseInput.GetPosition().Y <= (double)Camera.VisibleArea.Height * 0.60000002384185791)
                        this.endPosition.Y = (float)Camera.VisibleArea.Height * 0.6f;
                    if ((double)MouseInput.GetPosition().X <= (double)Camera.Position.X + 4.0)
                        this.endPosition.X = Camera.Position.X + 4f;
                    else if ((double)MouseInput.GetPosition().X >= (double)(Camera.VisibleArea.Width - 4))
                        this.endPosition.X = (float)(Camera.VisibleArea.Width - 4);

                    this.AngleBetween = Math.Atan2((double)MouseInput.GetPosition().Y
                        - (double)this.MiddlePoint.Y, (double)MouseInput.GetPosition().X - (double)this.MiddlePoint.X);
                    this.TextAngle.CurrentString = "ANGLE BETWEEN : "
                                  + (object)MathHelper.ToDegrees((float)this.AngleBetween);
                }
                this.StartPointLeftPart = new Vector2((float)(this.LeftPart.X - 3),
                    (float)(this.LeftPart.Y + 1));
                this.StartPointRightPart = new Vector2((float)(this.RightPart.X - 3),
                    (float)(this.RightPart.Y + 1));

                //RnD : 2f
                this.EndPointOppositeLeftPart = Vector2.Add(-(this.endPosition),
                  Vector2.Multiply(new Vector2(2f, 2f),
                  new Vector2((float)(this.LeftPart.X - 3),
                  (float)(this.LeftPart.Y + 1))));

                //RnD : 2f
                this.EndPointOppositeRightPart = Vector2.Add(-(this.endPosition),
                    Vector2.Multiply(new Vector2(2f, 2f), new Vector2((float)(this.RightPart.X - 3),
                    (float)(this.RightPart.Y + 1))));
                if (MouseInput.JustLeftClicked())
                {
                    this.ListBullets.Add(new Bullet());
                    AssetManager.Sound_Shoot.SoundEffect.Play(Game1.VOLUME_SFX - 0.1f, 0.98f, 0.0f);
                    this.captureFirstClickPosition = MouseInput.GetPosition();
                }
                if (this.ListBullets.Count > 0)
                {
                    foreach (Bullet listBullet in this.ListBullets)
                    {
                        if (!listBullet.CanUnleash)
                            listBullet.Position = Vector2.Add(this.endPosition,
                                new Vector2(listBullet.Scale.X / 4f, listBullet.Scale.Y / 4f));
                        if (MouseInput.JustLeftReleased())
                        {
                            AssetManager.Sound_Shoot.Instance.Play();
                            if (!listBullet.IsJustReleased)
                            {
                                listBullet.Position = this.lastEndPosition;
                                listBullet.IsJustReleased = true;
                            }
                            int force = (int)((double)this.lastEndPosition.Y - (double)this.StartPointLeftPart.Y + ((double)this.lastEndPosition.Y - (double)this.StartPointRightPart.Y)) / 8;
                            listBullet.Unleash(this.AngleBetween, force);
                        }
                        listBullet.Update(gameTime);
                    }
                }
                foreach (Enemy listEnemy in this.ListEnemies)
                {
                    if (this.ListBullets.Count > 0)
                    {
                        foreach (Bullet listBullet in this.ListBullets)
                        {
                            if (listBullet.CanUnleash && (double)listEnemy.Alpha > 0.0 && Util.Overlaps((IActor)listBullet, (IActor)listEnemy))
                            {
                                listEnemy.ReceiveDamage();
                                if (listEnemy.Type < Enemy.EnemyType.Big)
                                    listEnemy.Velocity.Y = (float)-listBullet.Force / 2.5f;
                                this.GenerateSplashParticle((byte)6, listEnemy.Position, listBullet.Color, true);
                                this.GenerateSplashParticle((byte)6, listEnemy.Position, Color.White);
                                if (listEnemy.HP <= (byte)0)
                                {
                                    this.RefreshScore(listEnemy.HP_MAX, listEnemy.Position);
                                    this.GenerateSplashParticle((byte)12, listEnemy.Position, listEnemy.Color, true);
                                    listEnemy.ToRemove = true;
                                    AssetManager.Sound_Destructblock.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
                                }
                                else
                                    AssetManager.Sound_Touchblock.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
                                listBullet.ToRemove = true;
                            }
                        }
                    }
                    foreach (Sprite protector in this.Protectors)
                    {
                        if ((double)protector.Alpha > 0.0 && Util.Overlaps((IActor)protector, (IActor)listEnemy))
                        {
                            this.GenerateSplashParticle((byte)24, protector.Position, Color.White, true);
                            listEnemy.ToRemove = true;
                            protector.Alpha = 0.0f;
                            Camera.Shake(1f, 0.14f, Axe.ANGLE);
                            AssetManager.Sound_Touchprotector.SoundEffect.Play(Game1.VOLUME_SFX, 0.0f, 0.0f);
                        }
                    }
                    listEnemy.Update(gameTime);
                }
                if (this.ListSplashParticle.Count > 0)
                {
                    for (int index = 0; index < this.ListSplashParticle.Count; ++index)
                    {
                        this.ListSplashParticle[index].Update(gameTime);
                        if (this.ListSplashParticle[index].ToRemove)
                            this.ListSplashParticle[index] = (SplashParticle)null;
                    }
                }
                this.ListSplashParticle.RemoveAll((Predicate<SplashParticle>)(item => item == null || item.ToRemove));
                this.ListBullets.RemoveAll((Predicate<Bullet>)(item => item.ToRemove));
                this.ListEnemies.RemoveAll((Predicate<Enemy>)(item => item.ToRemove));
                this.lastEndPosition = this.endPosition;
            }
            else
            {
                this.TextGameOver.IsActive = true;
                this.TextGameOver.CurrentString = "       GAME OVER\n      YOUR SCORE: " + (object)(this.Score * 50) + "\n\n\nClic to return to the menu.";
                if (MouseInput.JustLeftClicked())
                    Camera.Fade(1f, Color.Black);
            }
            this.Clean();
            base.Update(gameTime);
            foreach (Sprite protector in this.Protectors)
            {
                int num = (int)byte.Parse(protector.Name) % 2 == 0 ? -2 : 2;
                protector.Angle += (float)num;
                if ((double)protector.Velocity.X == 0.0 && ((IEnumerable<Sprite>)this.Protectors).Count<Sprite>((Func<Sprite, bool>)(item => (double)item.Alpha > 0.0)) == 1)
                    protector.Velocity.X = 3f;
                if ((double)protector.Position.X - (double)protector.Origin.X + (double)protector.Scale.X >= (double)Camera.VisibleArea.Width)
                    protector.Velocity.X = -Math.Abs(protector.Velocity.X);
                if ((double)protector.Position.X - (double)protector.Origin.X <= (double)Camera.Position.X)
                    protector.Velocity.X = Math.Abs(protector.Velocity.X);
                protector.BoundingBox = new Rectangle((int)((double)protector.Position.X - (double)protector.Origin.X * (double)protector.Scale.X), (int)((double)protector.Position.Y - (double)protector.Origin.Y * (double)protector.Scale.Y), (int)protector.Scale.X, (int)protector.Scale.Y);
            }
            if (Game1.Instance.Screen.Effect != null || ((IEnumerable<Sprite>)this.Protectors).Count<Sprite>((Func<Sprite, bool>)(item => (double)item.Alpha > 0.0)) > 0)
                return;
            Camera.Flash(0.5f, Color.White);
            Camera.Shake(16f, 0.5f);
            Game1.Instance.Screen.Effect = AssetManager.EffectBlackandwhite;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch1 = this.mainGame.spriteBatch;
            Point location1 = this.LeftPart.Location;
            Vector2 vector2_1 = location1.ToVector2();
            Vector2 endPoint1 = new Vector2((float)this.LeftPart.X, (float)(this.LeftPart.Y + 160));
            Color blue1 = Color.Blue;
            Primitive.DrawLine(spriteBatch1, vector2_1, endPoint1, blue1, 6);
            SpriteBatch spriteBatch2 = this.mainGame.spriteBatch;
            Point location2 = this.RightPart.Location;
            Vector2 vector2_2 = location2.ToVector2();
            Vector2 endPoint2 = new Vector2((float)this.RightPart.X, (float)(this.RightPart.Y + 160));
            Color blue2 = Color.Blue;
            Primitive.DrawLine(spriteBatch2, vector2_2, endPoint2, blue2, 6);
            Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointLeftPart, this.endPosition, Color.White, 4);
            Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointRightPart, this.endPosition, Color.White, 4);
            if (Game1.IS_DEBUG)
            {
                Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.mainGame.spriteBatch,
                    this.captureFirstClickPosition, new Vector2(12f), Color.Red);

                Primitive.DrawRectangle(Primitive.PrimitiveStyle.FILL, this.mainGame.spriteBatch,
                    this.MiddlePoint, new Vector2(12f), Color.Green);

                Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointLeftPart,
                    this.EndPointOppositeLeftPart, Color.Green, 4);

                Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointRightPart,
                    this.EndPointOppositeRightPart, Color.Green, 4);

                Primitive.DrawLine(this.mainGame.spriteBatch, this.EndPointOppositeLeftPart,
                    this.MiddlePoint, Color.Multiply(Color.Magenta, 0.2f), 4);

                Primitive.DrawLine(this.mainGame.spriteBatch, this.EndPointOppositeRightPart,
                    this.MiddlePoint, Color.Multiply(Color.Magenta, 0.2f), 4);

                Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointLeftPart,
                    this.MiddlePoint, Color.DarkMagenta, 4);
                Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointRightPart,
                    this.MiddlePoint, Color.DarkMagenta, 4);
            }
            if (this.ListBullets.Count > 0)
            {
                foreach (Sprite listBullet in this.ListBullets)
                    listBullet.Draw(this.mainGame.spriteBatch);
            }
            if (this.ListEnemies.Count > 0)
            {
                foreach (Sprite listEnemy in this.ListEnemies)
                    listEnemy.Draw(this.mainGame.spriteBatch);
            }
            if (this.ListSplashParticle.Count > 0)
            {
                foreach (Sprite sprite in this.ListSplashParticle)
                    sprite.Draw(this.mainGame.spriteBatch);
            }
            Primitive.DrawLine(this.mainGame.spriteBatch,
                new Vector2(Camera.Position.X + 8f, Camera.Position.Y),
                new Vector2(Camera.Position.X + 8f, (float)Camera.VisibleArea.Height),
                Color.DarkOliveGreen, 8);

            Primitive.DrawLine(this.mainGame.spriteBatch,
                new Vector2(Camera.Position.X, Camera.Position.Y),
                new Vector2(Camera.Position.X + (float)Camera.VisibleArea.Width, Camera.Position.Y),
                Color.DarkOliveGreen, 8);

            Primitive.DrawLine(this.mainGame.spriteBatch,
                new Vector2(Camera.Position.X + (float)Camera.VisibleArea.Width, Camera.Position.Y),
                new Vector2(Camera.Position.X + (float)Camera.VisibleArea.Width,
                Camera.Position.Y + (float)Camera.VisibleArea.Height), Color.DarkOliveGreen, 8);

            base.Draw(gameTime);
        }
    }
}
