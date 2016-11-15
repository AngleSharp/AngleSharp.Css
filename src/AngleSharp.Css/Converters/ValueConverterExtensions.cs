namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Essential extensions for using the value converters.
    /// </summary>
    static class ValueConverterExtensions
    {
        public static ICssValue Convert(this IValueConverter converter, String value)
        {
            var source = new StringSource(value);
            source.SkipSpaces();
            var result = converter.Convert(source);
            source.SkipSpaces();
            return source.IsDone ? result : null;
        }

        public static ICssValue ConvertDefault(this IValueConverter converter)
        {
            return converter.Convert(String.Empty);
        }

        public static IValueConverter Many(this IValueConverter converter, Int32 min = 1, Int32 max = UInt16.MaxValue)
        {
            return new OneOrMoreValueConverter(converter, min, max);
        }

        public static IValueConverter FromList(this IValueConverter converter)
        {
            return new ListValueConverter(converter);
        }

        public static IValueConverter ToConverter<T>(this Dictionary<String, T> values)
        {
            return new DictionaryValueConverter<T>(values);
        }

        public static IValueConverter Periodic(this IValueConverter converter, params String[] labels)
        {
            return new PeriodicValueConverter(converter);
        }

        public static IValueConverter RequiresEnd(this IValueConverter listConverter, IValueConverter endConverter)
        {
            return new EndListValueConverter(listConverter, endConverter);
        }

        public static IValueConverter Required(this IValueConverter converter)
        {
            return new RequiredValueConverter(converter);
        }

        public static IValueConverter Option(this IValueConverter converter)
        {
            return new OptionValueConverter(converter);
        }

        public static IValueConverter For(this IValueConverter converter, params String[] labels)
        {
            return new ConstraintValueConverter(converter, labels);
        }

        public static IValueConverter Option<T>(this IValueConverter converter, T defaultValue)
        {
            return new OptionValueConverter<T>(converter, defaultValue);
        }

        public static IValueConverter Or(this IValueConverter primary, IValueConverter secondary)
        {
            return new OrValueConverter(primary, secondary);
        }

        public static IValueConverter Or(this IValueConverter primary, String keyword)
        {
            return primary.Or<Object>(keyword, null);
        }

        public static IValueConverter Or<T>(this IValueConverter primary, String keyword, T value)
        {
            var identifier = new IdentifierValueConverter<T>(keyword, value);
            return new OrValueConverter(primary, identifier);
        }

        public static IValueConverter OrNone(this IValueConverter primary)
        {
            return primary.Or(CssKeywords.None);
        }

        public static IValueConverter OrDefault(this IValueConverter primary)
        {
            return primary.OrInherit().Or(CssKeywords.Initial);
        }

        public static IValueConverter OrDefault<T>(this IValueConverter primary, T value)
        {
            return primary.OrInherit().Or(CssKeywords.Initial, value);
        }

        public static IValueConverter OrInherit(this IValueConverter primary)
        {
            return primary.Or(CssKeywords.Inherit);
        }

        public static IValueConverter OrAuto(this IValueConverter primary)
        {
            return primary.Or(CssKeywords.Auto);
        }

        public static IValueConverter StartsWithKeyword(this IValueConverter converter, String keyword)
        {
            return new StartsWithValueConverter(keyword, converter);
        }

        public static IValueConverter StartsWithDelimiter(this IValueConverter converter)
        {
            return new StartsWithValueConverter("/", converter);
        }

        public static IValueConverter WithCurrentColor(this IValueConverter converter)
        {
            return converter.Or(CssKeywords.CurrentColor, Color.Transparent);
        }
    }
}
