namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the scale3d transformation.
    /// </summary>
    sealed class CssScaleValue : ICssTransformFunctionValue, IEquatable<CssScaleValue>
    {
        #region Fields

        private readonly ICssValue _sx;
        private readonly ICssValue _sy;
        private readonly ICssValue _sz;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new scale transform.
        /// </summary>
        /// <param name="sx">The x scaling factor.</param>
        /// <param name="sy">The y scaling factor.</param>
        /// <param name="sz">The z scaling factor.</param>
        public CssScaleValue(ICssValue sx, ICssValue sy, ICssValue sz)
        {
            _sx = sx;
            _sy = sy;
            _sz = sz;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get
            {
                if (_sx is null && _sz is null)
                {
                    return FunctionNames.ScaleY;
                }
                else if (_sy is null && _sz is null)
                {
                    return FunctionNames.ScaleX;
                }
                else if (_sx is null && _sy is null)
                {
                    return FunctionNames.ScaleX;
                }
                else if (_sz is null)
                {
                    return FunctionNames.Scale;
                }
                else
                {
                    return FunctionNames.Scale3d;
                }
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[]
        {
            _sx,
            _sy,
            _sz,
        };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                if (_sx is null && _sz is null)
                {
                    return FunctionNames.ScaleY.CssFunction(_sy.CssText);
                }
                else if (_sy is null && _sz is null)
                {
                    return FunctionNames.ScaleX.CssFunction(_sx.CssText);
                }
                else if (_sx is null && _sy is null)
                {
                    return FunctionNames.ScaleZ.CssFunction($"{_sz.CssText}");
                }
                else if (_sz is null && _sx.Equals(_sy))
                {
                    return FunctionNames.Scale.CssFunction(_sx.CssText);
                }
                else if (_sz is null)
                {
                    return FunctionNames.Scale.CssFunction($"{_sx.CssText}, {_sy.CssText}");
                }
                else
                {
                    return FunctionNames.Scale3d.CssFunction($"{_sx.CssText}, {_sy.CssText}, {_sz.CssText}");
                }
            }
        }

        /// <summary>
        /// Gets the scaling in x-direction.
        /// </summary>
        public ICssValue ScaleX => _sx;

        /// <summary>
        /// Gets the scaling in y-direction.
        /// </summary>
        public ICssValue ScaleY => _sy;

        /// <summary>
        /// Gets the scaling in z-direction.
        /// </summary>
        public ICssValue ScaleZ => _sz;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssScaleValue other)
        {
            return _sx.Equals(other._sx) && _sy.Equals(other._sy) && _sz.Equals(other._sz);
        }

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix(IRenderDimensions renderDimensions)
        {
            var sx = _sx?.AsDouble() ?? 1.0;
            var sy = _sy?.AsDouble() ?? 1.0;
            var sz = _sz?.AsDouble() ?? 1.0;
            return new TransformMatrix(sx, 0f, 0f, 0f, sy, 0f, 0f, 0f, sz, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var sx = _sx?.Compute(context);
            var sy = _sy?.Compute(context);
            var sz = _sz?.Compute(context);
            return new CssScaleValue(sx, sy, sz);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssScaleValue value && Equals(value);

        #endregion
    }
}
