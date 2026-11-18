namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The base types of a numeric value.
    /// https://www.w3.org/TR/css-typed-om-1/#enumdef-cssnumericbasetype
    /// </summary>
    [DomName("CSSNumericBaseType")]
    [DomLiterals]
    public enum CssNumericBaseType : byte
    {
        /// <summary>
        /// A length, e.g., px or em.
        /// </summary>
        [DomName("length")]
        Length,
        /// <summary>
        /// An angle, e.g., deg or rad.
        /// </summary>
        [DomName("angle")]
        Angle,
        /// <summary>
        /// A time, e.g., s or ms.
        /// </summary>
        [DomName("time")]
        Time,
        /// <summary>
        /// A frequency, e.g., hz or khz.
        /// </summary>
        [DomName("frequency")]
        Frequency,
        /// <summary>
        /// A resolution, e.g., dpi or dppx.
        /// </summary>
        [DomName("resolution")]
        Resolution,
        /// <summary>
        /// A flexible length, i.e., fr.
        /// </summary>
        [DomName("flex")]
        Flex,
        /// <summary>
        /// A percentage.
        /// </summary>
        [DomName("percent")]
        Percent,
    }
}
