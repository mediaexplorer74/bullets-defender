// Decompiled with JetBrains decompiler
// Type: HydroGene.Scene
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace HydroGene
{
    public abstract class Scene
    {
        protected Game1 mainGame;
        public List<IActor> listActors;

        public Scene()
        {
            this.mainGame = Game1.Instance;
            this.listActors = new List<IActor>();
        }

        public void Clean() => this.listActors.RemoveAll((Predicate<IActor>)(item => item.ToRemove));

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
            Camera.Unload();
            Tweening.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IActor listActor in this.listActors)
            {
                if (!Game1.IS_PAUSED && listActor.IsActive)
                    listActor.Update(gameTime);
            }
            Tweening.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (IActor listActor in this.listActors)
            {
                if (listActor.IsActive && listActor.IsVisible)
                    listActor.Draw(this.mainGame.spriteBatch);
            }
        }
    }
}
