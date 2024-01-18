namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents extensions to for color values.
    /// </summary>
    static class ColorParser
    {
        private static readonly Dictionary<String, Func<StringSource, CssColorValue?>> ColorFunctions = new(StringComparer.OrdinalIgnoreCase)
        {
            { FunctionNames.Rgb, ParseRgba },
            { FunctionNames.Rgba, ParseRgba },
            { FunctionNames.Hsl, ParseHsla },
            { FunctionNames.Hsla, ParseHsla },
            { FunctionNames.Gray, ParseGray },
            { FunctionNames.Hwb, ParseHwba },
            { FunctionNames.Hwba, ParseHwba },
            { FunctionNames.Lab, ParseLab },
            { FunctionNames.Lch, ParseLch },
            { FunctionNames.Oklab, ParseOklab},
            { FunctionNames.Oklch, ParseOklch },
        };

        /// <summary>
        /// Parses a color value, if any.
        /// </summary>
        public static CssColorValue? ParseColor(this StringSource source)
        {
            var pos = source.Index;
            var result = Start(source);

            if (result == null)
            {
                source.BackTo(pos);
            }

            return result;
        }

        /// <summary>
        /// Parses a the current color value, if any.
        /// </summary>
        public static CssColorValue? ParseCurrentColor(this StringSource source) =>
            source.IsIdentifier(CssKeywords.CurrentColor) ? CssColorValue.CurrentColor : ColorParser.ParseColor(source);

        private static CssColorValue? Start(StringSource source)
        {
            if (source.Current != Symbols.Num)
            {
                var ident = source.ParseIdent();

                if (ident != null)
                {
                    if (source.Current == Symbols.RoundBracketOpen)
                    {
                        if (ColorFunctions.TryGetValue(ident, out var function))
                        {
                            source.SkipCurrentAndSpaces();
                            return function.Invoke(source);
                        }

                        return null;
                    }

                    return CssColorValue.FromName(ident);
                }

                return null;
            }

            return Literal(source);
        }

        private static CssColorValue? Literal(StringSource source)
        {
            var current = source.Next();
            var buffer = StringBuilderPool.Obtain();

            while (current.IsHex())
            {
                buffer.Append(current);
                current = source.Next();
            }

            if (CssColorValue.TryFromHex(buffer.ToPool(), out var result))
            {
                return result;
            }

            return null;
        }

        private static CssColorValue? ParseRgba(StringSource source)
        {
            var pos = source.Index;
            var color = ParseRgbaLegacy(source);

            if (!color.HasValue)
            {
                source.BackTo(pos);
                return ParseRgbaModern(source);
            }

            return color.Value;
        }

        private static CssColorValue? ParseRgbaModern(StringSource source)
        {
            var r = ParseRgbOrNoneComponent(source);
            source.SkipSpacesAndComments();
            var g = ParseRgbOrNoneComponent(source);
            source.SkipSpacesAndComments();
            var b = ParseRgbOrNoneComponent(source);
            source.SkipSpacesAndComments();
            var c = source.Current;
            var a = new Nullable<Double>(1.0);

            if (r != null && g != null && b != null)
            {
                source.SkipCurrentAndSpaces();

                if (c == Symbols.Solidus)
                {
                    a = ParseAlpha(source);
                    source.SkipSpacesAndComments();
                    c = source.Current;
                    source.SkipCurrentAndSpaces();
                }

                if (c == Symbols.RoundBracketClose)
                {
                    return CssColorValue.FromRgba(r.Value, g.Value, b.Value, a.Value);
                }
            }

            return null;
        }

        private static CssColorValue? ParseRgbaLegacy(StringSource source)
        {
            var r = ParseRgbComponent(source);
            var c1 = source.SkipGetSkip();
            var g = ParseRgbComponent(source);
            var c2 = source.SkipGetSkip();
            var b = ParseRgbComponent(source);
            var c3 = source.SkipGetSkip();

            if (r != null && g != null && b != null)
            {
                if (Check(c3, c1, c2))
                {
                    return CssColorValue.FromRgb(r.Value, g.Value, b.Value);
                }
                else
                {
                    var a = ParseAlpha(source);
                    var f = source.SkipSpacesAndComments();
                    source.SkipCurrentAndSpaces();

                    if (a != null && Check(f, c1, c2, c3))
                    {
                        return CssColorValue.FromRgba(r.Value, g.Value, b.Value, a.Value);
                    }
                }
            }

            return null;
        }

        private static CssColorValue? ParseHsla(StringSource source)
        {
            var h = ParseAngle(source);
            var c1 = source.SkipGetSkip();
            var s = source.ParsePercent();
            var c2 = source.SkipGetSkip();
            var l = source.ParsePercent();
            var c3 = source.SkipGetSkip();

            if (h != null && s != null && l != null)
            {
                if (Check(c3, c1, c2))
                {
                    return CssColorValue.FromHsl(h.Value, s.Value, l.Value);
                }
                else
                {
                    var a = ParseAlpha(source);
                    var f = source.SkipGetSkip();

                    if (a != null && Check(f, c1, c2, c3))
                    {
                        return CssColorValue.FromHsla(h.Value, s.Value, l.Value, a.Value);
                    }
                }
            }

            return null;
        }

        private static CssColorValue? ParseGray(StringSource source)
        {
            var n = ParseRgbComponent(source);
            var c = source.SkipGetSkip();
            var a = ParseAlpha(source);
            var f = source.SkipGetSkip();

            if (n != null)
            {
                if (c == Symbols.RoundBracketClose)
                {
                    return CssColorValue.FromGray(n.Value);
                }
                else if (a != null && Check(f, c))
                {
                    return CssColorValue.FromGray(n.Value, a.Value);
                }
            }

            return null;
        }

        private static CssColorValue? ParseLab(StringSource source)
        {
            var l = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var a = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var b = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var c = source.Current;
            var alpha = new Nullable<Double>(1.0);

            if (l != null && a != null && b != null)
            {
                source.SkipCurrentAndSpaces();

                if (c == Symbols.Solidus)
                {
                    alpha = ParseAlpha(source);
                    source.SkipSpacesAndComments();
                    c = source.Current;
                    source.SkipCurrentAndSpaces();
                }

                if (c == Symbols.RoundBracketClose)
                {
                    return CssColorValue.FromLab(l.Value, a.Value, b.Value, alpha.Value);
                }
            }

            return null;
        }

        private static CssColorValue? ParseLch(StringSource source)
        {
            var l = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var c = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var h = ParseAngle(source);
            source.SkipSpacesAndComments();
            var chr = source.Current;
            var a = new Nullable<Double>(1.0);

            if (l != null && c != null && h != null)
            {
                source.SkipCurrentAndSpaces();

                if (chr == Symbols.Solidus)
                {
                    a = ParseAlpha(source);
                    source.SkipSpacesAndComments();
                    chr = source.Current;
                    source.SkipCurrentAndSpaces();
                }

                if (chr == Symbols.RoundBracketClose)
                {
                    return CssColorValue.FromLch(l.Value, c.Value, h.Value, a.Value);
                }
            }

            return null;
        }


        private static CssColorValue? ParseOklab(StringSource source)
        {
            var l = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var a = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var b = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var c = source.Current;
            var alpha = new Nullable<Double>(1.0);

            if (l != null && a != null && b != null)
            {
                source.SkipCurrentAndSpaces();

                if (c == Symbols.Solidus)
                {
                    alpha = ParseAlpha(source);
                    source.SkipSpacesAndComments();
                    c = source.Current;
                    source.SkipCurrentAndSpaces();
                }

                if (c == Symbols.RoundBracketClose)
                {
                    return CssColorValue.FromOklab(l.Value, a.Value, b.Value, alpha.Value);
                }
            }

            return null;
        }

        private static CssColorValue? ParseOklch(StringSource source)
        {
            var l = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var c = ParseLabComponent(source);
            source.SkipSpacesAndComments();
            var h = ParseAngle(source);
            source.SkipSpacesAndComments();
            var chr = source.Current;
            var a = new Nullable<Double>(1.0);

            if (l != null && c != null && h != null)
            {
                source.SkipCurrentAndSpaces();

                if (chr == Symbols.Solidus)
                {
                    a = ParseAlpha(source);
                    source.SkipSpacesAndComments();
                    chr = source.Current;
                    source.SkipCurrentAndSpaces();
                }

                if (chr == Symbols.RoundBracketClose)
                {
                    return CssColorValue.FromOklch(l.Value, c.Value, h.Value, a.Value);
                }
            }

            return null;
        }

        private static CssColorValue? ParseHwba(StringSource source)
        {
            var h = ParseAngle(source);
            var c1 = source.SkipGetSkip();
            var s = source.ParsePercent();
            var c2 = source.SkipGetSkip();
            var l = source.ParsePercent();
            var c3 = source.SkipGetSkip();

            if (h != null && s != null && l != null)
            {
                if (Check(c3, c1, c2))
                {
                    return CssColorValue.FromHwb(h.Value, s.Value, l.Value);
                }
                else
                {
                    var a = ParseAlpha(source);
                    var f = source.SkipSpacesAndComments();
                    source.SkipCurrentAndSpaces();

                    if (a != null && Check(f, c1, c2, c3))
                    {
                        return CssColorValue.FromHwba(h.Value, s.Value, l.Value, a.Value);
                    }
                }
            }

            return null;
        }

        private static Double? ParseLabComponent(StringSource source)
        {
            var pos = source.Index;
            var unit = source.ParseUnit();

            if (unit == null)
            {
                source.BackTo(pos);

                if (source.IsIdentifier(CssKeywords.None))
                {
                    return 0;
                }

                return null;
            }

            if ((unit.Dimension == String.Empty || unit.Dimension == "%") && Double.TryParse(unit.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return null;
        }

        private static Byte? ParseRgbOrNoneComponent(StringSource source)
        {
            var pos = source.Index;
            var value = ParseRgbComponent(source);

            if (value.HasValue)
            {
                return value;
            }

            source.BackTo(pos);

            if (source.IsIdentifier(CssKeywords.None))
            {
                return 0;
            }

            return null;
        }

        private static Byte? ParseRgbComponent(StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit == null)
            {
                return null;
            }

            if (unit.Dimension == String.Empty && Int32.TryParse(unit.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var num))
            {
                return (Byte)Math.Max(Math.Min(num, 255f), 0f);
            }

            if (unit.Dimension == "%" && Double.TryParse(unit.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
            {
                return (Byte)Math.Round((255.0 * val) / 100.0);
            }

            return null;
        }

        private static Double? ParseAlpha(StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit != null &&
                Double.TryParse(unit.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                if (unit.Dimension == "%")
                {
                    return value * 0.01;
                }
                else if (unit.Dimension == String.Empty)
                {
                    return Math.Max(Math.Min(value, 1.0), 0.0);
                }
            }

            return null;
        }

        private static Double? ParseAngle(StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit != null &&
                Double.TryParse(unit.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                var dim = Angle.Unit.Deg;

                if (unit.Dimension == String.Empty || (dim = Angle.GetUnit(unit.Dimension)) != Angle.Unit.None)
                {
                    var angle = new Angle(value, dim);
                    return angle.ToTurns();
                }
            }

            return null;
        }

        private static Boolean Check(Char closingBracket, Char firstComma = Symbols.Comma, Char secondComma = Symbols.Comma, Char thirdComma = Symbols.Comma) =>
            closingBracket == Symbols.RoundBracketClose &&
            firstComma == Symbols.Comma &&
            secondComma == Symbols.Comma &&
            thirdComma == Symbols.Comma;
    }
}
