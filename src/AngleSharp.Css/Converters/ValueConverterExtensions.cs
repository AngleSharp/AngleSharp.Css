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
            var varRefs = source.ParseVars();

            if (varRefs == null)
            {
                var result = converter.Convert(source);
                source.SkipSpacesAndComments();
                return source.IsDone ? result : null;
            }

            return varRefs;
        }

        public static IValueConverter Many(this IValueConverter converter, Int32 min = 1, Int32 max = UInt16.MaxValue, String separator = null) =>
            new OneOrMoreValueConverter(converter, min, max, separator);

        public static IValueConverter FromList(this IValueConverter converter) =>
            new ListValueConverter(converter);

        public static IValueConverter ToConverter<T>(this Dictionary<String, T> values) =>
            new DictionaryValueConverter<T>(values);

        public static IValueConverter Periodic(this IValueConverter converter) =>
            new PeriodicValueConverter(converter);

        public static IValueConverter Radius(this IValueConverter converter) =>
            new RadiusValueConverter(converter);

        public static IValueConverter Exclusive(this IValueConverter converter)
        {
            return new ClassValueConverter<ICssValue>(source =>
            {
                var pos = source.Index;
                var result = converter.Convert(source);
                source.SkipSpacesAndComments();

                if (result != null && !source.IsDone)
                {
                    source.BackTo(pos);
                    return null;
                }

                return result;
            });
        }

        public static IValueConverter Option(this IValueConverter converter, ICssValue defaultValue) =>
            new OptionValueConverter(converter, defaultValue);

        public static String Join<T>(this IEnumerable<T> values, String separator)
            where T : ICssValue
        {
            var buffer = StringBuilderPool.Obtain();
            var previous = false;

            foreach (var value in values)
            {
                var str = value?.CssText;

                if (!String.IsNullOrEmpty(str))
                {
                    if (previous)
                    {
                        buffer.Append(separator);
                    }

                    buffer.Append(str);
                    previous = true;
                }
            }

            return buffer.ToPool();
        }
    }
}
