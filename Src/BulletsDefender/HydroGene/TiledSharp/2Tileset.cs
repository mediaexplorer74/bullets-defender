// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxTerrain
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
    public class TmxTerrain : ITmxElement
    {
        public string Name { get; private set; }

        public int Tile { get; private set; }

        public PropertyDict Properties { get; private set; }

        public TmxTerrain(XElement xTerrain)
        {
            this.Name = (string)xTerrain.Attribute((XName)"name");
            this.Tile = (int)xTerrain.Attribute((XName)"tile");
            this.Properties = new PropertyDict((XContainer)xTerrain.Element((XName)"properties"));
        }
    }
}
