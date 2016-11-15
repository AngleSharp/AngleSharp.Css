namespace AngleSharp.Css.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Css.Dom;
    using System;

    public static class CssApiExtensions
    {
        /// <summary>
        /// Gets the computed style of the given element from the current view.
        /// </summary>
        /// <param name="element">The element to compute the style for.</param>
        /// <returns>The computed style declaration if available.</returns>
        public static ICssStyleDeclaration ComputeCurrentStyle(this IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var document = element.Owner;
            var window = document?.DefaultView;
            return window?.GetComputedStyle(element);
        }
    }
}
