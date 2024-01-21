namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rendered element.
    /// </summary>
    /// <remarks>
    /// Constructs a new rendered element.
    /// </remarks>
    /// <param name="reference">The reference to the original element.</param>
    /// <param name="parent">The used parent node if any.</param>
    /// <param name="children">The contained children.</param>
    /// <param name="specifiedStyle">The cascaded style for the element.</param>
    /// <param name="computedStyle">The computed style of the element.</param>
    public sealed class ElementRenderNode(IElement reference, ElementRenderNode? parent, IEnumerable<IRenderNode> children, ICssStyleDeclaration specifiedStyle, ICssStyleDeclaration computedStyle) : IRenderNode
    {

        /// <summary>
        /// Gets a reference to the element.
        /// </summary>
        public IElement Ref { get; } = reference;

        INode IRenderNode.Ref => Ref;

        /// <summary>
        /// Gets the contained render nodes.
        /// </summary>
        public IEnumerable<IRenderNode> Children { get; } = children;

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        public ElementRenderNode? Parent { get; } = parent;

        /// <summary>
        /// Gets the cascaded style of the element.
        /// </summary>
        public ICssStyleDeclaration SpecifiedStyle { get; } = specifiedStyle;

        /// <summary>
        /// Gets the computed style of the element.
        /// </summary>
        public ICssStyleDeclaration ComputedStyle { get; } = computedStyle;
    }
}
