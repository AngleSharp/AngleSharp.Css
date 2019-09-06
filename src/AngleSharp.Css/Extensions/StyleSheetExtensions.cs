namespace AngleSharp.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Useful extensions for dealing with stylesheets.
    /// </summary>
    public static class StyleSheetExtensions
    {
        /// <summary>
        /// Gets all rules that are of the provided type.
        /// </summary>
        /// <typeparam name="TRule">The type of rules to get.</typeparam>
        /// <param name="sheets">The list of stylesheets to consider.</param>
        /// <returns>The list of rules.</returns>
        public static IEnumerable<TRule> GetRules<TRule>(this IEnumerable<IStyleSheet> sheets)
            where TRule : ICssRule
        {
            sheets = sheets ?? throw new ArgumentNullException(nameof(sheets));
            return sheets.Where(m => !m.IsDisabled).OfType<ICssStyleSheet>().SelectMany(m => m.Rules).OfType<TRule>();
        }

        /// <summary>
        /// Gets the styles matching the given render device.
        /// </summary>
        /// <param name="rules">The set of rules.</param>
        /// <param name="device">The render device.</param>
        /// <returns>The style rules.</returns>
        public static IEnumerable<ICssStyleRule> GetMatchingStyles(this ICssRuleList rules, IRenderDevice device)
        {
            foreach (var rule in rules)
            {
                if (rule.Type == CssRuleType.Media)
                {
                    var media = (ICssMediaRule)rule;

                    if (media.IsValid(device))
                    {
                        var subrules = media.Rules.GetMatchingStyles(device);

                        foreach (var subrule in subrules)
                        {
                            yield return subrule;
                        }
                    }
                }
                else if (rule.Type == CssRuleType.Supports)
                {
                    var support = (ICssSupportsRule)rule;

                    if (support.IsValid(device))
                    {
                        var subrules = support.Rules.GetMatchingStyles(device);

                        foreach (var subrule in subrules)
                        {
                            yield return subrule;
                        }
                    }
                }
                else if (rule.Type == CssRuleType.Style)
                {
                    yield return (ICssStyleRule)rule;
                }
            }
        }

        /// <summary>
        /// Gets all style rules that have the same selector text.
        /// </summary>
        /// <param name="sheets">The list of stylesheets to consider.</param>
        /// <param name="selector">The selector to compare to.</param>
        /// <returns>The list of style rules.</returns>
        public static IEnumerable<ICssStyleRule> StylesWith(this IEnumerable<IStyleSheet> sheets, ISelector selector)
        {
            selector = selector ?? throw new ArgumentNullException(nameof(selector));
            var selectorText = selector.Text;
            return sheets.GetRules<ICssStyleRule>().Where(m => m.SelectorText == selectorText);
        }

        /// <summary>
        /// Gets the associated document of the sheet if any.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <returns>The associated document, if any.</returns>
        public static IDocument GetDocument(this IStyleSheet sheet) => sheet?.OwnerNode?.Owner;
    }
}
