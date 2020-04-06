namespace AngleSharp.Css
{
    /// <summary>
    /// Used by To/ToPixel. If known, defines the axis the unit is in.
    /// This is use to calculate relative units such as %
    /// </summary>
    public enum RenderMode
    {
        /// <summary>
        /// Should be used if the unit is absolute, or the axis is unknown.
        /// </summary>
        Undefined,
        /// <summary>
        /// The current axis is horizontal meaning percentage will refer to percentage of width
        /// </summary>
        Horizontal,
        /// <summary>
        /// The current axis is vertical meaning percentage will refer to percentage of height
        /// </summary>
        Vertical,
    }
}