namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// Represents the selected Width display mode.
    /// </summary>
    public enum Sizing
    {
        /// <summary>
        /// Essentially fit-content(stretch) i.e. min(max-content, max(min-content, stretch)).
        /// <seealso href="https://drafts.csswg.org/css-sizing-4/#valdef-width-fit-content"/>
        /// </summary>
        FitContent,
        /// <summary>
        /// The intrinsic preferred width.
        /// </summary>
        MaxContent,
        /// <summary>
        /// The intrinsic minimum width.
        /// </summary>
        MinContent,
    }
}
