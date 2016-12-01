namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class TranslateParser
    {
        private static readonly Dictionary<String, Func<StringSource, TranslateTransform>> TranslateFunctions = new Dictionary<String, Func<StringSource, TranslateTransform>>
        {
            { FunctionNames.Translate, ParseTranslate2d },
            { FunctionNames.Translate3d, ParseTranslate3d },
            { FunctionNames.TranslateX, ParseTranslateX },
            { FunctionNames.TranslateY, ParseTranslateY },
            { FunctionNames.TranslateZ, ParseTranslateZ },
        };

        public static TranslateTransform Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseTranslate();
            return source.IsDone ? result : null;
        }

        public static TranslateTransform ParseTranslate(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, TranslateTransform>);

                    if (TranslateFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static TranslateTransform ParseTranslate2d(StringSource source)
        {
            var x = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Length?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistance();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new TranslateTransform(x.Value, y ?? Length.Zero, Length.Zero);
                }
            }

            return null;
        }

        private static TranslateTransform ParseTranslate3d(StringSource source)
        {
            var x = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Length?);
                var z = default(Length?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistance();
                    f = source.SkipGetSkip();

                    if (!y.HasValue)
                    {
                        return null;
                    }

                    if (f == Symbols.Comma)
                    {
                        z = source.ParseDistance();
                        f = source.SkipGetSkip();
                    }
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new TranslateTransform(x.Value, y ?? Length.Zero, z ?? Length.Zero);
                }
            }

            return null;
        }

        private static TranslateTransform ParseTranslateX(StringSource source)
        {
            var x = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (x.HasValue && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransform(x.Value, Length.Zero, Length.Zero);
            }

            return null;
        }

        private static TranslateTransform ParseTranslateY(StringSource source)
        {
            var y = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (y.HasValue && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransform(Length.Zero, y.Value, Length.Zero);
            }

            return null;
        }

        private static TranslateTransform ParseTranslateZ(StringSource source)
        {
            var z = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (z.HasValue && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransform(Length.Zero, Length.Zero, z.Value);
            }

            return null;
        }
    }
}
