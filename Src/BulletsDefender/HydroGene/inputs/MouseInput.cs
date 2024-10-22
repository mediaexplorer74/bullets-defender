// Decompiled with JetBrains decompiler
// Type: HydroGene.MouseInput
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace HydroGene
{
    internal class MouseInput
    {
        public static MouseState newMouseState;
        public static MouseState oldMouseState;

        public static bool JustLeftClicked()
        {
            return MouseInput.newMouseState.LeftButton == ButtonState.Pressed
                      && MouseInput.oldMouseState.LeftButton != ButtonState.Pressed;
        }

        public static bool JustLeftReleased()
        {
            return /*MouseInput.newMouseState.LeftButton == null &&*/
                   MouseInput.oldMouseState.LeftButton > 0;
        }

        public static bool LeftClicked()
        {
            return MouseInput.newMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool JustRightClicked()
        {
            return MouseInput.newMouseState.RightButton == ButtonState.Pressed
                      && MouseInput.oldMouseState.RightButton != ButtonState.Pressed;
        }

        public static bool JustRightReleased()
        {
            return /*MouseInput.newMouseState.RightButton == null &&*/ 
                   MouseInput.oldMouseState.RightButton > 0;
        }

        public static bool RightClicked()
        {
            return MouseInput.newMouseState.RightButton == ButtonState.Pressed;
        }

        public static Vector2 GetPosition()
        {
            MouseState state = Mouse.GetState();
            double x = (double)state.X;
            state = Mouse.GetState();
            double y = (double)state.Y;
            return Vector2.Divide(new Vector2((float)x, (float)y), MainGame.Instance.Screen.Scale);
        }
    }
}
