namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// An enumeration with possible overflow modes.
    /// </summary>
    public enum OverflowMode : byte
    {
        /// <summary>
        /// The overflow-mode is determined by the renderer.
        /// </summary>
        Auto,
        /// <summary>
        /// The content is allowed to overflow.
        /// </summary>
        Visible,
        /// <summary>
        /// The content is cut to prevent overflowing.
        /// </summary>
        Hidden,
        /// <summary>
        /// The content can be scrolled.
        /// </summary>
        Scroll,
        /// <summary>
        /// The content must be directionally clipped.
        /// This only applies to overflow-x or overflow-y.
        /// </summary>
        Clip
    }
}
