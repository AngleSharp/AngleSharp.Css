namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents extensions to for symbols() values.
    /// </summary>
    public static class SymbolsParser
    {
        /// <summary>
        /// Parse a CSS symbols() value.
        /// </summary>
        public static CssSymbolsValue ParseSymbols(this StringSource source)
        {
            var start = source.Index;

            if (source.IsFunction(FunctionNames.Symbols))
            {
                source.SkipSpacesAndComments();
                var pos = source.Index;
                var type = ValueConverters.SymbolsTypeConverter.Convert(source);
                var entries = new List<String>();

                if (type is null)
                {
                    source.BackTo(pos);
                }
                else
                {
                    source.SkipSpacesAndComments();
                }

                do
                {
                    var entry = StringParser.ParseString(source);

                    if (entry is null)
                    {
                        break;
                    }

                    entries.Add(entry);

                    var c = source.SkipSpacesAndComments();

                    if (c == Symbols.RoundBracketClose)
                    {
                        source.Next();
                        return new CssSymbolsValue(type, entries);
                    }
                }
                while (true);

                source.BackTo(start);
            }

            return null;
        }
    }
}
