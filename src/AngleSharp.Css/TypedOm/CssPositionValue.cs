namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a position, e.g., of object-position, given by two offsets.
    /// https://www.w3.org/TR/css-typed-om-1/#positionvalue-objects
    /// </summary>
    [DomName("CSSPositionValue")]
    public sealed class CssPositionValue : CssStyleValue
    {
        private CssNumericValue _x;
        private CssNumericValue _y;

        /// <summary>
        /// Creates a new position.
        /// </summary>
        /// <param name="x">The offset from the left edge.</param>
        /// <param name="y">The offset from the top edge.</param>
        /// <exception cref="ArgumentException">An offset is not a length-percentage.</exception>
        [DomConstructor]
        public CssPositionValue(CssNumericValue x, CssNumericValue y)
        {
            _x = Validate(x, nameof(x));
            _y = Validate(y, nameof(y));
        }

        /// <summary>
        /// Gets or sets the offset from the left edge.
        /// </summary>
        [DomName("x")]
        public CssNumericValue X
        {
            get => _x;
            set => _x = Validate(value, nameof(value));
        }

        /// <summary>
        /// Gets or sets the offset from the top edge.
        /// </summary>
        [DomName("y")]
        public CssNumericValue Y
        {
            get => _y;
            set => _y = Validate(value, nameof(value));
        }

        /// <summary>
        /// Serializes the position, e.g., 10px 50%.
        /// </summary>
        public override String ToString() => String.Concat(_x.ToString(), " ", _y.ToString());

        private static CssNumericValue Validate(CssNumericValue value, String name)
        {
            if (value is null)
                throw new ArgumentNullException(name);

            if (!value.NumericType.MatchesLengthPercentage())
                throw new ArgumentException("The offset has to be a length or percentage.", name);

            return value;
        }
    }
}
