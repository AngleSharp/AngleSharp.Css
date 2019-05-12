namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a transformation matrix value.
    /// http://dev.w3.org/csswg/css-transforms/#mathematical-description
    /// </summary>
    public class TransformMatrix : IEquatable<TransformMatrix>
    {
        #region Fields

        /// <summary>
        /// Gets the zero matrix.
        /// </summary>
        public static readonly TransformMatrix Zero = new TransformMatrix();

        /// <summary>
        /// Gets the unity matrix.
        /// </summary>
        public static readonly TransformMatrix One = new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);

        private readonly Double[,] _matrix;

        #endregion

        #region ctor

        private TransformMatrix()
        {
            _matrix = new Double[4, 4];
        }

        /// <summary>
        /// Creates a new transformation matrix from a 1D-array.
        /// </summary>
        /// <param name="values">The array with values.</param>
        public TransformMatrix(Double[] values)
            : this()
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.Length != 16)
                throw new ArgumentException("You need to provide 16 (4x4) values.", nameof(values));

            var k = 0;

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++, k++)
                {
                    _matrix[j, i] = values[k];
                }
            }
        }

        /// <summary>
        /// Creates a transformation matrix.
        /// </summary>
        /// <param name="m11">The (1, 1) entry.</param>
        /// <param name="m12">The (1, 2) entry.</param>
        /// <param name="m13">The (1, 3) entry.</param>
        /// <param name="m21">The (2, 1) entry.</param>
        /// <param name="m22">The (2, 2) entry.</param>
        /// <param name="m23">The (2, 3) entry.</param>
        /// <param name="m31">The (3, 1) entry.</param>
        /// <param name="m32">The (3, 2) entry.</param>
        /// <param name="m33">The (3, 3) entry.</param>
        /// <param name="tx">The x-translation entry.</param>
        /// <param name="ty">The y-translation entry.</param>
        /// <param name="tz">The z-translation entry.</param>
        /// <param name="px">The x-perspective entry.</param>
        /// <param name="py">The y-perspective entry.</param>
        /// <param name="pz">The z-perspective entry.</param>
        public TransformMatrix(
            Double m11, Double m12, Double m13,
            Double m21, Double m22, Double m23,
            Double m31, Double m32, Double m33,
            Double tx, Double ty, Double tz,
            Double px, Double py, Double pz)
            : this()
        {
            _matrix[0, 0] = m11;
            _matrix[0, 1] = m12;
            _matrix[0, 2] = m13;
            _matrix[1, 0] = m21;
            _matrix[1, 1] = m22;
            _matrix[1, 2] = m23;
            _matrix[2, 0] = m31;
            _matrix[2, 1] = m32;
            _matrix[2, 2] = m33;
            _matrix[0, 3] = tx;
            _matrix[1, 3] = ty;
            _matrix[2, 3] = tz;
            _matrix[3, 0] = px;
            _matrix[3, 1] = py;
            _matrix[3, 2] = pz;
            _matrix[3, 3] = 1f;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element at the specified indices.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <returns>The value of the cell.</returns>
        public Double this[Int32 row, Int32 col] => _matrix[row, col];

        /// <summary>
        /// Gets the element of the 1st row, 1st column.
        /// </summary>
        public Double M11 => _matrix[0, 0];

        /// <summary>
        /// Gets the element of the 1st row, 2nd column.
        /// </summary>
        public Double M12 => _matrix[0, 1];

        /// <summary>
        /// Gets the element of the 1st row, 3rd column.
        /// </summary>
        public Double M13 => _matrix[0, 2];

        /// <summary>
        /// Gets the element of the 2nd row, 1st column.
        /// </summary>
        public Double M21 => _matrix[1, 0];

        /// <summary>
        /// Gets the element of the 2nd row, 2nd column.
        /// </summary>
        public Double M22 => _matrix[1, 1];

        /// <summary>
        /// Gets the element of the 2nd row, 3rd column.
        /// </summary>
        public Double M23 => _matrix[1, 2];

        /// <summary>
        /// Gets the element of the 3rd row, 1st column.
        /// </summary>
        public Double M31 => _matrix[2, 0];

        /// <summary>
        /// Gets the element of the 3rd row, 2nd column.
        /// </summary>
        public Double M32 => _matrix[2, 1];

        /// <summary>
        /// Gets the element of the 3rd row, 3rd column.
        /// </summary>
        public Double M33 => _matrix[2, 2];

        /// <summary>
        /// Gets the x-element of the translation vector.
        /// </summary>
        public Double Tx => _matrix[0, 3];

        /// <summary>
        /// Gets the y-element of the translation vector.
        /// </summary>
        public Double Ty => _matrix[1, 3];

        /// <summary>
        /// Gets the z-element of the translation vector.
        /// </summary>
        public Double Tz => _matrix[2, 3];

        #endregion

        #region Methods

        /// <summary>
        /// Checks for equality with the given other transformation matrix.
        /// </summary>
        /// <param name="other">The other transformation matrix.</param>
        /// <returns>True if all elements are equal, otherwise false.</returns>
        public Boolean Equals(TransformMatrix other)
        {
            var A = _matrix;
            var B = other._matrix;

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (A[i, j] != B[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Equality

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) => obj is TransformMatrix other ? Equals(other) : false;

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            var sum = 0.0;

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    sum += _matrix[i, j] * (4 * i + j);
                }
            }

            return (Int32)(sum);
        }

        #endregion
    }
}
