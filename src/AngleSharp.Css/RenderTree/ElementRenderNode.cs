namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a rendered element.
    /// </summary>
    public sealed class ElementRenderNode : IRenderNode
    {
        private readonly IElement _reference;
        private readonly ElementRenderNode? _parent;
        private readonly IEnumerable<IRenderNode> _children;
        private readonly ICssStyleDeclaration _specifiedStyle;
        private readonly ICssStyleDeclaration _computedStyle;

        /// <summary>
        /// Constructs a new rendered element.
        /// </summary>
        /// <param name="reference">The reference to the original element.</param>
        /// <param name="parent">The used parent node if any.</param>
        /// <param name="children">The contained children.</param>
        /// <param name="specifiedStyle">The cascaded style for the element.</param>
        /// <param name="computedStyle">The computed style of the element.</param>
        public ElementRenderNode(
            IElement reference,
            ElementRenderNode? parent,
            IEnumerable<IRenderNode> children,
            ICssStyleDeclaration specifiedStyle,
            ICssStyleDeclaration computedStyle)
        {
            _reference = reference;
            _parent = parent;
            _children = children;
            _specifiedStyle = specifiedStyle;
            _computedStyle = computedStyle;
        }

        /// <summary>
        /// Gets a reference to the element.
        /// </summary>
        public IElement Ref => _reference;

        INode IRenderNode.Ref => Ref;

        /// <summary>
        /// Gets the contained render nodes.
        /// </summary>
        public IEnumerable<IRenderNode> Children => _children;

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        public ElementRenderNode? Parent => _parent;

        /// <summary>
        /// Gets the cascaded style of the element.
        /// </summary>
        public ICssStyleDeclaration SpecifiedStyle => _specifiedStyle;

        /// <summary>
        /// Gets the computed style of the element.
        /// </summary>
        public ICssStyleDeclaration ComputedStyle => _computedStyle;
    }
}
