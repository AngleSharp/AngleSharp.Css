namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
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
            source.SkipSpacesAndComments();
            var result = converter.Convert(source);
            source.SkipSpacesAndComments();
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
            return converter;
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

        public static IValueConverter StartsWithKeyword(this IValueConverter converter, String keyword)
        {
            return new StartsWithValueConverter(keyword, converter);
        }

        public static IValueConverter StartsWithDelimiter(this IValueConverter converter)
        {
            return new StartsWithValueConverter("/", converter);
        }
    }
}
