// Decompiled with JetBrains decompiler
// Type: HydroGene.KBInput
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework.Input;


namespace HydroGene
{
    public static class KBInput
    {
        public static KeyboardState oldKBState;
        public static KeyboardState newKBState;

        public static Keys? GetLastKeyJustPressed()
        {
            return KBInput.JustPressed((Keys)(int)KBInput.newKBState.GetPressedKeys()[0])
                      ? new Keys?(KBInput.newKBState.GetPressedKeys()[0]) : new Keys?();
        }

        public static bool JustPressed(Keys key)
        {
            return newKBState.IsKeyDown(key) && !KBInput.oldKBState.IsKeyDown(key);
        }

        public static bool JustReleased(Keys key)
        {
            return newKBState.IsKeyUp(key) && !KBInput.oldKBState.IsKeyUp(key);
        }

        public static bool Pressed(Keys key)
        {
            return KBInput.newKBState.IsKeyDown(key);
        }

        public static bool Released(Keys key)
        {
            return KBInput.newKBState.IsKeyUp(key);
        }
    }
}
