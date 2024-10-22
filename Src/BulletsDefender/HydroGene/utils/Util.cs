// Decompiled with JetBrains decompiler
// Type: HydroGene.Util
// Assembly: Bullets Defender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2152C338-0479-438C-8FC2-A98509E908DF
// Assembly location: C:\Users\Admin\Desktop\RE\BulletsDefender\Bullets Defender.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace HydroGene
{
    internal class Util
    {
        private static Random RandomGenerator = new Random();

        public static void SetRandomSeed(int Seed) => Util.RandomGenerator = new Random(Seed);

        public static int RandomInt(int min, int max, int[] excludeNumbers = null)
        {
            if (min > max)
            {
                int num = min;
                min = max;
                max = num;
            }
            int num1 = Util.RandomGenerator.Next(min, max + 1);
            if (excludeNumbers != null)
            {
                for (int index = 0; index < excludeNumbers.Length; ++index)
                {
                    do
                    {
                        num1 = Util.RandomGenerator.Next(min, max + 1);
                    }
                    while (num1 == excludeNumbers[index]);
                }
            }
            return num1;
        }

        public static float RandomFloat(float min, float max, float[] excludeNumbers = null)
        {
            min *= 100f;
            max *= 100f;
            if ((double)min > (double)max)
            {
                double num = (double)min;
                min = max;
                max = (float)num;
            }
            float num1 = (float)Util.RandomGenerator.Next((int)min, (int)max) / 100f;
            if (excludeNumbers != null)
            {
                for (int index = 0; index < excludeNumbers.Length; ++index)
                {
                    do
                    {
                        num1 = (float)Util.RandomGenerator.Next((int)min, (int)max) / 100f;
                    }
                    while ((double)num1 == (double)excludeNumbers[index]);
                }
            }
            return num1;
        }

        public static double RandomDouble(double min, double max, double[] excludeNumbers = null)
        {
            min *= 100.0;
            max *= 100.0;
            if (min > max)
            {
                double num = min;
                min = max;
                max = num;
            }
            double num1 = (double)(Util.RandomGenerator.Next((int)min, (int)max) / 100);
            if (excludeNumbers != null)
            {
                for (int index = 0; index < excludeNumbers.Length; ++index)
                {
                    do
                    {
                        num1 = (double)(Util.RandomGenerator.Next((int)min, (int)max) / 100);
                    }
                    while (num1 == excludeNumbers[index]);
                }
            }
            return num1;
        }

        public static int RandomIntBetween2Numbers(int number1, int number2)
        {
            return Util.RandomInt(1, 2) == 1 ? number1 : number2;
        }

        public static bool CollideLeft(
          Sprite mainActor,
          Sprite actorToCheck,
          float leftOffsetPrecision = 1f)
        {
            return (double)mainActor.Position.X - (double)mainActor.Origin.X
                      - (double)leftOffsetPrecision >= (double)actorToCheck.Position.X
                      - (double)actorToCheck.Origin.X && (double)mainActor.Position.X
                      - (double)mainActor.Origin.X - (double)leftOffsetPrecision
                      <= (double)actorToCheck.Position.X - (double)actorToCheck.Origin.X
                      + (double)actorToCheck.Width && (double)mainActor.Position.Y
                      - (double)mainActor.Origin.Y
                      + (double)(mainActor.Height / 2) >= (double)actorToCheck.Position.Y
                      - (double)actorToCheck.Origin.Y && (double)mainActor.Position.Y
                      - (double)mainActor.Origin.Y +
                      (double)(mainActor.Height / 2) <= (double)actorToCheck.Position.Y
                      - (double)actorToCheck.Origin.Y + (double)actorToCheck.Height;
        }

        public static bool CollideRight(
          Sprite mainActor,
          Sprite actorToCheck,
          float rightOffsetPrecision = 1f)
        {
            return (double)mainActor.Position.X + (double)mainActor.Width
                      + (double)rightOffsetPrecision >= (double)actorToCheck.Position.X
                      && (double)mainActor.Position.X + (double)mainActor.Width
                      + (double)rightOffsetPrecision <= (double)actorToCheck.Position.X
                      + (double)actorToCheck.Width && (double)mainActor.Position.Y
                      + (double)(mainActor.Height / 2) >= (double)actorToCheck.Position.Y
                      && (double)mainActor.Position.Y + (double)(mainActor.Height / 2)
                      <= (double)actorToCheck.Position.Y + (double)actorToCheck.Height;
        }

        public static bool CollideAbove(Sprite mainActor, Sprite actorToCheck, float upOffsetPrecision = 1f)
        {
            return (double)mainActor.Position.X + (double)(mainActor.Width / 2)
                      >= (double)actorToCheck.Position.X && (double)mainActor.Position.X
                      + (double)(mainActor.Width / 2) <= (double)actorToCheck.Position.X
                      + (double)actorToCheck.Width && (double)mainActor.Position.Y
                      - (double)upOffsetPrecision >= (double)actorToCheck.Position.Y
                      && (double)mainActor.Position.Y -
                      (double)upOffsetPrecision <= (double)actorToCheck.Position.Y
                      + (double)actorToCheck.Height;
        }

        public bool CollideBelow(Sprite mainActor, Sprite actorToCheck, float downOffsetPrecision = 1f)
        {
            return (double)mainActor.Position.X + (double)(mainActor.Width / 2)
                      >= (double)actorToCheck.Position.X
                      && (double)mainActor.Position.X + (double)(mainActor.Width / 2)
                      <= (double)actorToCheck.Position.X + (double)actorToCheck.Width
                      && (double)mainActor.Position.Y + (double)mainActor.Height
                      + (double)downOffsetPrecision >= (double)actorToCheck.Position.Y
                      && (double)mainActor.Position.Y + (double)mainActor.Height
                      + (double)downOffsetPrecision <= (double)actorToCheck.Position.Y
                      + (double)actorToCheck.Height;
        }

        public static bool Overlaps(IActor actor1, IActor actor2)
        {
            Rectangle boundingBox = actor1.BoundingBox;
            return boundingBox.Intersects(actor2.BoundingBox);
        }

        public static bool Overlaps(IActor actor1, Rectangle box)
        {
            Rectangle boundingBox = actor1.BoundingBox;
            return boundingBox.Intersects(box);
        }

        public static bool Overlaps(Rectangle r1, Rectangle r2)
        {
            return r1.Intersects(r2);
        }

        public static double DistanceBetween(IActor actor1, IActor actor2)
        {
            return Math.Pow(Math.Pow((double)actor2.Position.X
                - (double)actor1.Position.X, 2.0)
                + Math.Pow((double)actor2.Position.Y - (double)actor1.Position.Y, 2.0), 0.5);
        }

        public static double DistanceBetween(Vector2 object1, Vector2 object2)
        {
            return Math.Pow(Math.Pow((double)object2.X
                - (double)object1.X, 2.0)
                + Math.Pow((double)object2.Y - (double)object1.Y, 2.0), 0.5);
        }

        public enum Alignement : byte
        {
            NONE,
            CENTER_X,
            CENTER_Y,
        }

        public enum Direction : byte
        {
            None,
            Left,
            Right,
            Top,
            Bottom,
        }
    }
}
