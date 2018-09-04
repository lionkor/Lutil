/*
 * Author: @Lion_Kor
 */

using System;
namespace Lutil.Vectors
{
    /// <summary>A Vector is a quantity having direction as well as magnitude, especially as determining the position of one point in space relative to another.	This can have either 2 or 3 dimensions.</summary>
    public partial class Vector
    {
        #region Variables
        private bool twoD = false;
        private bool threeD = false;
        private float[] comps;
        private string uvces
        {
            get
            {
                return "2D Vectors only have 'x' and 'y' components, 'z' is undefined. Use a 3D contructor or the array based constructor (with Array.length == 3) instead.";
            }
        }

        /// <summary>
        /// Dimensions of the Vector, either 2 or 3.
        /// </summary>
        public int Dim
        {
            get
            {
                if (Is2D)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }
        /// <summary>
        /// <see langword="true"/> if Vector is 2-Dimensional.
        /// </summary>
        public bool Is2D
        {
            get
            {
                return twoD;
            }
            internal set
            {
                if (Is2D == value)
                {
                    return;
                }
                else
                {
                    Is2D = value;
                }
            }
        }
        /// <summary>
        /// <see langword="true"/> if Vector is 3-Dimensional.
        /// </summary>
        public bool Is3D
        {
            get
            {
                return threeD;
            }
            internal set
            {
                if (Is3D == value)
                {
                    return;
                }
                else
                {
                    Is3D = value;
                }
            }
        }
        /// <summary>
        /// X - component of the Vector.
        /// </summary>
        public float X
        {
            get
            {
                return comps[0];
            }
            set
            {
                comps[0] = value;
            }
        }
        /// <summary>
        /// Y - component of the Vector.
        /// </summary>
        public float Y
        {
            get
            {
                return comps[1];
            }
            set
            {
                comps[1] = value;
            }
        }
        /// <summary>
        /// Z - component of the Vector. Exclusive to 3D-Vectors.
        /// </summary>
        public float Z
        {
            get
            {
                if (Is3D)
                {
                    return comps[2];
                }
                else
                {
                    throw new UndefinedVectorComponentException (uvces);
                }
            }
            set
            {
                if (Is3D)
                {
                    comps[2] = value;
                }
                else
                {
                    throw new UndefinedVectorComponentException (uvces);
                }
            }
        }
        /// <summary>
        /// float[] with length 2 or 3 that contains the components of the Vector.
        /// If Vector is 2D this contains { X, Y }, 
        /// if Vector is 3D this contains { X, Y, Z };
        /// </summary>
        public float[] Components
        {
            get
            {
                return comps;
            }
            set
            {
                if (value.Length == Dim)
                {
                    X = value[0];
                    Y = value[1];
                    if (Is3D)
                    {
                        Z = value[2];
                    }
                }
                else
                {
                    throw new WrongVectorDimensionException ();
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a 2-Dimensional (2D) Vector.
        /// </summary>
        /// <param name="x">X-component (first dimension)</param>
        /// <param name="y">Y-component (second dimension)</param>
        public Vector (float x, float y)
        {
            comps = new float[2] { x, y };
            twoD = true;
        }

        /// <summary>
        /// Constructs a 3-Dimensional (3D) Vector.
        /// </summary>
        /// <param name="x">X-component (first dimension)</param>
        /// <param name="y">Y-component (second dimension)</param>
        /// <param name="z">Z-component (third dimension)</param>
        public Vector (float x, float y, float z)
        {
            comps = new float[3] { x, y, z };
            threeD = true;
        }

        /// <summary>
        /// Constructs a 2- or 3-Dimensional (2D / 3D) Vector.
        /// </summary>
        /// <param name="coordinates">Length of the array decides if 2D or 3D. Only use Arrays with length = 2 or = 3.</param>
        public Vector (float[] coordinates)
        {
            if (coordinates.Length == 2)
            {
                twoD = true;
                comps = coordinates;
            }
            else if (coordinates.Length == 3)
            {
                threeD = true;
                comps = coordinates;
            }
            else
            {
                throw new WrongVectorDimensionException ();
            }

        }

        /// <summary>
        /// Constructs a Vector with all components set to 1 (one). Dimension needs to be specified.
        /// </summary>
        /// <param name="isVector2D">'true' construts empty 2D Vector; 'false' constructs empty 3D Vector.</param>
        public Vector (bool isVector2D)
        {
            if (isVector2D)
            {
                twoD = true;
                comps = new float[2] { 1, 1 };
            }
            else
            {
                threeD = true;
                comps = new float[3] { 1, 1, 1 };
            }
        }

        /// <summary>
        /// Constructs a 2D Vector from two points, in the direction from point1 to point2.
        /// </summary>
        /// <param name="point1X">X-Coordinate of point1</param>
        /// <param name="point1Y">Y-Coordinate of point1</param>
        /// <param name="point2X">X-Coordinate of point2</param>
        /// <param name="point2Y">Y-Coordinate of point2</param>
        public Vector (float point1X, float point1Y, float point2X, float point2Y)
        {
            comps = new float[2] { point2X - point1X, point2Y - point1Y };
            twoD = true;
        }

        /// <summary>
        /// Constructs a 3D Vector from two points, in the direction from point1 to point2.
        /// </summary>
        /// <param name="point1X">X-Coordinate of point1</param>
        /// <param name="point1Y">Y-Coordinate of point1</param>
        /// <param name="point1Z">Z-Coordinate of point1</param>
        /// <param name="point2X">X-Coordinate of point2</param>
        /// <param name="point2Y">Y-Coordinate of point2</param>
        /// <param name="point2Z">Z-Coordinate of point2</param>
        public Vector (float point1X, float point1Y, float point1Z, float point2X, float point2Y, float point2Z)
        {
            comps = new float[3] {
                point2X - point1X,
                point2Y - point1Y,
                point2Z - point1Z
            };
            threeD = true;
        }

        /// <summary>
        /// Constructs a 2D / 3D Vector from two points, in the direction from point1 to point2.
        /// </summary>
        /// <param name="point1">Array where point1[0] is 'x' and point1[1] is 'y' (and point1[2] is 'Z', if 3D), of point1.</param>
        /// <param name="point2">Array where point2[0] is 'x' and point2[1] is 'y' (and point2[2] is 'Z', if 3D), of point2.</param>
        public Vector (float[] point1, float[] point2)
        {
            if ((point1.Length == 2 && point2.Length == 2) || (point1.Length == 3 && point2.Length == 3))
            {
                if (point1.Length == 2)
                {
                    comps = new float[2] { point2[0] - point1[0], point2[1] - point1[1] };
                    twoD = true;
                }
                else
                {
                    comps = new float[3] { point2[0] - point1[0], point2[1] - point1[1], point2[2] - point1[2] };
                }
            }
            else
            {
                throw new WrongVectorDimensionException ();
            }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// returns the vector as a string in the format (X;Y) or (X;Y;Z).
        /// </summary>
        public override string ToString ()
        {
            if (Is2D)
            {
                return $"({X};{Y})";
            }
            else
            {
                return $"({X};{Y};{Z})";
            }
        }

        /// <summary>
        /// Returns <see langword="true"/> if the dimensions and values of the Vectors are equal. 
        /// The "==" operator ignores dimensions.
        /// </summary>
        public override bool Equals (object obj)
        {
            if (GetType () == obj.GetType ())
            {
                if (Is2D != ((Vector) obj).Is2D)
                {
                    return false;
                }
                else
                {
                    if (Is2D && (X == ((Vector) obj).X && Y == ((Vector) obj).Y) ||
                        Is3D && (X == ((Vector) obj).X && Y == ((Vector) obj).Y && Z == ((Vector) obj).Z))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns the HashCode for this Vector.
        /// </summary>
        public override int GetHashCode ()
        {
            if (Is2D)
            {
                return X.GetHashCode () * 2 - Y.GetHashCode ();
            }
            else
            {
                return X.GetHashCode () * 2 - Y.GetHashCode () - (Z.GetHashCode () - 1);
            }

        }
        /// <summary>
        /// Compares the values of two Vectors. 
        /// Returns <see langword="true"/> if both Vectors have the same dimensions and their components are of the same value.
        /// Also returns <see langword="true"/> if one Vector is 2D and the other is 3D as long as the Z component of the Vector is 0.
        /// </summary>
        public static bool operator == (Vector obj1, Vector obj2)
        {
            if (obj1.Is2D && obj2.Is2D)
            {
                if (obj1.X == obj2.X && obj1.Y == obj2.Y)
                {
                    return true;
                }
            }
            else if (obj1.Is3D && obj2.Is3D)
            {
                if (obj1.X == obj2.X && obj1.Y == obj2.Y && obj1.Z == obj2.Z)
                {
                    return true;
                }
            }
            else
            {
                if (obj1.X == obj2.X && obj1.Y == obj2.Y)
                {
                    if (obj1.Is2D && obj2.Is3D)
                    {
                        if (obj2.Z == 0f)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (obj2.Is2D && obj1.Is3D)
                    {
                        if (obj1.Z == 0f)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns !(obj1 == obj2).
        /// </summary>
        public static bool operator != (Vector obj1, Vector obj2)
        {
            return !(obj1 == obj2);
        }
        /// <summary>
        /// Adds v1 and v2 by adding the corresponding components.
        /// Same as "v1.Add (v2)".
        /// </summary>
        public static Vector operator + (Vector v1, Vector v2)
        {
            return v1.Add (v2);
        }
        /// <summary>
        /// Subtracts v2 from v1 by subtracting the corresponding components.
        /// Same as "v1.Sub (v2)".
        /// </summary>
        public static Vector operator - (Vector v1, Vector v2)
        {
            return v1.Sub (v2);
        }
        /// <summary>
        /// Multiplies v1 by the scalar by multiplying each component with the scalar.
        /// Same as "v1.Mult (scalar)".
        /// </summary>
        public static Vector operator * (Vector v1, float scalar)
        {
            return v1.Mult (scalar);
        }
        /// <summary>
        /// Multiplies v1 by the scalar by multiplying each component with the scalar.
        /// Same as "v1.Mult (scalar)".
        /// </summary>
        public static Vector operator * (float scalar, Vector v1)
        {
            return v1.Mult (scalar);
        }
        #endregion

        #region Operations
        /// <summary>Normalizes vector (sets magnitude to 1.0).</summary>
        public Vector Normalize ()
        {
            if (Is2D)
            {
                var tmp = Vector2D.norm (this);
                this.X = tmp.X;
                this.Y = tmp.Y;
                return new Vector (X, Y);
            }
            else
            {
                var tmp = Vector3D.norm (this);
                this.X = tmp.X;
                this.Y = tmp.Y;
                this.Z = tmp.Z;
                return new Vector (X, Y, Z);
            }

        }

        // TODO implement angleBetween
        //		public float angleBetween(float x, float y)
        //		{
        //			Vector2D.angle(
        //		}


        /// <summary>Adds the given Vector to this Vector.</summary>
        public Vector Add (Vector v)
        {
            if (this.Dim != v.Dim)
            {
                throw new WrongVectorDimensionException ();
            }
            this.X += v.X;
            this.Y += v.Y;
            if (Is3D)
            {
                this.Z += v.Z;
                return new Vector (X, Y, Z);
            }
            return new Vector (X, Y);
        }

        /// <summary>Subtracts the given Vector from this Vector.</summary>
        public Vector Sub (Vector v)
        {
            if (this.Dim != v.Dim)
            {
                throw new WrongVectorDimensionException ();
            }
            this.X -= v.X;
            this.Y -= v.Y;
            if (Is3D)
            {
                this.Z -= v.Z;
                return new Vector (X, Y, Z);
            }
            return new Vector (X, Y);
        }

        /// <summary>Divides the Vector by the given scalar</summary>
        public Vector Div (float scalar)
        {
            if (scalar > 0d)
            {
                return Mult (1 / scalar);
            }
            else
            {
                throw new DivideByZeroException ();
            }
        }

        /// <summary>Multiplies the Vector by the given scalar.</summary>
        public Vector Mult (float scalar)
        {
            this.X = this.X * scalar;
            this.Y = this.Y * scalar;
            if (Is3D)
            {
                this.Z = this.Z * scalar;
                return new Vector (X, Y, Z);
            }
            return new Vector (X, Y);
        }

        /// <summary>Multiplies the Vector by the given scalar.</summary>
        public Vector Mult (int scalar)
        {
            return Mult ((float) scalar);
        }

        /// <summary>alias for Normalize().</summary>
        public Vector Norm ()
        {
            return Normalize ();
        }

        /// <summary>Calculates magnitude of Vector. Use 'MagnitudeD' for double precision.</summary>
        public float Magnitude ()
        {
            if (Is2D)
            {
                return Vector2D.mag (this);
            }
            else
            {
                return Vector3D.mag (this);
            }
        }

        /// <summary>alias for Magnitude().</summary>
        public float Mag ()
        {
            return Magnitude ();
        }

        /// <summary>Calculates magnitude of Vector in double precision.</summary>
        public double MagnitudeD ()
        {
            if (Is2D)
            {
                return Vector2D.magD (this);
            }
            else
            {
                return Vector3D.magD (this);
            }
        }

        /// <summary>Alias for MagnitudeD.</summary>
        public double MagD ()
        {
            return MagnitudeD ();
        }

        /// <summary>Calculates the distance to the given Vector.</summary>
        public float Distance (Vector v)
        {
            if (this.Dim != v.Dim)
            {
                throw new WrongVectorDimensionException ();
            }
            if (Is2D)
            {
                return Vector2D.dist (this, v);
            }
            else
            {
                return Vector3D.dist (this, v);
            }
        }

        /// <summary>Calculates the distance to the given Coordinates (same as "Distance(new Vector(float x, float y))").</summary>
        public float Distance (float x, float y)
        {
            return Distance (new Vector (x, y));
        }

        /// <summary>Calculates the distance to the given Coordinates (same as "Distance(new Vector(float x, float y, float z))").</summary>
        public float Distance (float x, float y, float z)
        {
            return Distance (new Vector (x, y, z));
        }

        /// <summary>Calculates the dot-product of two vectors.</summary>
        public float Dot (Vector v)
        {
            if (this.Dim != v.Dim)
            {
                throw new WrongVectorDimensionException ();
            }
            if (Is2D)
            {
                return Vector2D.dot (this, v);
            }
            else
            {
                return Vector3D.dot (this, v);
            }
        }

        /// <summary>Calculates the Cross-product of two vectors (3D Vectors only!).</summary>
        public Vector Cross (Vector v)
        {
            if (this.Dim != v.Dim)
            {
                throw new WrongVectorDimensionException ();
            }
            if (Is3D)
            {
                return Vector3D.cross (this, v);
            }
            else
            {
                throw new WrongVectorDimensionException ();
            }
        }
        #endregion
    }
}
