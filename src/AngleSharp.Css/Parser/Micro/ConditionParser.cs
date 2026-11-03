#nullable disable
namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents extensions to for CSS condition values.
    /// </summary>
    public static class ConditionParser
    {
        internal static IConditionFunction Parse(String str, IBrowsingContext context)
        {
            var source = new StringSource(str);
            source.SkipSpacesAndComments();
            var result = source.ParseConditionFunction(context);
            return source.IsDone ? result : null;
        }

        /// <summary>
        /// Parse a CSS condition function using the current browsing context.
        /// </summary>
        public static IConditionFunction ParseConditionFunction(this StringSource source, IBrowsingContext context) =>
            source.Condition(context, 0);

        private static IConditionFunction Condition(this StringSource source, IBrowsingContext context, Int32 depth) =>
            source.Negation(context, depth) ?? source.ConjunctionOrDisjunction(context, depth);

        private static IConditionFunction Negation(this StringSource source, IBrowsingContext context, Int32 depth)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null && ident.Isi(CssKeywords.Not))
            {
                source.SkipSpacesAndComments();
                var condition = source.Group(context, depth);

                if (condition != null)
                {
                    return new NotCondition(condition);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static IConditionFunction ConjunctionOrDisjunction(this StringSource source, IBrowsingContext context, Int32 depth)
        {
            var condition = source.Group(context, depth);
            source.SkipSpacesAndComments();
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                var isAnd = ident.Isi(CssKeywords.And);
                var isOr = ident.Isi(CssKeywords.Or);

                if (isAnd || isOr)
                {
                    var conditions = source.Scan(ident, condition, context, depth);

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

        private static IConditionFunction Group(this StringSource source, IBrowsingContext context, Int32 depth)
        {
            // each parenthesis recurses, so beyond the limit the condition is invalid
            if (source.Current == Symbols.RoundBracketOpen && depth < ValueNesting.Limit)
            {
                var current = source.SkipCurrentAndSpaces();
                var condition = default(IConditionFunction);

                if (current != Symbols.RoundBracketClose)
                {
                    condition = source.Condition(context, depth + 1) ?? source.Declaration(context);
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

        private static IEnumerable<IConditionFunction> Scan(this StringSource source, String keyword, IConditionFunction condition, IBrowsingContext context, Int32 depth)
        {
            var conditions = new List<IConditionFunction>();
            var ident = String.Empty;
            conditions.Add(condition);

            do
            {
                source.SkipSpacesAndComments();
                condition = source.Group(context, depth);

                if (condition == null)
                    break;

                conditions.Add(condition);
                source.SkipSpacesAndComments();
                ident = source.ParseIdent();
            }
            while (ident != null && ident.Isi(keyword));

            return conditions;
        }
    }
}
