#nullable enable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Decorates another pseudo-class selector factory so that any pseudo-class it recognizes can
    /// be forced on/off per element via <see cref="AngleSharp.Dom.ElementExtensions.SetPseudoClass"/>.
    /// </summary>
    /// <remarks>
    /// The <c>focus</c> pseudo-class is intentionally left untouched: AngleSharp already tracks
    /// real focus state (<see cref="IElement.IsFocused"/>, settable through
    /// <see cref="AngleSharp.Html.Dom.IHtmlElement.DoFocus"/> /
    /// <see cref="AngleSharp.Html.Dom.IHtmlElement.DoBlur"/>), so that remains the single source
    /// of truth instead of introducing a second, potentially conflicting one.
    /// </remarks>
    sealed class ForcingPseudoClassSelectorFactory : IPseudoClassSelectorFactory
    {
        private readonly IPseudoClassSelectorFactory _inner;

        public ForcingPseudoClassSelectorFactory(IPseudoClassSelectorFactory inner)
        {
            _inner = inner;
        }

        public ISelector? Create(String name)
        {
            var selector = _inner.Create(name);

            if (selector is null || name.Equals(PseudoClassNames.Focus, StringComparison.OrdinalIgnoreCase))
            {
                return selector;
            }

            return new ForcingPseudoClassSelector(name, selector);
        }
    }
}
