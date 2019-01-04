namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the matrix3d transformation.
    /// </summary>
    class MatrixTransform : ITransform, ICssFunctionValue
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
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return _values.Length == 6 ? FunctionNames.Matrix : FunctionNames.Matrix3d; }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var args = new ICssValue[_values.Length];

                for (var i = 0; i < args.Length; i++)
                {
                    args[i] = new Length(_values[i], Length.Unit.None);
                }

                return args;
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return Name.CssFunction(Arguments.Join(", ")); }
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
