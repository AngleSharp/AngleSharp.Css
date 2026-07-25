#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class ContainerDeclaration
    {
        public static String Name = PropertyNames.Container;

        public static String[] Longhands = new[]
        {
            PropertyNames.ContainerName,
            PropertyNames.ContainerType,
        };

        public static IValueConverter Converter = new ContainerAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class ContainerAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var pos = source.Index;
                var name = ParseContainerName(source);

                if (name is null)
                {
                    source.BackTo(pos);
                    return null;
                }

                source.SkipSpacesAndComments();

                if (source.Current == Symbols.Solidus)
                {
                    source.SkipCurrentAndSpaces();
                    var type = ContainerTypeDeclaration.Converter.Convert(source);

                    if (type is null)
                    {
                        source.BackTo(pos);
                        return null;
                    }

                    return new CssTupleValue(new[] { name, type }, " / ");
                }

                return name;
            }

            private static ICssValue ParseContainerName(StringSource source)
            {
                var pos = source.Index;

                if (source.IsIdentifier(CssKeywords.None))
                {
                    source.SkipSpacesAndComments();
                    return new CssIdentifierValue(CssKeywords.None);
                }

                source.BackTo(pos);
                var names = new List<ICssValue>();

                while (!source.IsDone)
                {
                    var ident = source.ParseCustomIdent();

                    if (ident is null || ident.Isi(CssKeywords.None))
                    {
                        source.BackTo(pos);
                        return null;
                    }

                    names.Add(new CssIdentifierValue(ident));
                    var c = source.SkipSpacesAndComments();

                    if (source.IsDone || c == Symbols.Solidus)
                    {
                        break;
                    }

                    if (c == Symbols.Comma)
                    {
                        source.BackTo(pos);
                        return null;
                    }
                }

                if (names.Count == 0)
                {
                    source.BackTo(pos);
                    return null;
                }

                return names.Count == 1 ? names[0] : new CssTupleValue(names.ToArray(), " ");
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var name = values[0];
                var type = values[1];

                if (name is null && type is null)
                {
                    return null;
                }

                if (name is null)
                {
                    name = InitialValues.ContainerNameDecl;
                }

                if (type is null || type.CssText.Isi(CssKeywords.Normal))
                {
                    return name;
                }

                return new CssTupleValue(new[] { name, type }, " / ");
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue tuple && tuple.Items.Length == 2)
                {
                    return new[]
                    {
                        tuple.Items[0],
                        tuple.Items[1],
                    };
                }

                return new[]
                {
                    value,
                    InitialValues.ContainerTypeDecl,
                };
            }
        }
    }
}
