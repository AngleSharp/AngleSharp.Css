namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// Represents the selected footnote policy.
    /// </summary>
    public enum FootnotePolicy
    {
        /// <summary>
        /// The user agent chooses how to render footnotes, and may
        /// place the footnote body on a later page than the footnote
        /// reference. A footnote body must never be placed on a page
        /// before the footnote reference.
        /// </summary>
        Auto,
        /// <summary>
        /// If a given footnote body cannot be placed on the current
        /// page due to lack of space, the user agent introduces a
        /// forced page break at the start of the line containing
        /// the footnote reference, so that both the reference and
        /// the footnote body fall on the next page. Note that the
        /// user agent must honor widow and orphan settings when
        /// doing this, and so may need to insert the page break
        /// on an earlier line.
        /// </summary>
        Line,
        /// <summary>
        /// As with line, except a forced page break is introduced
        /// before the paragraph that contains the footnote.
        /// </summary>
        Block,
    }
}
