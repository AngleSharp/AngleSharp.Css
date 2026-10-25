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
            return new StyleCollection(defaultSheets, currentSheets, renderDevice);
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
            => GetComputedDeclarations(styles, element, pseudoSelector);

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
            var declarations = new CssStyleDeclaration(ctx);
            var ancestors = element.GetAncestors().OfType<IElement>();

            if (!String.IsNullOrEmpty(pseudoSelector))
            {
                element = element.Pseudo(pseudoSelector!.TrimStart(':')) ?? element;
            }

            declarations.SetDeclarations(styles.ComputeExplicitStyle(element));

            foreach (var ancestor in ancestors)
            {
                declarations.UpdateDeclarations(styles.ComputeExplicitStyle(ancestor));
            }

            return declarations;
        }

        private static ICssStyleDeclaration GetComputedDeclarations(IStyleCollection styles, IElement element, String? pseudoSelector)
        {
            var ctx = element.Owner?.Context;
            ICssStyleDeclaration? parent = null;
            var nodes = new Stack<IElement>();

            if (!String.IsNullOrEmpty(pseudoSelector))
            {
                var pseudoElement = element.Pseudo(pseudoSelector!.TrimStart(':'));

                if (pseudoElement is not null)
                {
                    nodes.Push(pseudoElement);
                }
            }

            nodes.Push(element);

            foreach (var ancestor in element.GetAncestors().OfType<IElement>())
            {
                nodes.Push(ancestor);
            }

            while (nodes.Count > 0)
            {
                var node = nodes.Pop();
                var explicitStyle = styles.ComputeExplicitStyle(node);
                var context = new CssComputeContext(styles.Device, ctx, explicitStyle, parent);
                parent = explicitStyle.PrepareComputedDeclarations(parent!, context).Compute(context).Blockify(node);
            }

            return parent!;
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
            var declarations = (CssStyleDeclaration)styles.ComputeExplicitStyle(element);
            declarations.UpdateDeclarations(parent);
            return declarations;
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
            var i = 0;

            for (; i < matches.Count && matches[i].IsDefault; i++)
            {
                computedStyle.SetDeclarations(matches[i].Rule.Style);
            }

            ApplyPresentationalHints(computedStyle, element);

            for (; i < matches.Count; i++)
            {
                computedStyle.SetDeclarations(matches[i].Rule.Style);
            }

            ApplyImportantLayerOrder(computedStyle, matches);

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
            var explicitStyle = styles.ComputeExplicitStyle(element);
            var context = new CssComputeContext(styles.Device, ctx, explicitStyle, parentComputedStyle);
            return explicitStyle.PrepareComputedDeclarations(parentComputedStyle, context).Compute(context).Blockify(element);
        }

        /// <summary>
        /// Computes the explicit style of a shadow host: the :host rules of its shadow tree come
        /// after the user agent's rules, but lose against the author rules of the host's own tree,
        /// unless they are important (CSS Cascade 5, 6.1).
        /// </summary>
        /// <param name="styles">The styles of the host's own tree.</param>
        /// <param name="shadowStyles">The styles of the shadow tree, scoped to the host.</param>
        /// <param name="host">The shadow host.</param>
        /// <returns>Returns the explicit read-only style declaration.</returns>
        internal static ICssStyleDeclaration ComputeHostStyle(this IStyleCollection styles, StyleCollection shadowStyles, IElement host)
        {
            var shadowMatches = MatchHost(shadowStyles, host);

            if (shadowMatches.Count == 0)
            {
                return styles.ComputeExplicitStyle(host);
            }

            var ctx = host.Owner?.Context ?? throw new InvalidOperationException("The element must be associated with a browsing context.");
            var shadowStyle = new CssStyleDeclaration(ctx);
            var computedStyle = new CssStyleDeclaration(ctx);
            var matches = styles.SortBySpecificity(host);
            var i = 0;

            foreach (var match in shadowMatches)
            {
                shadowStyle.SetDeclarations(match.Rule.Style);
            }

            ApplyImportantLayerOrder(shadowStyle, shadowMatches);

            for (; i < matches.Count && matches[i].IsDefault; i++)
            {
                computedStyle.SetDeclarations(matches[i].Rule.Style);
            }

            ApplyPresentationalHints(computedStyle, host);
            computedStyle.SetDeclarations(shadowStyle);

            for (; i < matches.Count; i++)
            {
                computedStyle.SetDeclarations(matches[i].Rule.Style);
            }

            ApplyImportantLayerOrder(computedStyle, matches);
            computedStyle.SetDeclarations(host.GetStyle());
            computedStyle.SetDeclarations(shadowStyle.Where(m => m.IsImportant).ToList());
            return computedStyle;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Applies the presentational hints of the element (e.g. the width attribute of an img),
        /// which come after the user agent rules and before all other author rules (CSS Cascade 5, 6.1).
        /// </summary>
        private static void ApplyPresentationalHints(CssStyleDeclaration computedStyle, IElement element)
        {
            var hints = PresentationalHints.GetDeclarations(element);

            if (hints is not null)
            {
                var declarations = new CssStyleDeclaration(element.Owner?.Context);
                declarations.Update(hints);
                computedStyle.SetDeclarations(declarations);
            }
        }

        /// <summary>
        /// For important declarations the order of the cascade layers is reversed:
        /// an earlier layer wins against a later one and against unlayered rules
        /// (CSS Cascade 5, 6.4). The matches are sorted for normal declarations, so
        /// the important ones of the author are applied once more in that order.
        /// </summary>
        private static void ApplyImportantLayerOrder(CssStyleDeclaration computedStyle, List<RuleMatch> matches)
        {
            List<RuleMatch>? important = null;
            var layered = false;

            foreach (var match in matches)
            {
                if (!match.IsDefault && match.Rule.Style.Any(m => m.IsImportant))
                {
                    important ??= new List<RuleMatch>();
                    layered = layered || (important.Count > 0 && important[0].LayerRank != match.LayerRank);
                    important.Add(match);
                }
            }

            if (layered)
            {
                important!.Sort((x, y) => x.LayerRank != y.LayerRank ? y.LayerRank.CompareTo(x.LayerRank) : RuleMatchComparer.Instance.Compare(x, y));

                foreach (var match in important)
                {
                    computedStyle.SetDeclarations(match.Rule.Style.Where(m => m.IsImportant));
                }
            }
        }

        private static List<RuleMatch> MatchHost(StyleCollection shadowStyles, IElement host)
        {
            var rules = shadowStyles.GetRules();
            var matches = new List<RuleMatch>();

            for (var i = shadowStyles.GetDefaultRuleCount(); i < rules.Count; i++)
            {
                if (rules[i] is CssStyleRule rule && rule.TryMatchHost(host, out var specificity))
                {
                    matches.Add(new RuleMatch(rule, false, shadowStyles.GetLayerRank(i), specificity, matches.Count));
                }
            }

            matches.Sort(RuleMatchComparer.Instance);
            return matches;
        }

        private static List<RuleMatch> SortBySpecificity(this IStyleCollection styles, IElement element)
        {
            // Resolving the scope once is equivalent to letting every TryMatch call
            // fall back to it, but avoids re-reading DocumentElement per rule.
            var scope = (styles as StyleCollection)?.Scope ?? element.Owner?.DocumentElement;
            var matches = new List<RuleMatch>();

            if (styles is StyleCollection collection)
            {
                var rules = collection.GetRules();
                var defaultRuleCount = collection.GetDefaultRuleCount();

                // the candidates keep the order of the rules, as required by the cascade
                foreach (var i in collection.GetIndex().GetCandidates(element))
                {
                    MapPriority(rules[i], element, scope, i < defaultRuleCount, collection.GetLayerRank(i), matches);
                }
            }
            else
            {
                foreach (var rule in styles)
                {
                    MapPriority(rule, element, scope, false, 0, matches);
                }
            }

            // OrderBy is a stable sort; the index tie-break reproduces that for
            // rules that share the same specificity.
            matches.Sort(RuleMatchComparer.Instance);
            return matches;
        }

        private static void MapPriority(ICssStyleRule rule, IElement element, IElement? scope, Boolean isDefault, Int32 layerRank, List<RuleMatch> matches)
        {
            if (rule.TryMatch(element, scope, out var specificity))
            {
                matches.Add(new RuleMatch(rule, isDefault, layerRank, specificity, matches.Count));
            }

            var subRules = rule.Rules;

            for (var i = 0; i < subRules.Length; i++)
            {
                if (subRules[i] is ICssStyleRule style)
                {
                    MapPriority(style, element, scope, isDefault, layerRank, matches);
                }
            }
        }

        private readonly struct RuleMatch
        {
            public RuleMatch(ICssStyleRule rule, Boolean isDefault, Int32 layerRank, Priority priority, Int32 index)
            {
                Rule = rule;
                IsDefault = isDefault;
                LayerRank = layerRank;
                Priority = priority;
                Index = index;
            }

            public ICssStyleRule Rule { get; }

            public Boolean IsDefault { get; }

            public Int32 LayerRank { get; }

            public Priority Priority { get; }

            public Int32 Index { get; }
        }

        private sealed class RuleMatchComparer : IComparer<RuleMatch>
        {
            public static readonly RuleMatchComparer Instance = new RuleMatchComparer();

            public Int32 Compare(RuleMatch x, RuleMatch y)
            {
                // Rules of the default (user agent) stylesheets always precede author
                // rules, i.e., an author rule wins independent of its specificity.
                if (x.IsDefault != y.IsDefault)
                {
                    return x.IsDefault ? -1 : 1;
                }

                // Cascade layers precede specificity: a later layer wins (CSS Cascade 5, 6.4).
                if (x.LayerRank != y.LayerRank)
                {
                    return x.LayerRank.CompareTo(y.LayerRank);
                }

                var result = Comparer<Priority>.Default.Compare(x.Priority, y.Priority);
                return result != 0 ? result : x.Index.CompareTo(y.Index);
            }
        }

        #endregion
    }
}
