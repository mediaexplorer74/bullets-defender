// Decompiled with JetBrains decompiler
// Type: TiledSharp.PropertyDict
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System;
using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
namespace TiledSharp
{
    [Serializable]
    public class PropertyDict : Dictionary<string, string>
    {
        public PropertyDict(XContainer xmlProp)
        {
            if (xmlProp == null)
                return;
            foreach (XElement element in xmlProp.Elements((XName)"property"))
                this.Add(element.Attribute((XName)"name").Value, element.Attribute((XName)"value").Value);
        }
    }
}
