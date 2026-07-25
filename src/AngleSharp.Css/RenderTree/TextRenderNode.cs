namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Dom;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a rendered element.
    /// </summary>
    public sealed class TextRenderNode : IRenderNode
    {
        private readonly IText _reference;
        private readonly ElementRenderNode _parent;

        /// <summary>
        /// Constructs a new rendered text.
        /// </summary>
        /// <param name="reference">The reference to the original text node.</param>
        /// <param name="parent">The used parent element.</param>
        public TextRenderNode(IText reference, ElementRenderNode parent)
        {
            _reference = reference;
            _parent = parent;
        }

        /// <summary>
        /// Gets a reference to the text node.
        /// </summary>
        public IText Ref => _reference;

        INode IRenderNode.Ref => Ref;

        /// <summary>
        /// Gets the contained render nodes.
        /// </summary>
        public IEnumerable<IRenderNode> Children => Enumerable.Empty<IRenderNode>();

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        public IRenderNode? Parent => _parent;
    }
}