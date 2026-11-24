#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;

    /// <summary>
    /// Matches the `:modal` pseudo-class (Selectors 4 §13.4), i.e., a dialog shown as a modal.
    /// </summary>
    sealed class ModalPseudoClassSelector : ISelector
    {
        public Priority Specificity => Priority.OneClass;

        public String Text => PseudoClassNames.Separator + CssPseudoClassSelectorFactory.Modal;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(CssPseudoClassSelectorFactory.Modal);

        public Boolean Match(IElement element, IElement? scope) =>
            element is IHtmlDialogElement dialog && dialog.Open && dialog.GetIsModal();
    }
}
