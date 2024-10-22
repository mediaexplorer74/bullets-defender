// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxAnimationFrame
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System.Xml.Linq;


namespace TiledSharp
{
    public class TmxAnimationFrame
    {
        public int Id { get; private set; }

        public int Duration { get; private set; }

        public TmxAnimationFrame(XElement xFrame)
        {
            this.Id = (int)xFrame.Attribute((XName)"tileid");
            this.Duration = (int)xFrame.Attribute((XName)"duration");
        }
    }
}
