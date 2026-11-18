namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a translation, i.e., translate() or translate3d().
    /// https://www.w3.org/TR/css-typed-om-1/#csstranslate
    /// </summary>
    [DomName("CSSTranslate")]
    public sealed class CssTranslate : CssTransformComponent
    {
        private CssNumericValue _x;
        private CssNumericValue _y;
        private CssNumericValue _z;

        /// <summary>
        /// Creates a new translation. Without a z value it is a 2D translation.
        /// </summary>
        /// <param name="x">The shift along the x-axis (length or percentage).</param>
        /// <param name="y">The shift along the y-axis (length or percentage).</param>
        /// <param name="z">The optional shift along the z-axis (length).</param>
        /// <exception cref="ArgumentException">A value has the wrong type.</exception>
        [DomConstructor]
        public CssTranslate(CssNumericValue x, CssNumericValue y, CssNumericValue? z = null)
            : base(z is null)
        {
            _x = Require(x, nameof(x), IsLengthPercentage, "a length or percentage");
            _y = Require(y, nameof(y), IsLengthPercentage, "a length or percentage");
            _z = z is null ? new CssUnitValue(0.0, "px") : Require(z, nameof(z), IsLength, "a length");
        }

        /// <summary>
        /// Gets or sets the shift along the x-axis.
        /// </summary>
        [DomName("x")]
        public CssNumericValue X
        {
            get => _x;
            set => _x = Require(value, nameof(value), IsLengthPercentage, "a length or percentage");
        }

        /// <summary>
        /// Gets or sets the shift along the y-axis.
        /// </summary>
        [DomName("y")]
        public CssNumericValue Y
        {
            get => _y;
            set => _y = Require(value, nameof(value), IsLengthPercentage, "a length or percentage");
        }

        /// <summary>
        /// Gets or sets the shift along the z-axis.
        /// </summary>
        [DomName("z")]
        public CssNumericValue Z
        {
            get => _z;
            set => _z = Require(value, nameof(value), IsLength, "a length");
        }

        /// <summary>
        /// Computes the matrix of the translation.
        /// </summary>
        public override TransformMatrix ToMatrix()
        {
            var m = Identity();
            m[0, 3] = _x.To("px").Value;
            m[1, 3] = _y.To("px").Value;
            m[2, 3] = Is2D ? 0.0 : _z.To("px").Value;
            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the translation.
        /// </summary>
        public override String ToString() => Is2D ?
            String.Concat("translate(", Join(_x, _y), ")") :
            String.Concat("translate3d(", Join(_x, _y, _z), ")");
    }
}
