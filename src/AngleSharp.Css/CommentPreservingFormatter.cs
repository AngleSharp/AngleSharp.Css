namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;

    internal interface ICommentPreservingFormatter
    {
        Boolean PreserveComments { get; }
    }

    internal sealed class CommentPreservingFormatter : IStyleFormatter, ICommentPreservingFormatter
    {
        private readonly IStyleFormatter _inner;

        private CommentPreservingFormatter(IStyleFormatter inner)
        {
            _inner = inner;
        }

        internal static IStyleFormatter Instance { get; } = new CommentPreservingFormatter(CssStyleFormatter.Instance);

        public Boolean PreserveComments => true;

        public String Sheet(IEnumerable<IStyleFormattable> rules) => _inner.Sheet(rules);

        public String BlockRules(IEnumerable<IStyleFormattable> rules) => _inner.BlockRules(rules);

        public String Declaration(String name, String value, Boolean important) => _inner.Declaration(name, value, important);

        public String BlockDeclarations(IEnumerable<IStyleFormattable> declarations) => _inner.BlockDeclarations(declarations);

        public String Rule(String name, String value) => _inner.Rule(name, value);

        public String Rule(String name, String prelude, String rules) => _inner.Rule(name, prelude, rules);

        public String Comment(String data) => _inner.Comment(data);
    }
}
