namespace AngleSharp.Css.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    static class ValueExtensions
    {
        public static Length ToLength(this FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Big://1.5em
                    return new Length(1.5f, Length.Unit.Em);
                case FontSize.Huge://2em
                    return new Length(2f, Length.Unit.Em);
                case FontSize.Large://1.2em
                    return new Length(1.2f, Length.Unit.Em);
                case FontSize.Larger://*120%
                    return new Length(120f, Length.Unit.Percent);
                case FontSize.Little://0.75em
                    return new Length(0.75f, Length.Unit.Em);
                case FontSize.Small://8/9em
                    return new Length(8f / 9f, Length.Unit.Em);
                case FontSize.Smaller://*80%
                    return new Length(80f, Length.Unit.Percent);
                case FontSize.Tiny://0.6em
                    return new Length(0.6f, Length.Unit.Em);
                default://1em
                    return new Length(1f, Length.Unit.Em);
            }
        }

        public static Single? ToPercent(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null && test.Dimension == "%")
            {
                var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);
                return value * 0.01f;
            }

            str.BackTo(pos);
            return null;
        }

        public static Length? ToPercentOrNumber(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);

                if (test.Dimension == "%")
                {
                    return new Length(value, Length.Unit.Percent);
                }
                else if (test.Dimension.Length == 0)
                {
                    return new Length(value, Length.Unit.None);
                }
            }

            str.BackTo(pos);
            return null;
        }

        public static BorderImageSlice? ToBorderImageSlice(this StringSource str)
        {
            var lengths = new Length[4];
            var filled = false;
            var completed = 0;
            var pos = 0;

            do
            {
                pos = str.Index;

                if (completed < 4)
                {
                    var length = str.ToPercentOrNumber();

                    if (length.HasValue)
                    {
                        lengths[completed++] = length.Value;
                        str.SkipCurrentAndSpaces();
                    }
                }

                if (!filled)
                {
                    filled = str.IsIdentifier(CssKeywords.Fill);
                    str.SkipCurrentAndSpaces();
                }
            }
            while (str.Index != pos);

            while (completed < 4)
            {
                lengths[completed++] = Length.Full;
            }

            return new BorderImageSlice(lengths[0], lengths[1], lengths[2], lengths[3], filled);
        }

        public static Boolean IsFunction(this StringSource str, String name)
        {
            var rest = str.Content.Length - str.Index;

            if (rest >= name.Length + 2)
            {
                var length = 0;
                var current = str.Current;
                var pos = str.Index;

                while (length < name.Length)
                {
                    if (Char.ToLowerInvariant(current) != Char.ToLowerInvariant(name[length]))
                        break;

                    length++;
                    current = str.Next();
                }

                if (length == name.Length && current == Symbols.RoundBracketOpen)
                {
                    str.SkipCurrentAndSpaces();
                    return true;
                }

                str.BackTo(pos);
            }

            return false;
        }

        public static Boolean IsIdentifier(this StringSource str, String identifier)
        {
            var pos = str.Index;
            var test = str.ParseIdent();
            
            if (test != null && test.Isi(identifier))
            {
                return true;
            }

            str.BackTo(pos);
            return false;
        }

        public static Boolean IsIdentifier(this StringSource str)
        {
            var test = str.ParseIdent();
            return test != null;
        }

        public static Boolean IsLiteral(this StringSource str)
        {
            var detected = 0;

            while (!str.IsDone)
            {
                str.SkipSpacesAndComments();

                if (str.ParseIdent() == null)
                    break;

                detected++;
            }

            return detected > 0;
        }

        public static Boolean IsAnimatableIdentifier(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseIdent();

            if (test != null && (test.Isi(CssKeywords.All) || true))//TODO: Factory.Properties.IsAnimatable(value))
            {
                return true;
            }

            str.BackTo(pos);
            return false;
        }

        public static Single? ToSingle(this StringSource str)
        {
            return str.ParseNumber();
        }

        public static Single? ToNaturalSingle(this StringSource str)
        {
            var pos = str.Index;
            var element = str.ParseNumber();

            if (element.HasValue && element.Value >= 0f)
            {
                return element;
            }

            str.BackTo(pos);
            return null;
        }

		public static Single? ToGreaterOrEqualOneSingle(this StringSource str)
        {
            var pos = str.Index;
            var element = str.ParseNumber();

            if (element.HasValue && element.Value >= 1f)
            {
                return element;
            }

            str.BackTo(pos);
            return null;
		}

        public static Int32? ToInteger(this StringSource str)
        {
            return str.ParseInteger();
        }

        public static Int32? ToNaturalInteger(this StringSource str)
        {
            var pos = str.Index;
            var element = str.ParseInteger();

            if (element.HasValue && element.Value >= 0)
            {
                return element;
            }

            str.BackTo(pos);
            return null;
        }

        public static Int32? ToPositiveInteger(this StringSource str)
        {
            var pos = str.Index;
            var element = str.ParseInteger();

            if (element.HasValue && element.Value > 0)
            {
                return element;
            }

            str.BackTo(pos);
            return null;
        }

        public static Int32? ToWeightInteger(this StringSource str)
        {
            var pos = str.Index;
            var element = str.ToPositiveInteger();

            if (element.HasValue && IsWeight(element.Value))
            {
                return element;
            }

            str.BackTo(pos);
            return null;
        }

        public static Int32? ToBinary(this StringSource str)
        {
            var pos = str.Index;
            var element = str.ParseInteger();

            if (element.HasValue && (element.Value == 0 || element.Value == 1))
            {
                return element;
            }

            str.BackTo(pos);
            return null;
        }

        public static Point? ToSize(this StringSource str)
        {
            if (str.IsIdentifier(CssKeywords.Cover))
            {
                return Point.Center;
            }
            else if (str.IsIdentifier(CssKeywords.Contain))
            {
                return Point.Center;
            }
            else
            {
                var w = str.ToDistance();

                if (!w.HasValue && !str.IsIdentifier(CssKeywords.Auto))
                    return null;

                str.SkipSpacesAndComments();
                var h = str.ToDistance();
                var width = w ?? Length.Full;
                var height = h ?? (str.IsIdentifier(CssKeywords.Auto) ? Length.Full : width);
                return new Point(width, height);
            }
        }

        public static Tuple<BackgroundRepeat, BackgroundRepeat> ToBackgroundRepeat(this StringSource str)
        {
            if (str.IsIdentifier(CssKeywords.RepeatX))
            {
                return Tuple.Create(BackgroundRepeat.Repeat, BackgroundRepeat.NoRepeat);
            }
            else if (str.IsIdentifier(CssKeywords.RepeatY))
            {
                return Tuple.Create(BackgroundRepeat.NoRepeat, BackgroundRepeat.Repeat);
            }

            var repeatX = str.ToConstant(Map.BackgroundRepeats);
            str.SkipSpacesAndComments();
            var repeatY = str.ToConstant(Map.BackgroundRepeats);

            if (repeatY.HasValue)
            {
                return Tuple.Create(repeatX.Value, repeatY.Value);
            }
            else if (repeatX.HasValue)
            {
                return Tuple.Create(repeatX.Value, repeatX.Value);
            }

            return null;
        }

        public static IImageSource ToImageSource(this StringSource str)
        {
            var url = str.ParseUri();

            if (url != null)
            {
                return new ExternalImage(url);
            }

            return str.ParseGradient();
        }

        public static Point? ToPoint(this StringSource str)
        {
            var pos = str.Index;
            var x = new Length(50f, Length.Unit.Percent);
            var y = new Length(50f, Length.Unit.Percent);
            var l = str.ParseIdent();
            str.SkipSpacesAndComments();
            var r = str.ParseIdent();

            if (r != null)
            {
                if (IsHorizontal(r) && IsVertical(l))
                {
                    var t = l;
                    l = r;
                    r = t;
                }

                if (IsHorizontal(l) && IsVertical(r))
                {
                    x = KeywordToLength(l).Value;
                    y = KeywordToLength(r).Value;
                    return new Point(x, y);
                }
            }
            else if (l != null)
            {
                var s = str.ToDistance();

                if (IsHorizontal(l))
                {
                    x = KeywordToLength(l).Value;
                    return new Point(x, s ?? y);
                }
                else if (IsVertical(l))
                {
                    y = KeywordToLength(l).Value;
                    return new Point(s ?? x, y);
                }
            }
            else
            {
                var f = str.ToDistance();
                str.SkipSpacesAndComments();
                var s = str.ToDistance();

                if (s.HasValue)
                {
                    return new Point(f ?? x, s ?? y);
                }
                else if (f.HasValue)
                {
                    pos = str.Index;
                    r = str.ParseIdent();

                    if (r == null)
                    {
                        x = f.Value;
                        return new Point(x, y);
                    }
                    else if (IsVertical(r))
                    {
                        y = KeywordToLength(r).Value;
                        return new Point(f ?? x, y);
                    }
                    else if (IsHorizontal(r))
                    {
                        x = KeywordToLength(r).Value;
                        return new Point(x, f ?? y);
                    }
                    else
                    {
                        str.BackTo(pos);
                        return new Point(f ?? x, y);
                    }
                }
            }

            str.BackTo(pos);
            return null;
        }

        public static Angle? ToAngle(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var unit = Angle.GetUnit(test.Dimension);

                if (unit != Angle.Unit.None)
                {
                    var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);
                    return new Angle(value, unit);
                }

                str.BackTo(pos);
            }

            return null;
        }

        public static Frequency? ToFrequency(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var unit = Frequency.GetUnit(test.Dimension);

                if (unit != Frequency.Unit.None)
                {
                    var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);
                    return new Frequency(value, unit);
                }

                str.BackTo(pos);
            }

            return null;
        }
        public static Length? ToDistance(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var unit = Length.Unit.Px;
                var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);

                if ((test.Dimension == String.Empty && test.Value == "0") ||
                    (unit = Length.GetUnit(test.Dimension)) != Length.Unit.None)
                {
                    return new Length(value, unit);
                }

                str.BackTo(pos);
            }

            return null;
        }
        
        public static Length? ToLength(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var unit = Length.Unit.Px;

                if ((test.Dimension == String.Empty && test.Value == "0") ||
                    (unit = Length.GetUnit(test.Dimension)) != Length.Unit.None)
                {
                    if (unit != Length.Unit.Percent)
                    {
                        var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);
                        return new Length(value, unit);
                    }
                }

                str.BackTo(pos);
            }

            return null;
        }

        public static Resolution? ToResolution(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var unit = Resolution.GetUnit(test.Dimension);

                if (unit != Resolution.Unit.None)
                {
                    var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);
                    return new Resolution(value, unit);
                }

                str.BackTo(pos);
            }

            return null;
        }

        public static Time? ToTime(this StringSource str)
        {
            var pos = str.Index;
            var test = str.ParseUnit();

            if (test != null)
            {
                var unit = Time.GetUnit(test.Dimension);

                if (unit != Time.Unit.None)
                {
                    var value = Single.Parse(test.Value, CultureInfo.InvariantCulture);
                    return new Time(value, unit);
                }

                str.BackTo(pos);
            }

            return null;
        }

        public static Length? ToBorderWidth(this StringSource value)
        {
            return value.ToLength() ?? value.ToConstant(Map.BorderWidths);
        }

        public static T? ToConstant<T>(this StringSource str, IDictionary<String, T> values)
            where T : struct
        {
            var pos = str.Index;
            var ident = str.ParseIdent();
            var mode = default(T);

            if (ident != null && values.TryGetValue(ident, out mode))
            {
                return mode;
            }

            str.BackTo(pos);
            return null;
        }

        public static T ToStatic<T>(this StringSource str, IDictionary<String, T> values)
            where T : class
        {
            var ident = str.ParseIdent();
            var mode = default(T);
            return ident != null && values.TryGetValue(ident, out mode) ? mode : null;
        }

        public static Color? ToCurrentColor(this StringSource str)
        {
            return str.IsIdentifier(CssKeywords.CurrentColor) ? Color.Transparent : str.ParseColor();
        }

        public static Color? ToColor(this StringSource str)
        {
            return str.ParseColor();
        }

        private static Boolean IsWeight(Int32 value)
        {
            return value == 100 || value == 200 || value == 300 || value == 400 ||
                   value == 500 || value == 600 || value == 700 || value == 800 ||
                   value == 900;
        }

        private static Boolean IsHorizontal(String str)
        {
            return str.Isi(CssKeywords.Left) || str.Isi(CssKeywords.Right) || str.Isi(CssKeywords.Center);
        }

        private static Boolean IsVertical(String str)
        {
            return str.Isi(CssKeywords.Top) || str.Isi(CssKeywords.Bottom) || str.Isi(CssKeywords.Center);
        }

        private static Length? KeywordToLength(String keyword)
        {
            if (keyword.Isi(CssKeywords.Left) || keyword.Isi(CssKeywords.Top))
            {
                return new Length(0f, Length.Unit.Percent);
            }
            else if (keyword.Isi(CssKeywords.Right) || keyword.Isi(CssKeywords.Bottom))
            {
                return new Length(100f, Length.Unit.Percent);
            }
            else if (keyword.Isi(CssKeywords.Center))
            {
                return new Length(50f, Length.Unit.Percent);
            }

            return null;
        }
    }
}
