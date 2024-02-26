namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the distance transformation.
    /// </summary>
    sealed class CssPerspectiveValue : ICssTransformFunctionValue, IEquatable<CssPerspectiveValue>
    {
        #region Fields

        private readonly ICssValue _distance;

        #endregion

        #region ctor

        internal CssPerspectiveValue(ICssValue distance)
        {
            _distance = distance;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Perspective;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _distance };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_distance.CssText);

        /// <summary>
        /// Gets the distance from the origin.
        /// </summary>
        public ICssValue Distance => _distance;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssPerspectiveValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_distance, other._distance);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssPerspectiveValue value && Equals(value);

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix(IRenderDimensions renderDimensions)
        {
            var distance = _distance as CssLengthValue? ?? CssLengthValue.Zero;
            return new TransformMatrix(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0 / distance.ToPixel(renderDimensions, RenderMode.Undefined));
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var distance = _distance.Compute(context);

            if (distance != _distance)
            {
                return new CssPerspectiveValue(distance);
            }

            return this;
        }

        #endregion
    }
}
