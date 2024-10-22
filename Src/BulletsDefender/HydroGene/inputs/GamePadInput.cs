// Decompiled with JetBrains decompiler
// Type: HydroGene.GamePadInput
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace HydroGene
{
    internal static class GamePadInput
    {
        public static GamePadCapabilities capabilities;
        public static GamePadState newGPState;
        public static GamePadState oldGPState;
        public static Vector2 oldLeftThumbStick = Vector2.Zero;
        public static Vector2 oldRightThumbStick = Vector2.Zero;
        public static Timer TimerVibration = new Timer(0.0f);

        public static Vector2 LeftThumbstickValue()
        {
            GamePadThumbSticks thumbSticks = GamePadInput.newGPState.ThumbSticks;
            return thumbSticks.Left;
        }

        public static Vector2 RightThumbstickValue()
        {
            GamePadThumbSticks thumbSticks = GamePadInput.newGPState.ThumbSticks;
            return thumbSticks.Right;
        }

        public static bool JustPressed(Buttons button)
        {
            return GamePadInput.newGPState.IsButtonDown(button)
                      && !GamePadInput.oldGPState.IsButtonDown(button);
        }

        public static bool JustReleased(Buttons button)
        {
            return !GamePadInput.newGPState.IsButtonDown(button)
                      && GamePadInput.oldGPState.IsButtonDown(button);
        }

        public static bool Pressed(Buttons button)
        {
            return GamePadInput.newGPState.IsButtonDown(button);
        }

        public static bool Released(Buttons button)
        {
            return GamePadInput.newGPState.IsButtonUp(button);
        }

        public static void Vibrate(float intensityLeft, float intensityRight, PlayerIndex player = 0)
        {
            if (!GamePadInput.capabilities.IsConnected)
                return;
            GamePad.SetVibration(player, intensityLeft, intensityRight);
        }

        public static void Vibrate
        (
          float intensityLeft,
          float intensityRight,
          float timeVibration,
          PlayerIndex player = 0
        )
        {
            if (!GamePadInput.capabilities.IsConnected)
                return;
            GamePadInput.TimerVibration.ChangeTimerValue(timeVibration);
            GamePad.SetVibration(player, intensityLeft, intensityRight);
        }

        public static void StopVibration(PlayerIndex player = 0)
        {
            if (!GamePadInput.capabilities.IsConnected)
                return;
            GamePad.SetVibration(player, 0.0f, 0.0f);
        }
    }
}
