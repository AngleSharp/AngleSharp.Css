namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using System;

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
            var document = element.Owner;
            var window = document?.DefaultView ?? throw new InvalidOperationException("Requires an associated Window to be computed.");
            var device = document.Context.GetService<IRenderDevice>();
            var renderTree = RenderTreeBuilder.GetInstance(window);
            return renderTree.GetElementStyle(element, device);
        }

        /// <summary>
        /// Gets a live CSS declaration block with only the default
        /// properties representing the value for the context object.
        /// </summary>
        [DomName("defaultStyle")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetDefaultStyle(this IPseudoElement element)
        {
            var document = element.Owner;
            var window = document?.DefaultView ?? throw new InvalidOperationException("Requires an associated Window to be computed.");
            return window.ComputeDefaultStyle(element);
        }

        /// <summary>
        /// Gets a live CSS declaration block with properties
        /// that represent the value computed for the context object.
        /// </summary>
        [DomName("rawComputedStyle")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetRawComputedStyle(this IPseudoElement element)
        {
            var document = element.Owner;
            var window = document?.DefaultView ?? throw new InvalidOperationException("Requires an associated Window to be computed.");
            return window.ComputeRawStyle(element);
        }
    }
}
