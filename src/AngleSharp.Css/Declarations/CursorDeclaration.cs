namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using static ValueConverters;

    static class CursorDeclaration
    {
        public static String Name = PropertyNames.Cursor;

        public static IValueConverter Converter = Or(new CursorValueConverter(), AssignInitial(SystemCursor.Auto));

        public static PropertyFlags Flags = PropertyFlags.Inherited;

        sealed class CursorValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var cursor = default(ICssValue);
                var definitions = new List<ICssValue>();

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
                            return new Cursor(definitions.ToArray(), cursor);
                        }

                        break;
                    }
                }

                return null;
            }
        }
    }
}
