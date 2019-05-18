namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Dom;
    using System.Collections.Generic;

    interface IRenderNode
    {
        INode Ref { get; }

        IEnumerable<IRenderNode> Children { get; }
    }
}
