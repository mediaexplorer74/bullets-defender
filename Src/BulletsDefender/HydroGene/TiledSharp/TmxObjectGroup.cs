// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxObjectGroup
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
    public class TmxObjectGroup : ITmxElement
    {
        public string Name { get; private set; }

        public TmxColor Color { get; private set; }

        public DrawOrderType DrawOrder { get; private set; }

        public double Opacity { get; private set; }

        public bool Visible { get; private set; }

        public double OffsetX { get; private set; }

        public double OffsetY { get; private set; }

        public TmxList<TmxObject> Objects { get; private set; }

        public PropertyDict Properties { get; private set; }

        public TmxObjectGroup(XElement xObjectGroup)
        {
            this.Name = (string)xObjectGroup.Attribute((XName)"name") ?? string.Empty;
            this.Color = new TmxColor(xObjectGroup.Attribute((XName)"color"));
            this.Opacity = (double?)xObjectGroup.Attribute((XName)"opacity") ?? 1.0;
            this.Visible = ((bool?)xObjectGroup.Attribute((XName)"visible") ?? true) != false;
            this.OffsetX = (double?)xObjectGroup.Attribute((XName)"offsetx") ?? 0.0;
            this.OffsetY = (double?)xObjectGroup.Attribute((XName)"offsety") ?? 0.0;
            Dictionary<string, DrawOrderType> dictionary = new Dictionary<string, DrawOrderType>()
      {
        {
          "unknown",
          DrawOrderType.UnknownOrder
        },
        {
          "topdown",
          DrawOrderType.IndexOrder
        },
        {
          "index",
          DrawOrderType.TopDown
        }
      };
            string key = (string)xObjectGroup.Attribute((XName)"draworder");
            if (key != null)
                this.DrawOrder = dictionary[key];
            this.Objects = new TmxList<TmxObject>();
            foreach (XElement element in xObjectGroup.Elements((XName)"object"))
                this.Objects.Add(new TmxObject(element));
            this.Properties = new PropertyDict((XContainer)xObjectGroup.Element((XName)"properties"));
        }
    }
}
