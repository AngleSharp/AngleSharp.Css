namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents extensions to for identifier values.
    /// </summary>
    public static class IdentParser
    {
        /// <summary>
        /// Parses a normalized CSS identifier value.
        /// </summary>
        public static String ParseNormalizedIdent(this StringSource source)
        {
            var result = source.ParseIdent();
            return result != null ? result.ToLowerInvariant() : result;
        }

        /// <summary>
        /// Parses a custom CSS identifier value.
        /// </summary>
        public static String ParseCustomIdent(this StringSource source)
        {
            var current = source.Current;
            var buffer = StringBuilderPool.Obtain();

            if (current == Symbols.Minus)
            {
                buffer.Append(Symbols.Minus);
                current = source.Next();
            }

            return Start(source, current, buffer);
        }

        /// <summary>
        /// Parses a CSS identifier value.
        /// </summary>
        public static String ParseIdent(this StringSource source)
        {
            var current = source.Current;
            var buffer = StringBuilderPool.Obtain();
            return Start(source, current, buffer);
        }

        /// <summary>
        /// Parses a CSS identifier value.
        /// </summary>
        public static ICssValue ParseIdentAsValue(this StringSource source)
        {
            var value = source.ParseIdent();

            if (value is not null)
            {
                return new Identifier(value);
            }

            return null;
        }

        /// <summary>
        /// Parses a CSS constant value from a given dictionary.
        /// </summary>
        public static ICssValue ParseConstant<T>(this StringSource source, IDictionary<String, T> values)
            where T : struct
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null && values.TryGetValue(ident, out T mode))
            {
                return mode as ICssValue ?? new Constant<T>(ident.ToLowerInvariant(), mode);
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses a CSS static value from a given dictionary.
        /// </summary>
        public static Constant<T>? ParseStatic<T>(this StringSource source, IDictionary<String, T> values)
        {
            var ident = source.ParseIdent();

            if (ident != null && values.TryGetValue(ident, out T mode))
            {
                return new Constant<T>(ident.ToLowerInvariant(), mode);
            }

            return null;
        }

        /// <summary>
        /// Checks if a CSS function with the given name is at the current position.
        /// </summary>
        public static Boolean IsFunction(this StringSource source, String name)
        {
            var rest = source.Content.Length - source.Index;

            if (rest >= name.Length + 2)
            {
                var length = 0;
                var current = source.Current;
                var pos = source.Index;

                while (length < name.Length)
                {
                    if (current == Symbols.ReverseSolidus && source.IsValidEscape())
                    {
                        var next = source.ConsumeEscape();

                        if (next.Length != 1)
                            break;

                        current = next[0];
                    }

                    if (Char.ToLowerInvariant(current) != Char.ToLowerInvariant(name[length]))
                        break;

                    length++;
                    current = source.Next();
                }

                if (length == name.Length && current == Symbols.RoundBracketOpen)
                {
                    source.SkipCurrentAndSpaces();
                    return true;
                }

                source.BackTo(pos);
            }

            return false;
        }

        /// <summary>
        /// Checks if the provided identifier is available at the current position.
        /// </summary>
        public static Boolean IsIdentifier(this StringSource source, String identifier)
        {
            var pos = source.Index;
            var test = source.ParseIdent();

            if (test != null && test.Isi(identifier))
            {
                return true;
            }

            source.BackTo(pos);
            return false;
        }

        /// <summary>
        /// Parses the font family at the current position.
        /// </summary>
        public static ICssValue ParseFontFamily(this StringSource source)
        {
            var str = source.ParseString();

            if (str == null)
            {
                var literal = source.ParseLiteral();

                if (literal != null)
                {
                    return new Identifier(literal);
                }

                return null;
            }

            return new Label(str);
        }

        /// <summary>
        /// Parses the font families at the current position.
        /// </summary>
        public static ICssValue[] ParseFontFamilies(this StringSource source)
        {
            var family = source.ParseFontFamily();

            if (family != null)
            {
                var families = new List<ICssValue>();
                var c = source.SkipSpacesAndComments();
                families.Add(family);

                while (!source.IsDone)
                {
                    if (c != Symbols.Comma)
                    {
                        return null;
                    }
                    
                    source.SkipCurrentAndSpaces();
                    family = source.ParseFontFamily();
                    families.Add(family);
                    c = source.SkipSpacesAndComments();
                }

                return families.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Parses the string literal value.
        /// </summary>
        public static String ParseLiteral(this StringSource source)
        {
            var content = StringBuilderPool.Obtain();

            while (!source.IsDone)
            {
                source.SkipSpacesAndComments();
                var item = source.ParseIdent();

                if (item == null)
                    break;

                if (content.Length > 0)
                {
                    content.Append(Symbols.Space);
                }

                content.Append(item);
            }

            var result = content.ToPool();
            return result.Length > 0 ? result : null;
        }

        /// <summary>
        /// Parses the CSS identifier value - from the animatable list.
        /// </summary>
        public static String ParseAnimatableIdent(this StringSource source)
        {
            var pos = source.Index;
            var test = source.ParseNormalizedIdent();

            //TODO Replace Animatables with call to DeclarationFactory - get from flags
            if (test != null && (test.Isi(CssKeywords.All) || Animatables.Contains(test)))
            {
                return test;
            }

            source.BackTo(pos);
            return null;
        }

        private static String Start(StringSource source, Char current, StringBuilder buffer)
        {
            if (current == Symbols.Minus)
            {
                current = source.Next();

                if (current.IsNameStart() || source.IsValidEscape())
                {
                    buffer.Append(Symbols.Minus);
                    return Rest(source, current, buffer);
                }

                source.Back();
            }
            else if (current.IsNameStart())
            {
                buffer.Append(current);
                return Rest(source, source.Next(), buffer);
            }
            else if (current == Symbols.ReverseSolidus && source.IsValidEscape())
            {
                buffer.Append(source.ConsumeEscape());
                return Rest(source, source.Next(), buffer);
            }

            buffer.ToPool();
            return null;
        }

        private static String Rest(StringSource source, Char current, StringBuilder buffer)
        {
            while (true)
            {
                if (current.IsName())
                {
                    buffer.Append(current);
                }
                else if (source.IsValidEscape())
                {
                    buffer.Append(source.ConsumeEscape());
                }
                else
                {
                    return buffer.ToPool();
                }

                current = source.Next();
            }
        }

        private static readonly HashSet<String> Animatables = new(StringComparer.OrdinalIgnoreCase)
        {
            "backdrop-filter",
            "background",
            "background-color",
            "background-position",
            "background-size",
            "border",
            "border-bottom",
            "border-bottom-color",
            "border-bottom-left-radius",
            "border-bottom-right-radius",
            "border-bottom-width",
            "border-color",
            "border-left",
            "border-left-color",
            "border-left-width",
            "border-radius",
            "border-right",
            "border-right-color",
            "border-right-width",
            "border-top",
            "border-top-color",
            "border-top-left-radius",
            "border-top-right-radius",
            "border-top-width",
            "border-width",
            "bottom",
            "box-shadow",
            "clip",
            "clip-path",
            "color",
            "column-count",
            "column-gap",
            "column-rule",
            "column-rule-color",
            "column-rule-width",
            "column-width",
            "columns",
            "filter",
            "flex",
            "flex-basis",
            "flex-grow",
            "flex-shrink",
            "font",
            "font-size",
            "font-size-adjust",
            "font-stretch",
            "font-weight",
            "grid-column-gap",
            "grid-gap",
            "grid-row-gap",
            "height",
            "left",
            "letter-spacing",
            "line-height",
            "margin",
            "margin-bottom",
            "margin-left",
            "margin-right",
            "margin-top",
            "mask",
            "mask-position",
            "mask-size",
            "max-height",
            "max-width",
            "min-height",
            "min-width",
            "motion-offset",
            "motion-rotation",
            "object-position",
            "opacity",
            "order",
            "outline",
            "outline-color",
            "outline-offset",
            "outline-width",
            "padding",
            "padding-bottom",
            "padding-left",
            "padding-right",
            "padding-top",
            "perspective",
            "perspective-origin",
            "right",
            "scroll-snap-coordinate",
            "scroll-snap-destination",
            "shape-image-threshold",
            "shape-margin",
            "shape-outside",
            "text-decoration",
            "text-decoration-color",
            "text-emphasis",
            "text-emphasis-color",
            "text-indent",
            "text-shadow",
            "top",
            "transform",
            "transform-origin",
            "vertical-align",
            "visibility",
            "width",
            "word-spacing",
            "z-index"
        };
    }
}
