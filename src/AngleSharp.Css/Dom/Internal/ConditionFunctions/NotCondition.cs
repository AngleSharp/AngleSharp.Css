namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    sealed class NotCondition : IConditionFunction
    {
        private readonly IConditionFunction _content;

        public NotCondition(IConditionFunction condition)
        {
            _content = condition ?? new EmptyCondition();
        }

        public Boolean Check()
        {
            return !_content.Check();
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("not ");
            _content.ToCss(writer, formatter);
        }
    }
}
