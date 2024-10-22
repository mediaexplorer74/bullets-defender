// Decompiled with JetBrains decompiler
// Type: HydroGene.AssetManager
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

#nullable disable
namespace HydroGene
{
    internal class AssetManager
    {
        public static Effect EffectBlackandwhite { get; private set; }

        public static SpriteFont FontFont28 { get; private set; }

        public static Song Song_BulletDefenderInGame { get; private set; }

        public static Song Song_BulletDefenderMenu { get; private set; }

        public static Sound Sound_Destructblock { get; private set; }

        public static Sound Sound_Shoot { get; private set; }

        public static Sound Sound_Touchblock { get; private set; }

        public static Sound Sound_Touchprotector { get; private set; }

        public static Sound Sound_Touchwall { get; private set; }

        private static T Load<T>(string contentName)
        {
            return MainGame.Instance.Content.Load<T>(contentName);
        }

        public static void Load()
        {
            AssetManager.EffectBlackandwhite = default;//AssetManager.Load<Effect>("Effects/BlackAndWhite");
            AssetManager.FontFont28 = AssetManager.Load<SpriteFont>("Fonts/Font28");
            AssetManager.Song_BulletDefenderInGame = AssetManager.Load<Song>("Musics/Bullet Defender - In Game");
            AssetManager.Song_BulletDefenderMenu = AssetManager.Load<Song>("Musics/Bullet Defender - Menu");
            AssetManager.Sound_Destructblock = new Sound(AssetManager.Load<SoundEffect>("Sfx/DestructBlock"), MainGame.VOLUME_SFX);
            AssetManager.Sound_Shoot = new Sound(AssetManager.Load<SoundEffect>("Sfx/Shoot"), MainGame.VOLUME_SFX);
            AssetManager.Sound_Touchblock = new Sound(AssetManager.Load<SoundEffect>("Sfx/TouchBlock"), MainGame.VOLUME_SFX);
            AssetManager.Sound_Touchprotector = new Sound(AssetManager.Load<SoundEffect>("Sfx/TouchProtector"), MainGame.VOLUME_SFX);
            AssetManager.Sound_Touchwall = new Sound(AssetManager.Load<SoundEffect>("Sfx/TouchWall"), MainGame.VOLUME_SFX);
        }
    }
}
