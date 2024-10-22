// Decompiled with JetBrains decompiler
// Type: HydroGene.Sound
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework.Audio;

#nullable disable
namespace HydroGene
{
    internal class Sound
    {
        public SoundEffect SoundEffect { get; private set; }

        public SoundEffectInstance Instance { get; set; }

        public Sound(SoundEffect pSF, float pVolume = 1f, float pPan = 0.0f)
        {
            this.SoundEffect = pSF;
            this.Instance = this.SoundEffect.CreateInstance();
            this.Instance.Volume = pVolume;
            this.Instance.Pan = pPan;
        }
    }
}
