namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// The selected bookmark state.
    /// </summary>
    public enum BookmarkState
    {
        /// <summary>
        /// The bookmarks of the nearest descendants of an element
        /// with a bookmark-state of open will be displayed.
        /// </summary>
        Open,
        /// <summary>
        /// Any bookmarks of descendant elements are not initially
        /// displayed.
        /// </summary>
        Closed,
    }
}
