// Decompiled with JetBrains decompiler
// Type: HydroGene.RNG
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System;

#nullable disable
namespace HydroGene
{
    public static class RNG
    {
        private static Random rng;

        public static void Init(int pSeed = 0)
        {
            if (pSeed == 0)
                RNG.rng = new Random();
            else
                RNG.rng = new Random(pSeed);
        }

        public static void SetSeed(int pSeed) => RNG.rng = new Random(pSeed);

        public static int GetInt(int min, int max) => RNG.rng.Next(min, max + 1);

        public static float GetFloat(float range) => (float)RNG.rng.NextDouble() * range;

        public static float GetFloat(float min, float max)
        {
            return (float)RNG.rng.NextDouble() * (max - min) + min;
        }
    }
}
