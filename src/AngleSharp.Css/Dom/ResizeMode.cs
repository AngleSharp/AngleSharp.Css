namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// The values for resize.
    /// </summary>
    public enum ResizeMode
    {
        /// <summary>
        /// The element offers no user-controllable method for resizing it.
        /// </summary>
        None,
        /// <summary>
        /// The element displays a mechanism for allowing the user to resize it, which may be resized both horizontally and vertically.
        /// </summary>
        Both,
        /// <summary>
        /// The element displays a mechanism for allowing the user to resize it in the horizontal direction.
        /// </summary>
        Horizontal,
        /// <summary>
        /// The element displays a mechanism for allowing the user to resize it in the vertical direction.
        /// </summary>
        Vertical,
        /// <summary>
        /// The element displays a mechanism for allowing the user to resize it in the block direction.
        /// </summary>
        Block,
        /// <summary>
        /// The element displays a mechanism for allowing the user to resize it in the inline direction.
        /// </summary>
        Inline,
    }
}
