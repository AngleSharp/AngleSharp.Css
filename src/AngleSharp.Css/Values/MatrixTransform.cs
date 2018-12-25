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

        private readonly Double[] _values;

        #endregion

        #region ctor

        internal MatrixTransform(Double[] values)
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
            get
            {
                var fn = _values.Length == 6 ? FunctionNames.Matrix : FunctionNames.Matrix3d;
                var args = new String[_values.Length];

                for (var i = 0; i < args.Length; i++)
                {
                    args[i] = _values[i].ToString(CultureInfo.InvariantCulture);
                }

                return fn.CssFunction(String.Join(", ", args));
            }
        }

        /// <summary>
        /// Gets the value of the given index.
        /// </summary>
        /// <param name="index">The index to look for.</param>
        /// <returns>The value.</returns>
        public Double this[Int32 index]
        {
            get { return _values[index]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var values = _values;

            if (values.Length == 6)
            {
                values = new Double[]
                {
                    _values[0], _values[2], 0.0, _values[4],
                    _values[1], _values[3], 0.0, _values[5],
                    1.0, 0.0, 0.0, 0.0,
                    0.0, 0.0, 0.0, 1.0
                };
            }

            return new TransformMatrix(values);
        }

        #endregion
    }
}
