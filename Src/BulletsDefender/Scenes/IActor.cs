// Decompiled with JetBrains decompiler
// Type: HydroGene.IActor
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace HydroGene
{
    public interface IActor
    {
        Vector2 Position { get; }

        Rectangle BoundingBox { get; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        void TouchedBy(IActor By);

        bool ToRemove { get; set; }

        bool IsOnScreen();

        bool IsActive { get; set; }

        bool IsVisible { get; set; }
    }
}
