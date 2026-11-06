namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Adds constructed stylesheet operations to CSSStyleSheet without changing its interface.
    /// </summary>
    [DomExposed("CSSStyleSheet")]
    public static class CssStyleSheetExtensions
    {
        /// <summary>
        /// Replaces all rules in a constructed stylesheet.
        /// </summary>
        /// <param name="sheet">The stylesheet to update.</param>
        /// <param name="text">The stylesheet text to parse.</param>
        [DomName("replaceSync")]
        public static void ReplaceSync(this ICssStyleSheet sheet, String text)
        {
            GetConstructedSheet(sheet).ReplaceSync(text);
        }

        /// <summary>
        /// Replaces all rules in a constructed stylesheet asynchronously.
        /// </summary>
        /// <param name="sheet">The stylesheet to update.</param>
        /// <param name="text">The stylesheet text to parse.</param>
        /// <returns>A task that resolves with the stylesheet.</returns>
        [DomName("replace")]
        public static Task<ICssStyleSheet> Replace(this ICssStyleSheet sheet, String text)
        {
            return sheet is CssStyleSheet constructedSheet
                ? constructedSheet.Replace(text)
                : Task.FromException<ICssStyleSheet>(new DomException("NotAllowedError"));
        }

        private static CssStyleSheet GetConstructedSheet(ICssStyleSheet sheet)
        {
            if (sheet is CssStyleSheet constructedSheet && constructedSheet.IsConstructed)
            {
                return constructedSheet;
            }

            throw new DomException("NotAllowedError");
        }
    }
}
