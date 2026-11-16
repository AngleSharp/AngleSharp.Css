namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The operators of math values.
    /// https://www.w3.org/TR/css-typed-om-1/#enumdef-cssmathoperator
    /// </summary>
    [DomName("CSSMathOperator")]
    [DomLiterals]
    public enum CssMathOperator : byte
    {
        /// <summary>
        /// The sum of the values.
        /// </summary>
        [DomName("sum")]
        Sum,
        /// <summary>
        /// The product of the values.
        /// </summary>
        [DomName("product")]
        Product,
        /// <summary>
        /// The negation of a value.
        /// </summary>
        [DomName("negate")]
        Negate,
        /// <summary>
        /// The reciprocal of a value.
        /// </summary>
        [DomName("invert")]
        Invert,
        /// <summary>
        /// The minimum of the values.
        /// </summary>
        [DomName("min")]
        Min,
        /// <summary>
        /// The maximum of the values.
        /// </summary>
        [DomName("max")]
        Max,
        /// <summary>
        /// A value clamped between a lower and an upper bound.
        /// </summary>
        [DomName("clamp")]
        Clamp,
    }
}
