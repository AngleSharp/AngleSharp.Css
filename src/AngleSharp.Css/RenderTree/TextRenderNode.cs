namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Dom;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a rendered element.
    /// </summary>
    /// <remarks>
    /// Constructs a new rendered text.
    /// </remarks>
    /// <param name="reference">The reference to the original text node.</param>
    /// <param name="parent">The used parent element.</param>
    public sealed class TextRenderNode(IText reference, ElementRenderNode parent) : IRenderNode
    {

        /// <summary>
        /// Gets a reference to the text node.
        /// </summary>
        public IText Ref { get; } = reference;

        INode IRenderNode.Ref => Ref;

        /// <summary>
        /// Gets the contained render nodes.
        /// </summary>
        public IEnumerable<IRenderNode> Children => Enumerable.Empty<IRenderNode>();

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        public IRenderNode? Parent { get; } = parent;
    }
}
