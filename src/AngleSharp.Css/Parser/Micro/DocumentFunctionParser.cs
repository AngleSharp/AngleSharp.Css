namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class DocumentFunctionParser
    {
        public static IEnumerable<IDocumentFunction> Parse(String str, IDocumentFunctionFactory factory)
        {
            var source = new StringSource(str);
            var result = source.ParseDocumentFunctions(factory);
            return source.IsDone ? result : null;
        }

        public static IEnumerable<IDocumentFunction> ParseDocumentFunctions(this StringSource source, IDocumentFunctionFactory factory)
        {
            var functions = new List<IDocumentFunction>();

            while (!source.IsDone)
            {
                var current = source.SkipSpacesAndComments();

                if (functions.Count > 0)
                {
                    if (current != Symbols.Comma)
                        return null;

                    source.SkipCurrentAndSpaces();
                }

                var position = source.Index;
                var uri = source.ParseUri();
                var name = FunctionNames.Url;
                var data = uri?.Path;

                if (uri == null)
                {
                    source.BackTo(position);
                    name = source.ParseIdent();
                    var start = source.Current;

                    if (name == null || start != Symbols.RoundBracketOpen)
                        return null;

                    source.SkipCurrentAndSpaces();
                    data = source.ParseString();
                    var end = source.SkipSpacesAndComments();

                    if (end != Symbols.RoundBracketClose)
                        return null;

                    source.SkipCurrentAndSpaces();
                }

                var function = factory.Create(name, data);

                if (function == null)
                    return null;

                functions.Add(function);
            }

            return functions;
        }
    }
}
