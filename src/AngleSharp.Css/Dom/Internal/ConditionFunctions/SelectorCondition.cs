namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using System;
    using System.IO;

    /// <summary>
    /// The selector() function of a supports condition: true for a selector that is understood.
    /// </summary>
    sealed class SelectorCondition : IConditionFunction
    {
        private readonly IBrowsingContext _context;
        private readonly String _selector;

        public SelectorCondition(IBrowsingContext context, String selector)
        {
            _context = context;
            _selector = selector;
        }

        public Boolean Check(IRenderDevice device) =>
            _context?.GetService<ICssSelectorParser>()?.ParseSelector(_selector) != null;

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("selector(");
            writer.Write(_selector);
            writer.Write(")");
        }
    }
}
