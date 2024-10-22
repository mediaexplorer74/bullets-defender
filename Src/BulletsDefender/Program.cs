// Decompiled with JetBrains decompiler
// Type: HydroGene.Program
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System;

#nullable disable
namespace HydroGene
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (MainGame mainGame = new MainGame())
                mainGame.Run();
        }
    }
}
