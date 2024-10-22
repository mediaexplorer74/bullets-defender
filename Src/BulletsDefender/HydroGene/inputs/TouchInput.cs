// HydroGene.TouchInput

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

#nullable disable
namespace HydroGene
{
    internal class TouchInput
    {
        public static TouchCollection newTouchState;
        public static TouchCollection oldTouchState;

        public static bool JustLeftClicked()
        {
            return TouchInput.newTouchState.Count > 0
                      && TouchInput.oldTouchState.Count == 0;
        }

        public static bool JustLeftReleased()
        {
            return TouchInput.newTouchState.Count == 0 &&
                   TouchInput.oldTouchState.Count > 0;
        }

        public static bool LeftClicked()
        {
            return TouchInput.newTouchState.Count == 1;
        }

        public static bool JustRightClicked()
        {
            return  TouchInput.newTouchState.Count == 2
                      && TouchInput.oldTouchState.Count == 0;
        }

        public static bool JustRightReleased()
        {
            return TouchInput.newTouchState.Count == 0 &&  
                   TouchInput.oldTouchState.Count > 0;
        }

        public static bool RightClicked()
        {
            return TouchInput.newTouchState.Count == 2;
        }

        public static Vector2 GetPosition()
        {
            TouchCollection state = TouchPanel.GetState();
            double x = (double)state[0].Position.X;
            double y = (double)state[0].Position.Y;
            return Vector2.Divide(new Vector2((float)x, (float)y), Game1.Instance.Screen.Scale);
        }
    }
}
