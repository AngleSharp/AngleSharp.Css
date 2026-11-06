#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;

    /// <summary>
    /// Matches the `:popover-open` pseudo-class (HTML, "The popover attribute"), i.e., an element
    /// whose popover is currently being shown.
    /// </summary>
    sealed class PopoverOpenPseudoClassSelector : ISelector
    {
        public Priority Specificity => Priority.OneClass;

        public String Text => PseudoClassNames.Separator + CssPseudoClassSelectorFactory.PopoverOpen;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(CssPseudoClassSelectorFactory.PopoverOpen);

        public Boolean Match(IElement element, IElement? scope) => element is IHtmlElement html && html.IsPopoverOpen();
    }
}
