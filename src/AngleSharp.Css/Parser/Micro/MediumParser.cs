namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class MediumParser
    {
        public static CssMedium Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseMedium();
            return source.IsDone ? result : null;
        }

        public static CssMedium ParseMedium(this StringSource source)
        {
            source.SkipSpacesAndComments();
            var ident = source.ParseIdent();
            var inverse = false;
            var exclusive = false;
            var type = String.Empty;

            if (ident != null)
            {
                if (ident.Isi(CssKeywords.Not))
                {
                    inverse = true;
                    source.SkipSpacesAndComments();
                    ident = source.ParseIdent();
                }
                else if (ident.Isi(CssKeywords.Only))
                {
                    exclusive = true;
                    source.SkipSpacesAndComments();
                    ident = source.ParseIdent();
                }
            }

            if (ident != null)
            {
                type = ident;
                source.SkipSpacesAndComments();
                var position = source.Index;
                ident = source.ParseIdent();

                if (ident == null || !ident.Isi(CssKeywords.And))
                {
                    source.BackTo(position);
                    return new CssMedium(type, inverse, exclusive);
                }

                source.SkipSpacesAndComments();
            }

            var features = new List<IMediaFeature>();

            do
            {
                var start = source.Current;
                source.SkipCurrentAndSpaces();
                var feature = ParseFeature(source);
                var end = source.Current;

                if (feature == null || 
                    start != Symbols.RoundBracketOpen || 
                    end != Symbols.RoundBracketClose)
                {
                    return null;
                }

                features.Add(feature);
                source.SkipCurrentAndSpaces();
                var position = source.Index;
                ident = source.ParseIdent();

                if (ident == null || !ident.Isi(CssKeywords.And))
                {
                    source.BackTo(position);
                    break;
                }

                source.SkipSpacesAndComments();
            }
            while (!source.IsDone);

            return new CssMedium(type, inverse, exclusive, features);
        }

        private static IMediaFeature ParseFeature(StringSource source)
        {
            var name = source.ParseIdent();
            var value = default(String);

            if (name != null)
            {

                if (source.Current == Symbols.Colon)
                {
                    source.SkipCurrentAndSpaces();
                    value = source.TakeUntilClosed();
                }

                return new MediaFeature(name, value);
            }

            return null;
        }
    }
}
