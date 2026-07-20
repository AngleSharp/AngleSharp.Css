#nullable disable
namespace AngleSharp.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;
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
        /// Serializes the stylesheet from its current CSSOM state.
        /// </summary>
        /// <param name="sheet">The stylesheet to serialize.</param>
        /// <param name="preserveComments">
        /// If true, parsed comment trivia will be included in the output.
        /// </param>
        /// <returns>The source code snippet.</returns>
        public static String ToCss(this ICssStyleSheet sheet, Boolean preserveComments)
        {
            sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));

            if (preserveComments)
            {
                var css = ((IStyleFormattable)sheet).ToCss(CommentPreservingFormatter.Instance);
                var source = sheet.Source?.Text;

                if (!String.IsNullOrEmpty(source))
                {
                    var missing = StringBuilderPool.Obtain();

                    foreach (var comment in ExtractComments(source))
                    {
                        if (css.IndexOf(comment, StringComparison.Ordinal) < 0)
                        {
                            missing.Append(comment);
                        }
                    }

                    if (missing.Length > 0)
                    {
                        return missing.Append(css).ToPool();
                    }
                }

                return css;
            }

            return ((IStyleFormattable)sheet).ToCss();
        }

        private static IEnumerable<String> ExtractComments(String source)
        {
            var index = 0;

            while (index < source.Length)
            {
                var start = source.IndexOf("/*", index, StringComparison.Ordinal);

                if (start < 0)
                {
                    yield break;
                }

                var end = source.IndexOf("*/", start + 2, StringComparison.Ordinal);

                if (end < 0)
                {
                    yield return source.Substring(start);
                    yield break;
                }

                var length = end - start + 2;
                yield return source.Substring(start, length);
                index = end + 2;
            }
        }

        /// <summary>
        /// Gets the associated document of the sheet if any.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <returns>The associated document, if any.</returns>
        public static IDocument GetDocument(this IStyleSheet sheet) => sheet?.OwnerNode?.Owner;
    }
}
