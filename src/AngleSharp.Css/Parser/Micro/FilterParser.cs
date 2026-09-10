#nullable disable
namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Parses CSS filter function lists.
    /// </summary>
    public static class FilterParser
    {
        /// <summary>
        /// Parses a space-separated list of CSS filter functions.
        /// </summary>
        /// <param name="source">The source to parse.</param>
        /// <returns>The parsed filter value, if valid.</returns>
        public static ICssValue ParseFilter(StringSource source)
        {
            var start = source.Index;
            var functions = new List<ICssFilterFunctionValue>();

            while (!source.IsDone)
            {
                source.SkipSpacesAndComments();
                var functionStart = source.Index;
                var name = source.ParseIdent();

                if (name is null || source.Current != Symbols.RoundBracketOpen)
                {
                    source.BackTo(start);
                    return null;
                }

                var openIndex = source.Index;
                var depth = 1;
                var closeIndex = openIndex;

                for (var i = openIndex + 1; i < source.Content.Length; i++)
                {
                    var current = source.Content[i];

                    if (current == Symbols.RoundBracketOpen)
                    {
                        depth++;
                    }
                    else if (current == Symbols.RoundBracketClose)
                    {
                        depth--;

                        if (depth == 0)
                        {
                            closeIndex = i;
                            break;
                        }
                    }
                }

                if (depth != 0)
                {
                    source.BackTo(start);
                    return null;
                }

                var text = source.Content.Substring(functionStart, closeIndex - functionStart + 1);
                source.NextTo(closeIndex + 1);
                source.SkipSpacesAndComments();
                var argumentStart = text.IndexOf('(') + 1;
                var argumentText = text.Substring(argumentStart, text.Length - argumentStart - 1).Trim();
                functions.Add(new CssFilterFunctionValue(name, String.IsNullOrEmpty(argumentText) ? Array.Empty<ICssValue>() : new ICssValue[] { new CssAnyValue(argumentText, true) }, text));
            }

            return functions.Count > 0 ? new CssFilterValue(functions.ToArray()) : null;
        }
    }
}
