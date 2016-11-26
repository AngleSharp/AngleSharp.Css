namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class ConditionParser
    {
        public static IConditionFunction Parse(String str)
        {
            var source = new StringSource(str);
            source.SkipSpacesAndComments();
            var result = source.ParseConditionFunction();
            return source.IsDone ? result : null;
        }

        public static IConditionFunction ParseConditionFunction(this StringSource source)
        {
            return Condition(source);
        }

        private static IConditionFunction Condition(StringSource source)
        {
            return Negation(source) ?? ConjunctionOrDisjunction(source);
        }

        private static IConditionFunction Negation(StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null && ident.Isi(CssKeywords.Not))
            {
                source.SkipSpacesAndComments();
                var condition = Group(source);

                if (condition != null)
                {
                    return new NotCondition(condition);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static IConditionFunction ConjunctionOrDisjunction(StringSource source)
        {
            var condition = Group(source);
            source.SkipSpacesAndComments();
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                var isAnd = ident.Is(CssKeywords.And);
                var isOr = ident.Is(CssKeywords.Or);

                if (isAnd || isOr)
                {
                    var conditions = Scan(source, ident, condition);

                    if (isAnd)
                    {
                        return new AndCondition(conditions);
                    }
                    else if (isOr)
                    {
                        return new OrCondition(conditions);
                    }
                }
            }

            source.BackTo(pos);
            return condition;
        }

        private static IConditionFunction Group(StringSource source)
        {
            if (source.Current == Symbols.RoundBracketOpen)
            {
                var current = source.SkipCurrentAndSpaces();
                var condition = default(IConditionFunction);

                if (current != Symbols.RoundBracketClose)
                {
                    condition = Condition(source) ?? Declaration(source);
                    current = source.SkipSpacesAndComments();

                    if (condition == null)
                    {
                        return null;
                    }
                }

                if (current == Symbols.RoundBracketClose)
                {
                    source.SkipCurrentAndSpaces();
                    return new GroupCondition(condition);
                }
            }

            return null;
        }

        private static IConditionFunction Declaration(StringSource source)
        {
            var name = source.ParseIdent();
            var colon = source.SkipSpacesAndComments();
            source.SkipCurrentAndSpaces();
            var value = source.TakeUntilClosed();
            source.SkipSpacesAndComments();

            if (name != null && value != null && colon == Symbols.Colon)
            {
                return new DeclarationCondition(name, value);
            }

            return null;
        }

        private static IEnumerable<IConditionFunction> Scan(StringSource source, String keyword, IConditionFunction condition)
        {
            var conditions = new List<IConditionFunction>();
            var ident = String.Empty;
            conditions.Add(condition);

            do
            {
                source.SkipSpacesAndComments();
                condition = Group(source);

                if (condition == null)
                    break;

                conditions.Add(condition);
                source.SkipSpacesAndComments();
                ident = source.ParseIdent();
            }
            while (ident != null && ident.Is(keyword));

            return conditions;
        }
    }
}
