namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    sealed class RenderTreeBuilder
    {
        private static readonly ConditionalWeakTable<IWindow, RenderTreeBuilder> _windowRenderTrees = new();

        private readonly IBrowsingContext _context;
        private readonly IWindow _window;
        private readonly List<ICssStyleSheet> _defaultSheets;
        private readonly Dictionary<IElement, ICssStyleDeclaration> _cascadedStyles = new();

        private RenderTreeBuilder(IWindow window)
        {
            var ctx = window.Document.Context;
            var defaultStyleSheetProvider = ctx.GetServices<ICssDefaultStyleSheetProvider>();
            _context = ctx;
            _defaultSheets = defaultStyleSheetProvider.Select(m => m.Default).ToList();
            _window = window;
        }

        public IRenderDevice DefaultDevice => _context.GetService<IRenderDevice>() ?? new DefaultRenderDevice();

        public static RenderTreeBuilder GetInstance(IWindow window) => _windowRenderTrees.GetValue(window, (IWindow w) => new RenderTreeBuilder(w));

        public ElementRenderNode RenderDocument(IRenderDevice? device = null)
        {
            var document = _window.Document;
            var root = document.DocumentElement ?? throw new InvalidOperationException("The document does not have a root element.");
            var collection = CreateStyleCollection(device ?? DefaultDevice);

            _cascadedStyles.Clear();

            return RenderElement(root, collection, null, null, null);
        }

        public ICssStyleDeclaration GetElementStyle(IElement element, IRenderDevice? device = null)
        {
            if (_cascadedStyles.TryGetValue(element, out var cascadedStyle))
            {
                return cascadedStyle;
            }

            var collection = CreateStyleCollection(device ?? DefaultDevice);
            cascadedStyle = collection.GetDeclarations(element);
            _cascadedStyles[element] = cascadedStyle;
            return cascadedStyle;
        }

        public ElementRenderNode RenderElement(IElement element, IRenderDevice device)
        {
            var collection = CreateStyleCollection(device);
            var parent = element.ParentElement;
            var parentSpecifiedStyle = parent is not null ? collection.GetDeclarations(parent) : null;
            var parentComputedStyle = parent is not null ? collection.ComputeDeclarations(parent) : null;

            _cascadedStyles.Clear();

            return RenderElement(element, collection, null, parentSpecifiedStyle, parentComputedStyle);
        }

        private StyleCollection CreateStyleCollection(IRenderDevice device)
        {
            var document = _window.Document;
            var currentSheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
            var stylesheets = _defaultSheets.Concat(currentSheets).ToList();
            return new StyleCollection(stylesheets, device);
        }

        private ElementRenderNode RenderElement(
            IElement element,
            StyleCollection collection,
            ElementRenderNode? parent,
            ICssStyleDeclaration? parentSpecifiedStyle,
            ICssStyleDeclaration? parentComputedStyle)
        {
            var explicitStyle = collection.ComputeExplicitStyle(element);

            var specifiedStyle = new CssStyleDeclaration(_context);
            specifiedStyle.SetDeclarations(explicitStyle);

            if (parentSpecifiedStyle is not null)
            {
                specifiedStyle.UpdateDeclarations(parentSpecifiedStyle);
            }

            var computeContext = new CssComputeContext(collection.Device, _context, explicitStyle, parentComputedStyle);
            var computedStyle = explicitStyle.PrepareComputedDeclarations(parentComputedStyle!, computeContext).Compute(computeContext);
            var children = new List<IRenderNode>();
            var node = new ElementRenderNode(element, parent, children, specifiedStyle, computedStyle);

            _cascadedStyles[element] = specifiedStyle;

            foreach (var child in element.ChildNodes)
            {
                if (child is IElement childElement)
                {
                    children.Add(RenderElement(childElement, collection, node, specifiedStyle, computedStyle));
                }
                else if (child is IText childText)
                {
                    children.Add(new TextRenderNode(childText, node));
                }
            }

            return node;
        }
    }
}
