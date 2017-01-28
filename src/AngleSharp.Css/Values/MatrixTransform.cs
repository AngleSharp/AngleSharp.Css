namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the matrix3d transformation.
    /// </summary>
    public sealed class MatrixTransform : ITransform
    {
        #region Fields

        private readonly Single[] _values;

        #endregion

        #region ctor

        internal MatrixTransform(Single[] values)
        {
            _values = values;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        /// <summary>
        /// Gets the value of the given index.
        /// </summary>
        /// <param name="index">The index to look for.</param>
        /// <returns>The value.</returns>
        public Single this[Int32 index]
        {
            get { return _values[index]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes to a string.
        /// </summary>
        public override String ToString()
        {
            var fn = _values.Length == 6 ? FunctionNames.Matrix : FunctionNames.Matrix3d;
            var args = new String[_values.Length];

            for (var i = 0; i < args.Length; i++)
            {
                args[i] = _values[i].ToString(CultureInfo.InvariantCulture);
            }

            return fn.CssFunction(String.Join(", ", args));
        }

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var values = _values;

            if (values.Length == 6)
            {
                values = new Single[]
                {
                    _values[0], _values[2], 0f, _values[4],
                    _values[1], _values[3], 0f, _values[5],
                    1f, 0f, 0f, 0f,
                    0f, 0f, 0f, 1f
                };
            }

            return new TransformMatrix(values);
        }

        #endregion
    }
}
