namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    sealed class EmptyCondition : IConditionFunction
    {
        public Boolean Check(IRenderDevice device)
        {
            return false;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }
    }
}
