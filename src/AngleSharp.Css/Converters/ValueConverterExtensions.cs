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

        public static IValueConverter Many(this IValueConverter converter, Int32 min = 1, Int32 max = UInt16.MaxValue, String separator = null)
        {
            return new OneOrMoreValueConverter(converter, min, max, separator);
        }

        public static IValueConverter FromList(this IValueConverter converter)
        {
            return new ListValueConverter(converter);
        }

        public static IValueConverter ToConverter<T>(this Dictionary<String, T> values)
        {
            return new DictionaryValueConverter<T>(values);
        }

        public static IValueConverter Periodic(this IValueConverter converter)
        {
            return new PeriodicValueConverter(converter);
        }

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

        public static IValueConverter Option(this IValueConverter converter)
        {
            return new OptionValueConverter<Object>(converter, null);
        }

        public static IValueConverter Option<T>(this IValueConverter converter, T defaultValue)
        {
            return new OptionValueConverter<T>(converter, defaultValue);
        }

        public static String Join(this ICssValue[] values, String separator)
        {
            var buffer = StringBuilderPool.Obtain();
            var previous = false;

            for (var i = 0; i < values.Length; i++)
            {
                var str = values[i]?.CssText;

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

        public static String Join<T>(this T[] values, String separator)
        {
            var buffer = StringBuilderPool.Obtain();
            var previous = false;

            for (var i = 0; i < values.Length; i++)
            {
                var str = values[i].ToString();

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
