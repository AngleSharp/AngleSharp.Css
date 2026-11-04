namespace AngleSharp.Css.RenderTree
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Represents a rendered element.
    /// </summary>
    public sealed class ElementRenderNode : IRenderNode
    {
        private readonly IElement _reference;
        private readonly ElementRenderNode? _parent;
        private readonly IEnumerable<IRenderNode> _children;
        private readonly ICssStyleDeclaration _computedStyle;

        // The specified style is only built when it is read: from the explicit style of the
        // element and the specified style its parent had when it was built (or, for the root
        // of a rendering, the given one).
        private readonly ICssStyleDeclaration? _explicitStyle;
        private readonly ICssStyleDeclaration? _inheritedStyle;
        private readonly IBrowsingContext? _context;
        private SpecifiedStyles? _specified;

        /// <summary>
        /// Constructs a new rendered element.
        /// </summary>
        /// <param name="reference">The reference to the original element.</param>
        /// <param name="parent">The used parent node if any.</param>
        /// <param name="children">The contained children.</param>
        /// <param name="specifiedStyle">The cascaded style for the element.</param>
        /// <param name="computedStyle">The computed style of the element.</param>
        public ElementRenderNode(
            IElement reference,
            ElementRenderNode? parent,
            IEnumerable<IRenderNode> children,
            ICssStyleDeclaration specifiedStyle,
            ICssStyleDeclaration computedStyle)
        {
            _reference = reference;
            _parent = parent;
            _children = children;
            _specified = new SpecifiedStyles(specifiedStyle, specifiedStyle);
            _computedStyle = computedStyle;
        }

        /// <summary>
        /// Constructs a rendered element whose specified style is built on first read.
        /// </summary>
        /// <param name="reference">The reference to the original element.</param>
        /// <param name="parent">The used parent node if any.</param>
        /// <param name="children">The contained children.</param>
        /// <param name="explicitStyle">The explicit style of the element.</param>
        /// <param name="inheritedStyle">
        /// The specified style to inherit from, if not the one of the parent node.
        /// </param>
        /// <param name="computedStyle">The computed style of the element.</param>
        /// <param name="context">The context to create the specified style in.</param>
        internal ElementRenderNode(
            IElement reference,
            ElementRenderNode? parent,
            IEnumerable<IRenderNode> children,
            ICssStyleDeclaration explicitStyle,
            CssStyleDeclaration? inheritedStyle,
            ICssStyleDeclaration computedStyle,
            IBrowsingContext context)
        {
            _reference = reference;
            _parent = parent;
            _children = children;
            _explicitStyle = explicitStyle;
            _inheritedStyle = inheritedStyle;
            _context = context;
            _computedStyle = computedStyle;
        }

        /// <summary>
        /// Gets a reference to the element.
        /// </summary>
        public IElement Ref => _reference;

        INode IRenderNode.Ref => Ref;

        /// <summary>
        /// Gets the contained render nodes.
        /// </summary>
        public IEnumerable<IRenderNode> Children => _children;

        /// <summary>
        /// Gets the parent of the node.
        /// </summary>
        public ElementRenderNode? Parent => _parent;

        /// <summary>
        /// Gets the cascaded style of the element.
        /// </summary>
        public ICssStyleDeclaration SpecifiedStyle => GetSpecifiedStyles().Style;

        /// <summary>
        /// Gets the computed style of the element.
        /// </summary>
        public ICssStyleDeclaration ComputedStyle => _computedStyle;

        private Boolean InheritsFromParent => _inheritedStyle is null && _parent is not null;

        private SpecifiedStyles GetSpecifiedStyles()
        {
            var specified = Volatile.Read(ref _specified);

            if (specified is not null)
            {
                return specified;
            }

            // build the missing ones top-down without recursion, as the chain is as deep as the tree
            var pending = new Stack<ElementRenderNode>();

            for (var node = this; node is not null && Volatile.Read(ref node._specified) is null; node = node.InheritsFromParent ? node._parent : null)
            {
                pending.Push(node);
            }

            while (pending.Count > 0)
            {
                specified = pending.Pop().BuildSpecifiedStyles();
            }

            return specified!;
        }

        private SpecifiedStyles BuildSpecifiedStyles()
        {
            var inherited = InheritsFromParent ? Volatile.Read(ref _parent!._specified)!.ForChildren : _inheritedStyle;
            var style = new CssStyleDeclaration(_context);
            style.SetDeclarations(_explicitStyle!);

            if (inherited is not null)
            {
                style.InheritFrom(inherited);
            }

            // another thread may have been faster: then its style is the one of the node
            var specified = new SpecifiedStyles(style, style.Freeze());
            return Interlocked.CompareExchange(ref _specified, specified, null) ?? specified;
        }

        private sealed class SpecifiedStyles
        {
            public SpecifiedStyles(ICssStyleDeclaration style, ICssStyleDeclaration forChildren)
            {
                Style = style;
                ForChildren = forChildren;
            }

            /// <summary>
            /// Gets the specified style handed out, which may be modified.
            /// </summary>
            public ICssStyleDeclaration Style { get; }

            /// <summary>
            /// Gets the specified style as it was built, which the children inherit.
            /// </summary>
            public ICssStyleDeclaration ForChildren { get; }
        }
    }
}
