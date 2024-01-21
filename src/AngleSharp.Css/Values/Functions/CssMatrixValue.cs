namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the matrix3d transformation.
    /// </summary>
    sealed class CssMatrixValue : ICssTransformFunctionValue, IEquatable<CssMatrixValue>
    {
        #region Fields

        private readonly ICssValue[] _values;

        #endregion

        #region ctor

        internal CssMatrixValue(ICssValue[] values)
        {
            _values = values;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => _values.Length == 6 ? FunctionNames.Matrix : FunctionNames.Matrix3d;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => _values;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        /// <summary>
        /// Gets the value of the given index.
        /// </summary>
        /// <param name="index">The index to look for.</param>
        /// <returns>The value.</returns>
        public ICssValue this[Int32 index] => _values[index];

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssMatrixValue other)
        {
            var count = _values.Length;

            if (count == other._values.Length)
            {
                for (var i = 0; i < count; i++)
                {
                    var a = _values[i];
                    var b = other._values[i];

                    if (!a.Equals(b))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssMatrixValue value && Equals(value);

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix(IRenderDimensions dimensions)
        {
            var values = _values.Select(v => v.AsDouble()).ToList();

            if (values.Count == 6)
            {
                values.Add(1.0);
                values.Add(0.0);
                values.Add(0.0);
                values.Add(0.0);
                values.Add(0.0);
                values.Add(0.0);
                values.Add(0.0);
                values.Add(1.0);
            }

            return new TransformMatrix([.. values]);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var values = _values.Select(v => v.Compute(context)).ToArray();
            return new CssMatrixValue(values);
        }

        #endregion
    }
}
