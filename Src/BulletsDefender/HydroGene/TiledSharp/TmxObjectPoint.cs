// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxObjectPoint
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System;
using System.Globalization;

#nullable disable
namespace TiledSharp
{
    public class TmxObjectPoint
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public TmxObjectPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public TmxObjectPoint(string s)
        {
            string[] strArray = s.Split(',');
            this.X = double.Parse(strArray[0], NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture);
            this.Y = double.Parse(strArray[1], NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture);
        }
    }
}
