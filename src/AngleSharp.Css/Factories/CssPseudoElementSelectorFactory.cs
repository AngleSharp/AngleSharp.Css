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

        private static readonly ISelector PlaceholderSelector = new CssPseudoElementSelector(Placeholder);

        private readonly IPseudoElementSelectorFactory _inner;

        public CssPseudoElementSelectorFactory(IPseudoElementSelectorFactory inner)
        {
            _inner = inner;
        }

        public ISelector Create(String name)
        {
            var selector = _inner.Create(name);

            if (selector is null && name.Equals(Placeholder, StringComparison.OrdinalIgnoreCase))
            {
                return PlaceholderSelector;
            }

            return selector;
        }
    }
}
