// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxLayerTile
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe


namespace TiledSharp
{
    public class TmxLayerTile
    {
        private const uint FLIPPED_HORIZONTALLY_FLAG = 2147483648;
        private const uint FLIPPED_VERTICALLY_FLAG = 1073741824;
        private const uint FLIPPED_DIAGONALLY_FLAG = 536870912;

        public int Gid { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public bool HorizontalFlip { get; private set; }

        public bool VerticalFlip { get; private set; }

        public bool DiagonalFlip { get; private set; }

        public TmxLayerTile(uint id, int x, int y)
        {
            uint num = id;
            this.X = x;
            this.Y = y;
            this.HorizontalFlip = (num & 2147483648U) > 0U;
            this.VerticalFlip = (num & 1073741824U) > 0U;
            this.DiagonalFlip = (num & 536870912U) > 0U;
            this.Gid = (int)(num & 536870911U);
        }
    }
}
