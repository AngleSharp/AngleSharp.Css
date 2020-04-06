namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Dom;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a render node.
    /// </summary>
    public interface IRenderNode
    {
        /// <summary>
        /// References the original DOM node.
        /// </summary>
        INode Ref { get; }

        /// <summary>
        /// References the contained render children.
        /// </summary>
        IEnumerable<IRenderNode> Children { get; }
    }
}
