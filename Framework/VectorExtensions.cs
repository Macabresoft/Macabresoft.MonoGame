﻿namespace Macabre2D.Framework {

    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for vectors.
    /// </summary>
    public static class VectorExtensions {

        /// <summary>
        /// Gets the average of an array of vectors.
        /// </summary>
        /// <param name="vectors">The vectors.</param>
        /// <returns>A vector that should be in the mathematical center of all specified vectors.</returns>
        public static Vector2 GetAverage(this IEnumerable<Vector2> vectors) {
            var count = vectors.Count();
            if (count == 0) {
                return Vector2.Zero;
            }

            var x = 0f;
            var y = 0f;

            foreach (var vector in vectors) {
                x += vector.X;
                y += vector.Y;
            }

            return new Vector2(x / count, y / count);
        }

        /// <summary>
        /// Gets the normalized version of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The normalized version of the vector.</returns>
        public static Vector2 GetNormalized(this Vector2 vector) {
            vector.Normalize();
            return vector;
        }

        /// <summary>
        /// Gets the vector perpindicular (clockwise) to the provided vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The vector perpindicular (clockwise) to the provided vector.</returns>
        public static Vector2 GetPerpendicular(this Vector2 vector) {
            return new Vector2(-vector.Y, vector.X);
        }

        /// <summary>
        /// Gets the vector perpindicular (counter clockwise) to the provided vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The vector perpindicular (counter clockwise) to the provided vector.</returns>
        public static Vector2 GetPerpendicularCounterClockwise(this Vector2 vector) {
            return new Vector2(vector.Y, -vector.X);
        }

        /// <summary>
        /// Translates a Vector3 into a Vector2 by cutting off the Z value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>A Vector2 from the Vector3.</returns>
        public static Vector2 ToVector2(this Vector3 vector) {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        /// Translates a Vector2 into a Vector3 by appending a Z value of 0.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>A Vector3 from the Vector2.</returns>
        public static Vector3 ToVector3(this Vector2 vector) {
            return new Vector3(vector, 0f);
        }

        /// <summary>
        /// Translates a Vector2 into a Vector3 by appending the specified z value.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="zValue">The z value.</param>
        /// <returns>A Vector3 from the Vector2.</returns>
        public static Vector3 ToVector3(this Vector2 vector, float zValue) {
            return new Vector3(vector, zValue);
        }
    }
}