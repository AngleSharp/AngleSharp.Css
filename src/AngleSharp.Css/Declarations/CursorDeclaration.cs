namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class CursorDeclaration
    {
        public static String Name = PropertyNames.Cursor;

        public static IValueConverter Converter = new CursorValueConverter();

        public static ICssValue InitialValue = InitialValues.CursorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;

        sealed class CursorValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var cursor = default(ICssValue);
                var definitions = new List<ICssValue>();

                while (!source.IsDone)
                {
                    var imageSource = source.ParseImageSource();
                    var c = source.SkipSpacesAndComments();

                    if (imageSource != null)
                    {
                        var x = source.ParseNumber();
                        c = source.SkipSpacesAndComments();
                        var y = source.ParseNumber();
                        c = source.SkipSpacesAndComments();

                        if (x.HasValue != y.HasValue || c != Symbols.Comma)
                            break;

                        source.SkipCurrentAndSpaces();
                        var position = default(Point?);

                        if (x.HasValue)
                        {
                            var xp = new Length(x.Value, Length.Unit.None);
                            var yp = new Length(y.Value, Length.Unit.None);
                            position = new Point(xp, yp);
                        }

                        definitions.Add(new CssCustomCursorValue(imageSource, position));
                    }
                    else
                    {
                        cursor = source.ParseConstant(Map.SystemCursors);

                        if (cursor != null)
                        {
                            return new CssCursorValue(definitions.ToArray(), cursor);
                        }

                        break;
                    }
                }

                return null;
            }
        }
    }
}
