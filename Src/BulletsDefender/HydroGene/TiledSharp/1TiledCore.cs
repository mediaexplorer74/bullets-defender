﻿// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxList`1
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using System.Collections.ObjectModel;
using System.Linq;


namespace TiledSharp
{
    public class TmxList<T> : KeyedCollection<string, T> where T : ITmxElement
    {
        private System.Collections.Generic.Dictionary<string, int> nameCount = new System.Collections.Generic.Dictionary<string, int>();

        public new void Add(T t)
        {
            string name = t.Name;
            if (this.Contains(name))
                ++this.nameCount[name];
            else
                this.nameCount.Add(name, 0);
            base.Add(t);
        }

        protected override string GetKeyForItem(T item)
        {
            string key = item.Name;
            int num = this.nameCount[key];
            int count = 0;
            while (this.Contains(key))
            {
                key = key + string.Concat(Enumerable.Repeat<string>("_", count)) + num.ToString();
                ++count;
            }
            return key;
        }
    }
}
