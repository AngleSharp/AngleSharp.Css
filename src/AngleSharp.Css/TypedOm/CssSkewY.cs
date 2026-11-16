namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a skewing along the y-axis, i.e., skewY().
    /// https://www.w3.org/TR/css-typed-om-1/#cssskewy
    /// </summary>
    [DomName("CSSSkewY")]
    public sealed class CssSkewY : CssTransformComponent
    {
        private CssNumericValue _ay;

        /// <summary>
        /// Creates a new skewing along the y-axis.
        /// </summary>
        /// <param name="ay">The angle along the y-axis.</param>
        /// <exception cref="ArgumentException">The value is not an angle.</exception>
        [DomConstructor]
        public CssSkewY(CssNumericValue ay)
            : base(true)
        {
            _ay = Require(ay, nameof(ay), IsAngle, "an angle");
        }

        /// <summary>
        /// Gets or sets the angle along the y-axis.
        /// </summary>
        [DomName("ay")]
        public CssNumericValue Ay
        {
            get => _ay;
            set => _ay = Require(value, nameof(value), IsAngle, "an angle");
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
            m[1, 0] = Math.Tan(_ay.To("rad").Value);
            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the skewing.
        /// </summary>
        public override String ToString() => String.Concat("skewY(", _ay.ToString(), ")");
    }
}
