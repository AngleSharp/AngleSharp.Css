namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the skew transformation.
    /// </summary>
    public sealed class CssSkewValue : ICssTransformFunctionValue, IEquatable<CssSkewValue>
    {
        #region Fields

        private readonly ICssValue _alpha;
        private readonly ICssValue _beta;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new skew transform.
        /// </summary>
        /// <param name="alpha">The alpha skewing angle.</param>
        /// <param name="beta">The beta skewing angle.</param>
        public CssSkewValue(ICssValue alpha, ICssValue beta)
        {
            _alpha = alpha;
            _beta = beta;
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
                if (_alpha != _beta)
                {
                    if (_alpha == null)
                    {
                        return FunctionNames.SkewY;
                    }
                    else if (_beta == null)
                    {
                        return FunctionNames.SkewX;
                    }
                }

                return FunctionNames.Skew;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[] { };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var args = _alpha?.CssText ?? String.Empty;

                if (_alpha != _beta)
                {
                    if (_alpha == null)
                    {
                        args = _beta.CssText;
                    }
                    else if (_beta != null && _beta.CssText != "0rad")
                    {
                        args = String.Concat(args, ", ", _beta.CssText);
                    }
                }

                return Name.CssFunction(args);
            }
        }

        /// <summary>
        /// Gets the value of the first angle.
        /// </summary>
        public ICssValue Alpha => _alpha;

        /// <summary>
        /// Gets the value of the second angle.
        /// </summary>
        public ICssValue Beta => _beta;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssSkewValue other)
        {
            return _alpha.Equals(other._alpha) && _beta.Equals(other._beta);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssSkewValue value && Equals(value);

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix(IRenderDimensions renderDimensions)
        {
            var a = Math.Tan((_alpha as CssAngleValue ? ?? CssAngleValue.Zero).ToRadian());
            var b = Math.Tan((_beta as CssAngleValue? ?? CssAngleValue.Zero).ToRadian());
            return new TransformMatrix(1.0, a, 0.0, b, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var alpha = _alpha.Compute(context);
            var beta = _beta.Compute(context);

            if (alpha != _alpha || beta != _beta)
            {
                return new CssSkewValue(alpha, beta);
            }

            return this;
        }

        #endregion
    }
}
