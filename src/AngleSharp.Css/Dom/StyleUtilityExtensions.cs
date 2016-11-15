namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Dom;

    /// <summary>
    /// A set of useful methods for retrieving style information.
    /// </summary>
    [DomExposed("PseudoElement")]
    public static class StyleUtilityExtensions
    {
        /// <summary>
        /// Gets a live CSS declaration block with properties
        /// that have a cascaded value for the context object.
        /// </summary>
        [DomName("cascadedStyle")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetCascadedStyle(this IPseudoElement element)
        {
            return element.Owner.DefaultView.GetStyleCollection().ComputeCascadedStyle(element);
        }

        /// <summary>
        /// Gets a live CSS declaration block with only the default
        /// properties representing the value for the context object.
        /// </summary>
        [DomName("defaultStyle")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetDefaultStyle(this IPseudoElement element)
        {
            return element.Owner.DefaultView.ComputeDefaultStyle(element);
        }

        /// <summary>
        /// Gets a live CSS declaration block with properties
        /// that represent the value computed for the context object.
        /// </summary>
        [DomName("rawComputedStyle")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetRawComputedStyle(this IPseudoElement element)
        {
            return element.Owner.DefaultView.ComputeRawStyle(element);
        }

        /// <summary>
        /// Gets a live CSS declaration block with properties,
        /// whcih are the used values computed for the context object.
        /// </summary>
        [DomName("usedStyle")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetUsedStyle(this IPseudoElement element)
        {
            return element.Owner.DefaultView.ComputeUsedStyle(element);
        }
    }
}
