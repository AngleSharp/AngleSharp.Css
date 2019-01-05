namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    static class ShapeParser
    {
        public static Shape ParseShape(this StringSource source)
        {
            var pos = source.Index;

            do
            {
                if (source.IsFunction(FunctionNames.Rect))
                {
                    var top = source.ParseLength();
                    var isc = SkipIfComma(source);
                    var right = source.ParseLength();

                    if (SkipIfComma(source) != isc)
                        break;

                    var bottom = source.ParseLength();

                    if (SkipIfComma(source) != isc)
                        break;

                    var left = source.ParseLength();
                    var f = source.SkipSpacesAndComments();

                    if (top.HasValue && right.HasValue && bottom.HasValue && left.HasValue && f == Symbols.RoundBracketClose)
                    {
                        source.SkipCurrentAndSpaces();
                        return new Shape(top.Value, right.Value, bottom.Value, left.Value);
                    }
                }
            }
            while (false);

            source.BackTo(pos);
            return null;
        }

        private static Boolean SkipIfComma(StringSource source)
        {
            if (source.SkipSpacesAndComments() == Symbols.Comma)
            {
                source.SkipCurrentAndSpaces();
                return true;
            }

            return false;
        }
    }
}
