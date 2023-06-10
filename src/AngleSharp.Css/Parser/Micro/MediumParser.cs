namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents extensions to for medium (media type) values.
    /// </summary>
    public static class MediumParser
    {
        internal static CssMedium Parse(String str, IFeatureValidatorFactory factory)
        {
            var source = new StringSource(str);
            var result = source.ParseMedium(factory);
            return source.IsDone ? result : null;
        }

        /// <summary>
        /// Parses a medium value.
        /// </summary>
        public static CssMedium ParseMedium(this StringSource source, IFeatureValidatorFactory factory)
        {
            source.SkipSpacesAndComments();
            var ident = source.ParseMediumIdent();
            var inverse = false;
            var exclusive = false;
            var type = String.Empty;

            if (ident != null)
            {
                if (ident.Isi(CssKeywords.Not))
                {
                    inverse = true;
                    source.SkipSpacesAndComments();
                    ident = source.ParseMediumIdent();
                }
                else if (ident.Isi(CssKeywords.Only))
                {
                    exclusive = true;
                    source.SkipSpacesAndComments();
                    ident = source.ParseMediumIdent();
                }
            }

            if (ident != null)
            {
                type = ident;
                source.SkipSpacesAndComments();
                var position = source.Index;
                ident = source.ParseMediumIdent();

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

                var validator = factory?.Create(feature.Name);
                feature.AssociateValidator(validator);
                features.Add(feature);
                source.SkipCurrentAndSpaces();
                var position = source.Index;
                ident = source.ParseMediumIdent();

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

        /// <summary>
        /// Parses a medium value.
        /// </summary>
        public static CssMedium ParseMediumNew(this StringSource source, IFeatureValidatorFactory factory)
        {
            // <media-query> = <media-condition> | <media-type>
            source.SkipSpacesAndComments();
            return source.ParseMediaCondition() ?? source.ParseMediaType();
        }

        private static CssMedium ParseMediaCondition(this StringSource source)
        {
            // <media-condition> = <media-not> | <media-in-parens> [ <media-and>* | <media-or>* ]

            var medium = source.ParseMediaNot();

            if (medium is null)
            {
                medium = source.ParseMediaInParens();

                if (medium != null)
                {
                    var other = source.ParseMediaAnd();
                    //TODO
                }
            }

            return medium;
        }

        private static CssMedium ParseMediaConditionWithoutOr(this StringSource source)
        {
            // <media-condition-without-or> = <media-not> | <media-in-parens> <media-and>*

            var medium = source.ParseMediaNot();

            if (medium is null)
            {
                medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    do
                    {
                        source.SkipSpacesAndComments();
                        var other = source.ParseMediaAnd();

                        if (other is null)
                        {
                            break;
                        }

                        medium = new CssMedium(medium.Type, medium.IsInverse, medium.IsExclusive, medium.Features.Concat(other.Features));
                    }
                    while (!source.IsDone);
                }
            }

            return medium;
        }

        private static CssMedium ParseMediaNot(this StringSource source)
        {
            // <media-not> = not <media-in-parens>

            if (source.IsIdentifier("not"))
            {
                var pos = source.Index;
                source.ParseIdent();
                source.SkipCurrentAndSpaces();
                var medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    source.SkipSpacesAndComments();
                    return new CssMedium(medium.Type, !medium.IsInverse, medium.IsExclusive, medium.Features);
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static CssMedium ParseMediaInParens(this StringSource source)
        {
            // <media-in-parens> = ( <media-condition> ) | <media-feature> | <general-enclosed>

            if (source.Current == '(')
            {
                var pos = source.Index;
                source.SkipCurrentAndSpaces();
                var medium = source.ParseMediaCondition();

                if (medium is not null && source.Current == ')')
                {
                    source.SkipCurrentAndSpaces();
                    return medium;
                }

                source.BackTo(pos);
            }

            return source.ParseMediaFeature() ?? source.ParseGeneralEnclosed();
        }

        private static CssMedium ParseMediaAnd(this StringSource source)
        {
            // <media-and> = and <media-in-parens>

            if (source.IsIdentifier("and"))
            {
                var pos = source.Index;
                source.ParseIdent();
                source.SkipCurrentAndSpaces();
                var medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    source.SkipSpacesAndComments();
                    return new CssMedium(medium.Type, medium.IsInverse, medium.IsExclusive, medium.Features, "and");
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static CssMedium ParseMediaOr(this StringSource source)
        {
            // <media-or> = or <media-in-parens>

            if (source.IsIdentifier("or"))
            {
                var pos = source.Index;
                source.ParseIdent();
                source.SkipCurrentAndSpaces();
                var medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    source.SkipSpacesAndComments();
                    return new CssMedium(medium.Type, medium.IsInverse, medium.IsExclusive, medium.Features, "or");
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static CssMedium ParseMediaType(this StringSource source)
        {
            // <media-type> = [ not | only ]? <ident> [ and <media-condition-without-or> ]?
            //TODO
            return null;
        }

        private static CssMedium ParseMediaFeature(this StringSource source)
        {
            // <media-feature> = ( [ <mf-plain> | <mf-boolean> | <mf-range> ] )

            if (source.Current == '(')
            {
                var feature = source.ParseMediaFeaturePlain() ?? source.ParseMediaFeatureBoolean() ?? source.ParseMediaFeatureRange();

                if (feature is not null && source.Current == ')')
                {
                    source.SkipCurrentAndSpaces();
                    return new CssMedium("", false, false, Enumerable.Repeat(feature, 1), "and");
                }
            }

            return null;
        }

        private static CssMedium ParseGeneralEnclosed(this StringSource source)
        {
            // <general-enclosed> = [ <function-token> <any-value> ) ] | ( <ident> <any-value> )
            //TODO
            return null;
        }

        private static MediaFeature ParseMediaFeaturePlain(this StringSource source)
        {
            // <mf-plain> = <ident> : <mf-value>
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident is not null && source.Current == ':')
            {
                var value = source.ParseMediaFeatureValue();

                if (value is not null)
                {
                    source.SkipSpacesAndComments();
                    return new MediaFeature(ident, value);
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static MediaFeature ParseMediaFeatureBoolean(this StringSource source)
        {
            // <mf-boolean> = <ident>
            var ident = source.ParseIdent();

            if (ident is not null)
            {
                source.SkipSpacesAndComments();
                return new MediaFeature(ident);
            }

            return null;
        }

        private static MediaFeature ParseMediaFeatureRange(this StringSource source)
        {
            // <mf-range> = <ident> '<' '='? | '>' '='? | '=' <mf-value>
            //     | <mf-value> '<' '='? | '>' '='? | '=' <ident>
            //     | <mf-value> '<' '='? <ident> '<' '='? <mf-value>
            //     | <mf-value> '>' '='? <ident> '>' '='? <mf-value>
            //TODO
            return null;
        }

        private static ICssValue ParseMediaFeatureValue(this StringSource source)
        {
            // <mf-value> = <number> | <dimension> | <ident> | <ratio>
            //TODO
            var ident = source.ParseNumber() ?? source.ParseIdent() ?? source.ParseUnit() ?? source.ParseRatio();
            return null;
        }

        private static String ParseMediumIdent(this StringSource source)
        {
            var ident = source.ParseIdent();
            var current = source.Current;

            while (current == '&' || current == '#' || current == ';' || current == '-')
            {
                ident = null;
                current = source.Next();
            }

            return ident;
        }

        private static IMediaFeature ParseFeature(StringSource source)
        {
            var name = source.ParseMediumIdent();
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
