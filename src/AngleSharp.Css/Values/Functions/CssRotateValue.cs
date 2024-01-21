namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the rotate3d transformation.
    /// </summary>
    /// <remarks>
    /// Creates a new rotate transform.
    /// </remarks>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="z">The z coordinate.</param>
    /// <param name="angle">The angle of rotation.</param>
    sealed class CssRotateValue(ICssValue? x, ICssValue? y, ICssValue? z, ICssValue angle) : ICssTransformFunctionValue, IEquatable<CssRotateValue>
    {
        #region Fields

        private readonly ICssValue? _x = x;
        private readonly ICssValue? _y = y;
        private readonly ICssValue? _z = z;
        private readonly ICssValue _angle = angle;

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
                if (_x is null && _y is null && _z is null)
                {
                    return FunctionNames.Rotate;
                }
                else if (_x is not null && _y is null && _z is null)
                {
                    return FunctionNames.RotateX;
                }
                else if (_x is null && _y is not null && _z is null)
                {
                    return FunctionNames.RotateY;
                }
                else if (_x is null && _y is null && _z is not null)
                {
                    return FunctionNames.RotateY;
                }

                return FunctionNames.Rotate3d;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments =>
            _x is not null && _y is not null && _z is not null ? [_x, _y, _z, _angle] :
            _x is not null && _y is not null ? [_x, _y, _angle] :
            _x is not null && _z is not null ? [_x, _z, _angle] :
            _y is not null && _z is not null ?[_y, _z, _angle] :
            _x is not null ? [_x, _angle] :
            _y is not null ? [_y, _angle] :
            _z is not null ? [_z, _angle] :
            [_angle];

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var args = _angle.CssText;
                var fn = Name;

                if (fn == FunctionNames.Rotate3d)
                {
                    args = Arguments.Join(", ");
                }

                return fn.CssFunction(args);
            }
        }

        /// <summary>
        /// Gets the value of the x-component of the rotation vector.
        /// </summary>
        public ICssValue? X => _x;

        /// <summary>
        /// Gets the value of the y-component of the rotation vector.
        /// </summary>
        public ICssValue? Y => _y;

        /// <summary>
        /// Gets the value of the z-component of the rotation vector.
        /// </summary>
        public ICssValue? Z => _z;

        /// <summary>
        /// Gets the angle.
        /// </summary>
        public ICssValue Angle => _angle;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssRotateValue? other)
        {
            return other is not null && _angle.Equals(other._angle) && _x == other._x && _y == other._y && _z == other._z;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssRotateValue value && Equals(value);

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix(IRenderDimensions renderDimensions)
        {
            var x = _x.AsDouble();
            var y = _y.AsDouble();
            var z = _z.AsDouble();
            var norm = 1.0 / Math.Sqrt(x * x + y * y + z * z);
            var alpha = _angle.AsRad();
            var sina = Math.Sin(alpha);
            var cosa = Math.Cos(alpha);
            var l = x * norm;
            var m = y * norm;
            var n = z * norm;
            var omc = (1.0 - cosa);
            return new TransformMatrix(
                l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
        }

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var x = _x?.Compute(context);
            var y = _y?.Compute(context);
            var z = _z?.Compute(context);
            var angle = _angle.Compute(context);

            if (angle is not null)
            {
                return new CssRotateValue(x, y, z, angle);
            }

            return null;
        }

        #endregion
    }
}
