namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Css;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Reading inherited properties on a deep tree built with the DOM API: visibility is
    /// inherited and declared nowhere, so it is looked up through all ancestors. The depths stay
    /// within what the recursive walks over the tree survive on a 1 MB stack.
    /// </summary>
    [MemoryDiagnoser]
    public class DeepTreeBenchmarks
    {
        private IDocument _document = null!;
        private IRenderNode _tree = null!;

        [Params(100, 500)]
        public Int32 Depth { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var context = BrowsingContext.New(Configuration.Default.WithCss());
            _document = context.GetService<IHtmlParser>()!.ParseDocument("<style>div{color:red} div div{margin-left:1px}</style>");
            IElement current = _document.Body!;

            for (var i = 0; i < Depth; i++)
            {
                var child = _document.CreateElement("div");
                current.AppendChild(child);
                current = child;
            }

            current.TextContent = "x";
            _tree = _document.DefaultView!.Render();
        }

        [Benchmark]
        public Int32 InnerText() => _document.Body!.GetInnerText().Length;

        [Benchmark]
        public Int32 IsVisible()
        {
            // the render tree is built once, so only the lookups are measured
            var pending = new Stack<IRenderNode>();
            var visible = 0;
            pending.Push(_tree);

            while (pending.Count > 0)
            {
                var node = pending.Pop();

                if (node.IsVisible())
                {
                    visible++;
                }

                foreach (var child in node.Children)
                {
                    pending.Push(child);
                }
            }

            return visible;
        }
    }
}
