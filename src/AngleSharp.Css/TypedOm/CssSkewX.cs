namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a skewing along the x-axis, i.e., skewX().
    /// https://www.w3.org/TR/css-typed-om-1/#cssskewx
    /// </summary>
    [DomName("CSSSkewX")]
    public sealed class CssSkewX : CssTransformComponent
    {
        private CssNumericValue _ax;

        /// <summary>
        /// Creates a new skewing along the x-axis.
        /// </summary>
        /// <param name="ax">The angle along the x-axis.</param>
        /// <exception cref="ArgumentException">The value is not an angle.</exception>
        [DomConstructor]
        public CssSkewX(CssNumericValue ax)
            : base(true)
        {
            _ax = Require(ax, nameof(ax), IsAngle, "an angle");
        }

        /// <summary>
        /// Gets or sets the angle along the x-axis.
        /// </summary>
        [DomName("ax")]
        public CssNumericValue Ax
        {
            get => _ax;
            set => _ax = Require(value, nameof(value), IsAngle, "an angle");
        }

        /// <summary>
        /// Gets if the skewing is 2D, which it always is.
        /// </summary>
        public override Boolean Is2D
        {
            get => true;
            set { }
        }

        /// <summary>
        /// Computes the matrix of the skewing.
        /// </summary>
        public override TransformMatrix ToMatrix()
        {
            var m = Identity();
            m[0, 1] = Math.Tan(_ax.To("rad").Value);
            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the skewing.
        /// </summary>
        public override String ToString() => String.Concat("skewX(", _ax.ToString(), ")");
    }
}
