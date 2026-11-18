namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a skewing along both axes, i.e., skew().
    /// https://www.w3.org/TR/css-typed-om-1/#cssskew
    /// </summary>
    [DomName("CSSSkew")]
    public sealed class CssSkew : CssTransformComponent
    {
        private CssNumericValue _ax;
        private CssNumericValue _ay;

        /// <summary>
        /// Creates a new skewing.
        /// </summary>
        /// <param name="ax">The angle along the x-axis.</param>
        /// <param name="ay">The angle along the y-axis.</param>
        /// <exception cref="ArgumentException">A value is not an angle.</exception>
        [DomConstructor]
        public CssSkew(CssNumericValue ax, CssNumericValue ay)
            : base(true)
        {
            _ax = Require(ax, nameof(ax), IsAngle, "an angle");
            _ay = Require(ay, nameof(ay), IsAngle, "an angle");
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
            m[0, 1] = Math.Tan(_ax.To("rad").Value);
            m[1, 0] = Math.Tan(_ay.To("rad").Value);
            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the skewing, omitting a zero angle along the y-axis.
        /// </summary>
        public override String ToString() => _ay is CssUnitValue unit && unit.Value == 0.0 ?
            String.Concat("skew(", _ax.ToString(), ")") :
            String.Concat("skew(", Join(_ax, _ay), ")");
    }
}
