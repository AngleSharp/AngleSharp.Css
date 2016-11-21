namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    sealed class GroupCondition : IConditionFunction
    {
        private readonly IConditionFunction _content;

        public GroupCondition(IConditionFunction condition)
        {
            _content = condition ?? new EmptyCondition();
        }

        public Boolean Check(IRenderDevice device)
        {
            return _content.Check(device);
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("(");
            _content.ToCss(writer, formatter);
            writer.Write(")");
        }
    }
}
