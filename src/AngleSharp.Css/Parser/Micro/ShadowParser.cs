namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    static class ShadowParser
    {
        public static Shadow Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseShadow();
            return source.IsDone ? result : null;
        }

        public static Shadow ParseShadow(this StringSource source)
        {
            var start = source.Index;
            var inset = false;
            var offsetX = default(Length?);
            var offsetY = default(Length?);
            var blurRadius = default(Length?);
            var spreadRadius = default(Length?);
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

                if (!offsetX.HasValue)
                {
                    offsetX = source.ParseLength();
                    source.SkipSpacesAndComments();
                }

                if (!offsetY.HasValue)
                {
                    offsetY = source.ParseLength();
                    source.SkipSpacesAndComments();
                }

                if (!blurRadius.HasValue)
                {
                    blurRadius = source.ParseLength();
                    source.SkipSpacesAndComments();
                }

                if (!spreadRadius.HasValue)
                {
                    spreadRadius = source.ParseLength();
                    source.SkipSpacesAndComments();
                }

                if (!color.HasValue)
                {
                    color = source.ParseColor();
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            if (offsetX.HasValue && offsetY.HasValue)
            {
                return new Shadow(
                    inset,
                    offsetX ?? Length.Zero,
                    offsetY ?? Length.Zero,
                    blurRadius ?? Length.Zero,
                    spreadRadius ?? Length.Zero,
                    color ?? Color.Black);
            }

            source.BackTo(start);
            return null;
        }
    }
}
