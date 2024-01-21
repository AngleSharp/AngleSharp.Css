namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    /// <remarks>
    /// Creates a new translate transform.
    /// </remarks>
    /// <param name="x">The x shift.</param>
    /// <param name="y">The y shift.</param>
    /// <param name="z">The z shift.</param>
    sealed class CssTranslateValue(ICssValue x, ICssValue y, ICssValue z) : ICssTransformFunctionValue, IEquatable<CssTranslateValue>
    {
        #region Fields

        private readonly ICssValue _x = x;
        private readonly ICssValue _y = y;
        private readonly ICssValue _z = z;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get
            {
                if (_x != null && _y == null && _z == null)
                {
                    return FunctionNames.TranslateX;
                }
                else if (_x == null && _y != null && _z == null)
                {
                    return FunctionNames.TranslateY;
                }
                else if (_x == null && _y == null && _z != null)
                {
                    return FunctionNames.TranslateZ;
                }
                else if (_z == null)
                {
                    return FunctionNames.Translate;
                }

                return FunctionNames.Translate3d;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[]
        {
            _x,
            _y,
            _z
        };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        /// <summary>
        /// Gets the shift in x-direction.
        /// </summary>
        public ICssValue ShiftX => _x;

        /// <summary>
        /// Gets the shift in y-direction.
        /// </summary>
        public ICssValue ShiftY => _y;

        /// <summary>
        /// Gets the shift in z-direction.
        /// </summary>
        public ICssValue ShiftZ => _z;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssTranslateValue? other)
        {
            return other is not null && _x.Equals(other._x) && _y.Equals(other._y) && _z.Equals(other._z);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssTranslateValue value && Equals(value);

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix(IRenderDimensions renderDimensions)
        {
            var dx = _x.AsPx(renderDimensions, RenderMode.Horizontal);
            var dy = _y.AsPx(renderDimensions, RenderMode.Vertical);
            var dz = _z.AsPx(renderDimensions, RenderMode.Undefined);
            return new TransformMatrix(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, dx, dy, dz, 0.0, 0.0, 0.0);
        }

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var x = _x.Compute(context);
            var y = _y.Compute(context);
            var z = _z.Compute(context);

            if (x is not null && y is not null && z is not null)
            {
                return new CssTranslateValue(x, y, z);
            }

            return null;
        }

        #endregion
    }
}
