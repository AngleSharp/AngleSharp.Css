namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The ScrollBehavior enumeration.
    /// </summary>
    [DomName("ScrollBehavior")]
    [DomLiterals]
    public enum ScrollBehavior
    {
        /// <summary>
        /// The auto value.
        /// </summary>
        [DomName("auto")]
        Auto,
        /// <summary>
        /// The instant value.
        /// </summary>
        [DomName("instant")]
        Instant,
        /// <summary>
        /// The smooth value.
        /// </summary>
        [DomName("smooth")]
        Smooth
    }
}
