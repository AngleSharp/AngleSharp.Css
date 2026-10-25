namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css;
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
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
            return new StyleCollection(_defaultSheets, currentSheets.ToList(), device);
        }

        private ElementRenderNode RenderElement(
            IElement element,
            StyleCollection collection,
            ElementRenderNode? parent,
            ICssStyleDeclaration? parentSpecifiedStyle,
            ICssStyleDeclaration? parentComputedStyle,
            ICssStyleDeclaration? explicitStyle = null,
            StyleCollection? slottedCollection = null)
        {
            var shadowCollection = CreateShadowCollection(element, collection);
            explicitStyle ??= shadowCollection is not null
                ? collection.ComputeHostStyle(shadowCollection, element)
                : collection.ComputeExplicitStyle(element);

            var specifiedStyle = new CssStyleDeclaration(_context);
            specifiedStyle.SetDeclarations(explicitStyle);

            if (parentSpecifiedStyle is not null)
            {
                specifiedStyle.InheritFrom(parentSpecifiedStyle);
            }

            var computeContext = new CssComputeContext(collection.Device, _context, explicitStyle, parentComputedStyle);
            var computedStyle = explicitStyle.PrepareComputedDeclarations(parentComputedStyle!, computeContext).Compute(computeContext).Blockify(element);
            var children = new List<IRenderNode>();
            var node = new ElementRenderNode(element, parent, children, specifiedStyle, computedStyle);

            _cascadedStyles[element] = specifiedStyle;

            if (element is IPseudoElement)
            {
                // A ::before/::after pseudo-element has no DOM children of its own - IElement.ChildNodes
                // on the wrapper aliases its host's real children, which the generic walk below would
                // otherwise duplicate into the render tree under the pseudo. Its only "child" is the
                // text its own computed `content` generates (attr()/literal strings resolved against
                // this same element, since a pseudo-element has no attributes of its own to differ from
                // its host's).
                children.Add(new TextRenderNode(_window.Document.CreateTextNode(ContentDeclaration.Stringify(computedStyle, element)), node));
                return node;
            }

            var beforeNode = RenderPseudoElement(element, "before", collection, node, specifiedStyle, computedStyle);

            if (beforeNode is not null)
            {
                children.Add(beforeNode);
            }

            foreach (var child in GetFlatChildren(element, collection, shadowCollection, slottedCollection))
            {
                if (child.Node is IElement childElement)
                {
                    children.Add(RenderElement(childElement, child.Collection, node, specifiedStyle, computedStyle, slottedCollection: child.SlottedCollection));
                }
                else if (child.Node is IText childText)
                {
                    children.Add(new TextRenderNode(childText, node));
                }
            }

            var afterNode = RenderPseudoElement(element, "after", collection, node, specifiedStyle, computedStyle);

            if (afterNode is not null)
            {
                children.Add(afterNode);
            }

            return node;
        }

        /// <summary>
        /// The children of the element in the flat tree: a shadow host shows its shadow tree, which
        /// is styled by the shadow root's own style sheets, and a slot in there shows the light
        /// nodes assigned to it (styled as part of the host's tree) or else its own children.
        /// </summary>
        private static IEnumerable<FlatChild> GetFlatChildren(
            IElement element,
            StyleCollection collection,
            StyleCollection? shadowCollection,
            StyleCollection? slottedCollection)
        {
            if (shadowCollection is not null)
            {
                return ExpandSlots(element.ShadowRoot!.ChildNodes, shadowCollection, collection);
            }

            return ExpandSlots(element.ChildNodes, collection, slottedCollection);
        }

        /// <summary>
        /// Creates the styles of the shadow tree of a shadow host, i.e. the ones from the shadow
        /// root's own style sheets, which also style the host via :host.
        /// </summary>
        private StyleCollection? CreateShadowCollection(IElement element, StyleCollection collection)
        {
            if (element is IPseudoElement || element.ShadowRoot is not IShadowRoot shadowRoot)
            {
                return null;
            }

            var shadowSheets = shadowRoot.StyleSheets.OfType<ICssStyleSheet>().ToList();
            return new StyleCollection(_defaultSheets, shadowSheets, collection.Device, element);
        }

        private static IEnumerable<FlatChild> ExpandSlots(
            INodeList nodes,
            StyleCollection collection,
            StyleCollection? slottedCollection)
        {
            foreach (var node in nodes)
            {
                if (slottedCollection is not null && node is IHtmlSlotElement slot)
                {
                    // a hidden slot hides what it shows, e.g. :host(:not([open])) slot { display: none }
                    if (collection.ComputeExplicitStyle(slot).GetPropertyValue(PropertyNames.Display).Is(CssKeywords.None))
                    {
                        continue;
                    }

                    var assigned = slot.GetDistributedNodes().ToList();

                    if (assigned.Count > 0)
                    {
                        foreach (var assignedNode in assigned)
                        {
                            yield return new FlatChild(assignedNode, slottedCollection, null);
                        }
                    }
                    else
                    {
                        foreach (var fallback in ExpandSlots(slot.ChildNodes, collection, slottedCollection))
                        {
                            yield return fallback;
                        }
                    }
                }
                else
                {
                    yield return new FlatChild(node, collection, slottedCollection);
                }
            }
        }

        private readonly struct FlatChild
        {
            public FlatChild(INode node, StyleCollection collection, StyleCollection? slottedCollection)
            {
                Node = node;
                Collection = collection;
                SlottedCollection = slottedCollection;
            }

            public INode Node { get; }
            public StyleCollection Collection { get; }
            public StyleCollection? SlottedCollection { get; }
        }

        private ElementRenderNode? RenderPseudoElement(
            IElement element,
            String name,
            StyleCollection collection,
            ElementRenderNode parent,
            ICssStyleDeclaration parentSpecifiedStyle,
            ICssStyleDeclaration parentComputedStyle)
        {
            var pseudo = element.Pseudo(name);

            if (pseudo is null)
            {
                return null;
            }

            var explicitStyle = collection.ComputeExplicitStyle(pseudo);

            // Without a declaration `content` keeps its initial value `normal` (it is not inherited),
            // so no box is generated and the (expensive) computation of the full style can be skipped.
            if (!explicitStyle.Any(m => PropertyNames.Content.Equals(m.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            var node = RenderElement(pseudo, collection, parent, parentSpecifiedStyle, parentComputedStyle, explicitStyle);

            // Per spec, `content: none` (and the initial/unset `normal`, equivalent for these two
            // pseudo-elements specifically) means no box is generated at all - not merely an empty
            // one - so a content-less ::before/::after is simply left out of the tree entirely.
            return ContentDeclaration.HasContent(node.ComputedStyle) ? node : null;
        }
    }
}
