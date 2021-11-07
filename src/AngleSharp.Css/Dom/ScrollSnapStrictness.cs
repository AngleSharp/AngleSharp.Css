namespace AngleSharp.Css.Dom
{
	/// <summary>
	/// An enumeration with all possible scroll snap strictness options.
	/// </summary>
	public enum ScrollSnapStrictness : byte
	{
        /// <summary>
        /// If specified on a scroll container, the scroll container must not snap
        /// </summary>
        None,
        /// <summary>
        /// If specified on a scroll container, the scroll container is required to be snapped
        /// to a snap position when there are no active scrolling operations.
        /// If a valid snap position exists then the scroll container must snap
        /// at the termination of a scroll (if none exist then no snapping occurs)
        /// </summary>
        Mandatory,
        /// <summary>
        /// If specified on a scroll container, the scroll container may snap to a snap position
        /// at the termination of a scroll, at the discretion of the UA given the parameters of the scroll
        /// </summary>
        Proximity
	}
}
