namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System.Collections.Generic;
    using System.Linq;

    class ElementRenderNode : IRenderNode
    {
        public INode Ref { get; set; }

        public IEnumerable<IRenderNode> Children { get; set; } = Enumerable.Empty<IRenderNode>();

        public ICssStyleDeclaration SpecifiedStyle { get; set; }

        public ICssStyleDeclaration ComputedStyle { get; set; }

        public RenderValues UsedValue { get; set; }

        public RenderValues ActualValue { get; set; }
    }
}
