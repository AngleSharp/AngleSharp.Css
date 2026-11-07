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
    using System.Threading;
    using System.Threading.Tasks;

    sealed class RenderTreeBuilder
    {
        // below, computing the styles on several threads costs more than it saves
        private const Int32 MinElementsComputedInParallel = 200;

        /// <summary>
        /// The name of the AppContext switch turning off computing styles on several threads.
        /// </summary>
        internal const String DisableParallelismSwitch = "AngleSharp.Css.DisableParallelism";

        private static readonly ConditionalWeakTable<IWindow, RenderTreeBuilder> _windowRenderTrees = new();

        private readonly IBrowsingContext _context;
        private readonly IWindow _window;
        private readonly List<ICssStyleSheet> _defaultSheets;
        private readonly Dictionary<IElement, ICssStyleDeclaration> _cascadedStyles = new();
        private readonly Dictionary<IElement, ElementRenderNode> _renderedNodes = new();
        // the explicit styles computed up front for RenderDocument (see ComputeExplicitStyles)
        private Dictionary<IElement, ICssStyleDeclaration>? _explicitStyles;
        private StyleCollection? _explicitStylesCollection;

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
            _renderedNodes.Clear();
            _explicitStyles = ComputeExplicitStyles(root, collection);
            _explicitStylesCollection = collection;

            try
            {
                return RenderElement(root, collection, null, null, null);
            }
            finally
            {
                _explicitStyles = null;
                _explicitStylesCollection = null;
            }
        }

        /// <summary>
        /// Matching the selectors and running the cascade of an element is independent of all other
        /// elements, and makes up most of the time to build the render tree of a page, so it is done
        /// for all elements of the document on all cores first. Only the inheritance needs the
        /// parent's computed style, and is done while the tree is built. The DOM is just read.
        /// </summary>
        private static Dictionary<IElement, ICssStyleDeclaration>? ComputeExplicitStyles(IElement root, StyleCollection collection)
        {
            var elements = new List<IElement>();

            // shadow hosts (and the shadow trees) are styled by more than the document's rules
            foreach (var element in root.DescendantsAndSelf<IElement>())
            {
                if (element.ShadowRoot is null)
                {
                    elements.Add(element);
                }
            }

            if (elements.Count < MinElementsComputedInParallel || Environment.ProcessorCount < 2 ||
                (AppContext.TryGetSwitch(DisableParallelismSwitch, out var isDisabled) && isDisabled))
            {
                return null;
            }

            // built once, before the threads read them
            collection.GetIndex();
            collection.GetDefaultRuleCount();

            var styles = new ICssStyleDeclaration?[elements.Count];

            Parallel.For(0, elements.Count, i =>
            {
                try
                {
                    styles[i] = collection.ComputeExplicitStyle(elements[i]);
                }
                catch
                {
                    // computed (and thrown) once more in order, as without computing it up front
                }
            });

            var result = new Dictionary<IElement, ICssStyleDeclaration>(elements.Count);

            for (var i = 0; i < elements.Count; i++)
            {
                if (styles[i] is { } style)
                {
                    result[elements[i]] = style;
                }
            }

            return result;
        }

        public ICssStyleDeclaration GetElementStyle(IElement element, IRenderDevice? device = null)
        {
            if (_renderedNodes.TryGetValue(element, out var node))
            {
                return node.SpecifiedStyle;
            }

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
            var parentSpecifiedStyle = parent is not null ? (CssStyleDeclaration)collection.GetDeclarations(parent) : null;
            var parentComputedStyle = parent is not null ? collection.ComputeDeclarations(parent) : null;

            _cascadedStyles.Clear();
            _renderedNodes.Clear();

            return RenderElement(element, collection, null, parentSpecifiedStyle, parentComputedStyle);
        }

        public ElementRenderNode RenderElement(IElement element, ElementRenderNode parent, IRenderDevice device)
        {
            var collection = CreateStyleCollection(device);
            // the specified style of the parent as it is now, also if modified since
            var parentSpecifiedStyle = parent.SpecifiedStyle is CssStyleDeclaration style ? style.Freeze() : Copy(parent.SpecifiedStyle);
            return RenderElement(element, collection, parent, parentSpecifiedStyle, parent.ComputedStyle);
        }

        /// <summary>
        /// Renders the element below the given node of its parent into a partial render tree (see
        /// RenderPartial below) with the given styles, which are gathered anew if they are not a
        /// collection of this window's.
        /// </summary>
        public ElementRenderNode RenderPartial(IElement element, ElementRenderNode parent, IStyleCollection styles)
        {
            var collection = styles as StyleCollection ?? CreateStyleCollection(styles.Device);
            // the specified style of the parent as it is now, also if modified since
            var parentSpecifiedStyle = parent.SpecifiedStyle is CssStyleDeclaration style ? style.Freeze() : Copy(parent.SpecifiedStyle);
            return RenderElement(element, collection, parent, parentSpecifiedStyle, parent.ComputedStyle, partial: true);
        }

        /// <summary>
        /// Renders the element into a partial render tree: the children of a node are only
        /// built when they are read, so e.g. the content of a hidden element is never styled.
        /// The parts are built from the DOM and style sheets of the moment they are read, and
        /// the tree is not remembered (as GetElementStyle does for complete ones).
        /// </summary>
        public ElementRenderNode RenderPartial(IElement element, IRenderDevice device)
        {
            var collection = CreateStyleCollection(device);
            var parent = element.ParentElement;
            var parentComputedStyle = parent is not null ? collection.ComputeDeclarations(parent) : null;
            var getInheritedStyle = parent is not null ? () => collection.GetDeclarations(parent) : default(Func<ICssStyleDeclaration?>);
            return RenderElement(element, collection, null, null, parentComputedStyle, getInheritedStyle: getInheritedStyle, partial: true);
        }

        private CssStyleDeclaration Copy(ICssStyleDeclaration style)
        {
            var copy = new CssStyleDeclaration(_context);
            copy.SetDeclarations(style);
            return copy;
        }

        private StyleCollection CreateStyleCollection(IRenderDevice device)
        {
            var document = _window.Document;
            var currentSheets = document.GetEffectiveStyleSheets();
            return new StyleCollection(_defaultSheets, currentSheets.ToList(), device);
        }

        private ElementRenderNode RenderElement(
            IElement element,
            StyleCollection collection,
            ElementRenderNode? parent,
            CssStyleDeclaration? inheritedStyle,
            ICssStyleDeclaration? parentComputedStyle,
            ICssStyleDeclaration? explicitStyle = null,
            StyleCollection? slottedCollection = null,
            Func<ICssStyleDeclaration?>? getInheritedStyle = null,
            Boolean partial = false)
        {
            var shadowCollection = CreateShadowCollection(element, collection);

            if (explicitStyle is null && shadowCollection is null && _explicitStyles is not null &&
                ReferenceEquals(collection, _explicitStylesCollection) && _explicitStyles.TryGetValue(element, out var precomputed))
            {
                explicitStyle = precomputed;
            }

            explicitStyle ??= shadowCollection is not null
                ? collection.ComputeHostStyle(shadowCollection, element)
                : collection.ComputeExplicitStyle(element);

            var computeContext = new CssComputeContext(collection.Device, _context, explicitStyle, parentComputedStyle);
            var computedStyle = explicitStyle.PrepareComputedDeclarations(parentComputedStyle!, computeContext).Compute(computeContext).Blockify(element);
            var pending = partial ? new PendingChildren(this, collection, shadowCollection, slottedCollection, getInheritedStyle) : null;
            var children = partial ? null : new List<IRenderNode>();
            // inherits the specified style of the parent node, unless another one is given
            var node = new ElementRenderNode(element, parent, children, explicitStyle, inheritedStyle, computedStyle, _context, pending);

            if (children is not null)
            {
                _renderedNodes[element] = node;
                AddChildren(node, children, collection, shadowCollection, slottedCollection, false);
            }

            return node;
        }

        private void AddChildren(ElementRenderNode node, List<IRenderNode> children, StyleCollection collection, StyleCollection? shadowCollection, StyleCollection? slottedCollection, Boolean partial)
        {
            var element = node.Ref;
            var beforeNode = element is IPseudoElement ? null : RenderPseudoElement(element, "before", collection, node, node.ComputedStyle, partial);

            if (beforeNode is not null)
            {
                children.Add(beforeNode);
            }

            AddContent(node, children, collection, shadowCollection, slottedCollection, partial);

            var afterNode = element is IPseudoElement ? null : RenderPseudoElement(element, "after", collection, node, node.ComputedStyle, partial);

            if (afterNode is not null)
            {
                children.Add(afterNode);
            }
        }

        /// <summary>
        /// Adds the children of the node without its ::before and ::after.
        /// </summary>
        private void AddContent(ElementRenderNode node, List<IRenderNode> children, StyleCollection collection, StyleCollection? shadowCollection, StyleCollection? slottedCollection, Boolean partial)
        {
            var element = node.Ref;
            var computedStyle = node.ComputedStyle;

            if (element is IPseudoElement)
            {
                // A ::before/::after pseudo-element has no DOM children of its own - IElement.ChildNodes
                // on the wrapper aliases its host's real children, which the generic walk below would
                // otherwise duplicate into the render tree under the pseudo. Its only "child" is the
                // text its own computed `content` generates (attr()/literal strings resolved against
                // this same element, since a pseudo-element has no attributes of its own to differ from
                // its host's).
                children.Add(new TextRenderNode(_window.Document.CreateTextNode(ContentDeclaration.Stringify(computedStyle, element)), node));
                return;
            }

            foreach (var child in GetFlatChildren(element, collection, shadowCollection, slottedCollection))
            {
                if (child.Node is IElement childElement)
                {
                    children.Add(RenderElement(childElement, child.Collection, node, null, computedStyle, slottedCollection: child.SlottedCollection, partial: partial));
                }
                else if (child.Node is IText childText)
                {
                    children.Add(new TextRenderNode(childText, node));
                }
            }
        }

        /// <summary>
        /// What is needed to build the children of a node of a partial tree later on, and what
        /// only such a node needs.
        /// </summary>
        internal sealed class PendingChildren
        {
            private readonly RenderTreeBuilder _builder;
            private readonly StyleCollection _collection;
            private readonly StyleCollection? _shadowCollection;
            private readonly StyleCollection? _slottedCollection;
            private List<IRenderNode>? _content;
            private List<IRenderNode>? _children;

            public PendingChildren(RenderTreeBuilder builder, StyleCollection collection, StyleCollection? shadowCollection, StyleCollection? slottedCollection, Func<ICssStyleDeclaration?>? getInheritedStyle)
            {
                _builder = builder;
                _collection = collection;
                _shadowCollection = shadowCollection;
                _slottedCollection = slottedCollection;
                GetInheritedStyle = getInheritedStyle;
            }

            /// <summary>
            /// Gets the specified style to inherit from for the root of the partial tree, if
            /// it has a parent element.
            /// </summary>
            public Func<ICssStyleDeclaration?>? GetInheritedStyle { get; }

            /// <summary>
            /// Gets the children of the node without its ::before and ::after, which are built
            /// on first read.
            /// </summary>
            public List<IRenderNode> GetContent(ElementRenderNode node)
            {
                var content = Volatile.Read(ref _content);

                if (content is null)
                {
                    content = new List<IRenderNode>();
                    _builder.AddContent(node, content, _collection, _shadowCollection, _slottedCollection, true);
                    // another thread may have been faster: then its children are the ones of the node
                    content = Interlocked.CompareExchange(ref _content, content, null) ?? content;
                }

                return content;
            }

            /// <summary>
            /// Forgets the children without the ::before and ::after.
            /// </summary>
            public void ReleaseContent() => Volatile.Write(ref _content, null);

            /// <summary>
            /// Gets all children of the node, which are built on first read.
            /// </summary>
            public IEnumerable<IRenderNode> GetChildren(ElementRenderNode node)
            {
                var children = Volatile.Read(ref _children);

                if (children is null)
                {
                    var before = BuildPseudoElement(node, "before");
                    var content = GetContent(node);
                    var after = BuildPseudoElement(node, "after");
                    children = new List<IRenderNode>(content.Count + 2);

                    if (before is not null)
                    {
                        children.Add(before);
                    }

                    children.AddRange(content);

                    if (after is not null)
                    {
                        children.Add(after);
                    }

                    // another thread may have been faster: then its children are the ones of the node
                    children = Interlocked.CompareExchange(ref _children, children, null) ?? children;
                }

                return children;
            }

            /// <summary>
            /// Builds the ::before or ::after of the node, if it has one.
            /// </summary>
            public ElementRenderNode? BuildPseudoElement(ElementRenderNode node, String name) => node.Ref is IPseudoElement ?
                null :
                _builder.RenderPseudoElement(node.Ref, name, _collection, node, node.ComputedStyle, true);
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

            var shadowSheets = shadowRoot.GetEffectiveStyleSheets().ToList();
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
            ICssStyleDeclaration parentComputedStyle,
            Boolean partial)
        {
            var pseudo = element.Pseudo(name);

            // Only a rule can declare `content`: without one that may match the pseudo-element
            // (most elements have none) it has no box, and its style is not computed at all.
            if (pseudo is null || !collection.GetIndex().MayDeclareContent(pseudo))
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

            var node = RenderElement(pseudo, collection, parent, null, parentComputedStyle, explicitStyle, partial: partial);

            // Per spec, `content: none` (and the initial/unset `normal`, equivalent for these two
            // pseudo-elements specifically) means no box is generated at all - not merely an empty
            // one - so a content-less ::before/::after is simply left out of the tree entirely.
            return ContentDeclaration.HasContent(node.ComputedStyle) ? node : null;
        }
    }
}
