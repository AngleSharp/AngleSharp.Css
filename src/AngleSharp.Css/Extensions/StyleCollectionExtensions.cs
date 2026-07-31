#nullable enable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Svg.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A set of useful extension methods for the StyleCollection class.
    /// </summary>
    public static class StyleCollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Generates the style collection for the given window object.
        /// </summary>
        /// <param name="window">The window to host the style collection.</param>
        /// <param name="renderDevice">The device to get the style collection for.</param>
        /// <returns>The device-bound style collection.</returns>
        public static IStyleCollection GetStyleCollection(this IWindow window, IRenderDevice renderDevice)
        {
            var document = window.Document;
            var ctx = document.Context;
            var defaultStyleSheetProvider = ctx.GetServices<ICssDefaultStyleSheetProvider>();
            var defaultSheets = defaultStyleSheetProvider.Select(m => m.Default).Where(m => m != null);
            var currentSheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
            var stylesheets = defaultSheets.Concat(currentSheets);
            return new StyleCollection(stylesheets, renderDevice);
        }

        /// <summary>
        /// Computes the declarations for the given element in the context of
        /// the specified styling rules.
        /// </summary>
        /// <param name="styles">The styles to use.</param>
        /// <param name="element">The element that is questioned.</param>
        /// <param name="pseudoSelector">The optional pseudo selector to use.</param>
        /// <returns>The style declaration containing all the declarations.</returns>
        public static ICssStyleDeclaration ComputeDeclarations(this IStyleCollection styles, IElement element, String? pseudoSelector = null)
        {
            var ctx = element.Owner?.Context;
            var declarations = GetDeclarations(styles, element, pseudoSelector);
            var context = new CssComputeContext(styles.Device, ctx, declarations);

            return declarations.Compute(context);
        }

        /// <summary>
        /// Gets the declarations for the given element in the context of
        /// the specified styling rules.
        /// </summary>
        /// <param name="styles">The styles to use.</param>
        /// <param name="element">The element that is questioned.</param>
        /// <param name="pseudoSelector">The optional pseudo selector to use.</param>
        /// <returns>The style declaration containing all the declarations.</returns>
        public static ICssStyleDeclaration GetDeclarations(this IStyleCollection styles, IElement element, String? pseudoSelector = null)
        {
            var ctx = element.Owner?.Context;
            var computedStyle = new CssStyleDeclaration(ctx);
            var nodes = element.GetAncestors().OfType<IElement>();

            if (!String.IsNullOrEmpty(pseudoSelector))
            {
                var pseudoElement = element?.Pseudo(pseudoSelector!.TrimStart(':'));

                if (pseudoElement is not null)
                {
                    element = pseudoElement;
                }
            }

            computedStyle.SetDeclarations(styles.ComputeExplicitStyle(element!));

            foreach (var node in nodes)
            {
                computedStyle.UpdateDeclarations(styles.ComputeExplicitStyle(node));
            }

            return computedStyle;
        }

        /// <summary>
        /// Computes the cascaded style, i.e. merges the explicit style of the element with
        /// the style from the parent.
        /// </summary>
        /// <param name="styles">The style rules to apply.</param>
        /// <param name="element">The element to compute the cascade for.</param>
        /// <param name="parent">The potential parent for the cascade.</param>
        /// <returns>Returns the cascaded read-only style declaration.</returns>
        public static ICssStyleDeclaration ComputeCascadedStyle(this IStyleCollection styles, IElement element, ICssStyleDeclaration parent)
        {
            var computedStyle = (CssStyleDeclaration)styles.ComputeExplicitStyle(element);
            computedStyle.UpdateDeclarations(parent);
            return computedStyle;
        }

        /// <summary>
        /// Computes the explicit style, i.e., orders the rules after specificity.
        /// Two rules with the same specificity are ordered according to their appearance. The more
        /// recent declaration wins. Inheritance is not taken into account.
        /// </summary>
        /// <param name="styles">The style rules to apply.</param>
        /// <param name="element">The element to compute the cascade for.</param>
        /// <returns>Returns the explicit read-only style declaration.</returns>
        public static ICssStyleDeclaration ComputeExplicitStyle(this IStyleCollection styles, IElement element)
        {
            var ctx = element.Owner?.Context ?? throw new InvalidOperationException("The element must be associated with a browsing context.");
            var computedStyle = new CssStyleDeclaration(ctx);
            var matches = styles.SortBySpecificity(element);

            for (var i = 0; i < matches.Count; i++)
            {
                var inlineStyle = matches[i].Rule.Style;
                computedStyle.SetDeclarations(inlineStyle);
            }

            if (element is IHtmlElement || element is ISvgElement)
            {
                computedStyle.SetDeclarations(element.GetStyle());
            }

            return computedStyle;
        }

        /// <summary>
        /// Computes the declarations for the given element using a pre-computed
        /// parent style for inheritance, avoiding the O(depth) ancestor walk.
        /// </summary>
        /// <param name="styles">The styles to use.</param>
        /// <param name="element">The element that is questioned.</param>
        /// <param name="parentComputedStyle">The parent's already-computed style.</param>
        /// <returns>The style declaration containing all the declarations.</returns>
        internal static ICssStyleDeclaration ComputeDeclarationsWithParent(this IStyleCollection styles, IElement element, ICssStyleDeclaration parentComputedStyle)
        {
            var ctx = element.Owner?.Context;
            var computedStyle = new CssStyleDeclaration(ctx);

            // Element's own cascaded style (CSS rule matching + inline style).
            computedStyle.SetDeclarations(styles.ComputeExplicitStyle(element));

            // Inherit from the parent's already-computed style instead of walking
            // all ancestors individually. The parent style already includes the
            // full ancestor inheritance chain.
            computedStyle.UpdateDeclarations(parentComputedStyle);

            var context = new CssComputeContext(styles.Device, ctx, computedStyle);
            return computedStyle.Compute(context);
        }

        #endregion

        #region Helpers

        private static List<RuleMatch> SortBySpecificity(this IStyleCollection styles, IElement element)
        {
            // Resolving the scope once is equivalent to letting every TryMatch call
            // fall back to it, but avoids re-reading DocumentElement per rule.
            var scope = element.Owner?.DocumentElement;
            var matches = new List<RuleMatch>();

            if (styles is StyleCollection collection)
            {
                var rules = collection.GetRules();

                for (var i = 0; i < rules.Count; i++)
                {
                    MapPriority(rules[i], element, scope, matches);
                }
            }
            else
            {
                foreach (var rule in styles)
                {
                    MapPriority(rule, element, scope, matches);
                }
            }

            // OrderBy is a stable sort; the index tie-break reproduces that for
            // rules that share the same specificity.
            matches.Sort(RuleMatchComparer.Instance);
            return matches;
        }

        private static void MapPriority(ICssStyleRule rule, IElement element, IElement? scope, List<RuleMatch> matches)
        {
            if (rule.TryMatch(element, scope, out var specificity))
            {
                matches.Add(new RuleMatch(rule, specificity, matches.Count));
            }

            var subRules = rule.Rules;

            for (var i = 0; i < subRules.Length; i++)
            {
                if (subRules[i] is ICssStyleRule style)
                {
                    MapPriority(style, element, scope, matches);
                }
            }
        }

        private readonly struct RuleMatch
        {
            public RuleMatch(ICssStyleRule rule, Priority priority, Int32 index)
            {
                Rule = rule;
                Priority = priority;
                Index = index;
            }

            public ICssStyleRule Rule { get; }

            public Priority Priority { get; }

            public Int32 Index { get; }
        }

        private sealed class RuleMatchComparer : IComparer<RuleMatch>
        {
            public static readonly RuleMatchComparer Instance = new RuleMatchComparer();

            public Int32 Compare(RuleMatch x, RuleMatch y)
            {
                var result = Comparer<Priority>.Default.Compare(x.Priority, y.Priority);
                return result != 0 ? result : x.Index.CompareTo(y.Index);
            }
        }

        #endregion
    }
}
