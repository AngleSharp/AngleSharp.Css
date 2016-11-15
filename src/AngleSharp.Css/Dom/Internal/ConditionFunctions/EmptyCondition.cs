namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    sealed class EmptyCondition : IConditionFunction
    {
        public Boolean Check()
        {
            return true;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }
    }
}
