namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    sealed class NotCondition(IConditionFunction condition) : IConditionFunction
    {
        private readonly IConditionFunction _content = condition;

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
