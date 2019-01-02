namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class GradientParser
    {
        private static readonly Dictionary<String, Func<StringSource, IGradient>> GradientFunctions = new Dictionary<string, Func<StringSource, IGradient>>
        {
            { FunctionNames.LinearGradient, ParseLinearGradient },
            { FunctionNames.RepeatingLinearGradient, ParseRepeatingLinearGradient },
            { FunctionNames.RadialGradient, ParseRadialGradient },
            { FunctionNames.RepeatingRadialGradient, ParseRepeatingRadialGradient },
        };

        public static IGradient ParseGradient(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, IGradient>);

                    if (GradientFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static IGradient ParseLinearGradient(StringSource source)
        {
            return ParseLinearGradient(source, false);
        }

        private static IGradient ParseRepeatingLinearGradient(StringSource source)
        {
            return ParseLinearGradient(source, true);
        }

        /// <summary>
        /// Parses a linear gradient.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
        /// </summary>
        private static IGradient ParseLinearGradient(StringSource source, Boolean repeating)
        {
            var start = source.Index;
            var angle = ParseLinearAngle(source);

            if (angle != null)
            {
                var current = source.SkipSpacesAndComments();

                if (current != Symbols.Comma)
                {
                    return null;
                }

                source.SkipCurrentAndSpaces();
            }
            else
            {
                source.BackTo(start);
            }

            var stops = ParseGradientStops(source);

            if (stops != null && source.Current == Symbols.RoundBracketClose)
            {
                source.SkipCurrentAndSpaces();
                return new LinearGradient(angle, stops, repeating);
            }

            return null;
        }

        private static IGradient ParseRadialGradient(StringSource source)
        {
            return ParseRadialGradient(source, false);
        }

        private static IGradient ParseRepeatingRadialGradient(StringSource source)
        {
            return ParseRadialGradient(source, true);
        }

        /// <summary>
        /// Parses a radial gradient
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
        /// </summary>
        private static IGradient ParseRadialGradient(StringSource source, Boolean repeating)
        {
            var start = source.Index;
            var options = ParseRadialOptions(source);

            if (options.HasValue)
            {
                var current = source.SkipSpacesAndComments();

                if (current != Symbols.Comma)
                {
                    return null;
                }

                source.SkipCurrentAndSpaces();
            }
            else
            {
                source.BackTo(start);
            }

            var stops = ParseGradientStops(source);

            if (stops != null && source.Current == Symbols.RoundBracketClose)
            {
                var circle = options?.Circle ?? false;
                var center = options?.Center ?? Point.Center;
                var width = options?.Width;
                var height = options?.Height;
                var sizeMode = options?.Size ?? RadialGradient.SizeMode.None;
                source.SkipCurrentAndSpaces();
                return new RadialGradient(circle, center, width, height, sizeMode, stops, repeating);
            }

            return null;
        }

        private static GradientStop[] ParseGradientStops(StringSource source)
        {
            var stops = new List<GradientStop>();
            var current = source.Current;

            while (!source.IsDone)
            {
                if (stops.Count > 0)
                {
                    if (current != Symbols.Comma)
                        break;

                    source.SkipCurrentAndSpaces();
                }

                var stop = ParseGradientStop(source);

                if (stop == null)
                    break;

                stops.Add(stop.Value);
                current = source.SkipSpacesAndComments();
            }

            return stops.ToArray();
        }

        private static GradientStop? ParseGradientStop(StringSource source)
        {
            var color = source.ParseColor();
            source.SkipSpacesAndComments();
            var position = source.ParseDistanceOrCalc();

            if (color.HasValue)
            {
                return new GradientStop(color.Value, position);
            }

            return null;
        }
        
        private static ICssValue ParseLinearAngle(StringSource source)
        {
            if (source.IsIdentifier(CssKeywords.To))
            {
                var angle = Angle.Zero;
                source.SkipSpacesAndComments();
                var a = source.ParseIdent();
                source.SkipSpacesAndComments();
                var b = source.ParseIdent();
                var keyword = default(String);

                if (a != null && b != null)
                {
                    if (a.IsOneOf(CssKeywords.Top, CssKeywords.Bottom))
                    {
                        var t = b;
                        b = a;
                        a = t;
                    }

                    keyword = String.Concat(a, " ", b);
                }
                else if (a != null)
                {
                    keyword = a;
                }

                if (keyword != null && Map.GradientAngles.TryGetValue(keyword, out angle))
                {
                    return angle;
                }

                return null;
            }

            return source.ParseAngleOrCalc();
        }
        
        private static RadialOptions? ParseRadialOptions(StringSource source)
        {
            var circle = false;
            var center = Point.Center;
            var width = default(ICssValue);
            var height = default(ICssValue);
            var size = RadialGradient.SizeMode.None;
            var redo = false;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (ident.Isi(CssKeywords.Circle))
                {
                    circle = true;
                    source.SkipSpacesAndComments();
                    var radius = source.ParseLengthOrCalc();

                    if (radius != null)
                    {
                        width = height = radius;
                    }
                    else
                    {
                        size = ToSizeMode(source) ?? RadialGradient.SizeMode.None;
                    }

                    redo = true;
                }
                else if (ident.Isi(CssKeywords.Ellipse))
                {
                    circle = false;
                    source.SkipSpacesAndComments();
                    var el = source.ParseDistanceOrCalc();
                    source.SkipSpacesAndComments();
                    var es = source.ParseDistanceOrCalc();

                    if (el != null && es != null)
                    {
                        width = el;
                        height = es;
                    }
                    else if (el == null && es == null)
                    {
                        size = ToSizeMode(source) ?? RadialGradient.SizeMode.None;
                    }
                    else
                    {
                        return null;
                    }

                    redo = true;
                }
                else if (Map.RadialGradientSizeModes.ContainsKey(ident))
                {
                    size = Map.RadialGradientSizeModes[ident];
                    source.SkipSpacesAndComments();
                    ident = source.ParseIdent();

                    if (ident != null)
                    {
                        if (ident.Isi(CssKeywords.Circle))
                        {
                            circle = true;
                            redo = true;
                        }
                        else if (ident.Isi(CssKeywords.Ellipse))
                        {
                            circle = false;
                            redo = true;
                        }
                    }
                }
            }
            else
            {
                var el = source.ParseDistanceOrCalc();
                source.SkipSpacesAndComments();
                var es = source.ParseDistanceOrCalc();

                if (el != null && es != null)
                {
                    circle = false;
                    width = el;
                    height = es;
                }
                else if (el != null)
                {
                    circle = true;
                    width = el;
                }
                else
                {
                    return null;
                }

                redo = true;
            }

            if (redo)
            {
                source.SkipSpacesAndComments();
                ident = source.ParseIdent();
            }

            if (ident != null)
            {
                if (!ident.Isi(CssKeywords.At))
                {
                    return null;
                }

                source.SkipSpacesAndComments();
                var pt = source.ParsePoint();

                if (!pt.HasValue)
                {
                    return null;
                }

                center = pt.Value;
            }

            return new RadialOptions
            {
                Circle = circle,
                Center = center,
                Width = width,
                Height = height,
                Size = size
            };
        }

        public struct RadialOptions
        {
            public Boolean Circle;
            public Point Center;
            public ICssValue Width;
            public ICssValue Height;
            public RadialGradient.SizeMode Size;
        }

        private static RadialGradient.SizeMode? ToSizeMode(StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();
            var result = RadialGradient.SizeMode.None;

            if (ident != null && Map.RadialGradientSizeModes.TryGetValue(ident, out result))
            {
                return result;
            }

            source.BackTo(pos);
            return null;
        }
    }
}
