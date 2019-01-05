namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    static class ShadowParser
    {
        public static Shadow ParseShadow(this StringSource source)
        {
            var start = source.Index;
            var inset = false;
            var offsetX = default(ICssValue);
            var offsetY = default(ICssValue);
            var blurRadius = default(ICssValue);
            var spreadRadius = default(ICssValue);
            var color = default(Color?);
            var pos = start;

            do
            {
                pos = source.Index;

                if (!inset)
                {
                    inset = source.IsIdentifier(CssKeywords.Inset);
                    source.SkipSpacesAndComments();
                }

                if (offsetX == null)
                {
                    offsetX = source.ParseLengthOrCalc();
                    source.SkipSpacesAndComments();
                }

                if (offsetY == null)
                {
                    offsetY = source.ParseLengthOrCalc();
                    source.SkipSpacesAndComments();
                }

                if (blurRadius == null)
                {
                    blurRadius = source.ParseLengthOrCalc();
                    source.SkipSpacesAndComments();
                }

                if (spreadRadius == null)
                {
                    spreadRadius = source.ParseLengthOrCalc();
                    source.SkipSpacesAndComments();
                }

                if (!color.HasValue)
                {
                    color = source.ParseColor();
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            if (offsetX != null && offsetY != null)
            {
                return new Shadow(
                    inset,
                    offsetX,
                    offsetY,
                    blurRadius,
                    spreadRadius,
                    color ?? Color.Black);
            }

            source.BackTo(start);
            return null;
        }
    }
}
