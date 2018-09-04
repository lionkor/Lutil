/*
 * Author: @Lion_Kor
 */

using System;

namespace Lutil.Vectors
{
    // Internal functions for handling 2-Dimensional Vectors
    internal static class Vector2D
    {

        internal static double magD (Vector v)
        {
            return Math.Sqrt (v.X * v.X + v.Y * v.Y);
        }


        internal static float mag (Vector v)
        {
            return (float) Math.Sqrt (v.X * v.X + v.Y * v.Y);
        }

        internal static Vector norm (Vector v)
        {
            var f = new Vector (true);
            if (v.X == 0 && v.Y == 0)
            {
                return null;
            }
            var fact = 1 / mag (v);
            f.X = v.X * fact;
            f.Y = v.Y * fact;
            return f;
        }

        internal static float dist (Vector v1, Vector v2)
        {
            return (float) Math.Sqrt ((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
        }

        internal static float dot (Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }
    }
}
