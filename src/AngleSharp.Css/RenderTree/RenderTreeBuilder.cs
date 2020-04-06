namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System.Collections.Generic;
    using System.Linq;

    class RenderTreeBuilder
    {
        private readonly IWindow _window;
        private readonly IEnumerable<ICssStyleSheet> _defaultSheets;
        private readonly IRenderDevice _device;

        public RenderTreeBuilder(IWindow window, IRenderDevice device = null)
        {
            var ctx = window.Document.Context;
            var defaultStyleSheetProvider = ctx.GetServices<ICssDefaultStyleSheetProvider>();
            _device = device ?? ctx.GetService<IRenderDevice>();
            _defaultSheets = defaultStyleSheetProvider.Select(m => m.Default).Where(m => m != null);
            _window = window;
        }

        public IRenderNode RenderDocument()
        {
            var document = _window.Document;
            var currentSheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
            var stylesheets = _defaultSheets.Concat(currentSheets);
            var collection = new StyleCollection(stylesheets, _device);
            return RenderElement(document.DocumentElement, collection);
        }

        private ElementRenderNode RenderElement(IElement reference, StyleCollection collection, ICssStyleDeclaration parent = null)
        {
            var style = collection.ComputeCascadedStyle(reference, parent);
            var children = new List<IRenderNode>();

            foreach (var child in reference.ChildNodes)
            {
                if (child is IText text)
                {
                    children.Add(RenderText(text, collection));
                }
                else  if (child is IElement element)
                {
                    children.Add(RenderElement(element, collection, style));
                }
            }

            return new ElementRenderNode
            {
                Ref = reference,
                SpecifiedStyle = style,
                ComputedStyle = style.Compute(_device),
                Children = children,
            };
        }

        private IRenderNode RenderText(IText text, StyleCollection collection) => new TextRenderNode
        {
            Ref = text,
        };
    }
}
