namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// Represents a perspective, i.e., perspective().
    /// https://www.w3.org/TR/css-typed-om-1/#cssperspective
    /// </summary>
    [DomName("CSSPerspective")]
    public sealed class CssPerspective : CssTransformComponent
    {
        private CssStyleValue _length;

        /// <summary>
        /// Creates a new perspective.
        /// </summary>
        /// <param name="length">The distance, i.e., a length or the keyword none.</param>
        /// <exception cref="ArgumentException">The value is neither a length nor none.</exception>
        [DomConstructor]
        public CssPerspective(CssStyleValue length)
            : base(false)
        {
            _length = Validate(length, nameof(length));
        }

        /// <summary>
        /// Gets or sets the distance, i.e., a length or the keyword none.
        /// </summary>
        [DomName("length")]
        public CssStyleValue Length
        {
            get => _length;
            set => _length = Validate(value, nameof(value));
        }

        /// <summary>
        /// Computes the matrix of the perspective, which is the identity in 2D or for none.
        /// </summary>
        public override TransformMatrix ToMatrix()
        {
            var m = Identity();

            if (!Is2D && _length is CssNumericValue length)
            {
                var distance = Math.Max(length.To("px").Value, 1.0);
                m[3, 2] = -1.0 / distance;
            }

            return CreateMatrix(m);
        }

        /// <summary>
        /// Serializes the perspective.
        /// </summary>
        public override String ToString() => String.Concat("perspective(", _length.ToString(), ")");

        private static CssStyleValue Validate(CssStyleValue value, String name)
        {
            if (value is null)
                throw new ArgumentNullException(name);

            if (value is CssKeywordValue keyword && keyword.Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                return value;

            if (value is CssNumericValue numeric && IsLength(numeric.NumericType))
                return value;

            throw new ArgumentException("The value has to be a length or none.", name);
        }
    }
}
