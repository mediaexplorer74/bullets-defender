// Decompiled with JetBrains decompiler
// Type: HydroGene.SceneMenu
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

#nullable disable
namespace HydroGene
{
    internal class SceneMenu : Scene
    {
        private const byte THICKNESS = 4;
        private const byte WIDTH_PARTS = 6;
        private const byte HEIGHT_PARTS = 160;
        private const byte NB_PARTICLES = 120;
        private Sprite SpritePlay;
        private Sprite SpriteExit;
        private Text TextTitle;
        private Text TextPlay;
        private Text TextExit;
        private Text TextBestScore;
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
        private List<Bullet> ListBullets = new List<Bullet>();
        private List<SplashParticle> ListSplashParticle = new List<SplashParticle>();

        public override void Load()
        {
            Camera.Flash(2f, Color.Black);
            MediaPlayer.Play(AssetManager.Song_BulletDefenderMenu);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = MainGame.VOLUME_MUSIC;

            this.TextTitle = new Text(AssetManager.FontFont28, "BULLETS DEFENDER",
                new Vector2(0.0f, 20f), Color.White);

            this.TextTitle.Align = Util.Alignement.CENTER_X;
            this.TextTitle.Scale = new Vector2(1.8f);
            this.listActors.Add((IActor)this.TextTitle);

            this.TextBestScore = new Text(AssetManager.FontFont28, "BEST SCORE: "
                + (object)MainGame.BEST_SCORE, new Vector2(0.0f, (float)Camera.VisibleArea.Height * 0.6f),
                Color.White);
            this.TextBestScore.Align = Util.Alignement.CENTER_X;
            this.TextBestScore.Scale = new Vector2(1.4f);
            this.listActors.Add((IActor)this.TextBestScore);
            this.SpritePlay = new Sprite(Primitive.CreatePixel());

            this.SpritePlay.Position = new Vector2(40f, Camera.Position.Y
                + (float)Camera.VisibleArea.Height * 0.3f);

            this.SpritePlay.Scale = new Vector2(120f, 40f);
            this.SpritePlay.Color = Color.Orange;
            this.listActors.Add((IActor)this.SpritePlay);
            this.SpriteExit = new Sprite(Primitive.CreatePixel());
            this.SpriteExit.Position = new Vector2((float)(40.0
                + (double)Camera.VisibleArea.Width * 0.57999998331069946),
                Camera.Position.Y + (float)Camera.VisibleArea.Height * 0.3f);
            this.SpriteExit.Scale = new Vector2(120f, 40f);
            this.SpriteExit.Color = Color.Orange;
            this.listActors.Add((IActor)this.SpriteExit);
            this.TextPlay = new Text(AssetManager.FontFont28, "PLAY", this.SpritePlay.Position, Color.White);

            this.TextPlay.Position = new Vector2(this.SpritePlay.Position.X
                + (float)(((double)this.TextPlay.Width - (double)this.SpritePlay.Width) / 2.0),
                this.SpritePlay.Position.Y + (float)(((double)this.TextPlay.Height / 4.0
                - (double)this.SpritePlay.Height) / 2.0));

            this.listActors.Add((IActor)this.TextPlay);

            this.TextExit = new Text(AssetManager.FontFont28, "EXIT", this.SpriteExit.Position,
                Color.White);
            this.TextExit.Position = new Vector2(this.SpriteExit.Position.X
                + (float)(((double)this.TextExit.Width - (double)this.SpriteExit.Width) / 2.0),
                this.SpriteExit.Position.Y + (float)(((double)this.TextExit.Height / 4.0
                - (double)this.SpriteExit.Height) / 2.0));
            this.listActors.Add((IActor)this.TextExit);

            this.LeftPart.Location = new Point((int)((double)Camera.Position.X
                + (double)Camera.VisibleArea.Width * 0.40000000596046448),
                (int)((double)Camera.Position.Y + (double)Camera.VisibleArea.Height - 160.0));
            this.RightPart.Location = new Point((int)((double)Camera.Position.X
                + (double)Camera.VisibleArea.Width * 0.60000002384185791),
                (int)((double)Camera.Position.Y + (double)Camera.VisibleArea.Height - 160.0));

            Camera.OnCompleteFade = (OnComplete)(() =>
            {
                if (!this.SpritePlay.IsActive)
                    this.mainGame.gameState.ChangeScene(GameState.SceneType.Game);
                else
                    this.mainGame.Exit();
            });
            base.Load();
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
                this.AngleBetween = Math.Atan2((double)MouseInput.GetPosition().Y - (double)this.MiddlePoint.Y, (double)MouseInput.GetPosition().X - (double)this.MiddlePoint.X);
            }
            this.StartPointLeftPart = new Vector2((float)(this.LeftPart.X - 3),
                (float)(this.LeftPart.Y + 1));

