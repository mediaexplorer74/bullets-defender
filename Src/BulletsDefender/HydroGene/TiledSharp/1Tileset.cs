// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxTileOffset
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System.Xml.Linq;


namespace TiledSharp
{
    public class TmxTileOffset
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public TmxTileOffset(XElement xTileOffset)
        {
            if (xTileOffset == null)
            {
                this.X = 0;
                this.Y = 0;
            }
            else
            {
                this.X = (int)xTileOffset.Attribute((XName)"x");
                this.Y = (int)xTileOffset.Attribute((XName)"y");
            }
        }
    }
}
