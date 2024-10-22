// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxColor
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System.Globalization;
using System.Xml.Linq;


namespace TiledSharp
{
    public class TmxColor
    {
        public int R { get; private set; }

        public int G { get; private set; }

        public int B { get; private set; }

        public TmxColor(XAttribute xColor)
        {
            if (xColor == null)
                return;
            string str = ((string)xColor).TrimStart("#".ToCharArray());
            this.R = int.Parse(str.Substring(0, 2), NumberStyles.HexNumber);
            this.G = int.Parse(str.Substring(2, 2), NumberStyles.HexNumber);
            this.B = int.Parse(str.Substring(4, 2), NumberStyles.HexNumber);
        }
    }
}
