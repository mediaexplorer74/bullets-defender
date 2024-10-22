﻿// Decompiled with JetBrains decompiler
// Type: Ionic.Zlib.WorkItem
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

#nullable disable
namespace Ionic.Zlib
{
    internal class WorkItem
    {
        public byte[] buffer;
        public byte[] compressed;
        public int crc;
        public int index;
        public int ordinal;
        public int inputBytesAvailable;
        public int compressedBytesAvailable;
        public ZlibCodec compressor;

        public WorkItem(
          int size,
          CompressionLevel compressLevel,
          CompressionStrategy strategy,
          int ix)
        {
            this.buffer = new byte[size];
            this.compressed = new byte[size + (size / 32768 + 1) * 5 * 2];
            this.compressor = new ZlibCodec();
            this.compressor.InitializeDeflate(compressLevel, false);
            this.compressor.OutputBuffer = this.compressed;
            this.compressor.InputBuffer = this.buffer;
            this.index = ix;
        }
    }
}
