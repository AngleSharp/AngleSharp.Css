namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    sealed class AndCondition : IConditionFunction
    {
        private readonly IConditionFunction[] _conditions;

        public AndCondition(IEnumerable<IConditionFunction> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public Boolean Check(IRenderDevice device)
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Check(device))
                {
                    return false;
                }
            }

            return true;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var first = true;

            foreach (var condition in _conditions)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    writer.Write(" and ");
                }

                condition.ToCss(writer, formatter);
            }
        }
    }
}
