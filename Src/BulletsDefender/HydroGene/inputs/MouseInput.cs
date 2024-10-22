// HydroGene.MouseInput

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
            return MouseInput.newMouseState.LeftButton == ButtonState.Pressed && 
                   MouseInput.oldMouseState.LeftButton != ButtonState.Pressed;
        }

        public static bool JustLeftReleased()
        {
            return MouseInput.newMouseState.LeftButton == ButtonState.Released &&
                   MouseInput.oldMouseState.LeftButton > 0;
        }

        public static bool LeftClicked()
        {
            return MouseInput.newMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool JustRightClicked()
        {
            return MouseInput.newMouseState.RightButton == ButtonState.Pressed &&
                   MouseInput.oldMouseState.RightButton != ButtonState.Pressed;
        }

        public static bool JustRightReleased()
        {
            return MouseInput.newMouseState.RightButton == ButtonState.Released && 
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
            double y = (double)state.Y;
            return Vector2.Divide(new Vector2((float)x, (float)y), Game1.Instance.Screen.Scale);
        }
    }
}
