namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents extensions to for color values.
    /// </summary>
    static class ColorParser
    {
        private static readonly Dictionary<String, Func<StringSource, Color?>> ColorFunctions = new Dictionary<String, Func<StringSource, Color?>>(StringComparer.OrdinalIgnoreCase)
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
        public static Color? ParseColor(this StringSource source)
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
        public static Color? ParseCurrentColor(this StringSource source) =>
            source.IsIdentifier(CssKeywords.CurrentColor) ? Color.CurrentColor : ColorParser.ParseColor(source);

        private static Color? Start(StringSource source)
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

                    return Color.FromName(ident);
                }

                return null;
            }

            return Literal(source);
        }

        private static Color? Literal(StringSource source)
        {
            var current = source.Next();
            var buffer = StringBuilderPool.Obtain();

            while (current.IsHex())
            {
                buffer.Append(current);
                current = source.Next();
            }

            if (Color.TryFromHex(buffer.ToPool(), out var result))
            {
                return result;
            }

            return null;
        }

        private static Color? ParseRgba(StringSource source)
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

        private static Color? ParseRgbaModern(StringSource source)
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
                    return Color.FromRgba(r.Value, g.Value, b.Value, a.Value);
                }
            }

            return null;
        }

        private static Color? ParseRgbaLegacy(StringSource source)
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
                    return Color.FromRgb(r.Value, g.Value, b.Value);
                }
                else
                {
                    var a = ParseAlpha(source);
                    var f = source.SkipSpacesAndComments();
                    source.SkipCurrentAndSpaces();

                    if (a != null && Check(f, c1, c2, c3))
                    {
                        return Color.FromRgba(r.Value, g.Value, b.Value, a.Value);
                    }
                }
            }

            return null;
        }

        private static Color? ParseHsla(StringSource source)
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
                    return Color.FromHsl(h.Value, s.Value, l.Value);
                }
                else
                {
                    var a = ParseAlpha(source);
                    var f = source.SkipGetSkip();

                    if (a != null && Check(f, c1, c2, c3))
                    {
                        return Color.FromHsla(h.Value, s.Value, l.Value, a.Value);
                    }
                }
            }

            return null;
        }

        private static Color? ParseGray(StringSource source)
        {
            var n = ParseRgbComponent(source);
            var c = source.SkipGetSkip();
            var a = ParseAlpha(source);
            var f = source.SkipGetSkip();

            if (n != null)
            {
                if (c == Symbols.RoundBracketClose)
                {
                    return Color.FromGray(n.Value);
                }
                else if (a != null && Check(f, c))
                {
                    return Color.FromGray(n.Value, a.Value);
                }
            }

            return null;
        }

        private static Color? ParseLab(StringSource source)
        {
            // lab(50% 40 59.5)
            // lab(50 % 40 59.5 / 0.5)
            return null;
        }

        private static Color? ParseLch(StringSource source)
        {
            // lch(52.2% 72.2 50)
            // lch(52.2 % 72.2 50 / 0.5)
            return null;
        }


        private static Color? ParseOklab(StringSource source)
        {
            // oklab(59% 0.1 0.1)
            // oklab(59 % 0.1 0.1 / 0.5)
            return null;
        }

        private static Color? ParseOklch(StringSource source)
        {
            // oklch(60% 0.15 50)
            // oklch(60 % 0.15 50 / 0.5)
            return null;
        }

        private static Color? ParseHwba(StringSource source)
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
                    return Color.FromHwb(h.Value, s.Value, l.Value);
                }
                else
                {
                    var a = ParseAlpha(source);
                    var f = source.SkipSpacesAndComments();
                    source.SkipCurrentAndSpaces();

                    if (a != null && Check(f, c1, c2, c3))
                    {
                        return Color.FromHwba(h.Value, s.Value, l.Value, a.Value);
                    }
                }
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

            if (unit != null &&
                Int32.TryParse(unit.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            {
                if (unit.Dimension == "%")
                {
                    return (Byte)Math.Round((255.0 * value) / 100.0);
                }
                else if (unit.Dimension == String.Empty)
                {
                    return (Byte)Math.Max(Math.Min(value, 255f), 0f);
                }
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
