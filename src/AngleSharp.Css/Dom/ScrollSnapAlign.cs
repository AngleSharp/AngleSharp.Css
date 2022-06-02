namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// An enumeration with all possible scroll snap align options.
    /// </summary>
    public enum ScrollSnapAlign : byte
    {
        /// <summary>
        /// This box does not define a snap position in the specified axis
        /// </summary>
        None,
        /// <summary>
        /// Start alignment of this box’s scroll snap area within the scroll container’s snapport is a snap position in the specified axis
        /// </summary>
        Start,
        /// <summary>
        /// End alignment of this box’s scroll snap area within the scroll container’s snapport is a snap position in the specified axis
        /// </summary>
        End,
        /// <summary>
        /// Center alignment of this box’s scroll snap area within the scroll container’s snapport is a snap position in the specified axis
        /// </summary>
        Center,
    }
}
