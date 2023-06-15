namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
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
            // Syntax:
            // <media-query> = <media-condition> | <media-type>

            source.SkipSpacesAndComments();
            var medium = source.ParseMediaCondition() ?? source.ParseMediaType();

            if (medium is not null)
            {
                foreach (var feature in medium.Features)
                {
                    var validator = factory?.Create(feature.Name);
                    feature.AssociateValidator(validator);
                }
            }

            return medium;
        }

        private static CssMedium ParseMediaCondition(this StringSource source)
        {
            // Syntax:
            // <media-condition> = <media-not> | <media-in-parens> [ <media-and>* | <media-or>* ]

            var medium = source.ParseMediaNot();

            if (medium is null)
            {
                medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    source.SkipSpacesAndComments();
                    var other = source.ParseMediaConnectorMultiple(CssKeywords.And) ?? source.ParseMediaConnectorMultiple(CssKeywords.Or);

                    if (other is not null)
                    {
                        medium = new CssMedium(medium.Type, medium.IsInverse, medium.IsExclusive, medium.Features.Concat(other.Features), other.Connector);
                    }
                }
            }

            return medium;
        }

        private static CssMedium ParseMediaConditionWithoutOr(this StringSource source)
        {
            // Syntax:
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
                        var other = source.ParseMediaConnector(CssKeywords.And);

                        if (other is null)
                        {
                            break;
                        }

                        medium = new CssMedium(medium.Type, medium.IsInverse, medium.IsExclusive, medium.Features.Concat(other.Features), medium.Connector);
                    }
                    while (!source.IsDone);
                }
            }

            return medium;
        }

        private static CssMedium ParseMediaNot(this StringSource source)
        {
            // Syntax:
            // <media-not> = not <media-in-parens>

            var pos = source.Index;

            if (source.IsIdentifier(CssKeywords.Not))
            {
                source.SkipSpacesAndComments();
                var medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    source.SkipSpacesAndComments();
                    return new CssMedium(medium.Type, !medium.IsInverse, medium.IsExclusive, medium.Features, medium.Connector);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static CssMedium ParseMediaInParens(this StringSource source)
        {
            // Syntax:
            // <media-in-parens> = ( <media-condition> ) | <media-feature> | <general-enclosed>

            if (source.Current == Symbols.RoundBracketOpen)
            {
                var pos = source.Index;
                source.SkipCurrentAndSpaces();
                var medium = source.ParseMediaCondition();

                if (medium is not null && source.Current == Symbols.RoundBracketClose)
                {
                    source.SkipCurrentAndSpaces();
                    return medium;
                }

                source.BackTo(pos);
            }

            return source.ParseMediaFeature() ?? source.ParseGeneralEnclosed();
        }

        private static CssMedium ParseMediaConnectorMultiple(this StringSource source, String connector)
        {
            // Syntax:
            // [ <media-and>* | <media-or>* ]

            var part = source.ParseMediaConnector(connector);

            if (part is not null)
            {
                var features = Enumerable.Empty<IMediaFeature>();

                while (part is not null)
                {
                    source.SkipSpacesAndComments();
                    features = features.Concat(part.Features);
                    part = source.ParseMediaConnector(connector);
                }

                return new CssMedium(String.Empty, false, false, features, connector);
            }

            return null;
        }

        private static CssMedium ParseMediaConnector(this StringSource source, String connector)
        {
            // Syntax:
            // <media-and> = and <media-in-parens>
            // or
            // <media-or> = or <media-in-parens>

            var pos = source.Index;

            if (source.IsIdentifier(connector))
            {
                source.SkipSpacesAndComments();
                var medium = source.ParseMediaInParens();

                if (medium is not null)
                {
                    source.SkipSpacesAndComments();
                    return new CssMedium(medium.Type, medium.IsInverse, medium.IsExclusive, medium.Features, connector);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static CssMedium ParseMediaType(this StringSource source)
        {
            // Syntax:
            // <media-type> = [ not | only ]? <ident> [ and <media-condition-without-or> ]?

            var pos = source.Index;
            var inverse = source.IsIdentifier(CssKeywords.Not);
            source.SkipSpacesAndComments();
            var only = !inverse && source.IsIdentifier(CssKeywords.Only);
            source.SkipSpacesAndComments();
            var type = source.ParseIdent();
            source.SkipSpacesAndComments();

            if (type is not null)
            {
                if (!source.IsIdentifier(CssKeywords.And))
                {
                    return new CssMedium(type, inverse, only);
                }

                source.SkipSpacesAndComments();
                var features = source.ParseMediaConditionWithoutOr();

                if (features is not null)
                {
                    return new CssMedium(type, inverse, only, features.Features, features.Connector);
                }

            }

            source.BackTo(pos);
            return null;
        }

        private static CssMedium ParseMediaFeature(this StringSource source)
        {
            // Syntax:
            // <media-feature> = ( [ <mf-plain> | <mf-boolean> | <mf-range> ] )

            if (source.Current == Symbols.RoundBracketOpen)
            {
                var pos = source.Index;
                source.SkipCurrentAndSpaces();
                var feature = source.ParseMediaFeaturePlain() ?? source.ParseMediaFeatureBoolean() ?? source.ParseMediaFeatureRange();

                if (feature is not null && source.Current == Symbols.RoundBracketClose)
                {
                    source.SkipCurrentAndSpaces();
                    return new CssMedium(String.Empty, false, false, Enumerable.Repeat(feature, 1), CssKeywords.And);
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static CssMedium ParseGeneralEnclosed(this StringSource source)
        {
            // Syntax:
            // <general-enclosed> = [ <function-token> <any-value> ) ] | ( <ident> <any-value> )
            //TODO
            return null;
        }

        private static MediaFeature ParseMediaFeaturePlain(this StringSource source)
        {
            // Syntax:
            // mf-plain> = <ident> : <mf-value>

            var pos = source.Index;
            var ident = source.ParseIdent();
            source.SkipSpacesAndComments();

            if (ident is not null && source.Current == Symbols.Colon)
            {
                source.SkipCurrentAndSpaces();
                var value = source.ParseMediaFeatureValue();

                if (value is not null)
                {
                    source.SkipSpacesAndComments();
                    return new MediaFeature(ident, value);
                }
            }

            source.BackTo(pos);
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
            // <mf-range> =
            //       <ident> '<' '='? | '>' '='? | '=' <mf-value>
            //     | <mf-value> '<' '='? | '>' '='? | '=' <ident>
            //     | <mf-value> '<' '='? <ident> '<' '='? <mf-value>
            //     | <mf-value> '>' '='? <ident> '>' '='? <mf-value>

            var pos = source.Index;
            var name = source.ParseIdentAsValue() ?? source.ParseMediaFeatureValue();

            if (name is not null)
            {
                source.SkipSpacesAndComments();
                var op = "";

                if (source.Current == Symbols.LessThan || source.Current == Symbols.GreaterThan)
                {
                    var c = source.Current;

                    if (source.Next() == Symbols.Equality)
                    {
                        op = $"{c}=";
                        source.SkipCurrentAndSpaces();
                    }
                    else
                    {
                        op = $"{c}";
                        source.SkipSpacesAndComments();
                    }
                }
                else if (source.Current == Symbols.Equality)
                {
                    op = "=";
                    source.SkipCurrentAndSpaces();
                }
                else
                {
                    source.BackTo(pos);
                    return null;
                }

                if (name is Identifier)
                {
                    var value = source.ParseMediaFeatureValue();

                    if (value is not null)
                    {
                        return new MediaFeature(name, value, op);
                    }
                }
                else
                {
                    var value = source.ParseIdentAsValue();

                    if (value is not null)
                    {
                        return new MediaFeature(name, value, op);
                    }
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static ICssValue ParseMediaFeatureValue(this StringSource source)
        {
            // Syntax:
            // <mf-value> = <number> | <dimension> | <ident> | <ratio>

            var ident = source.ParseIdent();
            
            if (ident is not null)
            {
                return new Identifier(ident);
            }

            var ratio = source.ParseRatio();

            if (ratio.HasValue)
            {
                return ratio.Value;
            }

            var unit = source.ParseAtomicExpression();
            
            if (unit is not null)
            {
                return unit;
            }

            return null;
        }
    }
}
