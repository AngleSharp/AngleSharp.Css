namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class ConditionParser
    {
        public static IConditionFunction Parse(String str, IBrowsingContext context)
        {
            var source = new StringSource(str);
            source.SkipSpacesAndComments();
            var result = source.ParseConditionFunction(context);
            return source.IsDone ? result : null;
        }

        public static IConditionFunction ParseConditionFunction(this StringSource source, IBrowsingContext context) =>
            source.Condition(context);

        private static IConditionFunction Condition(this StringSource source, IBrowsingContext context) =>
            source.Negation(context) ?? source.ConjunctionOrDisjunction(context);

        private static IConditionFunction Negation(this StringSource source, IBrowsingContext context)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null && ident.Isi(CssKeywords.Not))
            {
                source.SkipSpacesAndComments();
                var condition = source.Group(context);

                if (condition != null)
                {
                    return new NotCondition(condition);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static IConditionFunction ConjunctionOrDisjunction(this StringSource source, IBrowsingContext context)
        {
            var condition = source.Group(context);
            source.SkipSpacesAndComments();
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                var isAnd = ident.Is(CssKeywords.And);
                var isOr = ident.Is(CssKeywords.Or);

                if (isAnd || isOr)
                {
                    var conditions = source.Scan(ident, condition, context);

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

        private static IConditionFunction Group(this StringSource source, IBrowsingContext context)
        {
            if (source.Current == Symbols.RoundBracketOpen)
            {
                var current = source.SkipCurrentAndSpaces();
                var condition = default(IConditionFunction);

                if (current != Symbols.RoundBracketClose)
                {
                    condition = source.Condition(context) ?? source.Declaration(context);
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

        private static IConditionFunction Declaration(this StringSource source, IBrowsingContext context)
        {
            var name = source.ParseIdent();
            var colon = source.SkipSpacesAndComments();
            source.SkipCurrentAndSpaces();
            var value = source.TakeUntilClosed();
            source.SkipSpacesAndComments();

            if (name != null && value != null && colon == Symbols.Colon)
            {
                return new DeclarationCondition(context, name, value);
            }

            return null;
        }

        private static IEnumerable<IConditionFunction> Scan(this StringSource source, String keyword, IConditionFunction condition, IBrowsingContext context)
        {
            var conditions = new List<IConditionFunction>();
            var ident = String.Empty;
            conditions.Add(condition);

            do
            {
                source.SkipSpacesAndComments();
                condition = source.Group(context);

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
