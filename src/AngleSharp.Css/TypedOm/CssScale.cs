namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a scaling, i.e., scale() or scale3d().
    /// https://www.w3.org/TR/css-typed-om-1/#cssscale
    /// </summary>
    [DomName("CSSScale")]
    public sealed class CssScale : CssTransformComponent
    {
        private CssNumericValue _x;
        private CssNumericValue _y;
        private CssNumericValue _z;

        /// <summary>
        /// Creates a new scaling. Without a z value it is a 2D scaling.
        /// </summary>
        /// <param name="x">The factor along the x-axis.</param>
        /// <param name="y">The factor along the y-axis.</param>
        /// <param name="z">The optional factor along the z-axis.</param>
        /// <exception cref="ArgumentException">A value is not a number.</exception>
        [DomConstructor]
        public CssScale(CssNumericValue x, CssNumericValue y, CssNumericValue? z = null)
            : base(z is null)
        {
            _x = Require(x, nameof(x), IsNumber, "a number");
            _y = Require(y, nameof(y), IsNumber, "a number");
            _z = z is null ? 1.0 : Require(z, nameof(z), IsNumber, "a number");
        }

        /// <summary>
        /// Gets or sets the factor along the x-axis.
        /// </summary>
        [DomName("x")]
        public CssNumericValue X
        {
            get => _x;
            set => _x = Require(value, nameof(value), IsNumber, "a number");
        }

        /// <summary>
        /// Gets or sets the factor along the y-axis.
        /// </summary>
        [DomName("y")]
        public CssNumericValue Y
        {
            get => _y;
            set => _y = Require(value, nameof(value), IsNumber, "a number");
        }

        /// <summary>
        /// Gets or sets the factor along the z-axis.
        /// </summary>
        [DomName("z")]
        public CssNumericValue Z
        {
            get => _z;
            set => _z = Require(value, nameof(value), IsNumber, "a number");
        }

        /// <summary>
        /// Computes the matrix of the scaling.
        /// </summary>
        public override TransformMatrix ToMatrix()
        {
            var m = Identity();
            m[0, 0] = _x.To("number").Value;
            m[1, 1] = _y.To("number").Value;
            m[2, 2] = Is2D ? 1.0 : _z.To("number").Value;
            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the scaling.
        /// </summary>
        public override String ToString() => Is2D ?
            String.Concat("scale(", Join(_x, _y), ")") :
            String.Concat("scale3d(", Join(_x, _y, _z), ")");
    }
}
