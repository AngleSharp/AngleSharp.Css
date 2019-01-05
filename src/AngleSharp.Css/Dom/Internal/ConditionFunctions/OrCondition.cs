namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    sealed class OrCondition : IConditionFunction
    {
        private readonly IConditionFunction[] _conditions;

        public OrCondition(IEnumerable<IConditionFunction> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public Boolean Check(IRenderDevice device)
        {
            foreach (var condition in _conditions)
            {
                if (condition.Check(device))
                {
                    return true;
                }
            }

            return false;
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
                    writer.Write(" or ");
                }

                condition.ToCss(writer, formatter);
            }
        }
    }
}
