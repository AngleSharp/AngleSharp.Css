namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Container for the ScrollOptions DOM interface.
    /// </summary>
    [DomName("ScrollOptions")]
    public class ScrollOptions
    {
        /// <summary>
        /// Gets or sets the scroll behavior. By default set to auto.
        /// </summary>
        [DomName("behavior")]
        public ScrollBehavior Behavior { get; set; } = ScrollBehavior.Auto;
    }
}
