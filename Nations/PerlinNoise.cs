using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perlin
{// --------------------------------------------------------------------------------------------------------------------
    using Microsoft.Xna.Framework;
    // <copyright file="PerlinNoise.cs" company="Slash Games">
    //   Copyright (c) Slash Games. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Vector2.cs" company="Slash Games">
    //   Copyright (c) Slash Games. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------


        using System;

        /// <summary>
        ///   Vector in two-dimensional space with double components. Note that vectors are immutable.
        /// </summary>
        [CLSCompliant(true)]
        public struct Vector2 : IEquatable<Vector2>
        {
            #region Fields

            /// <summary>
            ///   X-component of this vector.
            /// </summary>
            private readonly double x;

            /// <summary>
            ///   Y-component of this vector.
            /// </summary>
            private readonly double y;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            ///   Constructs a new vector with the specified components.
            /// </summary>
            /// <param name="x">
            ///   X-component of the new vector.
            /// </param>
            /// <param name="y">
            ///   Y-component of the new vector.
            /// </param>
            public Vector2(double x, double y)
                : this()
            {
                this.x = x;
                this.y = y;
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///   Gets the squared magnitude of this vector.
            /// </summary>
            public double LengthSquared
            {
                get
                {
                    return (this.x * this.x) + (this.y * this.y);
                }
            }

            /// <summary>
            ///   Gets the x-component of this vector.
            /// </summary>
            public double X
            {
                get
                {
                    return this.x;
                }
            }

            /// <summary>
            ///   Gets the y-component of this vector.
            /// </summary>
            public double Y
            {
                get
                {
                    return this.y;
                }
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///   Adds the passed vectors.
            /// </summary>
            /// <param name="v1">
            ///   First summand.
            /// </param>
            /// <param name="v2">
            ///   Second summand.
            /// </param>
            /// <returns>
            ///   Sum of the passed vectors.
            /// </returns>
            public static Vector2 Add(Vector2 v1, Vector2 v2)
            {
                return v1 + v2;
            }

            /// <summary>
            ///   Divides the passed vector by the specified scalar.
            /// </summary>
            /// <param name="v">
            ///   Dividend.
            /// </param>
            /// <param name="f">
            ///   Divisor.
            /// </param>
            /// <returns>
            ///   Vector divided by the specified scalar.
            /// </returns>
            public static Vector2 Divide(Vector2 v, double f)
            {
                return v / f;
            }

            /// <summary>
            ///   Computes the dot product of the two passed vectors.
            /// </summary>
            /// <param name="v1">
            ///   First vector to compute the dot product of.
            /// </param>
            /// <param name="v2">
            ///   Second vector to compute the dot product of.
            /// </param>
            /// <returns>
            ///   Dot product of the two passed vectors.
            /// </returns>
            public static double Dot(Vector2 v1, Vector2 v2)
            {
                return (v1.x * v2.x) + (v1.y * v2.y);
            }

            /// <summary>
            ///   Multiplies the passed vector with the specified scalar.
            /// </summary>
            /// <param name="v">
            ///   Vector to multiply.
            /// </param>
            /// <param name="f">
            ///   Scalar to multiply the vector with.
            /// </param>
            /// <returns>
            ///   Product of the vector and the scalar.
            /// </returns>
            public static Vector2 Multiply(Vector2 v, double f)
            {
                return f * v;
            }

            /// <summary>
            ///   Multiplies the passed vector with the specified scalar.
            /// </summary>
            /// <param name="f">
            ///   Scalar to multiply the vector with.
            /// </param>
            /// <param name="v">
            ///   Vector to multiply.
            /// </param>
            /// <returns>
            ///   Product of the vector and the scalar.
            /// </returns>
            public static Vector2 Multiply(double f, Vector2 v)
            {
                return f * v;
            }

            /// <summary>
            ///   Normalizes the passed vector, returning a unit vector with the same orientation.
            ///   If the passed vector is the zero vector, the zero vector is returned instead.
            /// </summary>
            /// <param name="v">
            ///   Vector to normalize.
            /// </param>
            /// <returns>
            ///   Normalized passed vector.
            /// </returns>
            public static Vector2 Normalize(Vector2 v)
            {
                var lengthSquared = v.LengthSquared;
                if (Math.Abs(lengthSquared) < 0.001 || Math.Abs(lengthSquared - 1) < 0.001)
                {
                    return v;
                }

                return new Vector2(v.x, v.y) / Math.Sqrt(lengthSquared);
            }

            /// <summary>
            ///   Subtracts the second vector from the first one.
            /// </summary>
            /// <param name="v1">
            ///   Vector to subtract from.
            /// </param>
            /// <param name="v2">
            ///   Vector to subtract.
            /// </param>
            /// <returns>
            ///   Difference of both vectors.
            /// </returns>
            public static Vector2 Subtract(Vector2 v1, Vector2 v2)
            {
                return v1 - v2;
            }

            /// <summary>
            ///   Adds the passed vectors.
            /// </summary>
            /// <param name="v1">
            ///   First summand.
            /// </param>
            /// <param name="v2">
            ///   Second summand.
            /// </param>
            /// <returns>
            ///   Sum of the passed vectors.
            /// </returns>
            public static Vector2 operator +(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x + v2.x, v1.y + v2.y);
            }

            /// <summary>
            ///   Divides the passed vector by the specified scalar.
            /// </summary>
            /// <param name="v">
            ///   Dividend.
            /// </param>
            /// <param name="f">
            ///   Divisor.
            /// </param>
            /// <returns>
            ///   Vector divided by the specified scalar.
            /// </returns>
            public static Vector2 operator /(Vector2 v, double f)
            {
                return new Vector2(v.x / f, v.y / f);
            }

            /// <summary>
            ///   Compares the passed vectors for equality.
            /// </summary>
            /// <param name="v1">
            ///   First vector to compare.
            /// </param>
            /// <param name="v2">
            ///   Second vector to compare.
            /// </param>
            /// <returns>
            ///   <c>true</c>, if both vectors are equal, and <c>false</c> otherwise.
            /// </returns>
            public static bool operator ==(Vector2 v1, Vector2 v2)
            {
                return v1.Equals(v2);
            }

            /// <summary>
            ///   Compares the passed vectors for inequality.
            /// </summary>
            /// <param name="v1">
            ///   First vector to compare.
            /// </param>
            /// <param name="v2">
            ///   Second vector to compare.
            /// </param>
            /// <returns>
            ///   <c>true</c>, if the vectors are not equal, and <c>false</c> otherwise.
            /// </returns>
            public static bool operator !=(Vector2 v1, Vector2 v2)
            {
                return !(v1 == v2);
            }

            /// <summary>
            ///   Multiplies the passed vector with the specified scalar.
            /// </summary>
            /// <param name="v">
            ///   Vector to multiply.
            /// </param>
            /// <param name="f">
            ///   Scalar to multiply the vector with.
            /// </param>
            /// <returns>
            ///   Product of the vector and the scalar.
            /// </returns>
            public static Vector2 operator *(Vector2 v, double f)
            {
                return f * v;
            }

            /// <summary>
            ///   Multiplies the passed vector with the specified scalar.
            /// </summary>
            /// <param name="f">
            ///   Scalar to multiply the vector with.
            /// </param>
            /// <param name="v">
            ///   Vector to multiply.
            /// </param>
            /// <returns>
            ///   Product of the vector and the scalar.
            /// </returns>
            public static Vector2 operator *(double f, Vector2 v)
            {
                return new Vector2(v.x * f, v.y * f);
            }

            /// <summary>
            ///   Subtracts the second vector from the first one.
            /// </summary>
            /// <param name="v1">
            ///   Vector to subtract from.
            /// </param>
            /// <param name="v2">
            ///   Vector to subtract.
            /// </param>
            /// <returns>
            ///   Difference of both vectors.
            /// </returns>
            public static Vector2 operator -(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x - v2.x, v1.y - v2.y);
            }

            /// <summary>
            ///   Adds the passed vector to this one, returning the sum of both vectors.
            /// </summary>
            /// <param name="v">
            ///   Vector to add.
            /// </param>
            /// <returns>
            ///   Sum of both vectors.
            /// </returns>
            public Vector2 Add(Vector2 v)
            {
                return Add(this, v);
            }

            /// <summary>
            ///   Divides this vector by the specified scalar.
            /// </summary>
            /// <param name="f">
            ///   Divisor.
            /// </param>
            /// <returns>
            ///   Vector divided by the specified scalar.
            /// </returns>
            public Vector2 Divide(double f)
            {
                return Divide(this, f);
            }

            /// <summary>
            ///   Computes the dot product of the passed vector and this one.
            /// </summary>
            /// <param name="v">
            ///   Vector to compute the dot product of.
            /// </param>
            /// <returns>
            ///   Dot product of the two vectors.
            /// </returns>
            public double Dot(Vector2 v)
            {
                return Dot(this, v);
            }

            /// <summary>
            ///   Compares the passed vector to this one for equality.
            /// </summary>
            /// <param name="obj">
            ///   Vector to compare.
            /// </param>
            /// <returns>
            ///   <c>true</c>, if both vectors are equal, and <c>false</c> otherwise.
            /// </returns>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                return obj is Vector2 && this.Equals((Vector2)obj);
            }

            /// <summary>
            ///   Compares the passed vector to this one for equality.
            /// </summary>
            /// <param name="other">
            ///   Vector to compare.
            /// </param>
            /// <returns>
            ///   <c>true</c>, if both vectors are equal, and <c>false</c> otherwise.
            /// </returns>
            public bool Equals(Vector2 other)
            {
                return this.x.Equals(other.x) && this.y.Equals(other.y);
            }

            /// <summary>
            ///   Gets the hash code of this vector.
            /// </summary>
            /// <returns>
            ///   Hash code of this vector.
            /// </returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = this.x.GetHashCode();
                    hashCode = (hashCode * 397) ^ this.y.GetHashCode();
                    return hashCode;
                }
            }

            /// <summary>
            ///   Multiplies this vector with the specified scalar.
            /// </summary>
            /// <param name="f">
            ///   Scalar to multiply this vector with.
            /// </param>
            /// <returns>
            ///   Product of this vector and the scalar.
            /// </returns>
            public Vector2 Multiply(double f)
            {
                return Multiply(f, this);
            }

            /// <summary>
            ///   Normalizes this vector, returning a unit vector with the same orientation.
            ///   If this passed vector is the zero vector, the zero vector is returned instead.
            /// </summary>
            /// <returns>
            ///   Normalized vector.
            /// </returns>
            public Vector2 Normalize()
            {
                return Normalize(this);
            }

            /// <summary>
            ///   Subtracts the passed vector from this one.
            /// </summary>
            /// <param name="v">
            ///   Vector to subtract.
            /// </param>
            /// <returns>
            ///   Difference of both vectors.
            /// </returns>
            public Vector2 Subtract(Vector2 v)
            {
                return Subtract(this, v);
            }

            /// <summary>
            ///   Returns a <see cref="string" /> representation of this vector.
            /// </summary>
            /// <returns>
            ///   This vector as <see cref="string" />.
            /// </returns>
            public override string ToString()
            {
                return string.Format("({0}, {1})", this.x, this.y);
            }

            #endregion
        }
    
    internal class PerlinNoise
    {
        #region Fields

        /// <summary>
        ///   Pseudo-random gradients of unit length for all grid points.
        /// </summary>
        private readonly Vector2[,] gradients;

        /// <summary>
        ///   Height and width of each grid cell, in pixels.
        /// </summary>
        private readonly int gridCellSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructs a new perlin noise instance for filling a bitmap
        ///   of the specified size, broken down into a smaller grid of the
        ///   passed size.
        /// </summary>
        /// <param name="bitmapSize">
        ///   Size of the bitmap to generate.
        /// </param>
        /// <param name="gridSize">
        ///   Size of the grid imposed on the bitmap.
        /// </param>
        public PerlinNoise(int bitmapSize, int gridSize)
        {
            // Store cell size.
            this.gridCellSize = bitmapSize / gridSize;

            // Compute pseudo-random gradients.
            var random = new Random();

            this.gradients = new Vector2[gridSize + 1, gridSize + 1];

            for (int i = 0; i < gridSize + 1; i++)
            {
                for (int j = 0; j < gridSize + 1; j++)
                {
                    this.gradients[i, j] = this.RandomVector(random);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Returns the value of the two-dimensional noise function at the specified position.
        /// </summary>
        public double Noise2D(int x, int y)
        {
            // Transform position from pixel space into grid space.
            Vector2 p = new Vector2((double)x / this.gridCellSize, (double)y / this.gridCellSize);

            // Get surrounding grid points.
            var x0 = x / this.gridCellSize;
            var x1 = x0 + 1;
            var y0 = y / this.gridCellSize;
            var y1 = y0 + 1;

            Vector2 bottomLeft = new Vector2(x0, y0);
            Vector2 bottomRight = new Vector2(x1, y0);
            Vector2 topLeft = new Vector2(x0, y1);
            Vector2 topRight = new Vector2(x1, y1);

            // Get vectors from grid points to (x, y).
            Vector2 fromBottomLeft = p - bottomLeft;
            Vector2 fromBottomRight = p - bottomRight;
            Vector2 fromTopLeft = p - topLeft;
            Vector2 fromTopRight = p - topRight;

            // Compute influence of the gradients.
            double s = this.gradients[x0, y0].Dot(fromBottomLeft);
            double t = this.gradients[x1, y0].Dot(fromBottomRight);
            double u = this.gradients[x0, y1].Dot(fromTopLeft);
            double v = this.gradients[x1, y1].Dot(fromTopRight);

            // Ease and combine to noise value.
            double sX = this.Ease(p.X - x0);
            double a = this.Interpolate(s, t, sX);
            double b = this.Interpolate(u, v, sX);

            double sY = this.Ease(p.Y - y0);
            double z = this.Interpolate(a, b, sY);

            return z;
        }

        #endregion

        #region Methods

        private double Ease(double p)
        {
            return (3 * Math.Pow(p, 2) - 2 * Math.Pow(p, 3));
        }

        private double Interpolate(double a, double b, double t)
        {
            return b * t + a * (1 - t);
        }

        private Vector2 RandomVector(Random random)
        {
            var v = new Vector2(random.NextDouble() * 2 - 1, random.NextDouble() * 2 - 1);
            return v.Normalize();
        }

        #endregion
    }
    
}
