﻿// Decompiled with JetBrains decompiler
// Type: HydroGene.IParticle
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

#nullable disable
namespace HydroGene
{
    internal interface IParticle
    {
        float Life { get; set; }

        bool ToRemove { get; set; }
    }
}