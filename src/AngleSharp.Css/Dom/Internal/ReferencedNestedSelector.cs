namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    internal class ReferencedNestedSelector(ISelector referenced) : ISelector
    {
        private readonly ISelector _referenced = referenced;

        public String Text => "&";

        public Priority Specificity => _referenced.Specificity;

        public void Accept(ISelectorVisitor visitor)
        {
            // Right now we have nothing here.
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return _referenced.Match(element, scope);
        }
    }
}
