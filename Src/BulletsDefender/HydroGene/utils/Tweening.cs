// Decompiled with JetBrains decompiler
// Type: HydroGene.Tweening
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace HydroGene
{
    internal static class Tweening
    {
        public static List<Tween> list_tweens = new List<Tween>();

        public static void Update(GameTime gameTime)
        {
            foreach (Tween listTween in Tweening.list_tweens)
                listTween.Update(gameTime);
        }

        public static void Unload()
        {
            foreach (Tween listTween in Tweening.list_tweens)
                listTween.ToRemove = true;
            Tweening.list_tweens.RemoveAll((Predicate<Tween>)(item => item.ToRemove = true));
        }
    }
}
