namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class CursorValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var cursor = default(ICssValue);
            var definitions = new List<CursorDefinition>();

            while (!source.IsDone)
            {
                var definition = new CursorDefinition();
                definition.Source = source.ParseImageSource();
                var c = source.SkipSpacesAndComments();

                if (definition.Source != null)
                {
                    var x = source.ParseNumber();
                    c = source.SkipSpacesAndComments();
                    var y = source.ParseNumber();
                    c = source.SkipSpacesAndComments();

                    if (x.HasValue != y.HasValue || c != Symbols.Comma)
                        break;

                    source.SkipCurrentAndSpaces();

                    if (x.HasValue)
                    {
                        var xp = new Length(x.Value, Length.Unit.None);
                        var yp = new Length(y.Value, Length.Unit.None);
                        definition.Position = new Point(xp, yp);
                    }

                    definitions.Add(definition);
                }
                else
                {
                    cursor = source.ParseConstant(Map.SystemCursors);

                    if (cursor != null)
                    {
                        return new CursorValue(definitions.ToArray(), cursor);
                    }

                    break;
                }
            }

            return null;
        }

        struct CursorDefinition
        {
            public IImageSource Source;
            public Point? Position;

            public override String ToString()
            {
                var sb = StringBuilderPool.Obtain();

                sb.Append(Source.ToString());

                if (Position.HasValue)
                {
                    sb.Append(Symbols.Space);
                    sb.Append(Position.Value.ToString());
                }

                return sb.ToPool();
            }
        }

        sealed class CursorValue : ICssValue
        {
            private readonly CursorDefinition[] _definitions;
            private readonly ICssValue _cursor;

            public CursorValue(CursorDefinition[] definitions, ICssValue cursor)
            {
                _definitions = definitions;
                _cursor = cursor;
            }

            public String CssText
            {
                get
                {
                    var sb = StringBuilderPool.Obtain();

                    foreach (var definition in _definitions)
                    {
                        sb.Append(definition).Append(", ");
                    }

                    sb.Append(_cursor.CssText);
                    return sb.ToPool();
                }
            }
        }
    }
}
