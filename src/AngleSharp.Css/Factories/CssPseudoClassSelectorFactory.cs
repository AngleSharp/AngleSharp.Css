#nullable enable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Decorates another pseudo-class selector factory with the pseudo-classes
    /// that are supported by AngleSharp.Css, but not recognized by the decorated one.
    /// </summary>
    sealed class CssPseudoClassSelectorFactory : IPseudoClassSelectorFactory
    {
        /// <summary>
        /// The name of the `:popover-open` pseudo-class (HTML, "The popover attribute").
        /// </summary>
        public static readonly String PopoverOpen = "popover-open";

        private static readonly ISelector PopoverOpenSelector = new PopoverOpenPseudoClassSelector();

        private readonly IPseudoClassSelectorFactory _inner;

        public CssPseudoClassSelectorFactory(IPseudoClassSelectorFactory inner)
        {
            _inner = inner;
        }

        public ISelector? Create(String name)
        {
            var selector = _inner.Create(name);

            if (selector is null && name.Equals(PopoverOpen, StringComparison.OrdinalIgnoreCase))
            {
                return PopoverOpenSelector;
            }

            return selector;
        }
    }
}
