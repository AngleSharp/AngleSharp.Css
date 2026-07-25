#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class ContainerNameDeclaration
    {
        public static String Name = PropertyNames.ContainerName;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Container,
        };

        public static IValueConverter Converter = new ContainerNameValueConverter();

        public static ICssValue InitialValue = InitialValues.ContainerNameDecl;

        public static PropertyFlags Flags = PropertyFlags.None;

        sealed class ContainerNameValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
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

                    if (source.IsDone)
                    {
                        break;
                    }

                    if (c == Symbols.Comma || c == Symbols.Solidus)
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
        }
    }
}
