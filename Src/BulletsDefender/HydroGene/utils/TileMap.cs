// Decompiled with JetBrains decompiler
// Type: HydroGene.utils.TileMap
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TiledSharp;


namespace HydroGene.utils
{
    internal class TileMap : IActor
    {
        private Game1 mainGame;
        private TmxMap Map;
        private Texture2D Tileset;
        private bool HAS_SOLID_LAYER;
        private int SOLID_LAYER;
        public Flip Flip;
        private SpriteEffects flipEffect;

        public Rectangle BoundingBox { get; protected set; }

        public Vector2 Position { get; set; } = Vector2.Zero;

        public bool ToRemove { get; set; }

        public int MapWidthInLines { get; }

        public int MapHeightInColumn { get; }

        public int MapWidth { get; }

        public int MapHeight { get; }

        public int TilesetLines { get; }

        public int TilesetColumns { get; }

        public int TileWidth { get; }

        public int TileHeight { get; }

        public int NbLayer { get; }

        public int LayerEntity { get; set; }

        public bool IsVisible { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public List<int> SolidTiles { get; }

        public List<int> SlopesTiles_NorthEast { get; set; }

        public Vector2 Scale { get; set; } = Vector2.One;

        public float Angle { get; set; }

        public Vector2 Origin { get; set; } = Vector2.Zero;

        public TileMap(string mapPath, Texture2D pTileset)
        {
            this.mainGame = Game1.Instance;
            this.Flip = new Flip();
            this.flipEffect = (SpriteEffects)0;
            this.Map = new TmxMap(mapPath);
            this.Tileset = pTileset;
            this.TileWidth = this.Map.Tilesets[0].TileWidth;
            this.TileHeight = this.Map.Tilesets[0].TileHeight;
            this.MapWidthInLines = this.Map.Width;
            this.MapHeightInColumn = this.Map.Height;
            this.TilesetColumns = this.Tileset.Width / this.TileWidth;
            this.TilesetLines = this.Tileset.Height / this.TileHeight;
            this.MapWidth = this.MapWidthInLines * this.TileWidth;
            this.MapHeight = this.MapHeightInColumn * this.TileHeight;
            this.NbLayer = this.Map.Layers.Count;
            this.SolidTiles = new List<int>();
            this.SlopesTiles_NorthEast = new List<int>();
        }

        public bool IsOnScreen() => Util.Overlaps((IActor)this, Camera.VisibleArea);

        public virtual void TouchedBy(IActor By)
        {
        }

        public void SetSolidLayer(int layer)
        {
            this.HAS_SOLID_LAYER = true;
            this.SOLID_LAYER = layer;
        }

        public void SetSolidTiles(int[] arraySolidTiles)
        {
            foreach (int arraySolidTile in arraySolidTiles)
                this.SolidTiles.Add(arraySolidTile);
        }

        private bool isSolid(int pID)
        {
            foreach (int solidTile in this.SolidTiles)
            {
                if (solidTile == pID)
                    return true;
            }
            return false;
        }

        private bool isSolid(int pID, int layer)
        {
            switch (pID)
            {
                case -1:
                case 0:
                    return false;
                default:
                    return true;
            }
        }

        private bool isSlopesNorthEast(int pID, int layer)
        {
            int num1 = pID;
            foreach (int num2 in this.SlopesTiles_NorthEast)
            {
                if (num2 == num1)
                    return true;
            }
            return false;
        }

        public int GetTileAt(float pX, float pY, int layer = 0)
        {
            int num1 = (int)pX / this.TileWidth;
            int num2 = (int)pY / this.TileHeight;
            if (num1 < 0 || num2 < 0 || num1 >= this.MapWidthInLines || num2 >= this.MapHeightInColumn)
                return 0;
            int index = num1 + num2 * this.MapWidthInLines;
            int tileAt = this.Map.Layers[layer].Tiles[index].Gid - 1;
            switch (tileAt)
            {
                case -1:
                case 0:
                    return 0;
                default:
                    return tileAt;
            }
        }

        public int GetTileAt(Vector2 pos, int layer = 0)
        {
            int num1 = (int)pos.X / this.TileWidth;
            int num2 = (int)pos.Y / this.TileHeight;
            if (num1 < 0 || num2 < 0 || num1 >= this.MapWidth || num2 >= this.MapHeight)
                return 0;
            int index = num1 + num2 * this.MapWidthInLines;
            int tileAt = this.Map.Layers[layer].Tiles[index].Gid - 1;
            switch (tileAt)
            {
                case -1:
                case 0:
                    return 0;
                default:
                    return tileAt;
            }
        }

        public bool CollideRight(Sprite pSprite, bool useBoundingBox = false, float offsetX = 0.0f)
        {
            if (this.HAS_SOLID_LAYER)
            {
                if (!useBoundingBox)
                {
                    int tileAt1 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X + (double)pSprite.Width + 1.0) + offsetX, (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height * 0.5 - 2.0), this.SOLID_LAYER);
                    int tileAt2 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X + (double)pSprite.Width + 1.0) + offsetX, (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height * 0.5 + 2.0), this.SOLID_LAYER);
                    if (this.isSolid(tileAt1, this.SOLID_LAYER) || this.isSolid(tileAt2, this.SOLID_LAYER))
                        return true;
                }
                else
                {
                    int tileAt3 = this.GetTileAt((float)(pSprite.BoundingBox.X + pSprite.BoundingBox.Width + 1) + offsetX, (float)(pSprite.BoundingBox.Y + 2), this.SOLID_LAYER);
                    int tileAt4 = this.GetTileAt((float)(pSprite.BoundingBox.X + pSprite.BoundingBox.Width + 1) + offsetX, (float)(pSprite.BoundingBox.Y + pSprite.BoundingBox.Height - 2), this.SOLID_LAYER);
                    if (this.isSolid(tileAt3, this.SOLID_LAYER) || this.isSolid(tileAt4, this.SOLID_LAYER))
                        return true;
                }
            }
            else
            {
                int tileAt5 = this.GetTileAt((float)((double)pSprite.Position.X + (double)pSprite.Width + 2.0), pSprite.Position.Y + pSprite.Scale.Y);
                int tileAt6 = this.GetTileAt((float)((double)pSprite.Position.X + (double)pSprite.Width + 2.0), pSprite.Position.Y + (float)pSprite.Height - pSprite.Scale.Y);
                if (this.isSolid(tileAt5) || this.isSolid(tileAt6))
                    return true;
            }
            return false;
        }

        public bool CollideLeft(Sprite pSprite, bool useBoundingBox = false, float offsetX = 0.0f)
        {
            if (this.HAS_SOLID_LAYER)
            {
                if (!useBoundingBox)
                {
                    int tileAt1 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X - 1.0) + offsetX, (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height * 0.5 - 2.0), this.SOLID_LAYER);
                    int tileAt2 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X - 1.0) + offsetX, (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height * 0.5 - 2.0), this.SOLID_LAYER);
                    if (this.isSolid(tileAt1, this.SOLID_LAYER) || this.isSolid(tileAt2, this.SOLID_LAYER))
                        return true;
                }
                else
                {
                    int tileAt3 = this.GetTileAt((float)(pSprite.BoundingBox.X - 1) + offsetX, (float)(pSprite.BoundingBox.Y + 2), this.SOLID_LAYER);
                    int tileAt4 = this.GetTileAt((float)(pSprite.BoundingBox.X - 1) + offsetX, (float)(pSprite.BoundingBox.Y + pSprite.BoundingBox.Height - 2), this.SOLID_LAYER);
                    if (this.isSolid(tileAt3, this.SOLID_LAYER) || this.isSolid(tileAt4, this.SOLID_LAYER))
                        return true;
                }
            }
            else
            {
                int tileAt5 = this.GetTileAt(pSprite.Position.X - 1f, pSprite.Position.Y + pSprite.Scale.Y);
                int tileAt6 = this.GetTileAt(pSprite.Position.X - 1f, pSprite.Position.Y + (float)pSprite.Height - pSprite.Scale.Y);
                if (this.isSolid(tileAt5) || this.isSolid(tileAt6))
                    return true;
            }
            return false;
        }

        public bool CollideBelow(Sprite pSprite, bool useBoundingBox = false)
        {
            if (this.HAS_SOLID_LAYER)
            {
                if (useBoundingBox)
                {
                    int tileAt1 = this.GetTileAt((float)(pSprite.BoundingBox.X + 4), (float)(pSprite.BoundingBox.Y + pSprite.BoundingBox.Height), this.SOLID_LAYER);
                    int tileAt2 = this.GetTileAt((float)(pSprite.BoundingBox.X + pSprite.BoundingBox.Width - 4), (float)(pSprite.BoundingBox.Y + pSprite.BoundingBox.Height), this.SOLID_LAYER);
                    if (this.isSolid(tileAt1, this.SOLID_LAYER) || this.isSolid(tileAt2, this.SOLID_LAYER))
                        return true;
                }
                else
                {
                    int tileAt3 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X + 14.0), (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height * (double)pSprite.Scale.Y), this.SOLID_LAYER);
                    int tileAt4 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X + (double)pSprite.Width - 14.0), (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height * (double)pSprite.Scale.Y), this.SOLID_LAYER);
                    if (this.isSolid(tileAt3, this.SOLID_LAYER) || this.isSolid(tileAt4, this.SOLID_LAYER))
                        return true;
                }
            }
            else
            {
                int tileAt5 = this.GetTileAt(pSprite.Position.X + 2f, pSprite.Position.Y + (float)pSprite.Height);
                int tileAt6 = this.GetTileAt((float)((double)pSprite.Position.X + (double)pSprite.Width * (double)pSprite.Scale.X - 2.0), pSprite.Position.Y + (float)pSprite.Height * pSprite.Scale.Y);
                if (this.isSolid(tileAt5) || this.isSolid(tileAt6))
                    return true;
            }
            return false;
        }

        public bool CollideAbove(Sprite pSprite, bool useBoundingBox = false)
        {
            if (this.HAS_SOLID_LAYER)
            {
                int tileAt1;
                int tileAt2;
                if (useBoundingBox)
                {
                    tileAt1 = this.GetTileAt((float)(pSprite.BoundingBox.X + 2), (float)(pSprite.BoundingBox.Y - 2), this.SOLID_LAYER);
                    tileAt2 = this.GetTileAt((float)(pSprite.BoundingBox.X + pSprite.BoundingBox.Width - 2), (float)(pSprite.BoundingBox.Y - 2), this.SOLID_LAYER);
                }
                else
                {
                    tileAt1 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X + 2.0), (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y - 2.0), this.SOLID_LAYER);
                    tileAt2 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X + (double)pSprite.Width - 2.0), (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y - 2.0), this.SOLID_LAYER);
                }
                if (this.isSolid(tileAt1, this.SOLID_LAYER) || this.isSolid(tileAt2, this.SOLID_LAYER))
                    return true;
            }
            else
            {
                int tileAt3 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X - 2.0), (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + 2.0));
                int tileAt4 = this.GetTileAt((float)((double)pSprite.Position.X - (double)pSprite.Origin.X - 2.0), (float)((double)pSprite.Position.Y - (double)pSprite.Origin.Y + (double)pSprite.Height - 2.0));
                if (this.isSolid(tileAt3) || this.isSolid(tileAt4))
                    return true;
            }
            return false;
        }

        public void AlignOnLine(Sprite pSprite, float offsetY = 0.0f)
        {
            int num = (int)Math.Floor(((double)pSprite.Position.Y + (double)(this.TileHeight / 2)) / (double)this.TileHeight) + 1;
            pSprite.Position = new Vector2(pSprite.Position.X, (float)((num - 1) * this.TileHeight) + offsetY);
        }

        public void AlignOnColumn(Sprite pSprite, float offsetX = 0.0f)
        {
            int num = (int)Math.Floor(((double)pSprite.Position.X - (double)pSprite.Origin.X + (double)(this.TileHeight / 2)) / (double)this.TileHeight) + 1;
            pSprite.Position = new Vector2((float)((num - 1) * this.TileHeight) + offsetX, pSprite.Position.Y);
            pSprite.Velocity.X = 0.0f;
        }

        public bool CollideNorthEastSlopes(Sprite pSprite)
        {
            if (this.HAS_SOLID_LAYER)
            {
                int tileAt1 = this.GetTileAt((float)(pSprite.BoundingBox.X + 1), pSprite.Position.Y + (float)pSprite.Height, this.SOLID_LAYER);
                int tileAt2 = this.GetTileAt((float)(pSprite.BoundingBox.X + pSprite.BoundingBox.Width - 2), pSprite.Position.Y + (float)pSprite.Height, this.SOLID_LAYER);
                if (this.isSlopesNorthEast(tileAt1, this.SOLID_LAYER) || this.isSlopesNorthEast(tileAt2, this.SOLID_LAYER))
                {
                    int num1 = pSprite.BoundingBox.X + 1 / this.TileWidth;
                    Vector2 position1 = pSprite.Position;
                    int num2 = pSprite.Height / this.TileHeight;
                    Rectangle boundingBox1 = pSprite.BoundingBox;
                    Rectangle boundingBox2 = pSprite.BoundingBox;
                    int num3 = 2 / this.TileWidth;
                    Vector2 position2 = pSprite.Position;
                    int num4 = pSprite.Height / this.TileHeight;
                    Debug.WriteLine("MOD = " + (object)(num1 % 64));
                    return true;
                }
            }
            else
            {
                int tileAt3 = this.GetTileAt(pSprite.Position.X + 1f, pSprite.Position.Y + (float)pSprite.Height);
                int tileAt4 = this.GetTileAt((float)((double)pSprite.Position.X + (double)pSprite.Width - 2.0), pSprite.Position.Y + (float)pSprite.Height);
                if (this.isSolid(tileAt3) || this.isSolid(tileAt4))
                    return true;
            }
            return false;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public List<Vector2> CheckEntity(int pID, int pLayer = 0)
        {
            List<Vector2> vector2List = new List<Vector2>();
            if (pLayer >= this.Map.Layers.Count)
            {
                Debug.WriteLine("ERROR!! because : " + (object)pLayer + " is not existing on the " + (object)(this.Map.Layers.Count - 1) + " nb of Layers of this Tilemap!");
            }
            else
            {
                int num1 = 0;
                int num2 = 0;
                for (int index = 0; index < this.Map.Layers[pLayer].Tiles.Count; ++index)
                {
                    int gid = this.Map.Layers[pLayer].Tiles[index].Gid;
                    if (gid != 0)
                    {
                        int num3 = gid - 1;
                        if (num3 == pID)
                        {
                            int num4 = num3 % this.TilesetColumns;
                            Math.Floor((double)num3 / (double)this.TilesetColumns);
                            float num5 = (float)(num2 * this.TileWidth);
                            float num6 = (float)(num1 * this.TileHeight);
                            vector2List.Add(new Vector2(num5, num6));
                        }
                    }
                    ++num2;
                    if (num2 == this.MapWidthInLines)
                    {
                        num2 = 0;
                        ++num1;
                    }
                }
            }
            return vector2List;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.IsVisible)
                return;

            this.flipEffect = (SpriteEffects)0;

            if (this.Flip.X)
                this.flipEffect = (SpriteEffects)1;

            if (this.Flip.Y)
                this.flipEffect = (SpriteEffects)2;

            if (this.Flip.X && this.Flip.Y)
                this.flipEffect = (SpriteEffects)3;

            for (int index1 = 0; index1 < this.NbLayer - 1; ++index1)
            {
                int num1 = 0;
                int num2 = 0;
                for (int index2 = 0; index2 < this.Map.Layers[index1].Tiles.Count; ++index2)
                {
                    int gid = this.Map.Layers[index1].Tiles[index2].Gid;

                    if (gid != 0)
                    {
                        float num3 = (float)num2 * ((float)this.TileWidth * this.Scale.X);
                        float num4 = (float)num1 * ((float)this.TileHeight * this.Scale.Y);

                        if (Util.DistanceBetween(new Vector2(num3, num4), new Vector2(Camera.Position.X,
                            Camera.Position.Y)) <= (double)Camera.VisibleArea.Width * 1.5)
                        {
                            int num5 = gid - 1;
                            int num6 = num5 % this.TilesetColumns;
                            int num7 = (int)Math.Floor((double)num5 / (double)this.TilesetColumns);

                            Rectangle rectangle = new Rectangle(this.TileWidth * num6,
                                this.TileHeight * num7, this.TileWidth, this.TileHeight);

                            this.mainGame.spriteBatch.Draw(this.Tileset, new Vector2(num3, num4),
                                new Rectangle?(rectangle), Color.White, MathHelper.ToRadians(this.Angle),
                                this.Origin, this.Scale, this.flipEffect, 0.0f);
                        }
                    }
                    ++num2;
                    if (num2 == this.MapWidthInLines)
                    {
                        num2 = 0;
                        ++num1;
                    }
                }
            }
        }
    }
}
