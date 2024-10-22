// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.InternalInflateConstants
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe


namespace Ionic.Zlib
{
    internal static class InternalInflateConstants
    {
        internal static readonly int[] InflateMask = new int[17]
        {
      0,
      1,
      3,
      7,
      15,
      31,
      63,
      (int) sbyte.MaxValue,
      (int) byte.MaxValue,
      511,
      1023,
      2047,
      4095,
      8191,
      16383,
      (int) short.MaxValue,
      (int) ushort.MaxValue
        };
    }
}
