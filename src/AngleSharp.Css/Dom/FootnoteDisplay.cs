namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// Represents the selected footnote display mode.
    /// </summary>
    public enum FootnoteDisplay
    {
        /// <summary>
        /// The footnote element is placed in the footnote
        /// area as a block element.
        /// </summary>
        Block,
        /// <summary>
        /// The footnote element is placed in the footnote
        /// area as an inline element.
        /// </summary>
        Inline,
        /// <summary>
        /// The user agent determines whether a given footnote
        /// element is placed as a block element or an inline
        /// element. If two or more footnotes could fit on the
        /// same line in the footnote area, they should be
        /// placed inline.
        /// </summary>
        Compact,
    }
}
