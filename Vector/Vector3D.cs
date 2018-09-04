/*
 * Copyright © 2018 Lion Kortlepel
 */

using System;

namespace Lutil.Vectors
{
    // Internal functions for handling 3-Dimensional Vectors
    internal static class Vector3D
    {

        internal static double magD (Vector v)
        {
            return Math.Sqrt (v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }


        internal static float mag (Vector v)
        {
            return (float) Math.Sqrt (v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        internal static Vector norm (Vector v)
        {
            var f = new Vector (false);
            if (Math.Abs (mag (v)) < 0d)
            {
                return null;
            }
            var fact = 1 / mag (v);
            f.X = v.X * fact;
            f.Y = v.Y * fact;
            f.Z = v.Z * fact;
            return f;
        }

        internal static float dist (Vector v1, Vector v2)
        {
            return (float) Math.Sqrt ((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
        }

        internal static float dot (Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        internal static Vector cross (Vector v1, Vector v2)
        {
            return new Vector (v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }
    }
}