            this.StartPointRightPart = new Vector2((float)(this.RightPart.X - 3),
                (float)(this.RightPart.Y + 1));

            //RnD : 2f
            this.EndPointOppositeLeftPart = Vector2.Add(-(this.endPosition),
                Vector2.Multiply(new Vector2(2f, 2f), new Vector2((float)(this.LeftPart.X - 3),
                (float)(this.LeftPart.Y + 1))));

            //RnD : 2f
            this.EndPointOppositeRightPart = Vector2.Add(-(this.endPosition),
                Vector2.Multiply(new Vector2(2f, 2f), new Vector2((float)(this.RightPart.X - 3),
                (float)(this.RightPart.Y + 1))));

            if (MouseInput.JustLeftClicked())
            {
                this.ListBullets.Add(new Bullet());
                this.captureFirstClickPosition = MouseInput.GetPosition();
                AssetManager.Sound_Shoot.SoundEffect.Play(MainGame.VOLUME_SFX, 0.98f, 0.0f);
            }
            if (this.ListBullets.Count > 0)
            {
                foreach (Bullet listBullet in this.ListBullets)
                {
                    if (!listBullet.CanUnleash)
                        listBullet.Position = Vector2.Add(this.endPosition, new Vector2(listBullet.Scale.X / 4f,
                            listBullet.Scale.Y / 4f));

                    if (MouseInput.JustLeftReleased())
                    {
                        if (!listBullet.IsJustReleased)
                        {
                            listBullet.Position = this.lastEndPosition;
                            listBullet.IsJustReleased = true;
                        }
                        AssetManager.Sound_Shoot.SoundEffect.Play(MainGame.VOLUME_SFX * 0.2f, 0.0f, 0.0f);

                        int force = (int)((double)this.lastEndPosition.Y
                                        - (double)this.StartPointLeftPart.Y
                                        + ((double)this.lastEndPosition.Y
                                        - (double)this.StartPointRightPart.Y)) / 8;

                        listBullet.Unleash(this.AngleBetween, force);
                    }

                    if (this.SpritePlay.IsActive && Util.Overlaps((IActor)listBullet, (IActor)this.SpritePlay))
                    {
                        this.SpritePlay.IsActive = false;
                        this.TextPlay.IsActive = false;
                        this.GenerateSplashParticle((byte)120, listBullet.Position, this.SpritePlay.Color, true);
                        listBullet.ToRemove = true;
                        Camera.Shake(1f, 0.5f, Axe.ANGLE);
                        Camera.Fade(1f, Color.Black);
                        AssetManager.Sound_Touchblock.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
                    }
                    if (this.SpriteExit.IsActive && Util.Overlaps((IActor)listBullet, (IActor)this.SpriteExit))
                    {
                        this.SpriteExit.IsActive = false;
                        this.TextExit.IsActive = false;
                        this.GenerateSplashParticle((byte)120, listBullet.Position, this.SpriteExit.Color, true);
                        listBullet.ToRemove = true;
                        Camera.Fade(1f, Color.Black);
                        Camera.Shake(1f, 0.5f, Axe.ANGLE);
                        AssetManager.Sound_Touchblock.SoundEffect.Play(MainGame.VOLUME_SFX, 0.0f, 0.0f);
                    }
                    listBullet.Update(gameTime);
                }
            }
            this.ListBullets.RemoveAll((Predicate<Bullet>)(item => item.ToRemove));
            this.lastEndPosition = this.endPosition;
            if (this.ListSplashParticle.Count > 0)
            {
                foreach (Sprite sprite in this.ListSplashParticle)
                    sprite.Update(gameTime);
            }
            base.Update(gameTime);
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
            Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointLeftPart,
                this.endPosition, Color.White, 4);
            Primitive.DrawLine(this.mainGame.spriteBatch, this.StartPointRightPart,
                this.endPosition, Color.White, 4);

            if (this.ListSplashParticle.Count > 0)
            {
                foreach (Sprite sprite in this.ListSplashParticle)
                    sprite.Draw(this.mainGame.spriteBatch);
            }
            if (this.ListBullets.Count > 0)
            {
                foreach (Sprite listBullet in this.ListBullets)
                    listBullet.Draw(this.mainGame.spriteBatch);
            }
            base.Draw(gameTime);
        }
    }
}
