namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Dom;
    using System.Collections.Generic;
    using System.Linq;

    class TextRenderNode : IRenderNode
    {
        public INode Ref { get; set; }

        public IEnumerable<IRenderNode> Children => Enumerable.Empty<IRenderNode>();
    }
}
