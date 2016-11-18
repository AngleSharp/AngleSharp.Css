namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using static ValueConverters;

    abstract class GradientConverter<T> : IValueConverter
        where T : struct
    {
        private readonly Boolean _repeating;

        public GradientConverter(Boolean repeating)
        {
            _repeating = repeating;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var initial = ConvertInitial(source);

            if (initial.HasValue)
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
            
            var stops = ToGradientStops(source);
            return stops != null ? new GradientValue(source.Substring(start), _repeating, initial ?? GetDefaultValue(), stops) : null;
        }

        protected abstract T? ConvertInitial(StringSource source);

        protected abstract T GetDefaultValue();

        private static GradientStop[] ToGradientStops(StringSource source)
        {
            var stops = new List<GradientStop>();
            
            while (!source.IsDone)
            {
                var stop = ToGradientStop(source);

                if (stop == null)
                    break;

                var current = source.SkipSpacesAndComments();
                stops.Add(stop.Value);

                if (current != Symbols.Comma)
                    break;

                source.SkipCurrentAndSpaces();
            }

            return stops.ToArray();
        }

        private static GradientStop? ToGradientStop(StringSource source)
        {
            var color = source.ToColor();
            source.SkipSpacesAndComments();
            var position = source.ToDistance();

            if (color.HasValue)
            {
                if (position.HasValue)
                {
                    return new GradientStop(color.Value, position.Value);
                }
                else
                {
                    return new GradientStop(color.Value);
                }
            }

            return null;
        }

        private sealed class GradientValue : BaseValue
        {
            private readonly Boolean _repeating;
            private readonly T _initial;
            private readonly GradientStop[] _stops;

            public GradientValue(String value, Boolean repeating, T initial, GradientStop[] stops)
                : base(value)
            {
                _repeating = repeating;
                _initial = initial;
                _stops = stops;
            }
        }
    }

    sealed class LinearGradientConverter : GradientConverter<LinearGradientConverter.Options>
    {
        private static readonly Dictionary<String, Angle> Directions = new Dictionary<String, Angle>
        {
            { CssKeywords.Left, new Angle(270f, Angle.Unit.Deg) },
            { CssKeywords.Top, new Angle(0, Angle.Unit.Deg) },
            { CssKeywords.Right, new Angle(90f, Angle.Unit.Deg) },
            { CssKeywords.Bottom, new Angle(180f, Angle.Unit.Deg) },
            { CssKeywords.Left + " " + CssKeywords.Top, new Angle(315f, Angle.Unit.Deg) },
            { CssKeywords.Left + " " + CssKeywords.Bottom, new Angle(225f, Angle.Unit.Deg) },
            { CssKeywords.Right + " " + CssKeywords.Top, new Angle(45f, Angle.Unit.Deg) },
            { CssKeywords.Right + " " + CssKeywords.Bottom, new Angle(135f, Angle.Unit.Deg) },
        };

        public LinearGradientConverter(Boolean repeating)
            : base(repeating)
        {
        }

        protected override Options? ConvertInitial(StringSource source)
        {
            var ident = source.ParseIdent();
            var angle = default(Angle?);

            if (ident != null && ident.Is(CssKeywords.To))
            {
                var tmp = Angle.Zero;
                source.SkipSpacesAndComments();
                var a = source.ParseIdent();
                source.SkipSpacesAndComments();
                var b = source.ParseIdent();
                var keyword = default(String);

                if (a!= null && b == null)
                {
                    keyword = a;
                }
                else if (a != null && b != null)
                {
                    keyword = b.IsOneOf(CssKeywords.Top, CssKeywords.Bottom) ?
                        a + " " + b : b + " " + a;
                }

                if (keyword != null && Directions.TryGetValue(ident, out tmp))
                {
                    angle = tmp;
                }
            }
            else
            {
                angle = source.ToAngle();
            }

            if (angle.HasValue)
            {
                return new Options { Direction = angle.Value };
            }

            return null;
        }

        protected override Options GetDefaultValue()
        {
            return new Options { Direction = Angle.Zero };
        }

        public struct Options
        {
            public Angle Direction;
        }
    }

    sealed class RadialGradientConverter : GradientConverter<RadialGradientConverter.Options>
    {
        private readonly IValueConverter _initialConverter;

        public RadialGradientConverter(Boolean repeating)
            : base(repeating)
        {
            var position = PointConverter.StartsWithKeyword(CssKeywords.At).Option(Point.Center);
            var circle = WithOrder(WithAny(Assign(CssKeywords.Circle, true).Option(true), LengthConverter.Option()), position);
            var ellipse = WithOrder(WithAny(Assign(CssKeywords.Ellipse, false).Option(false), LengthOrPercentConverter.Many(2, 2).Option()), position);
            var extents = WithOrder(WithAny(Toggle(CssKeywords.Circle, CssKeywords.Ellipse).Option(false), Map.RadialGradientSizeModes.ToConverter()), position);

            _initialConverter = Or(circle, ellipse, extents);
        }

        protected override Options? ConvertInitial(StringSource source)
        {
            var value = _initialConverter.Convert(source);

            if (value != null)
            {
                return new Options { Bundle = value };
            }

            return null;
        }

        protected override Options GetDefaultValue()
        {
            return new Options { Bundle = new BaseValue(String.Empty) };
        }

        public struct Options
        {
            public ICssValue Bundle;
        }
    }
}
