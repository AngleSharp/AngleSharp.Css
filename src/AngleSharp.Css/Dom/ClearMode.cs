namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// The enumeration with all possible clear modes.
    /// </summary>
    public enum ClearMode : byte
    {
        /// <summary>
        /// The element is not moved down to clear past floating elements.
        /// </summary>
        None,
        /// <summary>
        /// The element is moved down to clear past left floats.
        /// </summary>
        Left,
        /// <summary>
        /// The element is moved down to clear past right floats.
        /// </summary>
        Right,
        /// <summary>
        /// The element is moved down to clear past both left and right floats.
        /// </summary>
        Both,
        /// <summary>
        /// The element is moved below the floats on the start side.
        /// </summary>
        InlineStart,
        /// <summary>
        /// The element is moved below the floats on the end side.
        /// </summary>
        InlineEnd
    }
}
