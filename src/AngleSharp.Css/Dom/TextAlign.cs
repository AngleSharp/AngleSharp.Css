namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// An enumeration with all possible text alignments.
    /// </summary>
    public enum TextAlign: byte
    {
        /// <summary>
        /// The inline contents are aligned to the left edge of the line box.
        /// This is the default value for table data.
        /// </summary>
        Left,
        /// <summary>
        /// The inline contents are centered within the line box. This is
        /// the default value for table headers.
        /// </summary>
        Center,
        /// <summary>
        /// The inline contents are aligned to the right edge of the line box.
        /// </summary>
        Right,
        /// <summary>
        /// The text is justified. Text should line up their left and right
        /// edges to the left and right content edges of the paragraph.
        /// </summary>
        Justify,
        /// <summary>
        /// The same as left if direction is left-to-right and right if direction is right-to-left.
        /// </summary>
        Start,
        /// <summary>
        /// The same as right if direction is left-to-right and left if direction is right-to-left.
        /// </summary>
        End,
        /// <summary>
        /// Same as justify, but also forces the last line to be justified.
        /// </summary>
        JustifyAll,
        /// <summary>
        /// Similar to inherit, but the values start and end are calculated according to the parent's direction and are replaced by the appropriate left or right value.
        /// </summary>
        MatchParent,
    }
}