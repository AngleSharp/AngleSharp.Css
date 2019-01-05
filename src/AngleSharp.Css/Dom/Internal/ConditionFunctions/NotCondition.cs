namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    sealed class NotCondition : IConditionFunction
    {
        private readonly IConditionFunction _content;

        public NotCondition(IConditionFunction condition)
        {
            _content = condition;
        }

        public Boolean Check(IRenderDevice device)
        {
            return !_content.Check(device);
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("not ");
            _content.ToCss(writer, formatter);
        }
    }
}
