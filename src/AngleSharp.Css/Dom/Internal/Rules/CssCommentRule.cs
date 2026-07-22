#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a parsed CSS comment as a rule node.
    /// </summary>
    sealed class CssCommentRule : CssRule
    {
        private readonly String _data;

        internal CssCommentRule(ICssStyleSheet owner, String data)
            : base(owner, CssRuleType.Comment)
        {
            _data = data;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (formatter is ICommentPreservingFormatter commentFormatter && commentFormatter.PreserveComments)
            {
                writer.Write("/*");
                writer.Write(_data);
                writer.Write("*/");
            }
        }

        protected override void ReplaceWith(ICssRule rule)
        {
        }
    }
}
