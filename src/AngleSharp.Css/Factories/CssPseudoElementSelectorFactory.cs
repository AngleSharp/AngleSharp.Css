#nullable disable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Decorates another pseudo-element selector factory with the pseudo-elements
    /// that are supported by AngleSharp.Css, but not recognized by the decorated one.
    /// </summary>
    sealed class CssPseudoElementSelectorFactory : IPseudoElementSelectorFactory
    {
        /// <summary>
        /// The name of the `::placeholder` pseudo-element (CSS Pseudo-Elements 4 §4.3).
        /// </summary>
        public static readonly String Placeholder = "placeholder";

        /// <summary>
        /// The name of the `::backdrop` pseudo-element (CSS Position 4 §4.2).
        /// </summary>
        public static readonly String Backdrop = "backdrop";

        /// <summary>
        /// The name of the `::marker` pseudo-element (CSS Pseudo-Elements 4 §3.1).
        /// </summary>
        public static readonly String Marker = "marker";

        /// <summary>
        /// The name of the `::first-line` pseudo-element (CSS Pseudo-Elements 4 §2.1).
        /// </summary>
        public static readonly String FirstLine = "first-line";

        /// <summary>
        /// The name of the `::first-letter` pseudo-element (CSS Pseudo-Elements 4 §2.2).
        /// </summary>
        public static readonly String FirstLetter = "first-letter";

        private static readonly ISelector PlaceholderSelector = new CssPseudoElementSelector(Placeholder);

        private static readonly ISelector MarkerSelector = new CssPseudoElementSelector(Marker);

        private static readonly ISelector FirstLineSelector = new CssPseudoElementSelector(FirstLine);

        private static readonly ISelector FirstLetterSelector = new CssPseudoElementSelector(FirstLetter);

        private static readonly ISelector BackdropSelector = new CssPseudoElementSelector(Backdrop);

        private readonly IPseudoElementSelectorFactory _inner;

        public CssPseudoElementSelectorFactory(IPseudoElementSelectorFactory inner)
        {
            _inner = inner;
        }

        public ISelector Create(String name)
        {
            // AngleSharp knows these, but leaves them unmatched; a renderer styles them
            if (name.Equals(FirstLine, StringComparison.OrdinalIgnoreCase))
            {
                return FirstLineSelector;
            }

            if (name.Equals(FirstLetter, StringComparison.OrdinalIgnoreCase))
            {
                return FirstLetterSelector;
            }

            var selector = _inner.Create(name);

            if (selector is null)
            {
                if (name.Equals(Placeholder, StringComparison.OrdinalIgnoreCase))
                {
                    return PlaceholderSelector;
                }

                if (name.Equals(Backdrop, StringComparison.OrdinalIgnoreCase))
                {
                    return BackdropSelector;
                }

                if (name.Equals(Marker, StringComparison.OrdinalIgnoreCase))
                {
                    return MarkerSelector;
                }
            }

            return selector;
        }
    }
}
